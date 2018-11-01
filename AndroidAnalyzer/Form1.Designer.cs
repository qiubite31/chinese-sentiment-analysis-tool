namespace AndroidAnalyzer
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.開啟資料庫ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.開啟AccessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.載入檔案ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.載入負向詞庫ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.載入CWN資料庫ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.載入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.載入種子詞集ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.載入選取特徵詞集ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.輸出檔案ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.輸出文章表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.輸出頻率表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.關閉ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.說明ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.關於ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panelFileUpload = new System.Windows.Forms.Panel();
            this.linkIGScore = new System.Windows.Forms.LinkLabel();
            this.checkBoxIGScore = new System.Windows.Forms.CheckBox();
            this.linkSeed = new System.Windows.Forms.LinkLabel();
            this.checkBoxSeed = new System.Windows.Forms.CheckBox();
            this.linkVSM = new System.Windows.Forms.LinkLabel();
            this.checkBoxVSM = new System.Windows.Forms.CheckBox();
            this.linkSelectedFeature = new System.Windows.Forms.LinkLabel();
            this.checkBoxSelectedFeature = new System.Windows.Forms.CheckBox();
            this.linkTermFrenquency = new System.Windows.Forms.LinkLabel();
            this.checkBoxTermFrenquency = new System.Windows.Forms.CheckBox();
            this.linkArticleTable = new System.Windows.Forms.LinkLabel();
            this.checkBoxArticleTable = new System.Windows.Forms.CheckBox();
            this.linkNegation = new System.Windows.Forms.LinkLabel();
            this.linkAccess = new System.Windows.Forms.LinkLabel();
            this.checkBoxNegation = new System.Windows.Forms.CheckBox();
            this.checkBoxAccess = new System.Windows.Forms.CheckBox();
            this.checkBoxFileUpload = new System.Windows.Forms.CheckBox();
            this.MSG = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.labelNegInterval = new System.Windows.Forms.Label();
            this.textBoxNegInterval = new System.Windows.Forms.TextBox();
            this.NegInterval = new System.Windows.Forms.CheckBox();
            this.NegForward = new System.Windows.Forms.CheckBox();
            this.btnSameContent = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnNonChinese = new System.Windows.Forms.Button();
            this.btnNegation = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.LoadVPos = new System.Windows.Forms.LinkLabel();
            this.labelVPos = new System.Windows.Forms.Label();
            this.btnBuildDetailPos = new System.Windows.Forms.Button();
            this.btnStopFilter = new System.Windows.Forms.Button();
            this.btnSpeechFilter = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.BuildFromDB_S = new System.Windows.Forms.CheckBox();
            this.btnBuildVerbPos = new System.Windows.Forms.Button();
            this.btnSentimentScore = new System.Windows.Forms.Button();
            this.btnImportVerbPos = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSentimentSet = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.DetailPos = new System.Windows.Forms.CheckBox();
            this.TermLenThr = new System.Windows.Forms.TextBox();
            this.LoadNPos = new System.Windows.Forms.LinkLabel();
            this.DFThr = new System.Windows.Forms.TextBox();
            this.labelNPos = new System.Windows.Forms.Label();
            this.labelTermLenThr = new System.Windows.Forms.Label();
            this.labelDFThr = new System.Windows.Forms.Label();
            this.btnMIScore = new System.Windows.Forms.Button();
            this.btnTopicCluster = new System.Windows.Forms.Button();
            this.LoadN2C = new System.Windows.Forms.LinkLabel();
            this.LoadT2N = new System.Windows.Forms.LinkLabel();
            this.labelN2C = new System.Windows.Forms.Label();
            this.labelT2N = new System.Windows.Forms.Label();
            this.BuildFromDB_T = new System.Windows.Forms.CheckBox();
            this.btnTopicSet = new System.Windows.Forms.Button();
            this.TermThr = new System.Windows.Forms.CheckBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.btnFilterTopic = new System.Windows.Forms.Button();
            this.BarRecord = new System.Windows.Forms.TextBox();
            this.btnClusterVSM = new System.Windows.Forms.Button();
            this.btnTopicTag = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.btnBuildF2D = new System.Windows.Forms.Button();
            this.VSMbyCSV = new System.Windows.Forms.CheckBox();
            this.btnFS = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnBuildVSM = new System.Windows.Forms.Button();
            this.VSMSelected = new System.Windows.Forms.ComboBox();
            this.NotSelectedFeature = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCountTFIDF = new System.Windows.Forms.Button();
            this.btnBuildFrenquency = new System.Windows.Forms.Button();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.comboBoxApp = new System.Windows.Forms.ComboBox();
            this.comboBoxTopic = new System.Windows.Forms.ComboBox();
            this.btnBuildTopicCA = new System.Windows.Forms.Button();
            this.btnBuildTopicTrend = new System.Windows.Forms.Button();
            this.btnBuildSemtiTrend = new System.Windows.Forms.Button();
            this.btnBuildCA = new System.Windows.Forms.Button();
            this.MatrixT = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.DateRange = new System.Windows.Forms.CheckBox();
            this.DateStart = new System.Windows.Forms.TextBox();
            this.DateEnd = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panelFileUpload.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.開啟資料庫ToolStripMenuItem,
            this.說明ToolStripMenuItem,
            this.關於ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(521, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 開啟資料庫ToolStripMenuItem
            // 
            this.開啟資料庫ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.開啟AccessToolStripMenuItem,
            this.載入檔案ToolStripMenuItem,
            this.輸出檔案ToolStripMenuItem,
            this.關閉ToolStripMenuItem});
            this.開啟資料庫ToolStripMenuItem.Name = "開啟資料庫ToolStripMenuItem";
            this.開啟資料庫ToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.開啟資料庫ToolStripMenuItem.Text = "檔案";
            // 
            // 開啟AccessToolStripMenuItem
            // 
            this.開啟AccessToolStripMenuItem.Name = "開啟AccessToolStripMenuItem";
            this.開啟AccessToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.開啟AccessToolStripMenuItem.Text = "開啟Access";
            this.開啟AccessToolStripMenuItem.Click += new System.EventHandler(this.開啟AccessToolStripMenuItem_Click);
            // 
            // 載入檔案ToolStripMenuItem
            // 
            this.載入檔案ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.載入負向詞庫ToolStripMenuItem,
            this.載入CWN資料庫ToolStripMenuItem,
            this.載入ToolStripMenuItem,
            this.載入種子詞集ToolStripMenuItem,
            this.載入選取特徵詞集ToolStripMenuItem});
            this.載入檔案ToolStripMenuItem.Name = "載入檔案ToolStripMenuItem";
            this.載入檔案ToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.載入檔案ToolStripMenuItem.Text = "載入檔案";
            // 
            // 載入負向詞庫ToolStripMenuItem
            // 
            this.載入負向詞庫ToolStripMenuItem.Name = "載入負向詞庫ToolStripMenuItem";
            this.載入負向詞庫ToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.載入負向詞庫ToolStripMenuItem.Text = "載入負向詞集";
            this.載入負向詞庫ToolStripMenuItem.Click += new System.EventHandler(this.載入負向詞庫ToolStripMenuItem_Click);
            // 
            // 載入CWN資料庫ToolStripMenuItem
            // 
            this.載入CWN資料庫ToolStripMenuItem.Name = "載入CWN資料庫ToolStripMenuItem";
            this.載入CWN資料庫ToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.載入CWN資料庫ToolStripMenuItem.Text = "載入CWN資料庫";
            this.載入CWN資料庫ToolStripMenuItem.Click += new System.EventHandler(this.載入CWN資料庫ToolStripMenuItem_Click);
            // 
            // 載入ToolStripMenuItem
            // 
            this.載入ToolStripMenuItem.Name = "載入ToolStripMenuItem";
            this.載入ToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.載入ToolStripMenuItem.Text = "載入中文詞運網路LMF檔";
            this.載入ToolStripMenuItem.Click += new System.EventHandler(this.載入中文詞彙網路LMF檔ToolStripMenuItem_Click);
            // 
            // 載入種子詞集ToolStripMenuItem
            // 
            this.載入種子詞集ToolStripMenuItem.Name = "載入種子詞集ToolStripMenuItem";
            this.載入種子詞集ToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.載入種子詞集ToolStripMenuItem.Text = "載入種子詞集";
            this.載入種子詞集ToolStripMenuItem.Click += new System.EventHandler(this.載入種子詞集ToolStripMenuItem_Click);
            // 
            // 載入選取特徵詞集ToolStripMenuItem
            // 
            this.載入選取特徵詞集ToolStripMenuItem.Name = "載入選取特徵詞集ToolStripMenuItem";
            this.載入選取特徵詞集ToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.載入選取特徵詞集ToolStripMenuItem.Text = "載入選取特徵詞集";
            this.載入選取特徵詞集ToolStripMenuItem.Click += new System.EventHandler(this.載入選取特徵詞集ToolStripMenuItem_Click);
            // 
            // 輸出檔案ToolStripMenuItem
            // 
            this.輸出檔案ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.輸出文章表ToolStripMenuItem,
            this.輸出頻率表ToolStripMenuItem});
            this.輸出檔案ToolStripMenuItem.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.輸出檔案ToolStripMenuItem.Name = "輸出檔案ToolStripMenuItem";
            this.輸出檔案ToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.輸出檔案ToolStripMenuItem.Text = "輸出檔案";
            // 
            // 輸出文章表ToolStripMenuItem
            // 
            this.輸出文章表ToolStripMenuItem.Enabled = false;
            this.輸出文章表ToolStripMenuItem.Name = "輸出文章表ToolStripMenuItem";
            this.輸出文章表ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.輸出文章表ToolStripMenuItem.Text = "輸出文章表";
            this.輸出文章表ToolStripMenuItem.Click += new System.EventHandler(this.輸出文章表ToolStripMenuItem_Click);
            // 
            // 輸出頻率表ToolStripMenuItem
            // 
            this.輸出頻率表ToolStripMenuItem.Enabled = false;
            this.輸出頻率表ToolStripMenuItem.Name = "輸出頻率表ToolStripMenuItem";
            this.輸出頻率表ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.輸出頻率表ToolStripMenuItem.Text = "輸出頻率表";
            this.輸出頻率表ToolStripMenuItem.Click += new System.EventHandler(this.輸出頻率表ToolStripMenuItem_Click);
            // 
            // 關閉ToolStripMenuItem
            // 
            this.關閉ToolStripMenuItem.Name = "關閉ToolStripMenuItem";
            this.關閉ToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.關閉ToolStripMenuItem.Text = "關閉";
            this.關閉ToolStripMenuItem.Click += new System.EventHandler(this.關閉ToolStripMenuItem_Click);
            // 
            // 說明ToolStripMenuItem
            // 
            this.說明ToolStripMenuItem.Name = "說明ToolStripMenuItem";
            this.說明ToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.說明ToolStripMenuItem.Text = "說明";
            // 
            // 關於ToolStripMenuItem
            // 
            this.關於ToolStripMenuItem.Name = "關於ToolStripMenuItem";
            this.關於ToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.關於ToolStripMenuItem.Text = "關於";
            this.關於ToolStripMenuItem.Click += new System.EventHandler(this.關於ToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(521, 310);
            this.panel1.TabIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(521, 310);
            this.splitContainer1.SplitterDistance = 234;
            this.splitContainer1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.panelFileUpload);
            this.groupBox1.Controls.Add(this.checkBoxFileUpload);
            this.groupBox1.Controls.Add(this.MSG);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(234, 310);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Console Screen";
            // 
            // panelFileUpload
            // 
            this.panelFileUpload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panelFileUpload.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelFileUpload.Controls.Add(this.linkIGScore);
            this.panelFileUpload.Controls.Add(this.checkBoxIGScore);
            this.panelFileUpload.Controls.Add(this.linkSeed);
            this.panelFileUpload.Controls.Add(this.checkBoxSeed);
            this.panelFileUpload.Controls.Add(this.linkVSM);
            this.panelFileUpload.Controls.Add(this.checkBoxVSM);
            this.panelFileUpload.Controls.Add(this.linkSelectedFeature);
            this.panelFileUpload.Controls.Add(this.checkBoxSelectedFeature);
            this.panelFileUpload.Controls.Add(this.linkTermFrenquency);
            this.panelFileUpload.Controls.Add(this.checkBoxTermFrenquency);
            this.panelFileUpload.Controls.Add(this.linkArticleTable);
            this.panelFileUpload.Controls.Add(this.checkBoxArticleTable);
            this.panelFileUpload.Controls.Add(this.linkNegation);
            this.panelFileUpload.Controls.Add(this.linkAccess);
            this.panelFileUpload.Controls.Add(this.checkBoxNegation);
            this.panelFileUpload.Controls.Add(this.checkBoxAccess);
            this.panelFileUpload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFileUpload.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.panelFileUpload.Location = new System.Drawing.Point(3, 18);
            this.panelFileUpload.Name = "panelFileUpload";
            this.panelFileUpload.Size = new System.Drawing.Size(228, 289);
            this.panelFileUpload.TabIndex = 4;
            this.panelFileUpload.Visible = false;
            // 
            // linkIGScore
            // 
            this.linkIGScore.AutoSize = true;
            this.linkIGScore.Enabled = false;
            this.linkIGScore.Location = new System.Drawing.Point(98, 196);
            this.linkIGScore.Name = "linkIGScore";
            this.linkIGScore.Size = new System.Drawing.Size(32, 16);
            this.linkIGScore.TabIndex = 16;
            this.linkIGScore.TabStop = true;
            this.linkIGScore.Text = "開啟";
            this.linkIGScore.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkIGScore_LinkClicked);
            // 
            // checkBoxIGScore
            // 
            this.checkBoxIGScore.AutoSize = true;
            this.checkBoxIGScore.Enabled = false;
            this.checkBoxIGScore.Location = new System.Drawing.Point(2, 192);
            this.checkBoxIGScore.Name = "checkBoxIGScore";
            this.checkBoxIGScore.Size = new System.Drawing.Size(99, 20);
            this.checkBoxIGScore.TabIndex = 15;
            this.checkBoxIGScore.Text = "特徵詞IG分數";
            this.checkBoxIGScore.UseVisualStyleBackColor = true;
            // 
            // linkSeed
            // 
            this.linkSeed.AutoSize = true;
            this.linkSeed.Enabled = false;
            this.linkSeed.Location = new System.Drawing.Point(99, 114);
            this.linkSeed.Name = "linkSeed";
            this.linkSeed.Size = new System.Drawing.Size(32, 16);
            this.linkSeed.TabIndex = 14;
            this.linkSeed.TabStop = true;
            this.linkSeed.Text = "開啟";
            this.linkSeed.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSeed_LinkClicked);
            // 
            // checkBoxSeed
            // 
            this.checkBoxSeed.AutoSize = true;
            this.checkBoxSeed.Enabled = false;
            this.checkBoxSeed.Location = new System.Drawing.Point(3, 113);
            this.checkBoxSeed.Name = "checkBoxSeed";
            this.checkBoxSeed.Size = new System.Drawing.Size(75, 20);
            this.checkBoxSeed.TabIndex = 13;
            this.checkBoxSeed.Text = "種子詞集";
            this.checkBoxSeed.UseVisualStyleBackColor = true;
            // 
            // linkVSM
            // 
            this.linkVSM.AutoSize = true;
            this.linkVSM.Enabled = false;
            this.linkVSM.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.linkVSM.Location = new System.Drawing.Point(99, 140);
            this.linkVSM.Name = "linkVSM";
            this.linkVSM.Size = new System.Drawing.Size(32, 16);
            this.linkVSM.TabIndex = 12;
            this.linkVSM.TabStop = true;
            this.linkVSM.Text = "開啟";
            this.linkVSM.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkVSM_LinkClicked);
            // 
            // checkBoxVSM
            // 
            this.checkBoxVSM.AutoSize = true;
            this.checkBoxVSM.Enabled = false;
            this.checkBoxVSM.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.checkBoxVSM.Location = new System.Drawing.Point(3, 139);
            this.checkBoxVSM.Name = "checkBoxVSM";
            this.checkBoxVSM.Size = new System.Drawing.Size(99, 20);
            this.checkBoxVSM.TabIndex = 11;
            this.checkBoxVSM.Text = "向量空間模型";
            this.checkBoxVSM.UseVisualStyleBackColor = true;
            // 
            // linkSelectedFeature
            // 
            this.linkSelectedFeature.AutoSize = true;
            this.linkSelectedFeature.Enabled = false;
            this.linkSelectedFeature.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.linkSelectedFeature.Location = new System.Drawing.Point(98, 165);
            this.linkSelectedFeature.Name = "linkSelectedFeature";
            this.linkSelectedFeature.Size = new System.Drawing.Size(32, 16);
            this.linkSelectedFeature.TabIndex = 10;
            this.linkSelectedFeature.TabStop = true;
            this.linkSelectedFeature.Text = "開啟";
            this.linkSelectedFeature.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSelectedFeature_LinkClicked);
            // 
            // checkBoxSelectedFeature
            // 
            this.checkBoxSelectedFeature.AutoSize = true;
            this.checkBoxSelectedFeature.Enabled = false;
            this.checkBoxSelectedFeature.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.checkBoxSelectedFeature.Location = new System.Drawing.Point(3, 165);
            this.checkBoxSelectedFeature.Name = "checkBoxSelectedFeature";
            this.checkBoxSelectedFeature.Size = new System.Drawing.Size(99, 20);
            this.checkBoxSelectedFeature.TabIndex = 9;
            this.checkBoxSelectedFeature.Text = "選取特徵詞集";
            this.checkBoxSelectedFeature.UseVisualStyleBackColor = true;
            // 
            // linkTermFrenquency
            // 
            this.linkTermFrenquency.AutoSize = true;
            this.linkTermFrenquency.Enabled = false;
            this.linkTermFrenquency.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.linkTermFrenquency.Location = new System.Drawing.Point(98, 87);
            this.linkTermFrenquency.Name = "linkTermFrenquency";
            this.linkTermFrenquency.Size = new System.Drawing.Size(32, 16);
            this.linkTermFrenquency.TabIndex = 8;
            this.linkTermFrenquency.TabStop = true;
            this.linkTermFrenquency.Text = "開啟";
            this.linkTermFrenquency.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkTermFrenquency_LinkClicked);
            // 
            // checkBoxTermFrenquency
            // 
            this.checkBoxTermFrenquency.AutoSize = true;
            this.checkBoxTermFrenquency.Enabled = false;
            this.checkBoxTermFrenquency.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.checkBoxTermFrenquency.Location = new System.Drawing.Point(3, 87);
            this.checkBoxTermFrenquency.Name = "checkBoxTermFrenquency";
            this.checkBoxTermFrenquency.Size = new System.Drawing.Size(87, 20);
            this.checkBoxTermFrenquency.TabIndex = 7;
            this.checkBoxTermFrenquency.Text = "字詞頻率表";
            this.checkBoxTermFrenquency.UseVisualStyleBackColor = true;
            // 
            // linkArticleTable
            // 
            this.linkArticleTable.AutoSize = true;
            this.linkArticleTable.Enabled = false;
            this.linkArticleTable.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.linkArticleTable.Location = new System.Drawing.Point(100, 65);
            this.linkArticleTable.Name = "linkArticleTable";
            this.linkArticleTable.Size = new System.Drawing.Size(32, 16);
            this.linkArticleTable.TabIndex = 6;
            this.linkArticleTable.TabStop = true;
            this.linkArticleTable.Text = "開啟";
            this.linkArticleTable.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkIndex_LinkClicked);
            // 
            // checkBoxArticleTable
            // 
            this.checkBoxArticleTable.AutoSize = true;
            this.checkBoxArticleTable.Enabled = false;
            this.checkBoxArticleTable.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.checkBoxArticleTable.Location = new System.Drawing.Point(3, 61);
            this.checkBoxArticleTable.Name = "checkBoxArticleTable";
            this.checkBoxArticleTable.Size = new System.Drawing.Size(63, 20);
            this.checkBoxArticleTable.TabIndex = 5;
            this.checkBoxArticleTable.Text = "文章表";
            this.checkBoxArticleTable.UseVisualStyleBackColor = true;
            // 
            // linkNegation
            // 
            this.linkNegation.AutoSize = true;
            this.linkNegation.Enabled = false;
            this.linkNegation.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.linkNegation.Location = new System.Drawing.Point(100, 36);
            this.linkNegation.Name = "linkNegation";
            this.linkNegation.Size = new System.Drawing.Size(32, 16);
            this.linkNegation.TabIndex = 4;
            this.linkNegation.TabStop = true;
            this.linkNegation.Text = "開啟";
            this.linkNegation.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkNegation_LinkClicked);
            // 
            // linkAccess
            // 
            this.linkAccess.AutoSize = true;
            this.linkAccess.Enabled = false;
            this.linkAccess.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.linkAccess.Location = new System.Drawing.Point(100, 10);
            this.linkAccess.Name = "linkAccess";
            this.linkAccess.Size = new System.Drawing.Size(32, 16);
            this.linkAccess.TabIndex = 3;
            this.linkAccess.TabStop = true;
            this.linkAccess.Text = "開啟";
            this.linkAccess.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkAccess_LinkClicked);
            // 
            // checkBoxNegation
            // 
            this.checkBoxNegation.AutoSize = true;
            this.checkBoxNegation.Enabled = false;
            this.checkBoxNegation.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.checkBoxNegation.Location = new System.Drawing.Point(3, 35);
            this.checkBoxNegation.Name = "checkBoxNegation";
            this.checkBoxNegation.Size = new System.Drawing.Size(87, 20);
            this.checkBoxNegation.TabIndex = 1;
            this.checkBoxNegation.Text = "負向詞詞集";
            this.checkBoxNegation.UseVisualStyleBackColor = true;
            // 
            // checkBoxAccess
            // 
            this.checkBoxAccess.AutoSize = true;
            this.checkBoxAccess.Enabled = false;
            this.checkBoxAccess.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.checkBoxAccess.Location = new System.Drawing.Point(3, 9);
            this.checkBoxAccess.Name = "checkBoxAccess";
            this.checkBoxAccess.Size = new System.Drawing.Size(100, 20);
            this.checkBoxAccess.TabIndex = 2;
            this.checkBoxAccess.Text = "Access資料庫";
            this.checkBoxAccess.UseVisualStyleBackColor = true;
            // 
            // checkBoxFileUpload
            // 
            this.checkBoxFileUpload.AutoSize = true;
            this.checkBoxFileUpload.Location = new System.Drawing.Point(119, 0);
            this.checkBoxFileUpload.Name = "checkBoxFileUpload";
            this.checkBoxFileUpload.Size = new System.Drawing.Size(96, 16);
            this.checkBoxFileUpload.TabIndex = 3;
            this.checkBoxFileUpload.Text = "檔案載入狀況";
            this.checkBoxFileUpload.UseVisualStyleBackColor = true;
            this.checkBoxFileUpload.CheckedChanged += new System.EventHandler(this.checkBoxFileUpload_CheckedChanged);
            // 
            // MSG
            // 
            this.MSG.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.MSG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MSG.Location = new System.Drawing.Point(3, 18);
            this.MSG.Multiline = true;
            this.MSG.Name = "MSG";
            this.MSG.ReadOnly = true;
            this.MSG.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.MSG.Size = new System.Drawing.Size(228, 289);
            this.MSG.TabIndex = 5;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(283, 310);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(275, 284);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "前處理";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.labelNegInterval);
            this.splitContainer2.Panel1.Controls.Add(this.textBoxNegInterval);
            this.splitContainer2.Panel1.Controls.Add(this.NegInterval);
            this.splitContainer2.Panel1.Controls.Add(this.NegForward);
            this.splitContainer2.Panel1.Controls.Add(this.btnSameContent);
            this.splitContainer2.Panel1.Controls.Add(this.label2);
            this.splitContainer2.Panel1.Controls.Add(this.btnNonChinese);
            this.splitContainer2.Panel1.Controls.Add(this.btnNegation);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer2.Size = new System.Drawing.Size(269, 278);
            this.splitContainer2.SplitterDistance = 113;
            this.splitContainer2.TabIndex = 1;
            // 
            // labelNegInterval
            // 
            this.labelNegInterval.AutoSize = true;
            this.labelNegInterval.Location = new System.Drawing.Point(124, 85);
            this.labelNegInterval.Name = "labelNegInterval";
            this.labelNegInterval.Size = new System.Drawing.Size(35, 12);
            this.labelNegInterval.TabIndex = 9;
            this.labelNegInterval.Text = "範圍: ";
            // 
            // textBoxNegInterval
            // 
            this.textBoxNegInterval.Location = new System.Drawing.Point(165, 80);
            this.textBoxNegInterval.Name = "textBoxNegInterval";
            this.textBoxNegInterval.Size = new System.Drawing.Size(31, 22);
            this.textBoxNegInterval.TabIndex = 8;
            this.textBoxNegInterval.Text = "2";
            // 
            // NegInterval
            // 
            this.NegInterval.AutoSize = true;
            this.NegInterval.Checked = true;
            this.NegInterval.CheckState = System.Windows.Forms.CheckState.Checked;
            this.NegInterval.Location = new System.Drawing.Point(31, 84);
            this.NegInterval.Name = "NegInterval";
            this.NegInterval.Size = new System.Drawing.Size(72, 16);
            this.NegInterval.TabIndex = 7;
            this.NegInterval.Text = "區間判斷";
            this.NegInterval.UseVisualStyleBackColor = true;
            this.NegInterval.CheckedChanged += new System.EventHandler(this.NegInterval_CheckedChanged);
            // 
            // NegForward
            // 
            this.NegForward.AutoSize = true;
            this.NegForward.Enabled = false;
            this.NegForward.Location = new System.Drawing.Point(31, 63);
            this.NegForward.Name = "NegForward";
            this.NegForward.Size = new System.Drawing.Size(72, 16);
            this.NegForward.TabIndex = 6;
            this.NegForward.Text = "向前判斷";
            this.NegForward.UseVisualStyleBackColor = true;
            this.NegForward.CheckedChanged += new System.EventHandler(this.NegForward_CheckedChanged);
            // 
            // btnSameContent
            // 
            this.btnSameContent.Location = new System.Drawing.Point(14, 5);
            this.btnSameContent.Name = "btnSameContent";
            this.btnSameContent.Size = new System.Drawing.Size(114, 23);
            this.btnSameContent.TabIndex = 5;
            this.btnSameContent.Text = "過濾相同文章資料";
            this.btnSameContent.UseVisualStyleBackColor = true;
            this.btnSameContent.Click += new System.EventHandler(this.btnSameContent_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(134, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "請先載入負向詞集";
            // 
            // btnNonChinese
            // 
            this.btnNonChinese.Enabled = false;
            this.btnNonChinese.Location = new System.Drawing.Point(134, 5);
            this.btnNonChinese.Name = "btnNonChinese";
            this.btnNonChinese.Size = new System.Drawing.Size(114, 23);
            this.btnNonChinese.TabIndex = 0;
            this.btnNonChinese.Text = "過濾非中文資料";
            this.btnNonChinese.UseVisualStyleBackColor = true;
            this.btnNonChinese.Click += new System.EventHandler(this.btnNonChinese_Click);
            // 
            // btnNegation
            // 
            this.btnNegation.Enabled = false;
            this.btnNegation.Location = new System.Drawing.Point(14, 34);
            this.btnNegation.Name = "btnNegation";
            this.btnNegation.Size = new System.Drawing.Size(114, 23);
            this.btnNegation.TabIndex = 3;
            this.btnNegation.Text = "處理負向文字";
            this.btnNegation.UseVisualStyleBackColor = true;
            this.btnNegation.Click += new System.EventHandler(this.btnNegation_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.LoadVPos);
            this.groupBox3.Controls.Add(this.labelVPos);
            this.groupBox3.Controls.Add(this.btnBuildDetailPos);
            this.groupBox3.Controls.Add(this.btnStopFilter);
            this.groupBox3.Controls.Add(this.btnSpeechFilter);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(269, 161);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "字詞過濾";
            // 
            // LoadVPos
            // 
            this.LoadVPos.AutoSize = true;
            this.LoadVPos.Location = new System.Drawing.Point(106, 98);
            this.LoadVPos.Name = "LoadVPos";
            this.LoadVPos.Size = new System.Drawing.Size(29, 12);
            this.LoadVPos.TabIndex = 10;
            this.LoadVPos.TabStop = true;
            this.LoadVPos.Text = "載入";
            this.LoadVPos.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LoadVPos_LinkClicked);
            // 
            // labelVPos
            // 
            this.labelVPos.AutoSize = true;
            this.labelVPos.Location = new System.Drawing.Point(15, 98);
            this.labelVPos.Name = "labelVPos";
            this.labelVPos.Size = new System.Drawing.Size(92, 12);
            this.labelVPos.TabIndex = 8;
            this.labelVPos.Text = "□ 述詞詳細詞表";
            // 
            // btnBuildDetailPos
            // 
            this.btnBuildDetailPos.Location = new System.Drawing.Point(134, 50);
            this.btnBuildDetailPos.Name = "btnBuildDetailPos";
            this.btnBuildDetailPos.Size = new System.Drawing.Size(118, 23);
            this.btnBuildDetailPos.TabIndex = 6;
            this.btnBuildDetailPos.Text = "建立詳細詞性表";
            this.btnBuildDetailPos.UseVisualStyleBackColor = true;
            this.btnBuildDetailPos.Click += new System.EventHandler(this.btnBuildDetailPos_Click);
            // 
            // btnStopFilter
            // 
            this.btnStopFilter.Enabled = false;
            this.btnStopFilter.Location = new System.Drawing.Point(14, 21);
            this.btnStopFilter.Name = "btnStopFilter";
            this.btnStopFilter.Size = new System.Drawing.Size(114, 23);
            this.btnStopFilter.TabIndex = 5;
            this.btnStopFilter.Text = "停用詞過濾";
            this.btnStopFilter.UseVisualStyleBackColor = true;
            this.btnStopFilter.Click += new System.EventHandler(this.btnStopFilter_Click);
            // 
            // btnSpeechFilter
            // 
            this.btnSpeechFilter.Enabled = false;
            this.btnSpeechFilter.Location = new System.Drawing.Point(14, 50);
            this.btnSpeechFilter.Name = "btnSpeechFilter";
            this.btnSpeechFilter.Size = new System.Drawing.Size(114, 23);
            this.btnSpeechFilter.TabIndex = 4;
            this.btnSpeechFilter.Text = "詞性過濾";
            this.btnSpeechFilter.UseVisualStyleBackColor = true;
            this.btnSpeechFilter.Click += new System.EventHandler(this.btnSpeechFilter_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.BuildFromDB_S);
            this.tabPage2.Controls.Add(this.btnBuildVerbPos);
            this.tabPage2.Controls.Add(this.btnSentimentScore);
            this.tabPage2.Controls.Add(this.btnImportVerbPos);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.btnSentimentSet);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(275, 284);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "情感傾向";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(73, 85);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(125, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "並載入詳細詞性對照表";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(235, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "請先載入:  CWN資料庫 、LMF檔、種子詞集";
            // 
            // BuildFromDB_S
            // 
            this.BuildFromDB_S.AutoSize = true;
            this.BuildFromDB_S.Location = new System.Drawing.Point(165, 17);
            this.BuildFromDB_S.Name = "BuildFromDB_S";
            this.BuildFromDB_S.Size = new System.Drawing.Size(96, 16);
            this.BuildFromDB_S.TabIndex = 3;
            this.BuildFromDB_S.Text = "由資料庫讀取";
            this.BuildFromDB_S.UseVisualStyleBackColor = true;
            this.BuildFromDB_S.CheckedChanged += new System.EventHandler(this.ReadFromDB_CheckedChanged);
            // 
            // btnBuildVerbPos
            // 
            this.btnBuildVerbPos.Enabled = false;
            this.btnBuildVerbPos.Location = new System.Drawing.Point(116, 226);
            this.btnBuildVerbPos.Name = "btnBuildVerbPos";
            this.btnBuildVerbPos.Size = new System.Drawing.Size(151, 23);
            this.btnBuildVerbPos.TabIndex = 1;
            this.btnBuildVerbPos.Text = "產生動詞與詳細詞性對照";
            this.btnBuildVerbPos.UseVisualStyleBackColor = true;
            this.btnBuildVerbPos.Click += new System.EventHandler(this.btnBuildVerbPos_Click);
            // 
            // btnSentimentScore
            // 
            this.btnSentimentScore.Enabled = false;
            this.btnSentimentScore.Location = new System.Drawing.Point(120, 156);
            this.btnSentimentScore.Name = "btnSentimentScore";
            this.btnSentimentScore.Size = new System.Drawing.Size(141, 23);
            this.btnSentimentScore.TabIndex = 2;
            this.btnSentimentScore.Text = "計算文章情感分數";
            this.btnSentimentScore.UseVisualStyleBackColor = true;
            this.btnSentimentScore.Click += new System.EventHandler(this.btnSentimentScore_Click);
            // 
            // btnImportVerbPos
            // 
            this.btnImportVerbPos.Location = new System.Drawing.Point(16, 109);
            this.btnImportVerbPos.Name = "btnImportVerbPos";
            this.btnImportVerbPos.Size = new System.Drawing.Size(151, 23);
            this.btnImportVerbPos.TabIndex = 0;
            this.btnImportVerbPos.Text = "載入動詞與詳細詞性對照";
            this.btnImportVerbPos.UseVisualStyleBackColor = true;
            this.btnImportVerbPos.Click += new System.EventHandler(this.btnVerbPos_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "從種子詞集開始建立情感詞集";
            // 
            // btnSentimentSet
            // 
            this.btnSentimentSet.Enabled = false;
            this.btnSentimentSet.Location = new System.Drawing.Point(16, 17);
            this.btnSentimentSet.Name = "btnSentimentSet";
            this.btnSentimentSet.Size = new System.Drawing.Size(143, 23);
            this.btnSentimentSet.TabIndex = 0;
            this.btnSentimentSet.Text = "建立情感詞集";
            this.btnSentimentSet.UseVisualStyleBackColor = true;
            this.btnSentimentSet.Click += new System.EventHandler(this.btnSentimentSet_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.DetailPos);
            this.tabPage3.Controls.Add(this.TermLenThr);
            this.tabPage3.Controls.Add(this.LoadNPos);
            this.tabPage3.Controls.Add(this.DFThr);
            this.tabPage3.Controls.Add(this.labelNPos);
            this.tabPage3.Controls.Add(this.labelTermLenThr);
            this.tabPage3.Controls.Add(this.labelDFThr);
            this.tabPage3.Controls.Add(this.btnMIScore);
            this.tabPage3.Controls.Add(this.btnTopicCluster);
            this.tabPage3.Controls.Add(this.LoadN2C);
            this.tabPage3.Controls.Add(this.LoadT2N);
            this.tabPage3.Controls.Add(this.labelN2C);
            this.tabPage3.Controls.Add(this.labelT2N);
            this.tabPage3.Controls.Add(this.BuildFromDB_T);
            this.tabPage3.Controls.Add(this.btnTopicSet);
            this.tabPage3.Controls.Add(this.TermThr);
            this.tabPage3.Controls.Add(this.trackBar1);
            this.tabPage3.Controls.Add(this.btnFilterTopic);
            this.tabPage3.Controls.Add(this.BarRecord);
            this.tabPage3.Controls.Add(this.btnClusterVSM);
            this.tabPage3.Controls.Add(this.btnTopicTag);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(275, 284);
            this.tabPage3.TabIndex = 4;
            this.tabPage3.Text = "議題類別";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // DetailPos
            // 
            this.DetailPos.AutoSize = true;
            this.DetailPos.Location = new System.Drawing.Point(137, 85);
            this.DetailPos.Name = "DetailPos";
            this.DetailPos.Size = new System.Drawing.Size(96, 16);
            this.DetailPos.TabIndex = 23;
            this.DetailPos.Text = "詳細詞性過濾";
            this.DetailPos.UseVisualStyleBackColor = true;
            // 
            // TermLenThr
            // 
            this.TermLenThr.Enabled = false;
            this.TermLenThr.Location = new System.Drawing.Point(86, 127);
            this.TermLenThr.Name = "TermLenThr";
            this.TermLenThr.Size = new System.Drawing.Size(43, 22);
            this.TermLenThr.TabIndex = 14;
            this.TermLenThr.Text = "1";
            // 
            // LoadNPos
            // 
            this.LoadNPos.AutoSize = true;
            this.LoadNPos.Location = new System.Drawing.Point(231, 111);
            this.LoadNPos.Name = "LoadNPos";
            this.LoadNPos.Size = new System.Drawing.Size(29, 12);
            this.LoadNPos.TabIndex = 9;
            this.LoadNPos.TabStop = true;
            this.LoadNPos.Text = "載入";
            this.LoadNPos.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LoadNPos_LinkClicked);
            // 
            // DFThr
            // 
            this.DFThr.Enabled = false;
            this.DFThr.Location = new System.Drawing.Point(86, 106);
            this.DFThr.Name = "DFThr";
            this.DFThr.Size = new System.Drawing.Size(43, 22);
            this.DFThr.TabIndex = 13;
            this.DFThr.Text = "1";
            // 
            // labelNPos
            // 
            this.labelNPos.AutoSize = true;
            this.labelNPos.Location = new System.Drawing.Point(135, 111);
            this.labelNPos.Name = "labelNPos";
            this.labelNPos.Size = new System.Drawing.Size(92, 12);
            this.labelNPos.TabIndex = 7;
            this.labelNPos.Text = "□ 體詞詳細詞表";
            // 
            // labelTermLenThr
            // 
            this.labelTermLenThr.AutoSize = true;
            this.labelTermLenThr.Location = new System.Drawing.Point(27, 130);
            this.labelTermLenThr.Name = "labelTermLenThr";
            this.labelTermLenThr.Size = new System.Drawing.Size(53, 12);
            this.labelTermLenThr.TabIndex = 16;
            this.labelTermLenThr.Text = "字詞長度";
            // 
            // labelDFThr
            // 
            this.labelDFThr.AutoSize = true;
            this.labelDFThr.Location = new System.Drawing.Point(27, 112);
            this.labelDFThr.Name = "labelDFThr";
            this.labelDFThr.Size = new System.Drawing.Size(43, 12);
            this.labelDFThr.TabIndex = 15;
            this.labelDFThr.Text = "DF門檻";
            // 
            // btnMIScore
            // 
            this.btnMIScore.Enabled = false;
            this.btnMIScore.Location = new System.Drawing.Point(21, 201);
            this.btnMIScore.Name = "btnMIScore";
            this.btnMIScore.Size = new System.Drawing.Size(119, 23);
            this.btnMIScore.TabIndex = 22;
            this.btnMIScore.Text = "計算MI分數";
            this.btnMIScore.UseVisualStyleBackColor = true;
            this.btnMIScore.Click += new System.EventHandler(this.btnMIScore_Click);
            // 
            // btnTopicCluster
            // 
            this.btnTopicCluster.Enabled = false;
            this.btnTopicCluster.Location = new System.Drawing.Point(167, 163);
            this.btnTopicCluster.Name = "btnTopicCluster";
            this.btnTopicCluster.Size = new System.Drawing.Size(99, 23);
            this.btnTopicCluster.TabIndex = 21;
            this.btnTopicCluster.Text = "議題詞分群結果";
            this.btnTopicCluster.UseVisualStyleBackColor = true;
            this.btnTopicCluster.Click += new System.EventHandler(this.btnTopicCluster_Click);
            // 
            // LoadN2C
            // 
            this.LoadN2C.AutoSize = true;
            this.LoadN2C.Enabled = false;
            this.LoadN2C.Location = new System.Drawing.Point(135, 174);
            this.LoadN2C.Name = "LoadN2C";
            this.LoadN2C.Size = new System.Drawing.Size(29, 12);
            this.LoadN2C.TabIndex = 20;
            this.LoadN2C.TabStop = true;
            this.LoadN2C.Text = "載入";
            this.LoadN2C.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LoadN2C_LinkClicked);
            // 
            // LoadT2N
            // 
            this.LoadT2N.AutoSize = true;
            this.LoadT2N.Enabled = false;
            this.LoadT2N.Location = new System.Drawing.Point(135, 158);
            this.LoadT2N.Name = "LoadT2N";
            this.LoadT2N.Size = new System.Drawing.Size(29, 12);
            this.LoadT2N.TabIndex = 19;
            this.LoadT2N.TabStop = true;
            this.LoadT2N.Text = "載入";
            this.LoadT2N.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LoadT2N_LinkClicked);
            // 
            // labelN2C
            // 
            this.labelN2C.AutoSize = true;
            this.labelN2C.Location = new System.Drawing.Point(19, 174);
            this.labelN2C.Name = "labelN2C";
            this.labelN2C.Size = new System.Drawing.Size(110, 12);
            this.labelN2C.TabIndex = 18;
            this.labelN2C.Text = "× 載入網路與群對照";
            // 
            // labelT2N
            // 
            this.labelT2N.AutoSize = true;
            this.labelT2N.Location = new System.Drawing.Point(19, 158);
            this.labelT2N.Name = "labelT2N";
            this.labelT2N.Size = new System.Drawing.Size(110, 12);
            this.labelT2N.TabIndex = 17;
            this.labelT2N.Text = "× 載入詞與網路對照";
            // 
            // BuildFromDB_T
            // 
            this.BuildFromDB_T.AutoSize = true;
            this.BuildFromDB_T.Location = new System.Drawing.Point(170, 24);
            this.BuildFromDB_T.Name = "BuildFromDB_T";
            this.BuildFromDB_T.Size = new System.Drawing.Size(96, 16);
            this.BuildFromDB_T.TabIndex = 9;
            this.BuildFromDB_T.Text = "由資料庫讀取";
            this.BuildFromDB_T.UseVisualStyleBackColor = true;
            this.BuildFromDB_T.CheckedChanged += new System.EventHandler(this.BuildFromDB_T_CheckedChanged);
            // 
            // btnTopicSet
            // 
            this.btnTopicSet.Enabled = false;
            this.btnTopicSet.Location = new System.Drawing.Point(21, 17);
            this.btnTopicSet.Name = "btnTopicSet";
            this.btnTopicSet.Size = new System.Drawing.Size(140, 23);
            this.btnTopicSet.TabIndex = 5;
            this.btnTopicSet.Text = "建立候選議題詞集";
            this.btnTopicSet.UseVisualStyleBackColor = true;
            this.btnTopicSet.Click += new System.EventHandler(this.btnTopicSet_Click);
            // 
            // TermThr
            // 
            this.TermThr.AutoSize = true;
            this.TermThr.Location = new System.Drawing.Point(29, 84);
            this.TermThr.Name = "TermThr";
            this.TermThr.Size = new System.Drawing.Size(96, 16);
            this.TermThr.TabIndex = 11;
            this.TermThr.Text = "字詞篩選門檻";
            this.TermThr.UseVisualStyleBackColor = true;
            this.TermThr.CheckedChanged += new System.EventHandler(this.TermThr_CheckedChanged);
            // 
            // trackBar1
            // 
            this.trackBar1.BackColor = System.Drawing.Color.White;
            this.trackBar1.Location = new System.Drawing.Point(21, 46);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(210, 45);
            this.trackBar1.TabIndex = 6;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // btnFilterTopic
            // 
            this.btnFilterTopic.Enabled = false;
            this.btnFilterTopic.Location = new System.Drawing.Point(135, 127);
            this.btnFilterTopic.Name = "btnFilterTopic";
            this.btnFilterTopic.Size = new System.Drawing.Size(125, 23);
            this.btnFilterTopic.TabIndex = 7;
            this.btnFilterTopic.Text = "過濾候選議題詞";
            this.btnFilterTopic.UseVisualStyleBackColor = true;
            this.btnFilterTopic.Click += new System.EventHandler(this.btnFilterTopic_Click);
            // 
            // BarRecord
            // 
            this.BarRecord.Location = new System.Drawing.Point(233, 57);
            this.BarRecord.Name = "BarRecord";
            this.BarRecord.Size = new System.Drawing.Size(33, 22);
            this.BarRecord.TabIndex = 8;
            this.BarRecord.Text = " 0%";
            this.BarRecord.TextChanged += new System.EventHandler(this.BarRecord_TextChanged);
            // 
            // btnClusterVSM
            // 
            this.btnClusterVSM.Enabled = false;
            this.btnClusterVSM.Location = new System.Drawing.Point(21, 230);
            this.btnClusterVSM.Name = "btnClusterVSM";
            this.btnClusterVSM.Size = new System.Drawing.Size(119, 23);
            this.btnClusterVSM.TabIndex = 12;
            this.btnClusterVSM.Text = "建立分群VSM";
            this.btnClusterVSM.UseVisualStyleBackColor = true;
            this.btnClusterVSM.Click += new System.EventHandler(this.btnClusterVSM_Click);
            // 
            // btnTopicTag
            // 
            this.btnTopicTag.Enabled = false;
            this.btnTopicTag.Location = new System.Drawing.Point(154, 230);
            this.btnTopicTag.Name = "btnTopicTag";
            this.btnTopicTag.Size = new System.Drawing.Size(116, 23);
            this.btnTopicTag.TabIndex = 10;
            this.btnTopicTag.Text = "標記文章議題類別";
            this.btnTopicTag.UseVisualStyleBackColor = true;
            this.btnTopicTag.Click += new System.EventHandler(this.btnTopicTag_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.btnBuildF2D);
            this.tabPage4.Controls.Add(this.VSMbyCSV);
            this.tabPage4.Controls.Add(this.btnFS);
            this.tabPage4.Controls.Add(this.button2);
            this.tabPage4.Controls.Add(this.btnBuildVSM);
            this.tabPage4.Controls.Add(this.VSMSelected);
            this.tabPage4.Controls.Add(this.NotSelectedFeature);
            this.tabPage4.Controls.Add(this.label3);
            this.tabPage4.Controls.Add(this.btnCountTFIDF);
            this.tabPage4.Controls.Add(this.btnBuildFrenquency);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(275, 284);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "VSM";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // btnBuildF2D
            // 
            this.btnBuildF2D.Location = new System.Drawing.Point(20, 226);
            this.btnBuildF2D.Name = "btnBuildF2D";
            this.btnBuildF2D.Size = new System.Drawing.Size(124, 23);
            this.btnBuildF2D.TabIndex = 14;
            this.btnBuildF2D.Text = "建立特徵對文章向量";
            this.btnBuildF2D.UseVisualStyleBackColor = true;
            this.btnBuildF2D.Click += new System.EventHandler(this.btnBuildF2D_Click);
            // 
            // VSMbyCSV
            // 
            this.VSMbyCSV.AutoSize = true;
            this.VSMbyCSV.Location = new System.Drawing.Point(150, 140);
            this.VSMbyCSV.Name = "VSMbyCSV";
            this.VSMbyCSV.Size = new System.Drawing.Size(94, 16);
            this.VSMbyCSV.TabIndex = 13;
            this.VSMbyCSV.Text = "從CSV檔輸入";
            this.VSMbyCSV.UseVisualStyleBackColor = true;
            this.VSMbyCSV.CheckedChanged += new System.EventHandler(this.VSMbyCSV_CheckedChanged);
            // 
            // btnFS
            // 
            this.btnFS.Enabled = false;
            this.btnFS.Location = new System.Drawing.Point(20, 138);
            this.btnFS.Name = "btnFS";
            this.btnFS.Size = new System.Drawing.Size(124, 23);
            this.btnFS.TabIndex = 12;
            this.btnFS.Text = "特徵詞選取";
            this.btnFS.UseVisualStyleBackColor = true;
            this.btnFS.Click += new System.EventHandler(this.btnFS_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(150, 226);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(117, 23);
            this.button2.TabIndex = 11;
            this.button2.Text = "分群測試(輸出座標)";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnBuildVSM
            // 
            this.btnBuildVSM.Enabled = false;
            this.btnBuildVSM.Location = new System.Drawing.Point(20, 60);
            this.btnBuildVSM.Name = "btnBuildVSM";
            this.btnBuildVSM.Size = new System.Drawing.Size(124, 23);
            this.btnBuildVSM.TabIndex = 10;
            this.btnBuildVSM.Text = "建立向量空間模型";
            this.btnBuildVSM.UseVisualStyleBackColor = true;
            this.btnBuildVSM.Click += new System.EventHandler(this.btnBuildVSM_Click);
            // 
            // VSMSelected
            // 
            this.VSMSelected.FormattingEnabled = true;
            this.VSMSelected.Items.AddRange(new object[] {
            "無類別",
            "情感類別",
            "議題類別"});
            this.VSMSelected.Location = new System.Drawing.Point(150, 83);
            this.VSMSelected.Name = "VSMSelected";
            this.VSMSelected.Size = new System.Drawing.Size(76, 20);
            this.VSMSelected.TabIndex = 9;
            this.VSMSelected.Text = "無類別";
            // 
            // NotSelectedFeature
            // 
            this.NotSelectedFeature.AutoSize = true;
            this.NotSelectedFeature.Location = new System.Drawing.Point(150, 61);
            this.NotSelectedFeature.Name = "NotSelectedFeature";
            this.NotSelectedFeature.Size = new System.Drawing.Size(60, 16);
            this.NotSelectedFeature.TabIndex = 8;
            this.NotSelectedFeature.Text = "不選詞";
            this.NotSelectedFeature.UseVisualStyleBackColor = true;
            this.NotSelectedFeature.CheckedChanged += new System.EventHandler(this.NotSelectedFeature_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(257, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "要輸出已選特徵的，請先載入已選擇的特徵詞集";
            // 
            // btnCountTFIDF
            // 
            this.btnCountTFIDF.Enabled = false;
            this.btnCountTFIDF.Location = new System.Drawing.Point(150, 17);
            this.btnCountTFIDF.Name = "btnCountTFIDF";
            this.btnCountTFIDF.Size = new System.Drawing.Size(117, 23);
            this.btnCountTFIDF.TabIndex = 5;
            this.btnCountTFIDF.Text = "計算文章TFIDF";
            this.btnCountTFIDF.UseVisualStyleBackColor = true;
            this.btnCountTFIDF.Click += new System.EventHandler(this.btnCountTFIDF_Click);
            // 
            // btnBuildFrenquency
            // 
            this.btnBuildFrenquency.Enabled = false;
            this.btnBuildFrenquency.Location = new System.Drawing.Point(20, 16);
            this.btnBuildFrenquency.Name = "btnBuildFrenquency";
            this.btnBuildFrenquency.Size = new System.Drawing.Size(124, 23);
            this.btnBuildFrenquency.TabIndex = 4;
            this.btnBuildFrenquency.Text = "計算字詞頻率";
            this.btnBuildFrenquency.UseVisualStyleBackColor = true;
            this.btnBuildFrenquency.Click += new System.EventHandler(this.btnBuildFrenquency_Click);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.DateEnd);
            this.tabPage5.Controls.Add(this.DateStart);
            this.tabPage5.Controls.Add(this.DateRange);
            this.tabPage5.Controls.Add(this.comboBoxApp);
            this.tabPage5.Controls.Add(this.comboBoxTopic);
            this.tabPage5.Controls.Add(this.btnBuildTopicCA);
            this.tabPage5.Controls.Add(this.btnBuildTopicTrend);
            this.tabPage5.Controls.Add(this.btnBuildSemtiTrend);
            this.tabPage5.Controls.Add(this.btnBuildCA);
            this.tabPage5.Controls.Add(this.MatrixT);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(275, 284);
            this.tabPage5.TabIndex = 5;
            this.tabPage5.Text = "工具";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // comboBoxApp
            // 
            this.comboBoxApp.FormattingEnabled = true;
            this.comboBoxApp.Items.AddRange(new object[] {
            "LINE",
            "Facebook 手機即時通",
            "WhatsApp Messenger",
            "WeChat"});
            this.comboBoxApp.Location = new System.Drawing.Point(16, 168);
            this.comboBoxApp.Name = "comboBoxApp";
            this.comboBoxApp.Size = new System.Drawing.Size(121, 20);
            this.comboBoxApp.TabIndex = 8;
            this.comboBoxApp.Text = "LINE";
            // 
            // comboBoxTopic
            // 
            this.comboBoxTopic.FormattingEnabled = true;
            this.comboBoxTopic.Items.AddRange(new object[] {
            "介面風格與設計",
            "訊息傳送與社群",
            "帳戶與安全性",
            "軟體表現與穩定性",
            "應用加值服務"});
            this.comboBoxTopic.Location = new System.Drawing.Point(16, 194);
            this.comboBoxTopic.Name = "comboBoxTopic";
            this.comboBoxTopic.Size = new System.Drawing.Size(121, 20);
            this.comboBoxTopic.TabIndex = 7;
            this.comboBoxTopic.Text = "介面風格與設計";
            // 
            // btnBuildTopicCA
            // 
            this.btnBuildTopicCA.Location = new System.Drawing.Point(16, 138);
            this.btnBuildTopicCA.Name = "btnBuildTopicCA";
            this.btnBuildTopicCA.Size = new System.Drawing.Size(121, 23);
            this.btnBuildTopicCA.TabIndex = 6;
            this.btnBuildTopicCA.Text = "產生議題CA列聯表";
            this.btnBuildTopicCA.UseVisualStyleBackColor = true;
            this.btnBuildTopicCA.Click += new System.EventHandler(this.btnBuildTopicCA_Click);
            // 
            // btnBuildTopicTrend
            // 
            this.btnBuildTopicTrend.Location = new System.Drawing.Point(16, 98);
            this.btnBuildTopicTrend.Name = "btnBuildTopicTrend";
            this.btnBuildTopicTrend.Size = new System.Drawing.Size(151, 23);
            this.btnBuildTopicTrend.TabIndex = 5;
            this.btnBuildTopicTrend.Text = "產生議題正負趨勢表";
            this.btnBuildTopicTrend.UseVisualStyleBackColor = true;
            this.btnBuildTopicTrend.Click += new System.EventHandler(this.btnBuildTopicTrend_Click);
            // 
            // btnBuildSemtiTrend
            // 
            this.btnBuildSemtiTrend.Location = new System.Drawing.Point(16, 68);
            this.btnBuildSemtiTrend.Name = "btnBuildSemtiTrend";
            this.btnBuildSemtiTrend.Size = new System.Drawing.Size(151, 23);
            this.btnBuildSemtiTrend.TabIndex = 4;
            this.btnBuildSemtiTrend.Text = "產生正負趨勢表";
            this.btnBuildSemtiTrend.UseVisualStyleBackColor = true;
            this.btnBuildSemtiTrend.Click += new System.EventHandler(this.btnBuildSemtiSeries_Click);
            // 
            // btnBuildCA
            // 
            this.btnBuildCA.Location = new System.Drawing.Point(16, 38);
            this.btnBuildCA.Name = "btnBuildCA";
            this.btnBuildCA.Size = new System.Drawing.Size(151, 23);
            this.btnBuildCA.TabIndex = 3;
            this.btnBuildCA.Text = "產生CA列聯表";
            this.btnBuildCA.UseVisualStyleBackColor = true;
            this.btnBuildCA.Click += new System.EventHandler(this.btnBuildCA_Click);
            // 
            // MatrixT
            // 
            this.MatrixT.Location = new System.Drawing.Point(16, 8);
            this.MatrixT.Name = "MatrixT";
            this.MatrixT.Size = new System.Drawing.Size(151, 23);
            this.MatrixT.TabIndex = 2;
            this.MatrixT.Text = "向量轉置";
            this.MatrixT.UseVisualStyleBackColor = true;
            this.MatrixT.Click += new System.EventHandler(this.MatrixT_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 312);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(521, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusLabel
            // 
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // DateRange
            // 
            this.DateRange.AutoSize = true;
            this.DateRange.Location = new System.Drawing.Point(143, 145);
            this.DateRange.Name = "DateRange";
            this.DateRange.Size = new System.Drawing.Size(96, 16);
            this.DateRange.TabIndex = 9;
            this.DateRange.Text = "限定時間區間";
            this.DateRange.UseVisualStyleBackColor = true;
            // 
            // DateStart
            // 
            this.DateStart.Location = new System.Drawing.Point(144, 165);
            this.DateStart.Name = "DateStart";
            this.DateStart.Size = new System.Drawing.Size(100, 22);
            this.DateStart.TabIndex = 10;
            this.DateStart.Text = "02/20/2014";
            // 
            // DateEnd
            // 
            this.DateEnd.Location = new System.Drawing.Point(144, 191);
            this.DateEnd.Name = "DateEnd";
            this.DateEnd.Size = new System.Drawing.Size(100, 22);
            this.DateEnd.TabIndex = 11;
            this.DateEnd.Text = "03/03/2014";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 334);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Sentiment Analyzer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panelFileUpload.ResumeLayout(false);
            this.panelFileUpload.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem 開啟資料庫ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 開啟AccessToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 說明ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 關於ToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnNonChinese;
        private System.Windows.Forms.ToolStripMenuItem 載入檔案ToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStripMenuItem 載入負向詞庫ToolStripMenuItem;
        private System.Windows.Forms.Button btnNegation;
        private System.Windows.Forms.ToolStripMenuItem 關閉ToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.CheckBox checkBoxFileUpload;
        private System.Windows.Forms.CheckBox checkBoxAccess;
        private System.Windows.Forms.CheckBox checkBoxNegation;
        private System.Windows.Forms.Panel panelFileUpload;
        private System.Windows.Forms.LinkLabel linkNegation;
        private System.Windows.Forms.LinkLabel linkAccess;
        private System.Windows.Forms.TextBox MSG;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.Button btnBuildFrenquency;
        private System.Windows.Forms.Button btnSpeechFilter;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.LinkLabel linkArticleTable;
        private System.Windows.Forms.CheckBox checkBoxArticleTable;
        private System.Windows.Forms.LinkLabel linkTermFrenquency;
        private System.Windows.Forms.CheckBox checkBoxTermFrenquency;
        private System.Windows.Forms.ToolStripMenuItem 輸出檔案ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 輸出文章表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 輸出頻率表ToolStripMenuItem;
        private System.Windows.Forms.Button btnCountTFIDF;
        private System.Windows.Forms.ToolStripMenuItem 載入選取特徵詞集ToolStripMenuItem;
        private System.Windows.Forms.LinkLabel linkSelectedFeature;
        private System.Windows.Forms.CheckBox checkBoxSelectedFeature;
        private System.Windows.Forms.LinkLabel linkVSM;
        private System.Windows.Forms.CheckBox checkBoxVSM;
        private System.Windows.Forms.ToolStripMenuItem 載入ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 載入種子詞集ToolStripMenuItem;
        private System.Windows.Forms.Button btnSentimentSet;
        private System.Windows.Forms.LinkLabel linkSeed;
        private System.Windows.Forms.CheckBox checkBoxSeed;
        private System.Windows.Forms.ToolStripMenuItem 載入CWN資料庫ToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSentimentScore;
        private System.Windows.Forms.CheckBox BuildFromDB_S;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnTopicSet;
        private System.Windows.Forms.Button btnFilterTopic;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.TextBox BarRecord;
        private System.Windows.Forms.CheckBox BuildFromDB_T;
        private System.Windows.Forms.Button btnTopicTag;
        private System.Windows.Forms.Button btnStopFilter;
        private System.Windows.Forms.CheckBox NotSelectedFeature;
        private System.Windows.Forms.ComboBox VSMSelected;
        private System.Windows.Forms.Button btnBuildVSM;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnFS;
        private System.Windows.Forms.CheckBox VSMbyCSV;
        private System.Windows.Forms.LinkLabel linkIGScore;
        private System.Windows.Forms.CheckBox checkBoxIGScore;
        private System.Windows.Forms.Button btnBuildF2D;
        private System.Windows.Forms.CheckBox TermThr;
        private System.Windows.Forms.Button btnSameContent;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btnImportVerbPos;
        private System.Windows.Forms.Button btnBuildVerbPos;
        private System.Windows.Forms.Button btnClusterVSM;
        private System.Windows.Forms.Button MatrixT;
        private System.Windows.Forms.Label labelTermLenThr;
        private System.Windows.Forms.Label labelDFThr;
        private System.Windows.Forms.TextBox TermLenThr;
        private System.Windows.Forms.TextBox DFThr;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelN2C;
        private System.Windows.Forms.Label labelT2N;
        private System.Windows.Forms.LinkLabel LoadN2C;
        private System.Windows.Forms.LinkLabel LoadT2N;
        private System.Windows.Forms.Button btnTopicCluster;
        private System.Windows.Forms.Button btnMIScore;
        private System.Windows.Forms.CheckBox NegInterval;
        private System.Windows.Forms.CheckBox NegForward;
        private System.Windows.Forms.Label labelNegInterval;
        private System.Windows.Forms.TextBox textBoxNegInterval;
        private System.Windows.Forms.Button btnBuildDetailPos;
        private System.Windows.Forms.Label labelVPos;
        private System.Windows.Forms.Label labelNPos;
        private System.Windows.Forms.LinkLabel LoadVPos;
        private System.Windows.Forms.LinkLabel LoadNPos;
        private System.Windows.Forms.CheckBox DetailPos;
        private System.Windows.Forms.Button btnBuildCA;
        private System.Windows.Forms.Button btnBuildSemtiTrend;
        private System.Windows.Forms.Button btnBuildTopicTrend;
        private System.Windows.Forms.ComboBox comboBoxTopic;
        private System.Windows.Forms.Button btnBuildTopicCA;
        private System.Windows.Forms.ComboBox comboBoxApp;
        private System.Windows.Forms.CheckBox DateRange;
        private System.Windows.Forms.TextBox DateEnd;
        private System.Windows.Forms.TextBox DateStart;
    }
}

