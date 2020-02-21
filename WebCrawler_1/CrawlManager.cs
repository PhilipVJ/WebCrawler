using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebCrawler_1.Properties;

namespace WebCrawler_1
{
    public partial class CrawlManager : Form
    {
        private String seedURLInit;
        private Dictionary<string, bool> visitedPages = new Dictionary<string, bool>();
        private Queue<Link> linksToCheck = new Queue<Link>();
        private Object theLinkLock = new object();
        private Object addToVisitedLock = new object();
        private Object addToSearchWordResult = new object();
        private CancellationTokenSource ts;
        private Boolean runningWatch = false;
        public int chosenDepth;
        private Dictionary<string, List<string>> hostRobotFiles = new Dictionary<string, List<string>>();
        private Dictionary<string, List<string>> hostRobotFilesAllowed = new Dictionary<string, List<string>>();
        private Object hostRobotFilesLock = new object();
        private int numberOfVisitedLinks = 0;
        private Stopwatch stopWatch;


        public CrawlManager()
        {
            // Setup everything
            InitializeComponent();
            visitedPagesList.View = View.Details;
            var column = visitedPagesList.Columns.Add("URL");
            column.Width = 401;

            foundLinks.View = View.Details;
            var columnTwo = foundLinks.Columns.Add("URL");
            columnTwo.Width = 401;

            toCheck.Text = "0";
            visPages.Text = "0";
            time.Text = "0";
            avgLinks.Text = "0";

            searchResultList.View = View.List;

        }

        private void Start(object sender, EventArgs e)
        {
            if (button.Text == "Stop")
            {
                runningWatch = false;
                ts.Cancel();
                button.Text = "Start";
                return;
            }

            //Clear everything - in case it has been started before
            ClearScene();
       
            ts = new CancellationTokenSource();

            // Get typed in URL
            var url = seedURL.Text;
            if (string.IsNullOrEmpty(url))
            {
                seedURL.Text = "Indtast en url";
                return;
            }
            UriBuilder ub = new UriBuilder(url);
            seedURLInit = ub.ToString();
            // Fetch its robot file and add to dictionary (if there is a file)
            SetupRobotFileIfPossible(url);
            if (CheckIfDisallowed(new Link { URL = url, Depth = 0 }))
            {
                seedURL.Text = "Ikke en tilladt URL";
                return;
            }

            linksToCheck.Enqueue(new Link { URL = seedURLInit, Depth = 1 });
            string wordToSearch = "";
            if (!string.IsNullOrEmpty(searchWord.Text))
            {
                wordToSearch = searchWord.Text; ;
            }
            // Get the chosen depth
            try
            {
                int.TryParse(depth.Text, out int outDepth);
                if (outDepth == 0)
                {
                    throw new Exception();
                }
                else
                {
                    chosenDepth = outDepth;
                }
            }
            catch (Exception ex)
            {
                depth.Text = "Indtast et tal";
                return;
            }
            // Get the chosen number of crawlers
            try
            {
                int.TryParse(numberOfCrawlers.Text, out int crawlers);
                if (crawlers == 0)
                {
                    throw new Exception();
                }
                for (int i = 0; i < crawlers; i++)
                {
                    // Start x number of agents
                    var crawler = new CrawlAgent(this);

                    Task t = Task.Run(() =>
                    {
                        crawler.StartCrawling(ts.Token, wordToSearch);
                    });
                }
                button.Text = "Stop";
            }
            catch (Exception ex)
            {
                numberOfCrawlers.Text = "Indtast et nummer";
                return;
            }
            // Start the running watch
            runningWatch = true;
            Thread watch = new Thread(() => { StartWatch(); });
            watch.Start();

            Thread updateGui = new Thread(() => { UpdateGui(); });
            updateGui.Start();
        }

        private void UpdateGui()
        {
            while (runningWatch)
            {
                MethodInvoker countUp = delegate
                { visPages.Text = "" + visitedPages.Count(); };
                visPages.BeginInvoke(countUp);

                CalculateAndSetAvgLinkPerMinute();
                UpdateLinkCount();

                Thread.Sleep(1000);
            }
        }

        private void StartWatch()
        {
            stopWatch = new Stopwatch();
            stopWatch.Start();
            while (runningWatch)
            {
                MethodInvoker updateTime = delegate
                { time.Text = "" + stopWatch.ElapsedMilliseconds / 1000; };
                time.BeginInvoke(updateTime);
                Thread.Sleep(1000);
            }
            stopWatch.Stop();
        }

        private void ClearScene()
        {
            linksToCheck = new Queue<Link>();
            visitedPages = new Dictionary<string, bool>();
            addToVisitedLock = new object();
            theLinkLock = new object();

            visPages.Text = "0";
            toCheck.Text = "0";
            time.Text = "0";
            visitedPagesList.Items.Clear();
            foundLinks.Items.Clear();
            searchResultList.Clear();
            numberOfVisitedLinks = 0;
        }

