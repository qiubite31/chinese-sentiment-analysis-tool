using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Data;

namespace AndroidAnalyzer
{
    class SentmentAnalysis
    {
        ChineseWordNet CWN;
        public Dictionary<String, String> PosSeedSet = new Dictionary<String, String>(); //存所有的正面種子情感詞
        public Dictionary<String, String> NegSeedSet = new Dictionary<String, String>(); //存所有的負面種情感詞

        public Dictionary<String, String> SentimentTermSet = new Dictionary<String, String>(); //存所有情感詞集
        public Dictionary<String, String> VerbTermSet = new Dictionary<String, String>(); //存所有動詞

        public SentmentAnalysis(String SeedPath)
        {
            SetSeedList(SeedPath);
        }
        public SentmentAnalysis()
        { 
        }
        private void SetSeedList(String SeedPath)
        {
            StreamReader reader = File.OpenText(SeedPath);  //載入種子詞集

            String PostiveTerm = reader.ReadLine();  //取出種子詞集中的正面詞

            ArrayList PosTermSeed = new ArrayList(Regex.Split(PostiveTerm, ","));  //將正面種子詞轉為Arraylist
            
            int count = 0;
            foreach (String seed in PosTermSeed)  //存入Dictionary
            {
                PosSeedSet.Add(seed, "PosSeed_" + (++count).ToString());
            }
            //---------------------------------
            String NegativeTerm = reader.ReadLine();  //取出種子詞集中的負面詞

            ArrayList NegTermSeed = new ArrayList(Regex.Split(NegativeTerm, ","));

            count = 0;
            foreach (String seed in NegTermSeed)  //存入Dictionary
            {
                NegSeedSet.Add(seed, "NegSeed_" + (++count).ToString());
            }
        }
        public void SetCWN(ChineseWordNet cwn)
        {
            this.CWN = cwn;
        }
       
