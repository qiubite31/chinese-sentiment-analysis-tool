using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using System.Windows.Forms;

namespace AndroidAnalyzer
{
    class TopicExtraction
    {
        public ArticleModel ArticleCollection;
        public Dictionary<String, String> TermCluster;
        public Dictionary<String, String> Term2Network = new Dictionary<String, String>();
        public Dictionary<String, String> Network2Cluster = new Dictionary<String, String>();

        public Dictionary<String, Term> TopicTermSet = new Dictionary<String, Term>(); //存所有議題詞集
        public Dictionary<String, Term> NounTermSet = new Dictionary<String, Term>(); //存所有名詞詞集

        public TopicExtraction()
        {
 
        }
        public TopicExtraction(ArticleModel ArticleCollection)
        {
            SetArticleCollection(ArticleCollection);
        }
        public void SetArticleCollection(ArticleModel ArticleCollection)
        {
            this.ArticleCollection = ArticleCollection;
        }
        public void SetTerm2Network(Dictionary<String,String> dic)
        {
            Term2Network = dic;
        }
        public void SetNetwork2Cluster(Dictionary<String, String> dic)
        {
            Network2Cluster = dic;
        }
        public void SetTerm2Cluster()
        {
            TermCluster = new Dictionary<String, String>();

            foreach (KeyValuePair<String, String> term in Term2Network)
            {
                String Term = term.Key;
                String Cluster = Network2Cluster[term.Value];

                TermCluster.Add(Term, Cluster);

                Form1.AccessOperate.UpdateQuery("UPDATE TopicTerm SET Topic = '" + Cluster.ToString() + "' WHERE Term = '" + term.Key + "'");
            }

        }
        public void SetTopicTermFromDB(Form1 form)  //從資料庫已存在的議題詞建立議題詞集物件
        {
            DataTable TopicSetTable = new DataTable();
            TopicSetTable = Form1.AccessOperate.SelectQuery("SELECT * FROM NounTerm WHERE Topic = 'V'", "NounTerm").Tables["NounTerm"];

            Dictionary<String, Term> TopicTermSetTmp = new Dictionary<String, Term>(); //暫存所有議題詞集

            int count = 0;
            for (int i = 0; i < TopicSetTable.Rows.Count; i++)
            {
                form.UpdateStatus("正在建立議題詞集，第(" + (++count).ToString() + "/" + TopicSetTable.Rows.Count + ")個情感詞...");

                String Word = TopicSetTable.Rows[i]["Term"].ToString().Trim();
                String Speech = TopicSetTable.Rows[i]["Speech"].ToString().Trim();
                int TF = (int)TopicSetTable.Rows[i]["TF"];
                int DF = (int)TopicSetTable.Rows[i]["DF"];
                double IDF = Math.Round((double)TopicSetTable.Rows[i]["IDF"], 3);
                double TFIDF = Math.Round((double)TopicSetTable.Rows[i]["TFIDF"], 3);

                Term term = new Term(Word, Speech, TF, DF, TF, IDF);

                if (!TopicTermSetTmp.ContainsKey(Word))  //判斷掉"神奇"的情感詞重複問題
                {
                    TopicTermSetTmp.Add(Word, term);
                }
            }
            TopicTermSet = new Dictionary<String, Term>(TopicTermSetTmp);
            PrintTopicTerm(form);
        }
        public void SelectCandidateTopic(Dictionary<String, String> SentimentTermSet, Form1 form)
        {
            if (Form1.AccessOperate.CheckTableExist("NounTerm"))
            {
                Form1.AccessOperate.DropTable("NounTerm");
                Form1.AccessOperate.CreateTable("CREATE TABLE NounTerm(Term char(50), Speech char(2),TF integer, DF integer, IDF double, TFIDF double,  Topic char(2))");
            }
            else
                Form1.AccessOperate.CreateTable("CREATE TABLE NounTerm(Term char(50), Speech char(2),TF integer, DF integer, IDF double, TFIDF double,  Topic char(2))");


            Dictionary<String, Term> CandidateTopicSetTmp = new Dictionary<String, Term>(); //暫存議題候選詞
            Dictionary<String, Term> NounSetTmp = new Dictionary<String, Term>(); //暫存名詞詞集

            int count = 0;
            foreach (Article article in ArticleCollection.GetArticleList())
            {
                form.UpdateStatus("正在搜尋候選議題詞，第(" + (++count).ToString() + "/" + ArticleCollection.Count.ToString() + ")篇文章...");

                Term[] terms = article.GetTermArray(); //取出一篇文章的字詞陣列

                ArrayList SentimentTermLocation = new ArrayList(); //記錄一篇文章中情感詞出現的位置

                for (int TermLocation = 0; TermLocation < terms.Length; TermLocation++)  //找出一篇文章所有情感詞位置的迴圈
                {
                    String word = terms[TermLocation].GetWord();

                    if (word.Contains("_NOT")) //判斷是否為經過負向處理的詞，則去除_NOT再計算
                        word = word.Substring(0, word.Length - 4);

                    if ((terms[TermLocation].GetSpeech() == "N" || terms[TermLocation].GetSpeech() == "Nv") && !NounSetTmp.ContainsKey(word))
                    {
                        //找到所有不重複的名詞，加入名詞詞集暫存，並新增到資料庫
                        NounSetTmp.Add(word, terms[TermLocation]);

                        Form1.AccessOperate.UpdateQuery("INSERT INTO NounTerm(Term, Speech, TF, DF, IDF, TFIDF, Topic) VALUES('"
                            + word + "', '" + terms[TermLocation].GetSpeech() + "', " + terms[TermLocation].Tfrequncy + ", "
                            + terms[TermLocation].Dfrequency + ", " + Math.Round(terms[TermLocation].IDF, 3) + ", " +
                            +Math.Round(terms[TermLocation].Tfrequncy * terms[TermLocation].IDF, 3) + ", 'X')");
                    }

                    //找到所有情感詞位置
                    if ((terms[TermLocation].GetSpeech() == "Vi" || terms[TermLocation].GetSpeech() == "Vt") && SentimentTermSet.ContainsKey(terms[TermLocation].GetWord()))
                        SentimentTermLocation.Add(TermLocation);  //記錄情感詞的位置

                }

                foreach (int location in SentimentTermLocation)  //找到每篇文章中，所有情感詞附近的名詞
                {
                    //form.UpdateStatus("正在搜尋候選議題詞，第(" + (++count).ToString() + "/" + ArticleCollection.Count.ToString() + ")篇文章中的...");

                    Boolean flag = false; //是否找到候選議題詞
                    int index = 1;

                    while (index <= terms.Length && !flag)  //不超出陣列且未找到候選議題詞
                    {
                        if (location + index < terms.Length && (terms[location + index].GetSpeech() == "N" || terms[location + index].GetSpeech() == "Nv"))
                        {
                            String word = terms[location + index].GetWord();
                            if (word.Contains("_NOT")) //判斷是否為經過負向處理的詞，則去除_NOT再計算
                                word = word.Substring(0, word.Length - 4);

                            if (!CandidateTopicSetTmp.ContainsKey(word))  //如果候選議題詞不重複則加入dictionary
                            {
                                //存入Dic格式－[字詞,文章ID_字詞位置_情感字詞]
                                //CandidateTopicSetTmp.Add(terms[location + index].GetWord(), article.GetArticleID() + "_" + (location + index).ToString() + "_" + terms[location].GetWord());
                                CandidateTopicSetTmp.Add(word, terms[location + index]);
                                Form1.AccessOperate.UpdateQuery("UPDATE NounTerm SET Topic = 'V' WHERE Term = '" + word + "'");
                            }
                            flag = true;
                        }
                        if (location - index >= 0 && (terms[location - index].GetSpeech() == "N" || terms[location - index].GetSpeech() == "Nv"))
                        {
                            String word = terms[location - index].GetWord();
                            if (word.Contains("_NOT")) //判斷是否為經過負向處理的詞，則去除_NOT再計算
                                word = word.Substring(0, word.Length - 4);

                            if (!CandidateTopicSetTmp.ContainsKey(word))  //如果候選議題詞不重複則加入dictionary
                            {
                                //存入Dic格式－[字詞,文章ID_字詞位置_情感字詞]
                                //CandidateTopicSetTmp.Add(terms[location - index].GetWord(), article.GetArticleID() + "_" + (location - index).ToString() + "_" + terms[location].GetWord());
                                CandidateTopicSetTmp.Add(word, terms[location - index]);
                                Form1.AccessOperate.UpdateQuery("UPDATE NounTerm SET Topic = 'V' WHERE Term = '" + word + "'");
                            }
                            flag = true;
                        }
                        index++;
                    }
                }
            }
            TopicTermSet = new Dictionary<String, Term>(CandidateTopicSetTmp);  //將暫存指派給候選詞集
            NounTermSet = new Dictionary<String, Term>(NounSetTmp);  //將暫存指派給名詞詞集
            PrintTopicTerm(form);


        }
        public void FilterTopicTerm(int p, int DF, int TermLen, Boolean PosFilter, Form1 form)
        {
            Dictionary<String, Term> TopicTermSetTmp = new Dictionary<String, Term>();

            if (Form1.AccessOperate.CheckTableExist("TopicTerm"))
            {
                Form1.AccessOperate.DropTable("TopicTerm");
                Form1.AccessOperate.CreateTable("CREATE TABLE TopicTerm(Term char(50), Speech char(2),TF integer, DF integer, IDF double, TFIDF double,  Topic char(10))");
            }
            else
                Form1.AccessOperate.CreateTable("CREATE TABLE TopicTerm(Term char(50), Speech char(2),TF integer, DF integer, IDF double, TFIDF double,  Topic char(10))");

            //用DF排序
            List<KeyValuePair<String, Term>> DFRank = TopicTermSet.ToList();
            DFRank.Sort(delegate(KeyValuePair<String, Term> firstPair, KeyValuePair<String, Term> nextPair) { return firstPair.Value.CompareTo(nextPair.Value); });

            int threshold = DFRank.Count - (int)(DFRank.Count * p * 0.01);

            form.UpdateMSG("過濾前共有: " + DFRank.Count + "筆");
            //MessageBox.Show(threshold.ToString());
            int count = 0;
            foreach (KeyValuePair<String, Term> term in DFRank)
            {
                form.UpdateStatus("正在篩選議題字詞，第(" + (count + 1).ToString() + "/" + DFRank.Count + ")個情感詞...");

                if (PosFilter)
                {
                    if (Form1.NounDetailPos[term.Key] != "(Na)")
                        continue;
                }

                if ((++count) <= threshold && term.Value.Dfrequency > DF && term.Value.GetWord().Length > TermLen)
                {
                    Form1.AccessOperate.UpdateQuery("INSERT INTO TopicTerm(Term, Speech, TF, DF, IDF, TFIDF, Topic) VALUES('"
                           + term.Key + "', '" + term.Value.GetSpeech() + "', " + term.Value.Tfrequncy + ", "
                           + term.Value.Dfrequency + ", " + Math.Round(term.Value.IDF, 3) + ", " +
                           +Math.Round(term.Value.Tfrequncy * term.Value.IDF, 3) + ", 'V')");

                    TopicTermSetTmp.Add(term.Key, term.Value);
                }


            }
            TopicTermSet = TopicTermSetTmp;
            form.UpdateMSG("過濾後共有: " + TopicTermSet.Count + "筆");

        }
        public SparseMatrix<double> ConputeMIScore(Form1 form)
        {
            SparseMatrix<double> MIScore = new SparseMatrix<double>();  //裝MI分數的矩陣

            DataTable DataTable = new DataTable();

            DataTable = Form1.AccessOperate.SelectQuery("SELECT Term,DF FROM TopicTerm", "TopicTerm").Tables["TopicTerm"];  //查詢出所有的議題詞

            List<String> NTerm = new List<String>();  //裝所有議題詞
            Dictionary<String, int> DF = new Dictionary<String, int>();  //裝所有的DF值

            for (int i = 0; i < DataTable.Rows.Count; i++)  
            {
                form.UpdateStatus("正在建立議題詞與DF集合...第(" + (i + 1) + "/" + DataTable.Rows.Count + ")筆資料");
                NTerm.Add(DataTable.Rows[i]["Term"].ToString().Trim());
                DF.Add(NTerm[i], int.Parse(DataTable.Rows[i]["DF"].ToString().Trim()));
            }


            for (int i = 0; i < NTerm.Count; i++)  
            {
                for (int j = i; j < NTerm.Count; j++)
                {
                    form.UpdateStatus("正在計算MIScore值...第(" + (i + 1) + "/" + NTerm.Count + ")筆資料，第(" + (j + 1) + "/" + NTerm.Count + ")個目標");

                    if (i == j) continue;

                    int count = 0;
                    foreach (Article article in ArticleCollection.GetArticleList())
                    {
                        Boolean Exist = false;  
                        Boolean CompExist = false;  

                        Term[] terms = article.GetTermArray(); //取出一篇文章的字詞陣列

                        //將文章的詞陣列打成Dictionary，使查詢速度為O(1)，加快速度。用詞為key，Term物件為Value
                        Dictionary<String, Term> TermDic = new Dictionary<string, Term>();

                        for (int k = 0; k < terms.Length; k++)
                        {
                            if (!TermDic.ContainsKey(terms[k].GetWord()))
                                TermDic.Add(terms[k].GetWord(), terms[k]);
                        }
                        //==================================

                        if(TermDic.ContainsKey(NTerm[i]))
                            Exist = true;
                        if (TermDic.ContainsKey(NTerm[j]))
                            CompExist = true;

                        if (Exist && CompExist)  //如果兩個詞都有共同出現在一篇文章，則count增加
                            count++;

                    }

                    double joint = (double)count / (double)ArticleCollection.Count;
                    double margin1 = (double)DF[NTerm[i]] / (double)ArticleCollection.Count;
                    double margin2 = (double)DF[NTerm[j]] / (double)ArticleCollection.Count;
                    /*
                    double score = joint / (margin1 * margin2);

                    if (score == 0.0)
                        score = -1.0;
                    else
                    {
                        score = Math.Log(score, 2.0);  //取log
                        score = score /  -Math.Log(joint, 2.0);  //正規化
                    }
 */
                    double score = (double)count;
                    //double score = (double)count / ((double)DF[NTerm[i]] * (double)DF[NTerm[j]]) * (double)ArticleCollection.Count;  //計算MI分數
                    //double score = (double)count;
                    MIScore.SetAt(i, j, score);  //寫入MI分數
                    MIScore.SetAt(j, i, score);  //寫入對稱位置的MI分數
                }
            }

            return MIScore;
        }
        public static Dictionary<String, String> BuildDetailNounPos(ArticleModel ArticleCollection, Form1 form)
        {
            Dictionary<String, String> NounPos = new Dictionary<String, String>();  //暫存所有述詞

            int count = 0;
            foreach (Article article in ArticleCollection.GetArticleList())
            {
                form.UpdateStatus("正在建立體詞與其詳細詞性...第(" + (++count).ToString() + "/" + ArticleCollection.Count.ToString() + "筆)...");
                Term[] terms = article.GetTermArray(); //取出一篇文章的字詞陣列

                for (int j = 0; j < terms.Length; j++)  //不重複的動詞存進Temp
                {
                    Term term = terms[j];

                    if ((!term.GetSpeech().Contains("N") && !term.GetSpeech().Contains("Nv")) || NounPos.ContainsKey(term.GetWord()))
                        continue;

                    if (term.GetWord().Contains("_NOT")) //判斷是否為經過負向處理的詞，則去除_NOT再計算
                    {
                        String word = term.GetWord();
                        term.SetWord(word.Substring(0, word.Length - 4));
                    }
                    String pos = CKIPSegmentation.CKIPbyDetailPOS(term.GetWord());  //用ckip斷一個詞並回傳詞性 

                    if (pos == "")
                        NounPos.Add(term.GetWord(), "");
                    else
                    {
                        if (!pos.Contains(" "))
                        {
                            pos = pos.Substring(pos.IndexOf("("));
                            NounPos.Add(term.GetWord(), pos);
                        }
                        else
                        {
                            pos = pos.Substring(pos.IndexOf(" "));
                            pos = pos.Substring(pos.IndexOf("("));
                            NounPos.Add(term.GetWord(), pos);
                        }
                    }
                }
            }

            return NounPos;
        }
        private void PrintTopicTerm(Form1 form)
        {
            String output = "";
            int count = 0;
            foreach (KeyValuePair<String, Term> term in TopicTermSet)
            {
                //form.UpdateMSG(term.Key.ToString() + " " + term.Value.GetSpeech());
                output += term.Key.ToString() + " " + term.Value.GetSpeech() + "\n";
                count++;
            }
            //form.UpdateMSG("====================================");
            //MessageBox.Show(count.ToString() + "\n" + output);
        }

    }
}