        public Link GetLink()
        {
            lock (theLinkLock)
            {
                bool foundValidLink = false;
                Link link = null;
                // Find first link which hasn't been visited before
                while (!foundValidLink)
                {
                    // Check if any links are available
                    if (linksToCheck.Count == 0)
                    {
                        return null;
                    }
                    link = linksToCheck.Dequeue();
                    SetupRobotFileIfPossible(link.URL);
                    if (!HasBeenVisited(link.URL) && !CheckIfDisallowed(link))
                    {
                        foundValidLink = true;
                    }
                }
                UpdateLinkCount();
                return link;
            }
        }

        private bool CheckIfDisallowed(Link link)
        {
            lock (hostRobotFilesLock)
            {
                // Give the host url as argument
                UriBuilder ub = new UriBuilder(link.URL);
                List<string> list;
                if (hostRobotFiles.ContainsKey(ub.Host))
                {
                    list = hostRobotFiles[ub.Host];
                }
                else
                {
                    return false;
                }
                // The host has been checked for a robots file, but wasn't there
                if (list == null)
                {
                    return false;
                }
                else
                {
                    foreach (var item in list)
                    {
                        UriBuilder builder = new UriBuilder(link.URL);
                        UriBuilder newBuild = new UriBuilder(builder.Host);
                        Uri.TryCreate(newBuild.Uri, item, out Uri result);
                        FormatStrings(result, link, out string resultFormatted, out string linkFormatted);

                        if (resultFormatted == linkFormatted)
                        {
                            return true;
                        }
                        // Check if the url is in a banned subfolder
                        var charArray = linkFormatted.ToCharArray();
                        var charArray2 = resultFormatted.ToCharArray();
                        bool isInBannedSubFolder = false;
        
                        if (charArray.Length >= charArray2.Length)
                        {
                            bool firstPartIsEqual = true;
                            for (int i = 0; i < charArray2.Length; i++)
                            {
                                if (charArray2[i] != charArray[i])
                                {
                                    firstPartIsEqual = false;
                                }
                            }
                            if (firstPartIsEqual)
                            {
                                isInBannedSubFolder = true;
                            }
                        }
                        if (isInBannedSubFolder)
                        {
                            //Check if it somehow allowed anyway
                            var robotAllowed = hostRobotFilesAllowed[builder.Host];
                            foreach (var allowedItem in robotAllowed)
                            {
                              
                                int hostLength = builder.Host.Length;
                                var subString = linkFormatted.Substring(hostLength);
                              
                                int lastSlashAllowed = allowedItem.LastIndexOf("/");
                                string removeSlashFromAllowed = allowedItem.Substring(0, lastSlashAllowed);
                                if (removeSlashFromAllowed == subString)
                                {
        
                                    return false;
                                }
                            }

                            return true;
                        }

                    }
                }
                return false;
            }
        }

        public void FormatStrings(Uri result, Link link, out string resultFormatted, out string linkFormatted)
        {
            int indexOfSlash1 = result.AbsoluteUri.IndexOf("/");
            int indexOfSlash2 = link.URL.IndexOf("/");
            //Remove the http(s) part of it is there
            if (result.AbsoluteUri.Contains("http"))
            {
                resultFormatted = result.AbsoluteUri.Substring(indexOfSlash1 + 2);
            }
            else
            {
                resultFormatted = result.AbsoluteUri;
            }
            if (link.URL.Contains("http"))
            {
                linkFormatted = link.URL.Substring(indexOfSlash2 + 2);
            }
            else
            {
                linkFormatted = link.URL;
            }
            // Remove final slash if it is there
            if (linkFormatted.LastIndexOf("/") == linkFormatted.Length - 1)
            {
                linkFormatted = linkFormatted.Remove(linkFormatted.Length - 1);
            }
            if (resultFormatted.LastIndexOf("/") == resultFormatted.Length - 1)
            {
                resultFormatted = resultFormatted.Remove(resultFormatted.Length - 1);
            }

        }

        public void AddToVisitedLink(string uri, bool state)
        {
            lock (addToVisitedLock)
            {
                // Sometimes two agents have a URL which is the same - and therefore I
                // have to check if it has already been visited    
                if (!visitedPages.ContainsKey(uri))
                {
                    visitedPages.Add(uri, state);

                    MethodInvoker addToList = delegate
                    { visitedPagesList.Items.Add(new ListViewItem(new string[] { uri })); };
                    visitedPagesList.BeginInvoke(addToList);

                    numberOfVisitedLinks++;
                    
                }
            }
        }

