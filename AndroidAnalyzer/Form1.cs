using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.Collections;
using System.Net;

namespace AndroidAnalyzer
{
    public partial class Form1 : Form
    {
        public String AccessPath = null;  //資料庫路徑
        public String NegationPath = null; //否向詞集路徑
        public String ArticleTablePath = null; //文章表路徑
        public String TermFrenquencyPath = null; //詞頻表路徑
        public String SelectedFeaturePath = null; //特徵詞表路徑
        public String VSMPath = null;  //向量空間模型路徑
        public String LMFPath = null; //LMF檔路衡
        public String SeedPath = null; //種子詞集路徑
        public String IGScorePath = null; //IG分數表
        static ArticleModel ArticleCollection = null;  //文章集合物件
        static NegationProcess NegationHandle = null;  //控制否向詞物件
        static Dictionary<String, Term> TermDic = null;  //所有字詞表
        static VectorSpaceModel VSM = null;
        static ChineseWordNet CWN  = null;  //中文詞彙網路處理物件
        static SentmentAnalysis SA = null;
        static TopicExtraction TE = null; //處理議題擷取
        static Dictionary<String, double> FeatureIGScore = null;
        public static AccessManipulate AccessOperate = null;  //資料庫操作物件
        public static AccessManipulate CWNOperate = null;
        public static Dictionary<String, String> VerbDetailPos = null;
        public static Dictionary<String, String> NounDetailPos = null;
        Boolean tfidfFinish = false; //檢查tfidf是否計算完畢
        static SparseMatrix<double> MIScore = null;


        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        private void 開啟AccessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Access檔案(*.mdb, *.accdb)|*.mdb;*.accdb";
            openFileDialog1.FilterIndex = 1;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                AccessPath = openFileDialog1.FileName;
                
                AccessOperate = new AccessManipulate(openFileDialog1.FileName);
                AccessOperate.ConnAccess();

                checkBoxAccess.Checked = true;
                linkAccess.Enabled = true;
                btnNonChinese.Enabled = true;
                btnBuildVerbPos.Enabled = true;

                BuildModel();
                UpdateStatus("已開啟Access資料庫: " + AccessPath);
            }

