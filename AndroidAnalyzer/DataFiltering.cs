using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Data;
using System.Windows.Forms;
using System.Collections;

namespace AndroidAnalyzer
{
    class DataFiltering
    {
        private DataTable table;
        private ArticleModel ArticleCollection;

        public DataFiltering(DataTable table)
        {
            this.table = table;
        }
        public DataFiltering(ArticleModel Articles)
        {
            this.ArticleCollection = Articles;
        }
        public ArticleModel NonChineseFiltering(Form1 form)
        {
            int NonChinese = 0;
            int count = 0;

            foreach (Article article in ArticleCollection.GetArticleList().ToArray())
            {
                form.UpdateStatus("正在過瀘資料...第(" + (count + 1) + "/" + ArticleCollection.Count + ")筆資料");
                count++;

                Term[] Terms = article.GetTermArray();  //裝該文章所有詞的陣列
                String Content = "";  //儲存原文

                for (int i = 0; i < article.GetTermArray().Length; i++)  //將每個字串起來還原本文
                {
                    Content += Terms[i].GetWord() + " ";
                }

                Boolean HasChinese = CheckHasChinese(Content);  //判斷是否有中文

                if (!HasChinese)
                {
                    NonChinese++;
                    Form1.AccessOperate.UpdateQuery("DELETE FROM AppComment WHERE CommentId = " + article.GetArticleID());  //於資料庫中刪除該筆資料
                    ArticleCollection.GetArticleList().Remove(article);  //於建立的文章集合中，移除該結點
                }
            }
            ArticleCollection.refresh();
            form.UpdateMSG(("總共過濾" + NonChinese.ToString() + "篇文章"));

            return this.ArticleCollection;
        }
        public ArticleModel SameContentFiltering(Form1 form)
        {
            int SameContent = 0;
            int count = 0;
            Dictionary<String, String> Article = new Dictionary<String, String>();

            foreach (Article article in ArticleCollection.GetArticleList().ToArray())
            {
                form.UpdateStatus("正在過瀘資料...第(" + (count + 1) + "/" + ArticleCollection.Count + ")筆資料");
                count++;

                Term[] Terms = article.GetTermArray();  //裝該文章所有詞的陣列
                String Content = "";  //儲存原文

                for (int i = 0; i < article.GetTermArray().Length; i++)  //將每個字串起來還原本文
                {
                    Content += Terms[i].GetWord();
                }
                form.UpdateMSG(Content);
                if (!Article.ContainsKey(Content))
                {
                    Article.Add(Content, "");
                }
                else
                {
                    SameContent++;
                    Form1.AccessOperate.UpdateQuery("DELETE FROM AppComment WHERE CommentId = " + article.GetArticleID());  //於資料庫中刪除該筆資料
                    ArticleCollection.GetArticleList().Remove(article);  //於建立的文章集合中，移除該結點
                }
            }
            ArticleCollection.refresh();
            form.UpdateMSG(("總共過濾" + SameContent.ToString() + "篇文章"));

            return this.ArticleCollection;
        }
        public Boolean CheckHasChinese(String content)
        {
            //判定為中文或純英文資料
            if (Regex.Match(content, "[一-龥]").Success)
            {
                return true;
            }
            else
            {
                return false;
            } 
        }
        public ArticleModel SpeechFiltering(Form1 form)
        {
            int count = 0;

            List<String> SpeechList = new List<String> { "Vi", "Vt", "N", "Nv" };
            //List<String> SpeechList = new List<String> { "(VH)", "(VHC)", "(VI)", "(VJ)", "(VK)", "(VL)", "(Na)" };
            //List<String> SpeechList = new List<String> { "Vi", "Vt", "(Na)" };

            foreach (Article article in ArticleCollection.GetArticleList())
            {
                form.UpdateStatus("正在進行詞性過濾...第(" + (count + 1) + "/" + ArticleCollection.Count + ")筆資料");
                count++;

                Term[] Terms = article.GetTermArray();  //裝該文章所有詞的陣列
                ArrayList AfterFilterTermList = new ArrayList();  //儲存過濾後的詞
                for (int i = 0; i < article.GetTermArray().Length; i++ )
                {
                    /*
                    String word = Terms[i].GetWord();

                    if (Terms[i].GetWord().Contains("_NOT")) //判斷是否為經過負向處理的詞，則去除_NOT再計算
                        word = word.Substring(0, word.Length - 4);
                    
                    if (Terms[i].GetSpeech() == "Vi" || Terms[i].GetSpeech() == "Vt")
                    {
                        if (SpeechList.Contains(Terms[i].GetSpeech()))  //包含所需要的詞則加入Arraylist
                            AfterFilterTermList.Add(Terms[i]);

                        //if(SpeechList.Contains(Form1.VerbDetailPos[word]))
                          //  AfterFilterTermList.Add(Terms[i]);
                    }
                    if (Terms[i].GetSpeech() == "N" || Terms[i].GetSpeech() == "Nv")
                    {
                        if (SpeechList.Contains(Form1.NounDetailPos[word]))
                            AfterFilterTermList.Add(Terms[i]);
                    }
                    */
                    if (SpeechList.Contains(Terms[i].GetSpeech()))  //包含所需要的詞則加入Arraylist
                        AfterFilterTermList.Add(Terms[i]);

                }
                article.ResetTermList((Term[])AfterFilterTermList.ToArray(typeof(Term))); //將過濾後得到的新詞陣列作設定
            }
            return ArticleCollection;
        }
        public ArticleModel StopWordFiltering(List<String> StopWordList, Form1 form)
        {
            int count = 0;

            foreach (Article article in ArticleCollection.GetArticleList())
            {
                form.UpdateStatus("正在進行停用字過濾...第(" + (count + 1) + "/" + ArticleCollection.Count + ")筆資料");
                count++;

                Term[] Terms = article.GetTermArray();  //裝該文章所有詞的陣列
                ArrayList AfterFilterTermList = new ArrayList();  //儲存過濾後的詞
                for (int i = 0; i < article.GetTermArray().Length; i++)
                {
                    if (!StopWordList.Contains(Terms[i].GetWord()))  //包含所需要的詞則加入Arraylist
                        AfterFilterTermList.Add(Terms[i]);
                }
                article.ResetTermList((Term[])AfterFilterTermList.ToArray(typeof(Term))); //將過濾後得到的新詞陣列作設定
            }
            return ArticleCollection;
        }
        public ArticleModel ArticleFiltering(List<String> Feature, Form1 form)
        {
            form.UpdateStatus("正在過濾低特徵詞數量的文章...");

            int Thr = 0;
            int count = 0;
            int max = 0;
            int ArticleNum = ArticleCollection.GetArticleList().Count();
            foreach (Article article in ArticleCollection.GetArticleList().ToArray())
            {
                form.UpdateStatus("正在過濾低特徵詞數量的文章...第(" + (++count) + "/" + ArticleNum.ToString() + ")筆資料");

                Boolean isEmpty = true;

                Term[] termArray = article.GetTermArray();
                int FeatureCount = 0;

                for (int i = 0; i < termArray.Length; i++)
                {
                    if (Feature.Contains(termArray[i].GetWord()))
                    {
                        isEmpty = false;
                        FeatureCount++;
                        //break;
                    }
                }
                if (FeatureCount > max)
                    max = FeatureCount;

                if (isEmpty || FeatureCount < Thr)
                    ArticleCollection.GetArticleList().Remove(article);
            }
            //MessageBox.Show(max.ToString());
            return ArticleCollection;
        }

       

    }
}