        public void CalculateAndSetAvgLinkPerMinute()
        {
            var elapsedTimeInMinutes = stopWatch.ElapsedMilliseconds / 1000M / 60;
            if(numberOfVisitedLinks == 0)
            {
                return;
            }
            var avg = numberOfVisitedLinks / elapsedTimeInMinutes;
            MethodInvoker setText = delegate
            { avgLinks.Text = "" + Decimal.Round(avg); };
            avgLinks.BeginInvoke(setText);

        }

        public void AddToSearchResultView(string uri)
        {
            lock (addToSearchWordResult)
            {
                if (!visitedPages.ContainsKey(uri))
                {
                    MethodInvoker addToList = delegate
                    { searchResultList.Items.Add(new ListViewItem(new string[] { uri })); };
                    searchResultList.BeginInvoke(addToList);
                }
            }
        }



        public void AddLinks(List<Link> links)
        {
            lock (theLinkLock)
            {
                List<ListViewItem> items = new List<ListViewItem>();
                foreach (var link in links)
                {
                   
                    // Check if the link has been visited before
                    if (!visitedPages.ContainsKey(link.URL))
                    {
                        if (link.Depth <= chosenDepth)
                        {
                            linksToCheck.Enqueue(link);
                        }
                        items.Add(new ListViewItem(new string[] { link.URL }));
  
                    }
                }
                MethodInvoker addRangeToList = delegate
                { foundLinks.Items.AddRange(items.ToArray()); };
                foundLinks.BeginInvoke(addRangeToList);

            }
        }

        private void UpdateLinkCount()
        {
            MethodInvoker action = delegate
            { toCheck.Text = "" + linksToCheck.Count(); };
            toCheck.BeginInvoke(action);
        }


        public bool HasBeenVisited(string url)
        {
            if (visitedPages.ContainsKey(url))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SetupRobotFileIfPossible(string url)
        {
            UriBuilder ub = new UriBuilder(url);
            if (hostRobotFiles.ContainsKey(ub.Host))
            {
                return;
            }
            else
            {
                var robotResult = GetRobotsFileFromURL(url);
                if (robotResult == null)
                {
                    hostRobotFiles.Add(ub.Host, null);
                    hostRobotFilesAllowed.Add(ub.Host, null);
                }
                else
                {
                    hostRobotFiles.Add(ub.Host, robotResult.disallowedLinks);
                    hostRobotFilesAllowed.Add(ub.Host, robotResult.allowedLinks);
                }
      
            }
        }


        public RobotResult GetRobotsFileFromURL(string url)
        {
            UriBuilder ub = new UriBuilder(url);
            WebClient wc = new WebClient();
            string robotsFile = "";
            try
            {
                robotsFile = wc.DownloadString("http://" + ub.Host.ToString() + "/robots.txt");
            }
            catch (Exception ex)
            {
                // No robots.txt file found
                return null;
            }

            //Make list of disallowed links
            string[] lines = robotsFile.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            List<string> disallowedLinks = new List<string>();
            List<string> allowedLinks = new List<string>();
            int indexOfAllowedBot = 0;
            if (!robotsFile.Contains("User-agent: *"))
            {
                // Not allowed to crawl
                System.Diagnostics.Debug.WriteLine("Not allowed to crawl");
                return null;
            }
            else
            {
                indexOfAllowedBot = robotsFile.IndexOf("User-agent: *");
            }
            for (int i = indexOfAllowedBot+1; i < lines.Length; i++)
            {
                if (lines[i].Contains("Disallow:"))
                {
                    //Get index of first slash
                    int index = lines[i].IndexOf("/");
                    //Check if an asterix is there - removes it if yes
                    string toAdd;
                    if (lines[i].Contains("*"))
                    {
                        int indexOfA = lines[i].IndexOf("*");
                        toAdd = lines[i].Substring(index, indexOfA - index);
                    }
                    else
                    {
                        toAdd = lines[i].Substring(index);
                    }
                    disallowedLinks.Add(toAdd);
                }

                else if (lines[i].Contains("Allow:"))
                {
                    //Get index of first slash
                    int index = lines[i].IndexOf("/");
                    //Check if an asterix is there - removes it if yes
                    string toAdd;
                    if (lines[i].Contains("*"))
                    {
                        int indexOfA = lines[i].IndexOf("*");
                        toAdd = lines[i].Substring(index, indexOfA - index);
                    }
                    else
                    {
                        toAdd = lines[i].Substring(index);
                    }
                    allowedLinks.Add(toAdd);
                }


                if (lines[i].Contains("User-agent"))
                {
                    break;
                }
            }
            var result = new RobotResult { allowedLinks = allowedLinks, disallowedLinks = disallowedLinks };
            return result;
        }

    }
}