        public void ExtendTermBySeed(Form1 form)
        {
            Dictionary<String, String> SentimentTermTmp = new Dictionary<String, String>(); 

            Dictionary<String, String> SenseidList = new Dictionary<string, string>();   //建立一個裝所有要查資料庫資料的senseid集合

            form.UpdateStatus("正在取得正面種子詞的CWNid清單...");
            SenseidList = CWN.SearchTerm(PosSeedSet);  //將種子詞集的正面詞丟到CWN找到所有同義詞的senseid，傳入種子詞集

            form.UpdateStatus("正在擴充情感詞集...");
            //將CWNid拿來查詢同義詞與反與詞資料庫
            SentimentTermTmp = QueryAndAdd(SentimentTermTmp, SenseidList, "postive", "synonym",form);  //正面詞的同義詞加入同義詞
            SentimentTermTmp = QueryAndAdd(SentimentTermTmp, SenseidList, "postive", "antonym",form);  //正面詞的反義詞加入反義詞

            form.UpdateStatus("正在取得負面種子詞的CWNid清單...");
            SenseidList = CWN.SearchTerm(NegSeedSet);  //將種子詞集的負面詞丟到CWN找到所有同義詞的CWNid

            form.UpdateStatus("正在擴充情感詞集...");
            //將CWNid拿來查詢同義詞與反與詞資料庫
            SentimentTermTmp = QueryAndAdd(SentimentTermTmp, SenseidList, "negtive", "synonym",form);
            SentimentTermTmp = QueryAndAdd(SentimentTermTmp, SenseidList, "negtive", "antonym",form);

            SentimentTermSet = new Dictionary<String, String>(SentimentTermTmp);  //將暫存的Dictionary指派給情感詞集的Dictionary物件

            form.UpdateStatus("正在執行種子詞補齊情感詞集...");
            //種子詞補齊情感詞集
            //如果種子詞沒有出現在後續擴充階段，則種子詞會沒有加入進情感詞集，故要作補齊
            foreach (KeyValuePair<String, String> term in PosSeedSet)
            {
                if(!SentimentTermSet.ContainsKey(term.Key))
                    SentimentTermSet.Add(term.Key, "Pos");
            }
            foreach (KeyValuePair<String, String> term in NegSeedSet)
            {
                if (!SentimentTermSet.ContainsKey(term.Key))
                    SentimentTermSet.Add(term.Key, "Neg");
            }
            //==================================================

            form.UpdateMSG("\n");
            form.UpdateMSG("種子詞集擴增的情感詞：");

            PrintSentimentTerm(form);
        }
        public Dictionary<String, String> QueryAndAdd(Dictionary<String, String> SentimentTermSet, Dictionary<String, String> SenseidList, String seedOrien, String target, Form1 form)
        {
            foreach (KeyValuePair<String,String> senseid in SenseidList.ToArray())
            {
                //form.UpdateMSG("CWNid: " + senseid.Key + " " + target);
                Boolean TermOrientation = OrientationDet(seedOrien,target,senseid.Value);  //判斷該詞是要加至正面情感詞集，還是負面情感詞集

                DataTable DataTable = new DataTable();  //接收查詢資料庫回傳的資料

                DataTable = CWN.QueryCWN(senseid.Key , target).Tables["cwn_" + target]; //取出查詢完回傳的資料

                if (target == "synonym") //判斷所查詢的資料表是同義詞還是反義詞
                {
                    for (int i = 0; i < DataTable.Rows.Count; i++)
                    {
                        String word = DataTable.Rows[i]["synonym_word"].ToString();

                        //form.UpdateMSG("Orientation: " + TermOrientation.ToString() + " Term: " + DataTable.Rows[i]["synonym_word"].ToString() + " ");
                        //if (CheckPos(Form1.VerbPos[word]))
                           SentimentTermSet = Add2Dic(TermOrientation, word, SentimentTermSet, form);
                    }
                }
                else if (target == "antonym")
                {
                    for (int i = 0; i < DataTable.Rows.Count; i++)
                    {
                        String word = DataTable.Rows[i]["antonym_word"].ToString();

                        //if (CheckPos(Form1.VerbPos[word]))
                           SentimentTermSet = Add2Dic(TermOrientation, word, SentimentTermSet, form);
                        //form.UpdateMSG("Orientation: " + TermOrientation.ToString() + " Term: " + DataTable.Rows[i]["antonym_word"].ToString() + " ");
                        
                    }
                }
            }

            return SentimentTermSet;
        }
        private Boolean OrientationDet(String seedOrien, String target, String relType)
         {
            //判斷要把詞判為正面詞還是反面詞

            int SenseRelType;
            int SeedOrientation;
            int QueryTarget;

            if (seedOrien.Contains("postive"))  //原本的詞是正面還是負面
                SeedOrientation = 1;
            else
                SeedOrientation = -1;

            if (target.Contains("synonym"))  //查詢的資料表是同義還是反義
                QueryTarget = 1;
            else
                QueryTarget = -1;

            if(relType.Contains("synonym") || relType.Contains("Pos"))  //該詞和原本詞是正面還是負面
                SenseRelType = 1;
            else
                SenseRelType = -1;

            if (SeedOrientation * QueryTarget * SenseRelType == 1)
                return true;
            else
                return false;

        }
        public Dictionary<String, String> Add2Dic(Boolean flag, String key, Dictionary<String, String> SentimentTermSet,Form1 form)
        {
            Regex regex = new Regex("[1-9]{1}");
            key = regex.Replace(key, "");

            //if ( CheckPosbyDic(Form1.VerbPos[key]))
            if ((Form1.VerbDetailPos.ContainsKey(key) && CheckPosbyDic(Form1.VerbDetailPos[key]) || CheckPosbyCKIP(key)))  //如果該述詞為狀態述詞
            {
                if (!SentimentTermSet.ContainsKey(key))  //若情感詞還未包含於情感詞集中，則加入情感詞集
                {
                    if (flag)
                        SentimentTermSet.Add(key, "Pos");
                    else
                        SentimentTermSet.Add(key, "Neg");
                }
                else  //若情感詞已出現於情感詞集中，則判斷目前情感詞和已存在情感詞集中的情感詞，兩者的情感傾向是否衝突，衝突則移除丟棄
                {
                    String Orientation = flag == true ? "Pos" : "Neg";

                    if (SentimentTermSet[key] != Orientation)
                    {
                        form.UpdateMSG("情感傾向衝突的詞：" + key + " " + SentimentTermSet[key]);
                        //MessageBox.Show("情感傾向衝突的詞：" + key + " " + SentimentTermSet[key]);
                        SentimentTermSet.Remove(key);
                    }

                }
            }
            return SentimentTermSet;
             
        }
        public void ExtendTermByVerb(ArticleModel ArticleCollection, Form1 form)
        {
            form.UpdateStatus("正在統計不重複的動詞...");
            
            Dictionary<String, String> VerbTermSetTemp = BuildVerbSet(ArticleCollection);

            VerbTermSet = new Dictionary<String, String>(VerbTermSetTemp);  //設定動詞詞集
            Dictionary<String,Dictionary<String,String>> VerbRelatedTerm = new Dictionary<String,Dictionary<String,String>>();  //裝所有動詞的同義詞與反義詞
            Dictionary<String, String> ConflictTerm = new Dictionary<String, String>();  //裝所有出現矛盾感情傾向的動詞
            Dictionary<String, String> NotStateVerb = new Dictionary<string, string>();  //裝所有非狀態類動詞的動詞

            int Verbcount = 0;
            foreach (KeyValuePair<String, String> VerbSet in VerbTermSet) //查詢每一個字的同義與反義詞
            {
                form.UpdateStatus("正在查詢每個動詞的正反義相關詞...第(" + (++Verbcount).ToString() + "/" + VerbTermSet.Count + ")個動詞...");

                if (!CheckPosbyDic(Form1.VerbDetailPos[VerbSet.Key]))  //動詞非狀態類動詞則加入dictionary
                {
                    NotStateVerb.Add(VerbSet.Key, "");
                    continue;
                }

                Dictionary<String, String> VerbTerm = new Dictionary<String, String>() { { VerbSet.Key, VerbSet.Value } };  //將每一個動詞用Dictionary格式查詢CWNid
                Dictionary<String, String> SenseidList = new Dictionary<String, String>();   //建立一個裝所有要查資料庫資料的senseid集合
                Dictionary<String, String> SingleRelatedTerm = new Dictionary<String, String>();   //一個動詞從CWNid查詢所有的相關詞
                
                SenseidList = CWN.SearchTerm(VerbTerm); //取得動詞的所有詞義

                foreach (KeyValuePair<String, String> senseid in SenseidList.ToArray()) //查詢所有詞義的同義詞與反義詞，並加入相關詞集
                {
                    DataTable DataTable = new DataTable();  //接收將詞義查詢資料庫回傳的資料

                    String SenseDirection = senseid.Value.Contains("synonym") ? "synonym" : "antonym"; //抓出目前詞義和原本的詞是同義還是反義
                    int SynOrAnt = SenseDirection == "synonym" ? 1 : -1; //同義為1或反義為-1

                    DataTable = CWN.QueryCWN(senseid.Key, "synonym").Tables["cwn_" + "synonym"]; //取出查詢完同義詞資料表回傳的同義詞資料

                    for (int i = 0; i < DataTable.Rows.Count; i++)
                    {
                        String word = DataTable.Rows[i]["synonym_word"].ToString();  //去除詞義編號
                        Regex regex = new Regex("[1-9]{1}");
                        word = regex.Replace(word, "");

                        String TermDirection = SynOrAnt * 1 == 1 ? "synonym" : "antonym";  //判斷該詞為同義或是反義(反義同的同義為反義，反義詞的反義為同義.....)

                        if (!SingleRelatedTerm.ContainsKey(word))  //判斷是否已存在
                            SingleRelatedTerm.Add(word, TermDirection);
                        else  //已存在的話則判斷同義反義是否衝突
                        {
                            if (TermDirection != SingleRelatedTerm[word])
                            {
                                form.UpdateMSG("字詞「" + word + "」同義反義出現衝突，新的為" + TermDirection + "已存在為" + SingleRelatedTerm[word]);

                                if (!ConflictTerm.ContainsKey(word))
                                    ConflictTerm.Add(word, "Conflict in Sense");
                            }
                        }   
                    }
                    DataTable = CWN.QueryCWN(senseid.Key, "antonym").Tables["cwn_" + "antonym"]; //取出查詢完反義詞資料表回傳的反義詞資料

                    for (int i = 0; i < DataTable.Rows.Count; i++)  //判斷是否已存在
                    {
                        String word = DataTable.Rows[i]["antonym_word"].ToString();
                        Regex regex = new Regex("[1-9]{1}");
                        word = regex.Replace(word, "");

                        String TermDirection = SynOrAnt * -1 == 1 ? "synonym" : "antonym";  //判斷該詞為同義或是反義(反義同的同義為反義，反義詞的反義為同義.....)

                        if (!SingleRelatedTerm.ContainsKey(word))  //判斷是否已存在
                            SingleRelatedTerm.Add(word, TermDirection);
                        else  //已存在的話則判斷同義反義是否衝突
                        {
                            if (TermDirection != SingleRelatedTerm[word])
                            {
                                form.UpdateMSG("字詞「" + word + "」同義反義出現衝突，新的為" + TermDirection + "已存在為" + SingleRelatedTerm[word]);

                                if(!ConflictTerm.ContainsKey(word))
                                    ConflictTerm.Add(word, "Conflict in Sense");
                            }
                        } 
                    }
                }
                //儲存一個動詞與其相同的同義詞和反義詞 格式： 動詞→<相關詞,同義>....<相關詞,反義>....
                    VerbRelatedTerm.Add(VerbSet.Key, SingleRelatedTerm);
            }

            Verbcount = 0;
            int ThisAddNum = 0;
            int AddGeneration = 1;

            while (true) //跑到沒有新的情感詞被增加
            {
                foreach (KeyValuePair<String, String> VerbSet in VerbTermSet)
                {   
                    //判斷所有動詞的每一個相關同反義詞，是否有出現在情感詞集中，有則將動詞加入情感詞集
                    form.UpdateStatus("正在透過動詞擴增的情感詞集...第(" + (++Verbcount).ToString() + "/" + VerbTermSet.Count + ")個動詞...第" + AddGeneration.ToString() + "輪...");

                    if (NotStateVerb.ContainsKey(VerbSet.Key))  //其動詞非狀態類動詞則直接跳過
                        continue;

                    foreach (KeyValuePair<String, String> RelatedTerm in VerbRelatedTerm[VerbSet.Key].ToArray())  //判斷候選情感詞是否有出現在已存在的情感詞集中
                    {
                        if (ConflictTerm.ContainsKey(VerbSet.Key)) //如該詞已被判斷為詞義矛盾，則直接跳過
                            continue;

                        //如果該動詞所查到的同義詞或是反義詞有出現在情感詞集中
                        if (SentimentTermSet.ContainsKey(RelatedTerm.Key)) //&& !SentimentTermSet.ContainsKey(VerbSet.Key)
                        {
                            //if (VerbSet.Key == "死" || VerbSet.Key == "弱" || VerbSet.Key == "俗" || VerbSet.Key == "死掉" || VerbSet.Key == "淡" || VerbSet.Key == "活")
                            //if(VerbSet.Key == "俗")    
                            //form.UpdateMSG("");  

                            String Word = VerbSet.Key;

                            int SynOrAnt = RelatedTerm.Value.ToString() == "synonym" ? 1 : -1; //同義與反義
                            int PosOrNeg = SentimentTermSet[RelatedTerm.Key] == "Pos" ? 1 : -1; //所找到的為正面或負面

                            String Orientation = (SynOrAnt * PosOrNeg) == 1 ? "Pos" : "Neg";

                            if (!SentimentTermSet.ContainsKey(VerbSet.Key)) //如該動詞沒有出現在情感詞集中則加入，並標記情感傾向
                            {
                                SentimentTermSet.Add(Word, Orientation);
                                ThisAddNum++;
                            }
                            else
                            {   //如該動詞已經出現在情感詞集中，則判斷與之前的情感傾向是否矛盾，是則從情感詞集中移除該動詞，並加入情感矛盾詞集
                                if (SentimentTermSet[VerbSet.Key] != Orientation)
                                {
                                    SentimentTermSet.Remove(VerbSet.Key);
                                    ConflictTerm.Add(VerbSet.Key, RelatedTerm.Key + "_" + Orientation);  // [情感傾向衝突的詞,該詞的哪一個相關詞引起衝動_該相關詞的情感傾向]
                                }
                            }
                        }
                    }
                }
                if (ThisAddNum == 0)
                    break;  //如果沒有新的情感詞被增加，則離開迴圈
                else
                {
                    ThisAddNum = 0;
                    AddGeneration++;
                    Verbcount = 0;
                }
            }
            form.UpdateMSG("\n");
            form.UpdateMSG("所有情感傾向矛盾的詞共有：" + ConflictTerm.Count.ToString());
            form.UpdateMSG("所有情感傾向矛盾的詞：");
            String ConflictOutput = "所有情感傾向矛盾的詞：";
            foreach (KeyValuePair<String, String> Conflict in ConflictTerm)
                ConflictOutput += "," + Conflict.Key + " " + Conflict.Value; ;
            
            form.UpdateMSG(ConflictOutput);

            form.UpdateMSG("\n");
            form.UpdateMSG("所有動詞擴增的情感詞：");
            PrintSentimentTerm(form);

            form.UpdateMSG("正在寫入情感詞集到資料庫...");
            form.UpdateStatus("正在寫入情感詞集...");
            if (Form1.AccessOperate.CheckTableExist("SentimentTermSet"))
            {
                Form1.AccessOperate.DropTable("SentimentTermSet");
                Form1.AccessOperate.CreateTable("CREATE TABLE SentimentTermSet(Term char(50), Orientation char(2))");
            }
            else
                Form1.AccessOperate.CreateTable("CREATE TABLE SentimentTermSet(Term char(50), Orientation char(2))");

            //統計數據
            int count = 0;
            int VerbPos = 0;
            int VerbNeg = 0;
            int SenPos = 0;

            foreach (KeyValuePair<String, String> term in SentimentTermSet)
            {
                form.UpdateStatus("正在寫入第(" + (++count).ToString() + "/" + SentimentTermSet.Count + ")個情感詞...");

                if (term.Value == "Pos")
                {
                    Form1.AccessOperate.UpdateQuery("INSERT INTO SentimentTermSet(Term, Orientation) VALUES('" + term.Key.Trim() + "', 'P')");
                    SenPos++;
                }
                else
                    Form1.AccessOperate.UpdateQuery("INSERT INTO SentimentTermSet(Term, Orientation) VALUES('" + term.Key.Trim() + "', 'N')");

                //補足資料庫裡面VerbTerm資料表的Orientation欄位
                if (VerbTermSet.ContainsKey(term.Key))
                {
                    if (term.Value == "Pos")
                    {
                        Form1.AccessOperate.UpdateQuery("UPDATE VerbTerm SET Orientation = 'P' WHERE Term = '" + term.Key + "'");
                        VerbPos++;
                    }
                    else
                    {
                        Form1.AccessOperate.UpdateQuery("UPDATE VerbTerm SET Orientation = 'N' WHERE Term = '" + term.Key + "'");
                        VerbNeg++;
                    }
                }
            }

            form.UpdateMSG("情感詞集情感詞數量: " + SentimentTermSet.Count.ToString());
            form.UpdateMSG("正面: " + SenPos.ToString() + " 負面: " + (SentimentTermSet.Count - SenPos).ToString());
            //form.UpdateMSG("情感詞集正面詞數量: " + SenPos.ToString());
            //form.UpdateMSG("情感詞集負面詞數量: " + (SentimentTermSet.Count - SenPos).ToString());
            form.UpdateMSG("動詞詞集情感詞數量: " + (VerbNeg + VerbPos).ToString());
            form.UpdateMSG("正面: " + VerbPos.ToString() + " 負面: " + VerbNeg.ToString());
            //form.UpdateMSG("動詞詞集正面詞數量: " + VerbPos.ToString());
            //form.UpdateMSG("動詞詞集負面詞數量: " + VerbNeg.ToString());




        }
        private Boolean CheckPosbyDic(String POS)
        {
            //判斷動詞的詳細詞性是否為VH~VL(狀態動詞)
            List<String> Vpos = new List<String> { "(VH)", "(VHC)", "(VI)", "(VJ)", "(VK)", "(VL)" };

            if (Vpos.Contains(POS))
                return true;
            else
                return false;
        }
        private Boolean CheckPosbyCKIP(String Term)
        {
            //判斷動詞的詳細詞性是否為VH~VL(狀態動詞)
            List<String> Vpos = new List<String> { "(VH)", "(VHC)", "(VI)", "(VJ)", "(VK)", "(VL)" };

            String pos = CKIPSegmentation.CKIPbyDetailPOS(Term); //取得字詞與pos

            if (pos == "") //如果傳回空的pos，則直接移除
                return false;
            else
            {
                //pos = pos.Substring(pos.IndexOf("(") + 1, pos.IndexOf(")") - (pos.IndexOf("(") + 1)); //取出pos
                pos = pos.Substring(pos.IndexOf("("));

                if (Vpos.Contains(pos))
                    return true;
                else
                    return false;
            }
        }
        private static Dictionary<string, string> BuildVerbSet(ArticleModel ArticleCollection)
        {
            if (Form1.AccessOperate.CheckTableExist("VerbTerm"))
            {
                Form1.AccessOperate.DropTable("VerbTerm");
                Form1.AccessOperate.CreateTable("CREATE TABLE VerbTerm(Term char(50), Speech char(5), TF integer,  Orientation char(100))");
            }
            else
                Form1.AccessOperate.CreateTable("CREATE TABLE VerbTerm(Term char(50), Speech char(5), TF integer,  Orientation char(100))");

            Dictionary<String, String> VerbTermSetTemp = new Dictionary<String, String>();  //暫存

            foreach (Article article in ArticleCollection.GetArticleList())
            {
                Term[] terms = article.GetTermArray(); //取出一篇文章的字詞陣列

                for (int j = 0; j < terms.Length; j++)  //不重複的動詞存進Temp
                {
                    Term term = terms[j];

                    String word = term.GetWord();
                    String speech = term.GetSpeech();
                    int Tfrenquency = term.Tfrequncy;

                    if (term.GetWord().Contains("_NOT")) //判斷是否為經過負向處理的詞，則去除_NOT再計算
                        word = word.Substring(0, word.Length - 4);

                    /*
                    if (term.GetWord().Contains("_NOT")) //判斷是否為經過負向處理的詞，則去除_NOT再計算
                    {
                        String word = term.GetWord();
                        term.SetWord(word.Substring(0, word.Length - 4));
                    }*/

                    if ((speech == "Vi" || speech =="Vt") && (!VerbTermSetTemp.ContainsKey(word))) //為Vi或Vt且在Dictionary中不重複
                    {                          
                        VerbTermSetTemp.Add(word, speech);
                        Form1.AccessOperate.UpdateQuery("INSERT INTO VerbTerm(Term, Speech, TF, Orientation) VALUES('" + word + "', '" + speech + "', " + Tfrenquency + ", '')");
                    }
                }
            }
             return VerbTermSetTemp;
        }
        public void SetSentimentTermSetFromDB(Form1 form)  //從資料庫已存在的情感詞建立情感詞集物件
        {
            DataTable SentimentSetTable = new DataTable();
            SentimentSetTable =  Form1.AccessOperate.SelectQuery("SELECT * FROM SentimentTermSet", "SentimentTermSet").Tables["SentimentTermSet"];

            Dictionary<String, String> SentimentTermSetTmp = new Dictionary<String, String>(); //存所有情感詞集

            int count = 0;
            for (int i = 0; i < SentimentSetTable.Rows.Count; i++)
            {
                form.UpdateStatus("正在建立情感詞集，第(" + (++count).ToString() + "/" + SentimentSetTable.Rows.Count + ")個情感詞...");

                String Term = SentimentSetTable.Rows[i]["Term"].ToString().Trim();
                String Orientation = SentimentSetTable.Rows[i]["Orientation"].ToString().Trim();

                if (!SentimentTermSetTmp.ContainsKey(Term))  //判斷掉"神奇"的情感詞重複問題
                {
                    SentimentTermSetTmp.Add(Term, Orientation);          
                }
            }
            SentimentTermSet = new Dictionary<string,string>(SentimentTermSetTmp);
            PrintSentimentTerm(form);
        }
        public void CalSentimentScore(ArticleModel ArticleCollection, Form1 form)
        {

            if (!Form1.AccessOperate.CheckColumnExist("AppComment", "C_Sentiment"))  //沒有C_Sentiment欄位則先新增
                Form1.AccessOperate.UpdateQuery("ALTER TABLE AppComment ADD C_Sentiment int");

            int count = 0;
            foreach (Article article in ArticleCollection.GetArticleList())
            {
                form.UpdateStatus("正在計算情感傾向第(" + (++count).ToString() + "/" + ArticleCollection.Count.ToString() + ")篇文章...");

                int score = 0;
                Term[] terms = article.GetTermArray(); //取出一篇文章的字詞陣列

                for (int j = 0; j < terms.Length; j++)  
                {

                    String word = terms[j].GetWord();  //取出文章裡的每一個詞

                    if (terms[j].GetSpeech() == "Vi" || terms[j].GetSpeech() == "Vt")
                    {
                        if (word.Contains("_NOT")) //判斷是否為經過負向處理的詞，則去除_NOT再計算
                        {
                            word = word.Substring(0, word.Length - 4);

                            if (SentimentTermSet.ContainsKey(word))
                            {
                                if (SentimentTermSet[word] == "P" || SentimentTermSet[word] == "Pos")
                                    score--;
                                else
                                    score++;
                            }

                        }
                        else
                        {
                            if (SentimentTermSet.ContainsKey(terms[j].GetWord()))
                            {
                                if (SentimentTermSet[word] == "P" || SentimentTermSet[word] == "Pos")
                                    score++;
                                else
                                    score--;
                            }
                        }
                    }
                }



                if (!Form1.AccessOperate.CheckColumnExist("AppComment", "SentimentScore"))  //沒有SentimentScore欄位則先新增
                    Form1.AccessOperate.UpdateQuery("ALTER TABLE AppComment ADD SentimentScore int");

                Form1.AccessOperate.UpdateQuery("UPDATE AppComment SET SentimentScore = " + score + " WHERE CommentId = " + article.GetArticleID());

                article.SentimentScore = score;

                if(score > 0)
                    Form1.AccessOperate.UpdateQuery("UPDATE AppComment SET C_Sentiment = 0 WHERE CommentId = " + article.GetArticleID());
                if(score < 0)
                    Form1.AccessOperate.UpdateQuery("UPDATE AppComment SET C_Sentiment = 1 WHERE CommentId = " + article.GetArticleID());
                if(score == 0)
                    Form1.AccessOperate.UpdateQuery("UPDATE AppComment SET C_Sentiment = 2 WHERE CommentId = " + article.GetArticleID());
            }


            //計算情感傾向判斷結果
            DataTable CountResultTable = new DataTable();

            CountResultTable = Form1.AccessOperate.SelectQuery("SELECT COUNT(CommentId) FROM AppComment WHERE C_Sentiment = 0", "AppComment").Tables["AppComment"];
            int PosNum = int.Parse(CountResultTable.Rows[0][0].ToString());

            CountResultTable = Form1.AccessOperate.SelectQuery("SELECT COUNT(CommentId) FROM AppComment WHERE C_Sentiment = 1", "AppComment").Tables["AppComment"];
            int NegNum = int.Parse(CountResultTable.Rows[0][0].ToString()); 

            CountResultTable = Form1.AccessOperate.SelectQuery("SELECT COUNT(CommentId) FROM AppComment WHERE C_Sentiment = 2", "AppComment").Tables["AppComment"];
            int Neutral = int.Parse(CountResultTable.Rows[0][0].ToString());

            form.UpdateMSG("正面筆數: " + PosNum.ToString() + "(" + ((double)PosNum / (double)ArticleCollection.Count * 100).ToString("0.000") + " %)");
            form.UpdateMSG("負面筆數: " + NegNum.ToString() + "(" + ((double)NegNum / (double)ArticleCollection.Count * 100).ToString("0.000") + " %)");
            form.UpdateMSG("中立筆數: " + Neutral.ToString() + "(" + ((double)Neutral / (double)ArticleCollection.Count * 100).ToString("0.000") + " %)");




            Form1.AccessOperate.SelectQuery("SELECT COUNT(CommentId) FROM AppComment WHERE C_Sentiment = 0", "AppComment");
        }
        public ArticleModel TopicClassTag(ArticleModel ArticleCollection, Form1 form)
        {
            if (!Form1.AccessOperate.CheckColumnExist("AppComment", "C_Topic"))
                Form1.AccessOperate.UpdateQuery("ALTER TABLE AppComment ADD C_Topic char(20)");

            Dictionary<String, String> SelectedTopicTerm = new Dictionary<String, String>();  //用來裝從資料庫讀出來已選好的議題詞，格式 [具有該類別的關鍵議題詞,被規類的類別]
            Dictionary<String, int> TopicClassCount = new Dictionary<String, int>(); //裝計算類別分類的字典，格式[議題類別,分類(出現次數)]

            DataTable TopicTable = Form1.AccessOperate.SelectQuery("SELECT Term, Topic From TopicTerm WHERE Topic IS NOT NULL", "TopicTerm").Tables["TopicTerm"]; //讀取已選好的議題詞

            int count = 0;
            for (int i = 0; i < TopicTable.Rows.Count; i++)
            {
                form.UpdateStatus("正在讀取已選好的議題詞類別，第(" + (++count).ToString() + "/" + TopicTable.Rows.Count + ")個議題詞...");

                String Word = TopicTable.Rows[i]["Term"].ToString().Trim();
                String Class = TopicTable.Rows[i]["Topic"].ToString().Trim();

                SelectedTopicTerm.Add(Word, Class); //加入關鍵字對應類別字典

                if (!TopicClassCount.ContainsKey(Class))
                    TopicClassCount.Add(Class,0);  //若不包含重複類別，則加入類別計算字典

            }
            count = 0;
            //判斷文章類別
            foreach (Article article in ArticleCollection.GetArticleList())
            {
                form.UpdateStatus("正在判斷文章議題類別，第(" + (++count).ToString() + "/" + ArticleCollection.Count.ToString() + ")篇文章...");

                Term[] terms = article.GetTermArray(); //取出一篇文章的字詞陣列
                int Max_topic = 0;

                for (int TermLocation = 0; TermLocation < terms.Length; TermLocation++)  
                {
                    if (SelectedTopicTerm.ContainsKey(terms[TermLocation].GetWord())) //如果文章中有出現已選出來的議題詞
                    {
                        String Class = SelectedTopicTerm[terms[TermLocation].GetWord()];  //將該議題詞的次數加1
                        TopicClassCount[Class] += 1;

                        if (TopicClassCount[Class] > Max_topic)  //記錄的類別出現次數
                            Max_topic = TopicClassCount[Class];

                    }
                }

                String TargetClass = "";

                if (Max_topic != 0)
                {
                    foreach (KeyValuePair<String, int> topic in TopicClassCount.ToArray())  //將出現次數最多的類別指派給文章議題類別(可重複)
                    {
                        if (topic.Value == Max_topic)
                            TargetClass += topic.Key + " ";

                        TopicClassCount[topic.Key] = 0;
                    }
                }

                Form1.AccessOperate.UpdateQuery("UPDATE AppComment SET C_Topic = '" + TargetClass + "' WHERE CommentId = " + article.GetArticleID());
                article.TopicClass = TargetClass.Trim();

            }

            return ArticleCollection;
        }
        public static Dictionary<String, String> BuildDetailVerbPos(ArticleModel ArticleCollection, Form1 form)
        {
            Dictionary<String, String> VerbPos = new Dictionary<String, String>();  //暫存所有述詞

            int count = 0;
            foreach (Article article in ArticleCollection.GetArticleList())
            {
                form.UpdateStatus("正在建立述詞與其詳細詞性...第(" + (++count).ToString() + "/" + ArticleCollection.Count.ToString() + "筆)...");
                Term[] terms = article.GetTermArray(); //取出一篇文章的字詞陣列

                for (int j = 0; j < terms.Length; j++)  //不重複的動詞存進Temp
                {
                    Term term = terms[j];

                    if ((!term.GetSpeech().Contains("Vi") && !term.GetSpeech().Contains("Vt")) || VerbPos.ContainsKey(term.GetWord()))
                        continue;

                    if (term.GetWord().Contains("_NOT")) //判斷是否為經過負向處理的詞，則去除_NOT再計算
                    {
                        String word = term.GetWord();
                        term.SetWord(word.Substring(0, word.Length - 4));
                    }
                    String pos = CKIPSegmentation.CKIPbyDetailPOS(term.GetWord());  //用ckip斷一個詞並回傳詞性 

                    if (pos == "")
                        VerbPos.Add(term.GetWord(), "");
                    else
                    {
                        pos = pos.Substring(pos.IndexOf("("));
                        VerbPos.Add(term.GetWord(), pos);
                    }
                }
            }

            return VerbPos;
        }
        private void PrintSentimentTerm(Form1 form)
        {
            String PosOutput = "正面詞: ";
            String NegOutput = "負面詞: ";
            //int count = 0;
            foreach (KeyValuePair<String, String> term in SentimentTermSet)
            {
                if (term.Value.ToString() == "Pos")
                    PosOutput += "," + term.Key.ToString();
                else
                    NegOutput += "," + term.Key.ToString();
                //form.UpdateMSG(term.Key.ToString() + " " + term.Value.ToString());
                //output += term.Key.ToString() + " " + term.Value.ToString() + "\n";
                //count++;
            }
            form.UpdateMSG(PosOutput);
            form.UpdateMSG(NegOutput);
            form.UpdateMSG("共有: " + SentimentTermSet.Count.ToString() + "個詞");
            //form.UpdateMSG("====================================");
            //MessageBox.Show(count.ToString() + "\n" + output);
        }
    }
}