            //判斷開啟建立情成詞集功能
            if ((ArticleCollection != null && checkBoxSeed.Checked == true && CWNOperate != null && LMFPath != null && VerbDetailPos != null) || BuildFromDB_S.Checked == true)
                btnSentimentSet.Enabled = true;
            //判斷開啟建立議題詞集功能
            if (ArticleCollection != null && SA != null && SA.SentimentTermSet != null)
                btnTopicSet.Enabled = true;
            //判斷開啟議題類別計算功能
            if (Form1.AccessOperate != null && Form1.AccessOperate.CheckTableExist("TopicTerm"))
                btnTopicTag.Enabled = true;
            if(tfidfFinish)
                btnClusterVSM.Enabled = true;


        }
        private void 載入負向詞庫ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "CSV檔案(*.mdb, *.txt)|*.csv;*.txt";
            openFileDialog1.FilterIndex = 1;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                NegationPath = openFileDialog1.FileName;
                checkBoxNegation.Checked = true;
                linkNegation.Enabled = true;

                NegationHandle = new NegationProcess(NegationPath); //將負向詞集建立NegationProcess

                UpdateStatus("已載入負向詞集: " + NegationPath);
            }

            if(ArticleCollection != null) //確定已建立ArticleModel，開啟負向詞處理功能
                btnNegation.Enabled = true;
        }
        private void 載入選取特徵詞集ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "CSV檔案(*.mdb, *.txt)|*.csv;*.txt";
            openFileDialog1.FilterIndex = 1;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                SelectedFeaturePath = openFileDialog1.FileName;

                checkBoxSelectedFeature.Checked = true;
                linkSelectedFeature.Enabled = true;

                if (tfidfFinish)  //確認已算完TFIDF則開啟建立VSM功能
                    btnBuildVSM.Enabled = true;

                UpdateStatus("已載入選取的特徵詞集: " + SelectedFeaturePath);
            }


        }
        private void 載入CWN資料庫ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Access檔案(*.mdb, *.accdb)|*.mdb;*.accdb";
            openFileDialog1.FilterIndex = 1;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                CWNOperate = new AccessManipulate(openFileDialog1.FileName);
                CWNOperate.ConnAccess();

                if (ArticleCollection != null && checkBoxSeed.Checked == true && CWNOperate != null && LMFPath != null)
                    btnSentimentSet.Enabled = true;

                UpdateStatus("已載入中文詞彙網路資料庫: " + openFileDialog1.FileName);
            }
        }
        private void 載入中文詞彙網路LMF檔ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "xml檔案(*.xml)|*.xml";
            openFileDialog1.FilterIndex = 1;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                LMFPath = openFileDialog1.FileName;

                CWN = new ChineseWordNet(openFileDialog1.FileName);  //產生ChineseWordNet物件
                CWN.SetForm(this);

                if (ArticleCollection != null && checkBoxSeed.Checked == true && CWNOperate != null && LMFPath != null)
                    btnSentimentSet.Enabled = true;

                UpdateStatus("已載入中文詞彙網路LMF檔: " + LMFPath);
            }


        }
        private void 載入種子詞集ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "CSV檔案(*.mdb, *.txt)|*.csv;*.txt";
            openFileDialog1.FilterIndex = 1;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                checkBoxSeed.Checked = true;
                linkSeed.Enabled = true;
                SeedPath = openFileDialog1.FileName;

                SA = new SentmentAnalysis(SeedPath);  //設定原始種子譽

                if (ArticleCollection != null && checkBoxSeed.Checked == true && CWNOperate != null && LMFPath != null)
                    btnSentimentSet.Enabled = true;

                UpdateStatus("已載入種子詞集: " + SeedPath);
            }


        }
        private void BuildModel()
        {
            UpdateMSG("\n");
            UpdateMSG("建立ArticleModel...");

            DataSet AllData = AccessOperate.SelectQuery("SELECT * FROM AppComment", "AppComment");

            ArticleCollection = new ArticleModel(AllData.Tables["AppComment"], this);

            UpdateMSG("建立ArticleModel完成，共" + ArticleCollection.Count + "筆資料" + "\n");

            if (checkBoxNegation.Checked == true)  //建立模型後，確定是否載入負向詞集，並開啟負向詞處理功能
                btnNegation.Enabled = true;

            btnSpeechFilter.Enabled = true;
            btnStopFilter.Enabled = true;
            btnBuildFrenquency.Enabled = true;
            輸出文章表ToolStripMenuItem.Enabled = true;
        }
        private void 輸出文章表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateMSG("\n");
            UpdateMSG("正在建立文章表...");
            OutputFile.OutputArticleTable(this, Path.GetDirectoryName(AccessPath), ArticleCollection);

            ArticleTablePath = Path.GetDirectoryName(AccessPath) + "\\ArticleTable.csv";
            checkBoxArticleTable.Checked = true;
            linkArticleTable.Enabled = true;

            MessageBox.Show("建立文章表完成！");
            UpdateMSG("建立文章表完成！");
        }

        private void 輸出頻率表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateMSG("\n");
            UpdateMSG("正在輸出字詞頻率表...");
            OutputFile.OutputTermFrenquency(this, Path.GetDirectoryName(AccessPath), Form1.TermDic);

            MessageBox.Show("輸出字詞頻率表完成！");
            UpdateMSG("輸出字詞頻率表完成！");

            TermFrenquencyPath = Path.GetDirectoryName(AccessPath) + "\\TermFrenquency.csv";
            checkBoxTermFrenquency.Checked = true;
            linkTermFrenquency.Enabled = true;

        }
        private void btnNonChinese_Click(object sender, EventArgs e)
        {
            UpdateMSG("\n");
            UpdateMSG("正在過濾文章...");

            DataFiltering filter = new DataFiltering(ArticleCollection);
            Form1.ArticleCollection = filter.NonChineseFiltering(this);

            MessageBox.Show("過濾文章完成！");
            UpdateMSG("過濾文章完成！");
            UpdateStatus("過濾文章完成！");
        }
        private void btnNegation_Click(object sender, EventArgs e)
        {
            UpdateMSG("\n");
            UpdateMSG("開始處理否定詞...");

            if(NegForward.Checked == true)
                NegationHandle.NegationDetect(ArticleCollection, 0, this);
            if(NegInterval.Checked == true)
                NegationHandle.NegationDetect(ArticleCollection, int.Parse(textBoxNegInterval.Text), this);


            MessageBox.Show("處理否定詞完成！");
            UpdateMSG("處理否定詞完成！");
            UpdateStatus("處理否定詞完成！");
            
        }
        private void btnSpeechFilter_Click(object sender, EventArgs e)
        {
            UpdateMSG("\n");
            UpdateMSG("開始詞性過濾...");

            DataFiltering filtering = new DataFiltering(ArticleCollection);
            ArticleCollection = filtering.SpeechFiltering(this);

            MessageBox.Show("詞性過濾完成！");
            UpdateMSG("詞性過濾完成！");
            UpdateStatus("詞性過濾完成！");
        }
        private void btnStopFilter_Click(object sender, EventArgs e)
        {
            String StopWordPath;

            openFileDialog1.Filter = "CSV檔案(*.mdb, *.txt)|*.csv;*.txt";
            openFileDialog1.FilterIndex = 1;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                UpdateMSG("\n");
                UpdateMSG("開始停用字過濾...");

                StopWordPath = openFileDialog1.FileName;

                List<String> StopWordList = ReadFile.ReadOneRowCSV(StopWordPath).ToList();

                DataFiltering filtering = new DataFiltering(ArticleCollection);
                ArticleCollection = filtering.StopWordFiltering(StopWordList, this);

                MessageBox.Show("停用字過濾完成！");
                UpdateMSG("停用字過濾完成！");
                UpdateStatus("停用字過濾完成！");

            }
        }
        private void btnSentimentSet_Click(object sender, EventArgs e)
        {
            if (BuildFromDB_S.Checked == false)
            {
                UpdateMSG("\n");
                UpdateMSG("正在使用種子詞集作情感詞擴充...");
                SA.SetCWN(CWN);  //設定所需要的ChineseWordNet物件
                SA.ExtendTermBySeed(this);  //擴展原始種子詞
                UpdateMSG("正在使用動詞詞集作情感詞擴充...");
                SA.ExtendTermByVerb(ArticleCollection, this);  //使用動詞擴展情感詞集
                UpdateMSG("建置情感詞集完成！");
                UpdateStatus("建置情感詞集完成！");
            }
            else
            {
                UpdateMSG("\n");
                UpdateMSG("正在從資料庫已存在的情感詞建立情感詞集物件...");
                SA = new SentmentAnalysis();
                SA.SetSentimentTermSetFromDB(this);
                UpdateMSG("建立情感詞集物件完成！");
                UpdateStatus("建立情感詞集物件完成！");
            }

            btnSentimentScore.Enabled = true;
            btnTopicSet.Enabled = true;
        }
        private void ReadFromDB_CheckedChanged(object sender, EventArgs e)
        {
            if (BuildFromDB_S.Checked == true && AccessOperate != null)
                btnSentimentSet.Enabled = true;
            else
                btnSentimentSet.Enabled = false;
        }
        private void btnSentimentScore_Click(object sender, EventArgs e)
        {
            UpdateMSG("\n");
            UpdateMSG("正在計算文章情感傾向...");
            SA.CalSentimentScore(ArticleCollection, this);
            
            UpdateMSG("正在計算文章情感傾向完成！");
            UpdateStatus("正在計算文章情感傾向完成！");
        }
        private void btnTopicSet_Click(object sender, EventArgs e)
        {
            if (BuildFromDB_T.Checked == true)
            {
                UpdateMSG("\n");
                UpdateMSG("正在從資料庫已存在的名詞詞集建立議題詞集物件...");

                TE = new TopicExtraction(ArticleCollection);
                TE.SetTopicTermFromDB(this);

                UpdateMSG("建立議題詞集物件完成！");
                UpdateStatus("建立議題詞集物件完成！");
            }
            else
            {
                UpdateMSG("\n");
                UpdateMSG("正在建立候選情感詞集...");

                TE = new TopicExtraction(ArticleCollection);
                TE.SelectCandidateTopic(SA.SentimentTermSet, this);

                UpdateMSG("建置候選詞集完成！");
                UpdateStatus("建置候選詞集完成！");
            }
            btnFilterTopic.Enabled = true;
            LoadT2N.Enabled = true;
            LoadN2C.Enabled = true;
            if (tfidfFinish)
                btnClusterVSM.Enabled = true;
        }
        private void btnFilterTopic_Click(object sender, EventArgs e)
        {
            if (TermThr.Checked == true && DetailPos.Checked == true)
                TE.FilterTopicTerm(trackBar1.Value, int.Parse(DFThr.Text), int.Parse(TermLenThr.Text),true, this);
            else if(TermThr.Checked == true && DetailPos.Checked != true)
                TE.FilterTopicTerm(trackBar1.Value, int.Parse(DFThr.Text), int.Parse(TermLenThr.Text), false, this);
            else if(TermThr.Checked != true && DetailPos.Checked == true)
                TE.FilterTopicTerm(trackBar1.Value, 0, 0, true, this);
            else
                TE.FilterTopicTerm(trackBar1.Value, 0, 0,false, this);
             
            btnMIScore.Enabled = true;
             UpdateStatus("名詞過濾完成！");
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            int p = trackBar1.Value;
            BarRecord.Text = p.ToString() + "%";
        }
        private void BarRecord_TextChanged(object sender, EventArgs e)
        {
            int p = int.Parse(BarRecord.Text.Replace("%", ""));
            trackBar1.Value = p;
        }
        private void btnBuildFrenquency_Click(object sender, EventArgs e)
        {
            UpdateMSG("\n");
            UpdateMSG("開始計算字詞出現頻率...");
            FeatureWeight FW = new FeatureWeight(ArticleCollection);
            Form1.TermDic = FW.BuildTermList(this);

            UpdateMSG("正在取代原有的Term物件...");
            Form1.ArticleCollection = FW.ReplaceTerm(this);

            MessageBox.Show("計算字詞頻率完成！");
            UpdateStatus("字詞頻率計算完成！");
            UpdateMSG("計算字詞頻率完成！");


            輸出頻率表ToolStripMenuItem.Enabled = true;
            btnCountTFIDF.Enabled = true;

        }
        private void btnCountTFIDF_Click(object sender, EventArgs e)
        {
            UpdateMSG("\n");
            UpdateMSG("正在計算TFIDF值");
            FeatureWeight FW = new FeatureWeight(ArticleCollection);
            Form1.ArticleCollection = FW.CountTFIDF(this);

            UpdateStatus("文章字詞的TFIDF值計算完成！");
            UpdateMSG("計算TFIDF值完成！");

            tfidfFinish = true;

            if(TE != null)
                btnClusterVSM.Enabled = true;

            if(checkBoxSelectedFeature.Checked == true || NotSelectedFeature.Checked == true)  //確認已載入選取特徵詞集，則開啟計算VSM功能
                btnBuildVSM.Enabled = true;

        }
        private void btnTopicTag_Click(object sender, EventArgs e)
        {
            UpdateMSG("\n");
            UpdateMSG("正在判斷文章的議題類別...");

            if(SA == null)
                SA = new SentmentAnalysis();

            ArticleCollection = SA.TopicClassTag(ArticleCollection, this);

            UpdateMSG("判斷文章的議題類別完成！");
            UpdateStatus("判斷文章類別完成！");

        }
        private void btnBuildVSM_Click(object sender, EventArgs e)
        {
            UpdateMSG("\n");
            UpdateMSG("正在建立向量空間模型");

            VSM = new VectorSpaceModel();

            if (NotSelectedFeature.Checked == true)
            {
                VSM.SetFeatureCollection(TermDic);
                VSM.BuildVSM(this, ArticleCollection);

                VSM.ClassTag(ArticleCollection, VSMSelected.SelectedIndex, this);
                OutputFile.OutputVSMbySparseMatrix(this, VSM.FeatureCollection, Path.GetDirectoryName(AccessPath), VSM.VSM);
            }
            else
            {
                VSM.SetFeatureCollection(SelectedFeaturePath);
                VSM.BuildVSM(this, ArticleCollection);

                VSM.ClassTag(ArticleCollection, VSMSelected.SelectedIndex, this);
                OutputFile.OutputVSMbySparseMatrix(this, VSM.FeatureCollection, Path.GetDirectoryName(AccessPath), VSM.VSM);
            }
            UpdateStatus("向量空間模型完成！");
            UpdateMSG("向量空間模型完成！");

            VSMPath = Path.GetDirectoryName(AccessPath) + "\\VectorSpaceModel.csv";
            checkBoxVSM.Checked = true;
            linkVSM.Enabled = true;
            btnFS.Enabled = true;
        }
        private void btnFS_Click(object sender, EventArgs e)
        {
            VectorSpaceModel VSM;
            String VSMFilePath = "";

            if (VSMbyCSV.Checked == true)  //從外部csv建立VSM進行特徵詞選取
            {
                openFileDialog1.Filter = "csv檔案(*.csv)|*.csv";
                openFileDialog1.FilterIndex = 1;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    UpdateMSG("\n");
                    UpdateMSG("正在建立向量空間模型");

                    VSMFilePath = openFileDialog1.FileName;

                    //從載入的CSV檔建立向量空間模型
                    VSM = new VectorSpaceModel();

                    VSM.SetFeatureCollection(VSMFilePath);  //設定特徵詞詞集
                    VSM.BuildVSMbyCSV(this, VSMFilePath);  //建立向量空間模型
                    //-------------------------------------

                    VSMPath = VSMFilePath;  //設定向量空間模型的位址

                    checkBoxVSM.Checked = true;
                    linkVSM.Enabled = true;

                    UpdateMSG("\n");
                    UpdateMSG("正在進行特徵詞選取...");

                    //建立特徵詞選取
                    FeatureSelection FS = new FeatureSelection(VSM);  
                    FeatureIGScore = FS.InformationGain(this);  //進行IG特徵詞選取方法
                    OutputFile.OutputFeatureIGScore(this, FeatureIGScore, Path.GetDirectoryName(VSMPath));  //輸出IG分數表

                    checkBoxIGScore.Checked = true;
                    linkIGScore.Enabled = true;

                    IGScorePath = Path.GetDirectoryName(VSMPath) + "\\IGScore.csv";

                    UpdateStatus("特徵詞選取方法完成！");
                }

            }
            else  //從程式建立的VSM進行特徵詞選取
            {
                UpdateMSG("\n");
                UpdateMSG("正在進行特徵詞選取...");

                VSM = Form1.VSM;
                VSMFilePath = VSMPath;

                FeatureSelection FS = new FeatureSelection(VSM);
                FeatureIGScore = FS.InformationGain(this);

                OutputFile.OutputFeatureIGScore(this, FeatureIGScore, Path.GetDirectoryName(VSMPath));

                checkBoxIGScore.Checked = true;
                linkIGScore.Enabled = true;

                IGScorePath = Path.GetDirectoryName(VSMPath) + "\\IGScore.csv";

                UpdateStatus("特徵詞選取方法完成！");
            }
        }
        public void UpdateMSG(String status)
        {
            MSG.Text += status + Environment.NewLine;
            MSG.Refresh();
            MSG.SelectionStart = MSG.Text.Length;
            MSG.ScrollToCaret();
            Application.DoEvents();

        }
        public void UpdateStatus(String status)
        {
            StatusLabel.Text = status;
            Application.DoEvents();
        }
        private void checkBoxFileUpload_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxFileUpload.Checked == true)
            {
                MSG.Visible = false;
                panelFileUpload.Visible = true;
                groupBox1.Text = "File Upload Status";
                /*
                foreach (Control ctl in panelFileUpload.Controls)
                {
                    if (ctl is CheckBox)
                        ((CheckBox)ctl).Visible = true;
                }*/
            }
            else
            {
                MSG.Visible = true;
                panelFileUpload.Visible = false;
                groupBox1.Text = "Console Screen";
            }

        }

        private void linkAccess_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(AccessPath);
        }

        private void linkNegation_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(NegationPath);
        }
        private void linkIndex_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(ArticleTablePath);
        }
        private void linkTermFrenquency_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(TermFrenquencyPath);
        }
        private void linkSelectedFeature_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(SelectedFeaturePath);
        }
        private void linkVSM_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(VSMPath);
        }
        private void linkSeed_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(SeedPath);
        }
        private void 關閉ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 關於ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("情感分析與文字分類工具 V1.0" + "\n" +
                            "Dragon" + "\n" +
                            "ICT Lab");
        }
        private void linkIGScore_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(IGScorePath);
        }
        private void NotSelectedFeature_CheckedChanged(object sender, EventArgs e)
        {
            if (NotSelectedFeature.Checked == true && Form1.AccessOperate != null)
            {
                if(ArticleCollection.GetArticleList().First.Value.GetTermArray()[0].TF != 0)
                    btnBuildVSM.Enabled = true;
                else
                    btnBuildVSM.Enabled = false;
            }
            else if (SelectedFeaturePath != null && ArticleCollection.GetArticleList().First.Value.GetTermArray()[0].TF != 0)
                btnBuildVSM.Enabled = true;
            else
                btnBuildVSM.Enabled = false;
        }
        private void VSMbyCSV_CheckedChanged(object sender, EventArgs e)
        {
            if (VSMbyCSV.Checked == true)
                btnFS.Enabled = true;
            else if (VSM != null)
                btnFS.Enabled = true;
            else
                btnFS.Enabled = false;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            /*double ClassEntropy = (-(2.0 / 5.0) * Math.Log(2.0 / 5.0, 2)) + (-(2.0 / 5.0) * Math.Log(2.0 / 5.0, 2)) + (-(1.0 / 5.0) * Math.Log(1.0 / 5.0, 2));

            double x1 = (2.0 / 5.0) * 2.0 * (-(1.0 / 2.0) * Math.Log(1.0 / 2.0, 2));
            x1 += (3.0 / 5.0) * 3.0 * (-(1.0 / 3.0) * Math.Log(1.0 / 3.0, 2));


            MessageBox.Show(ClassEntropy.ToString());
            MessageBox.Show((ClassEntropy - x1).ToString());
            double first = -(1.0 / 6.0) * Math.Log(1.0 / 6.0, 2);
            double second = -(5.0 / 6.0) * Math.Log(5.0 / 6.0, 2);
            */
            

            //MessageBox.Show((first + second).ToS tring());
            
            String filepath = "";

            openFileDialog1.Filter = "csv檔案(*.csv)|*.csv";
            openFileDialog1.FilterIndex = 1;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filepath = openFileDialog1.FileName;
            }
            new Map(17, 1500, filepath, this);
            Console.ReadLine();
       
        }

        private void btnBuildF2D_Click(object sender, EventArgs e)
        {

            double joint = 0.0 / 100.0;
            double margin1 = 20.0 / 100.0;
            double margin2 = 30.0 / 100.0;

            double score = joint / (margin1 * margin2);
            score =  Math.Log(score, 2.0);  //取log
            

           score = score / (-Math.Log(joint, 2.0));  //正規化
           UpdateMSG(score.ToString());

            //UpdateMSG((Math.Log((-1.0/0.0),2)).ToString());
            /*
            String path = "C:\\Users\\Dragon\\Desktop\\實驗資料";
            String name = "Miscore.csv";


            if (File.Exists(path + "\\" + name))
                File.Delete(path + "\\" + name);

            StreamWriter writer = new StreamWriter(path + "\\" + name, false, System.Text.Encoding.UTF8);

            writer.Write("test");
            writer.Close();*/


            /*
            String returnResult = CKIPSegmentation.CKIPbyDetailPOS("開心");
            returnResult = returnResult.Substring(0, returnResult.IndexOf("("));
            //returnResult = returnResult.Substring(returnResult.IndexOf("(") + 1, returnResult.IndexOf(")") - (returnResult.IndexOf("(") + 1));
            UpdateMSG(returnResult);
            */
            //AccessOperate = new AccessManipulate(openFileDialog1.FileName);
            //AccessOperate.ConnAccess();
             

        }
        private void btnSameContent_Click(object sender, EventArgs e)
        {
            UpdateMSG("\n");
            UpdateMSG("正在過濾相同文章內容的資料...");

            DataFiltering filter = new DataFiltering(ArticleCollection);
            Form1.ArticleCollection = filter.SameContentFiltering(this);

            MessageBox.Show("過濾文章完成！");
            UpdateMSG("過濾文章完成！");
            UpdateStatus("過濾文章完成！");
        }

        private void btnVerbPos_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "csv檔案(*.csv)|*.csv";
            openFileDialog1.FilterIndex = 1;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                UpdateMSG("正在載入述詞與詳細詳性對照...");

                Dictionary<String, String> VerPostmp = new Dictionary<String, String>();

                String filepath = "";

                filepath = openFileDialog1.FileName;

                VerPostmp = ReadFile.Read2ColumnCSV(filepath);

                VerbDetailPos = VerPostmp;

                UpdateStatus("載入述詞與詳細詳性對照完成！");
                UpdateMSG("載入述詞與詳細詳性對照完成！");
            }


        }

        private void btnBuildVerbPos_Click(object sender, EventArgs e)
        {
            UpdateMSG("正在建立述詞與詳細詳性對照完成...");

            VerbDetailPos = SentmentAnalysis.BuildDetailVerbPos(ArticleCollection, this);

            OutputFile.Output2ColumnCSV(VerbDetailPos, Path.GetDirectoryName(AccessPath), "VerbPos.csv", "述詞與其詳細詞性對照", this);

            UpdateStatus("建立述詞與詳細詳性對照完成！");
            UpdateMSG("建立述詞與詳細詳性對照完成！");
        }

        private void btnClusterVSM_Click(object sender, EventArgs e)
        {
            if (Form1.AccessOperate.CheckTableExist("TopicTerm"))
            {
                DataTable DataTable = new DataTable(); 

                DataTable = Form1.AccessOperate.SelectQuery("SELECT Term FROM TopicTerm","TopicTerm").Tables["TopicTerm"];

                List<String> NTerm = new List<String>();

                for (int i = 0; i < DataTable.Rows.Count; i++)
                {
                    NTerm.Add(DataTable.Rows[i]["Term"].ToString().Trim());
                }

                DataFiltering filter = new DataFiltering(ArticleCollection);
                ArticleCollection = filter.ArticleFiltering(NTerm, this);

                VectorSpaceModel ClusterVSM = new VectorSpaceModel();

                ClusterVSM.SetFeatureCollection(NTerm);
                ClusterVSM.BuildVSM(this, ArticleCollection);

                ClusterVSM.VSMTranspose(this);

                OutputFile.OutputVSMbySparseMatrix(this, ClusterVSM.FeatureCollection, Path.GetDirectoryName(AccessPath), ClusterVSM.VSM);




            }
            else
                MessageBox.Show("尚未建立TopicTerm!");
        }

        private void MatrixT_Click(object sender, EventArgs e)
        {
            VectorSpaceModel VSM_T;
            String VSMFilePath = "";

            openFileDialog1.Filter = "csv檔案(*.csv)|*.csv";
            openFileDialog1.FilterIndex = 1;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                UpdateMSG("\n");
                UpdateMSG("正在載入csv並建立向量空間模型");

                VSMFilePath = openFileDialog1.FileName;

                //從載入的CSV檔建立向量空間模型
                VSM_T = new VectorSpaceModel();

                VSM_T.SetFeatureCollection(VSMFilePath);  //設定特徵詞詞集
                VSM_T.BuildVSMbyCSV(this, VSMFilePath);  //建立向量空間模型

                VSM_T.VSMTranspose(this);

                OutputFile.OutputVSMbySparseMatrix(this, VSM_T.FeatureCollection,  Path.GetDirectoryName(VSMFilePath), VSM_T.VSM);
            }
        }

        private void TermThr_CheckedChanged(object sender, EventArgs e)
        {
            if (TermThr.Checked == true)
            {
                DFThr.Enabled = true;
                TermLenThr.Enabled = true;
            }
            else
            {
                DFThr.Enabled = false;
                TermLenThr.Enabled = false;
            }
        }

        private void LoadT2N_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            String Path = "";
            Dictionary<String, String> Term2Network = new Dictionary<String, String>();

            openFileDialog1.Filter = "csv檔案(*.csv)|*.csv";
            openFileDialog1.FilterIndex = 1;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                Path = openFileDialog1.FileName;

                Term2Network =  ReadFile.Read2ColumnCSV(Path);
                Form1.TE.SetTerm2Network(Term2Network);

                labelT2N.Text = "✓ 載入詞與網路對照";
                UpdateStatus("已建立詞與SOM網路對照: " + Path);
            }

            if (TE.Term2Network != null && TE.Network2Cluster != null)
                btnTopicCluster.Enabled = true;
        }

        private void LoadN2C_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            String Path = "";
            Dictionary<String, String> Network2Cluster = new Dictionary<String, String>();

            openFileDialog1.Filter = "csv檔案(*.csv)|*.csv";
            openFileDialog1.FilterIndex = 1;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                Path = openFileDialog1.FileName;

                Network2Cluster = ReadFile.Read2ColumnCSV(Path);
                Form1.TE.SetNetwork2Cluster(Network2Cluster);

                labelN2C.Text = "✓ 載入網路與群對照";
                UpdateStatus("已建立網路對群對照: " + Path);
            }
            if (TE.Term2Network != null && TE.Network2Cluster != null)
                btnTopicCluster.Enabled = true;
        }

        private void btnTopicCluster_Click(object sender, EventArgs e)
        {
            UpdateMSG("\n");
            UpdateMSG("正在計算詞與群體的分群結果...");
            UpdateStatus("正在計算詞與群體的分群結果...");

            TE.SetTerm2Cluster();
            UpdateMSG("計算詞與群體的分群結果完成！");
            UpdateStatus("計算詞與群體的分群結果完成！");
        }

        private void BuildFromDB_T_CheckedChanged(object sender, EventArgs e)
        {
            Boolean TableExist = false;

            if (Form1.AccessOperate != null && Form1.AccessOperate.CheckTableExist("NounTerm"))
                TableExist = true;

            if (TableExist && BuildFromDB_T.Checked == true)
                btnTopicSet.Enabled = true;
            else
                btnTopicSet.Enabled = false;
        }

        private void btnMIScore_Click(object sender, EventArgs e)
        {
            MIScore = TE.ConputeMIScore(this);

            OutputFile.OutputMatrixCSV(MIScore, TE.TopicTermSet.Keys.ToList(), TE.TopicTermSet.Keys.ToList(), Path.GetDirectoryName(AccessPath), "MIScore.csv", "MI分數", this);
        }

        private void NegForward_CheckedChanged(object sender, EventArgs e)
        {
            if (NegForward.Checked == true)
                NegInterval.Enabled = false;
            else
                NegInterval.Enabled = true;
        }

        private void NegInterval_CheckedChanged(object sender, EventArgs e)
        {
            if (NegInterval.Checked == true)
                NegForward.Enabled = false;
            else
                NegForward.Enabled = true;
        }

        private void btnBuildDetailPos_Click(object sender, EventArgs e)
        {
            UpdateMSG("正在建立體詞與詳細詞性對照...");

            //NounDetailPos = TopicExtraction.BuildDetailNounPos(ArticleCollection, this);

            OutputFile.Output2ColumnCSV(NounDetailPos, Path.GetDirectoryName(AccessPath), "NounPos.csv", "體詞與其詳細詞性對照", this);

            UpdateStatus("建立體詞與詳細詞性對照完成！");
            UpdateMSG("建立體詞與詳細詳詞性對照完成！");

            /*
            UpdateMSG("正在建立述詞與詳細詳性對照...");

            VerbDetailPos = SentmentAnalysis.BuildDetailVerbPos(ArticleCollection, this);

            OutputFile.Output2ColumnCSV(VerbDetailPos, Path.GetDirectoryName(AccessPath), "VerbPos.csv", "述詞與其詳細詞性對照", this);

            UpdateStatus("建立述詞與詳細詳性對照完成！");
            UpdateMSG("建立述詞與詳細詳性對照完成！");
             * */
        }

        private void LoadNPos_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "csv檔案(*.csv)|*.csv";
            openFileDialog1.FilterIndex = 1;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                UpdateMSG("正在載入述體與詳細詳性對照...");

                Dictionary<String, String> NounPostmp = new Dictionary<String, String>();

                String filepath = "";

                filepath = openFileDialog1.FileName;

                NounPostmp = ReadFile.Read2ColumnCSV(filepath);

                NounDetailPos = NounPostmp;

                labelNPos.Text = "■ 體詞詳細詞表";

                UpdateStatus("載入體詞與詳細詳性對照完成！");
                UpdateMSG("載入體詞與詳細詳性對照完成！");
            }

        }

        private void LoadVPos_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "csv檔案(*.csv)|*.csv";
            openFileDialog1.FilterIndex = 1;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                UpdateMSG("\n");
                UpdateMSG("正在載入述詞與詳細詳性對照...");

                Dictionary<String, String> VerPostmp = new Dictionary<String, String>();

                String filepath = "";

                filepath = openFileDialog1.FileName;

                VerPostmp = ReadFile.Read2ColumnCSV(filepath);

                VerbDetailPos = VerPostmp;

                labelVPos.Text = "■ 體詞詳細詞表";

                UpdateStatus("載入述詞與詳細詳性對照完成！");
                UpdateMSG("載入述詞與詳細詳性對照完成！");
            }
        }

        private void btnBuildCA_Click(object sender, EventArgs e)
        {
            List<String> FeatureList = new List<String>();
            List<String> IDList = new List<String>();
            SparseMatrix<double> CAScore = new SparseMatrix<double>();

            UpdateStatus("正統計所有議題類別...");

            DataTable TopicClassTable = new DataTable();
            TopicClassTable = Form1.AccessOperate.SelectQuery("SELECT Distinct(C_Topic) FROM AppComment WHERE C_Topic <> \"\"", "AppComment").Tables["AppComment"];

            Dictionary<int, String> TopicClassSet = new Dictionary<int, String>(); //存所有議題類別

            int TopicNum = -1;
            for (int i = 0; i < TopicClassTable.Rows.Count; i++)
            {
                String TopicClass = TopicClassTable.Rows[i]["C_Topic"].ToString().Trim();

                if (TopicClass.Length > 10)
                    continue;
                else
                {
                    TopicClassSet.Add((++TopicNum), TopicClass);
                    FeatureList.Add(TopicClass + "P");
                    FeatureList.Add(TopicClass + "N");
                }
            }

            UpdateStatus("正統計所有app數量...");

            DataTable AppTable = new DataTable();
            AppTable = Form1.AccessOperate.SelectQuery("SELECT Distinct(AppTitle) FROM AppComment", "AppComment").Tables["AppComment"];

            Dictionary<int, String> AppSet = new Dictionary<int, String>(); //存所有app類別

            int AppNum = -1;
            for (int i = 0; i < AppTable.Rows.Count; i++)
            {
                String AppTitle = AppTable.Rows[i]["AppTitle"].ToString().Trim();

                AppSet.Add((++AppNum), AppTitle);
                IDList.Add(AppTitle);
            }

            int count = 0;
            foreach (KeyValuePair<int, String> app in AppSet)
            {
                foreach (KeyValuePair<int, String> topic in TopicClassSet)
                {
                    for(int i = count ; i < count + 2; i++)
                    {
                        int Orientation = i%2;
                        String Orientation_word = Orientation == 0 ? "正面" : "負面";
                        UpdateStatus("正在計算App: " + app.Value + "於類別: " + topic.Value + Orientation_word + "的次數...");
   
                        DataTable Table = new DataTable();
                        Table = Form1.AccessOperate.SelectQuery("SELECT COUNT(CommentId) FROM AppComment WHERE AppTitle = '" + app.Value + "' AND C_Topic = '" + topic.Value + "' AND C_Sentiment = " + Orientation, "AppComment").Tables["AppComment"];

                        int score = int.Parse(Table.Rows[0][0].ToString().Trim());

                        CAScore.SetAt(app.Key, i, (double)score);
                    }
                    count += 2;
                    
                }
                count = 0;
            }

            OutputFile.OutputMatrixCSV(CAScore, IDList, FeatureList, Path.GetDirectoryName(AccessPath), "CATable.csv", "對應分析列聯表", this);
        }

        private void btnBuildSemtiSeries_Click(object sender, EventArgs e)
        {
            UpdateStatus("正統計所有app數量...");

            DataTable AppTable = new DataTable();
            AppTable = Form1.AccessOperate.SelectQuery("SELECT Distinct(AppTitle) FROM AppComment", "AppComment").Tables["AppComment"];

            Dictionary<int, String> AppSet = new Dictionary<int, String>(); //存所有app類別

            int AppNum = -1;
            for (int i = 0; i < AppTable.Rows.Count; i++)
            {
                String AppTitle = AppTable.Rows[i]["AppTitle"].ToString().Trim();

                AppSet.Add((++AppNum), AppTitle);
            }

            foreach (KeyValuePair<int, String> app in AppSet)
            {
                List<String> Feature = new List<String>();
                List<String> ID = new List<String> { "Pos", "Neg" };

                Dictionary<String, int> DateSetPos = new Dictionary<String, int>();  //存所有時間之下的正面數量
                Dictionary<String, int> DateSetNeg = new Dictionary<String, int>();  //存所有時間之下的負面數量


                //查詢每一個app所有時間資料
                DataTable DateTable = new DataTable();
                DateTable = Form1.AccessOperate.SelectQuery("SELECT CreationDate,C_Sentiment FROM AppComment Where AppTitle = '" + app.Value + "' ORDER BY CreationDate ASC", "AppComment").Tables["AppComment"];

                for (int i = 0; i < DateTable.Rows.Count; i++)  //將所有時間轉為日期格式存入
                {
                    //將時間資料轉為Date格式
                    String DateFormat = Convert.ToDateTime(DateTable.Rows[i]["CreationDate"]).ToShortDateString();

                    if (!Feature.Contains(DateFormat))
                        Feature.Add(DateFormat.ToString());

                    if (int.Parse(DateTable.Rows[i]["C_Sentiment"].ToString()) == 0)  //是正面則存入正面dic
                    {
                        if (DateSetPos.ContainsKey(DateFormat))
                            DateSetPos[DateFormat]++;
                        else
                            DateSetPos.Add(DateFormat, 1);
                    }
                    else if (int.Parse(DateTable.Rows[i]["C_Sentiment"].ToString()) == 1)  //是負面則存入負面dic
                    {
                        if (DateSetNeg.ContainsKey(DateFormat))
                            DateSetNeg[DateFormat]++;
                        else
                            DateSetNeg.Add(DateFormat, 1);
                    }
                    else
                    { }

                    

                }
                //寫入成matrix並輸出
                SparseMatrix<double> matrix = new SparseMatrix<double>();

                int count = 0;
                foreach (String date in Feature)
                {
                    if (DateSetPos.ContainsKey(date))
                        matrix.SetAt(0, count, DateSetPos[date]);
                    else
                        matrix.SetAt(0, count, 0);

                    if (DateSetNeg.ContainsKey(date))
                        matrix.SetAt(1, count, DateSetNeg[date]);
                    else
                        matrix.SetAt(1, count, 0);

                    count++;
                }
                /*
                int count = -1;
                foreach(KeyValuePair<string,int> countNum in DateSetPos)
                    matrix.SetAt(0,(++count),countNum.Value);
                count = 0;
                foreach (KeyValuePair<string, int> countNum in DateSetNeg)
                    matrix.SetAt(1, (++count), countNum.Value);*/

                OutputFile.OutputMatrixCSV(matrix, ID, Feature, Path.GetDirectoryName(AccessPath), app.Value +  "情感趨勢表.csv", app.Value + "情感趨勢表", this);
            }
        }

        private void btnBuildTopicTrend_Click(object sender, EventArgs e)
        {
            UpdateStatus("正統計所有議題類別...");

            Directory.CreateDirectory(Path.GetDirectoryName(AccessPath) + "\\議題類別情感趨勢");

            DataTable TopicClassTable = new DataTable();
            TopicClassTable = Form1.AccessOperate.SelectQuery("SELECT Distinct(C_Topic) FROM AppComment WHERE C_Topic <> \"\"", "AppComment").Tables["AppComment"];

            Dictionary<int, String> TopicClassSet = new Dictionary<int, String>(); //存所有議題類別

            int TopicNum = -1;
            for (int i = 0; i < TopicClassTable.Rows.Count; i++)
            {
                String TopicClass = TopicClassTable.Rows[i]["C_Topic"].ToString().Trim();

                if (TopicClass.Length > 10)
                    continue;
                else
                {
                    TopicClassSet.Add((++TopicNum), TopicClass);
                }
            }

            UpdateStatus("正統計所有app數量...");

            DataTable AppTable = new DataTable();
            AppTable = Form1.AccessOperate.SelectQuery("SELECT Distinct(AppTitle) FROM AppComment", "AppComment").Tables["AppComment"];

            Dictionary<int, String> AppSet = new Dictionary<int, String>(); //存所有app類別

            int AppNum = -1;
            for (int i = 0; i < AppTable.Rows.Count; i++)
            {
                String AppTitle = AppTable.Rows[i]["AppTitle"].ToString().Trim();

                AppSet.Add((++AppNum), AppTitle);
            }

            foreach (KeyValuePair<int, String> app in AppSet)
            {
                foreach (KeyValuePair<int, String> topic in TopicClassSet)
                {
                    List<String> Feature = new List<String>();
                    List<String> ID = new List<String> { "Pos", "Neg" };

                    Dictionary<String, int> TopicPos = new Dictionary<String, int>();  //存所有時間之下的正面數量
                    Dictionary<String, int> TopicNeg = new Dictionary<String, int>();  //存所有時間之下的負面數量


                    //查詢每一個app所有時間資料
                    DataTable DateTable = new DataTable();
                    String query = "SELECT CreationDate,C_Sentiment FROM AppComment Where AppTitle = '" + app.Value + "' AND C_Topic = '" + topic.Value + "' ORDER BY CreationDate ASC";
                    DateTable = Form1.AccessOperate.SelectQuery(query, "AppComment").Tables["AppComment"];

                    for (int i = 0; i < DateTable.Rows.Count; i++)  //將所有時間轉為日期格式存入
                    {
                        //將時間資料轉為Date格式
                        String DateFormat = Convert.ToDateTime(DateTable.Rows[i]["CreationDate"]).ToShortDateString();

                        if (!Feature.Contains(DateFormat))
                            Feature.Add(DateFormat.ToString());

                        if (int.Parse(DateTable.Rows[i]["C_Sentiment"].ToString()) == 0)  //是正面則存入正面dic
                        {
                            if (TopicPos.ContainsKey(DateFormat))
                                TopicPos[DateFormat]++;
                            else
                                TopicPos.Add(DateFormat, 1);
                        }
                        else if (int.Parse(DateTable.Rows[i]["C_Sentiment"].ToString()) == 1)  //是負面則存入負面dic
                        {
                            if (TopicNeg.ContainsKey(DateFormat))
                                TopicNeg[DateFormat]++;
                            else
                                TopicNeg.Add(DateFormat, 1);
                        }
                        else
                        { }



                    }
                    //寫入成matrix並輸出
                    SparseMatrix<double> matrix = new SparseMatrix<double>();

                    int count = 0;
                    foreach (String date in Feature)
                    {
                        if(TopicPos.ContainsKey(date))
                            matrix.SetAt(0, count, TopicPos[date]);
                        else
                            matrix.SetAt(0, count, 0);

                        if (TopicNeg.ContainsKey(date))
                            matrix.SetAt(1, count, TopicNeg[date]);
                        else
                            matrix.SetAt(1, count, 0);

                        count++;
                    }

                    /*
                    int count = -1;
                    foreach (KeyValuePair<string, int> countNum in TopicPos)
                        matrix.SetAt(0, (++count), countNum.Value);
                    count = -1;
                    foreach (KeyValuePair<string, int> countNum in TopicNeg)
                        matrix.SetAt(1, (++count), countNum.Value);
                    */
                    OutputFile.OutputMatrixCSV(matrix, ID, Feature, Path.GetDirectoryName(AccessPath) + "\\議題類別情感趨勢", app.Value + topic.Value + "類別情感趨勢表.csv", app.Value + topic.Value + "情感趨勢表", this);
                }
            }
        }

        private void btnBuildTopicCA_Click(object sender, EventArgs e)
        {
            String App = comboBoxApp.SelectedItem.ToString();
            String Topic = comboBoxTopic.SelectedItem.ToString();

            List<String> Feature = new List<String>();
            List<String> ID = new List<String> { "Pos", "Neg" };

            UpdateStatus("正統計議題類別所有議題詞...");

            DataTable TopicTermTable = new DataTable();
            TopicTermTable = Form1.AccessOperate.SelectQuery("SELECT Term FROM TopicTerm WHERE Topic = '" + Topic + "'", "TopicTerm").Tables["TopicTerm"];

            Dictionary<int, String> TopicTermSet = new Dictionary<int, String>(); //存所有app類別

            int TopicTermNum = -1;
            for (int i = 0; i < TopicTermTable.Rows.Count; i++)
            {
                String TopicTerm = TopicTermTable.Rows[i]["Term"].ToString().Trim();

                TopicTermSet.Add((++TopicTermNum), TopicTerm);
                Feature.Add(TopicTerm);
            }

            SparseMatrix<double> TopicCA = new SparseMatrix<double>();

            foreach (KeyValuePair<int, String> term in TopicTermSet)
            {
                String query = "SELECT COUNT(CommentId) FROM AppComment WHERE AppTitle = '" + App + "' AND C_Topic = '" + Topic + "' AND Textcontent LIKE '%" + term.Value + "%' AND C_Sentiment = 0";
                DataTable DateTable = new DataTable();

                if (DateRange.Checked == true)
                    query += " AND CreationDate BETWEEN #" + DateStart.Text + "# AND #" + DateEnd.Text + "#";

                DateTable = Form1.AccessOperate.SelectQuery(query, "AppComment").Tables["AppComment"];

                int count = int.Parse(DateTable.Rows[0][0].ToString());
                TopicCA.SetAt(0, term.Key, count);

                query = "SELECT COUNT(CommentId) FROM AppComment WHERE AppTitle = '" + App + "' AND C_Topic = '" + Topic + "' AND Textcontent LIKE '%" + term.Value + "%' AND C_Sentiment = 1";
                if (DateRange.Checked == true)
                    query += " AND CreationDate BETWEEN #" + DateStart.Text + "# AND #" + DateEnd.Text + "#";
                
                DateTable = Form1.AccessOperate.SelectQuery(query, "AppComment").Tables["AppComment"];

                count = int.Parse(DateTable.Rows[0][0].ToString());
                TopicCA.SetAt(1, term.Key, count);
            }

            OutputFile.OutputMatrixCSV(TopicCA, ID, Feature, Path.GetDirectoryName(AccessPath), App + " " + Topic + "CA表.csv", App + " " + Topic + "CA表", this);
        }





















    }
}
