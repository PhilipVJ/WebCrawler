namespace WebCrawler_1
{
    partial class CrawlManager
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.visPages = new System.Windows.Forms.TextBox();
            this.toCheck = new System.Windows.Forms.TextBox();
            this.time = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.avgLinks = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.seedURL = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button = new System.Windows.Forms.Button();
            this.visitedPagesList = new System.Windows.Forms.ListView();
            this.label6 = new System.Windows.Forms.Label();
            this.numberOfCrawlers = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.foundLinks = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.searchWord = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.depth = new System.Windows.Forms.TextBox();
            this.searchResultList = new System.Windows.Forms.ListView();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Besøgte sider:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Links at tjekke: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 129);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "Tid gået: ";
            // 
            // visPages
            // 
            this.visPages.Location = new System.Drawing.Point(144, 50);
            this.visPages.Name = "visPages";
            this.visPages.ReadOnly = true;
            this.visPages.Size = new System.Drawing.Size(100, 22);
            this.visPages.TabIndex = 5;
            // 
            // toCheck
            // 
            this.toCheck.Location = new System.Drawing.Point(144, 90);
            this.toCheck.Name = "toCheck";
            this.toCheck.ReadOnly = true;
            this.toCheck.Size = new System.Drawing.Size(100, 22);
            this.toCheck.TabIndex = 6;
            // 
            // time
            // 
            this.time.Location = new System.Drawing.Point(144, 124);
            this.time.Name = "time";
            this.time.ReadOnly = true;
            this.time.Size = new System.Drawing.Size(100, 22);
            this.time.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.avgLinks);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.time);
            this.groupBox1.Controls.Add(this.toCheck);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.visPages);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(491, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(288, 203);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Information";
            // 
            // avgLinks
            // 
            this.avgLinks.Location = new System.Drawing.Point(144, 159);
            this.avgLinks.Name = "avgLinks";
            this.avgLinks.ReadOnly = true;
            this.avgLinks.Size = new System.Drawing.Size(100, 22);
            this.avgLinks.TabIndex = 9;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(32, 164);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(104, 17);
            this.label11.TabIndex = 8;
            this.label11.Text = "Links pr. minut:";
            // 
            // seedURL
            // 
            this.seedURL.Location = new System.Drawing.Point(58, 62);
            this.seedURL.Name = "seedURL";
            this.seedURL.Size = new System.Drawing.Size(401, 22);
            this.seedURL.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(195, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 17);
            this.label5.TabIndex = 10;
            this.label5.Text = "Indtast seed-URL";
            // 
            // button
            // 
            this.button.Location = new System.Drawing.Point(384, 157);
            this.button.Name = "button";
            this.button.Size = new System.Drawing.Size(75, 23);
            this.button.TabIndex = 11;
            this.button.Text = "Start";
            this.button.UseVisualStyleBackColor = true;
            this.button.Click += new System.EventHandler(this.Start);
            // 
            // visitedPagesList
            // 
            this.visitedPagesList.HideSelection = false;
            this.visitedPagesList.Location = new System.Drawing.Point(58, 233);
            this.visitedPagesList.Name = "visitedPagesList";
            this.visitedPagesList.Size = new System.Drawing.Size(401, 129);
            this.visitedPagesList.TabIndex = 12;
            this.visitedPagesList.UseCompatibleStateImageBehavior = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(58, 197);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 17);
            this.label6.TabIndex = 13;
            this.label6.Text = "Besøgte sider";
            // 
            // numberOfCrawlers
            // 
            this.numberOfCrawlers.Location = new System.Drawing.Point(172, 157);
            this.numberOfCrawlers.Name = "numberOfCrawlers";
            this.numberOfCrawlers.Size = new System.Drawing.Size(141, 22);
            this.numberOfCrawlers.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(58, 157);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 17);
            this.label7.TabIndex = 15;
            this.label7.Text = "Antal crawlers:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(55, 381);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 17);
            this.label8.TabIndex = 16;
            this.label8.Text = "Fundne links";
            // 
            // foundLinks
            // 
            this.foundLinks.HideSelection = false;
            this.foundLinks.Location = new System.Drawing.Point(58, 414);
            this.foundLinks.Name = "foundLinks";
            this.foundLinks.Size = new System.Drawing.Size(401, 143);
            this.foundLinks.TabIndex = 17;
            this.foundLinks.UseCompatibleStateImageBehavior = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 17);
            this.label1.TabIndex = 18;
            this.label1.Text = "Søgeord: ";
            // 
            // searchWord
            // 
            this.searchWord.Location = new System.Drawing.Point(123, 110);
            this.searchWord.Name = "searchWord";
            this.searchWord.Size = new System.Drawing.Size(190, 22);
            this.searchWord.TabIndex = 19;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(491, 233);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(195, 17);
            this.label9.TabIndex = 20;
            this.label9.Text = "Sider indeholdende søgeord: ";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(331, 114);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(49, 17);
            this.label10.TabIndex = 22;
            this.label10.Text = "Dybde";
            // 
            // depth
            // 
            this.depth.Location = new System.Drawing.Point(387, 110);
            this.depth.Name = "depth";
            this.depth.Size = new System.Drawing.Size(72, 22);
            this.depth.TabIndex = 23;
            // 
            // searchResultList
            // 
            this.searchResultList.HideSelection = false;
            this.searchResultList.Location = new System.Drawing.Point(494, 263);
            this.searchResultList.Name = "searchResultList";
            this.searchResultList.Size = new System.Drawing.Size(285, 294);
            this.searchResultList.TabIndex = 21;
            this.searchResultList.UseCompatibleStateImageBehavior = false;
            // 
            // CrawlManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 582);
            this.Controls.Add(this.depth);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.searchResultList);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.searchWord);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.foundLinks);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.numberOfCrawlers);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.visitedPagesList);
            this.Controls.Add(this.button);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.seedURL);
            this.Controls.Add(this.groupBox1);
            this.Name = "CrawlManager";
            this.Text = "WebCrawler 1.0";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox visPages;
        private System.Windows.Forms.TextBox toCheck;
        private System.Windows.Forms.TextBox time;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox seedURL;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button;
        private System.Windows.Forms.ListView visitedPagesList;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox numberOfCrawlers;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListView foundLinks;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox searchWord;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox depth;
        private System.Windows.Forms.TextBox avgLinks;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ListView searchResultList;
    }
}

