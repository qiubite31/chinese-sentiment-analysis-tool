using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Data;

namespace AndroidAnalyzer
{
    class FeatureWeight
    {
        private ArticleModel ArticleCollection;
        private Dictionary<String, Term> TermDic;

        public FeatureWeight(ArticleModel ArticleCollecion)
        {
            SetArticleModel(ArticleCollecion);
        }
        private void SetArticleModel(ArticleModel ArticleCollecion)
        {
            this.ArticleCollection = ArticleCollecion;
        }
        private void SetTermDic(Dictionary<String, Term> TermDic)
        {
            this.TermDic = TermDic;
        }

        public Dictionary<String, Term> BuildTermList(Form1 form)
        {
            int count = 0;

            Dictionary<String, Term> TermDic = new Dictionary<String, Term>();  //儲存所有的字

            foreach (Article article in ArticleCollection.GetArticleList())
            {
                form.UpdateStatus("正在計算字詞出現次數...第(" + (count + 1) + "/" + ArticleCollection.Count + ")筆資料");
                count++;

                Term[] terms = article.GetTermArray(); //取出一篇文章的字詞陣列

                ArrayList Distinct = new ArrayList(); //裝一篇文章中所有不重複的字，讓Dfrenquency一篇文章只會加一次

                for (int j = 0; j < article.GetTermArray().Length; j++)
                {
                    String word = terms[j].GetWord();
                    String speech = terms[j].GetSpeech();

                    if (word.Contains("_NOT")) //判斷是否為經過負向處理的詞，則去除_NOT再計算
                    {

                        word = word.Substring(0, word.Length - 4);
                    }


                    if (TermDic.ContainsKey(word))  //判斷該詞是否已被加被字典
                    {
                        Term term = TermDic[word];
                        term.Tfrequncy += 1;  //將Tfrenquency加1
                        if (!Distinct.Contains(word))  //判斷該詞的Dfrenquency沒被加過
                        {
                            term.Dfrequency += 1;  //將Dfrenquency加1
                            Distinct.Add(word);    //將該詞記錄起來，使同篇文章再出現也不會讓Dfrenquency增加
                        }
                        TermDic[word] = term;

                    }
                    else   //如果該詞還沒被加入字典，則建立一個Term物件並加入字典中
                    {
                        Term term = new Term(word, speech, 1, 1);  //建構Term物件
                        Distinct.Add(word);    //將該詞記錄起來，使同篇文章再出現也不會讓Dfrenquency增加

                        TermDic.Add(word, term); //加入字典

                    }
                    //MessageBox.Show(TermDic[word].GetWord() + " " + TermDic[word].GetSpeech() + " " + "TF: " + TermDic[word].Tfrequncy + "DF: " + TermDic[word].Dfrequency);
                }
            }
            SetTermDic(TermDic);

            return TermDic;
        }
        public ArticleModel ReplaceTerm(Form1 form)  //將算好Tfrenquency和Dfrenquency的Term物件取代原本沒有計算字詞頻率的Term物件
        {
            int count = 0;

            foreach (Article article in ArticleCollection.GetArticleList())
            {
                form.UpdateStatus("正在重建Term物件...第(" + (count + 1) + "/" + ArticleCollection.Count + ")篇文章");
                count++;

                Term[] terms = article.GetTermArray(); //取出一篇文章的字詞陣列

                for (int j = 0; j < article.GetTermArray().Length; j++)
                {
                    String word = terms[j].GetWord();
                    Term term;

                    if (word.Contains("_NOT")) //判斷是否為經過負向處理的詞，則去除_NOT再計算
                    {
                        word = word.Substring(0, word.Length - 4);

                        term = TermDic[word]; //取出Dictionary裡的Term物件
                        terms[j] = new Term(term.GetWord() + "_NOT", term.GetSpeech(), term.Tfrequncy, term.Dfrequency);  //加入TF和DF並重後建構ArticleModel內所有Article的Term物件

                        continue;
                    }

                    term = TermDic[word]; //取出Dictionary裡的Term物件

                    terms[j] = new Term(term.GetWord(), term.GetSpeech(), term.Tfrequncy, term.Dfrequency);  //加入TF和DF並重後建構ArticleModel內所有Article的Term物件
                }
            }

            return this.ArticleCollection;
        }
        public ArticleModel CountTFIDF(Form1 form)
        {
            int count = 0;
            foreach (Article article in ArticleCollection.GetArticleList())
            {
                form.UpdateStatus("正在計算TFIDF值...第(" + (count + 1) + "/" + ArticleCollection.Count + ")篇文章");
                count++;

                Term[] terms = article.GetTermArray(); //取出一篇文章的字詞陣列

                int RawTF = 0;

                for (int i = 0; i < article.GetTermArray().Length; i++)
                {

                    String word = terms[i].GetWord(); //取出要計算TF的詞

                    if (word.Contains("_NOT")) //判斷是否為經過負向處理的詞，則去除_NOT
                        word = word.Substring(0, word.Length - 4);

                    for (int j = 0; j < article.GetTermArray().Length; j++)  //將每一個字，和所有的字比較，找出出現次數
                    {
                        String wordConpare = terms[j].GetWord(); //取出要被比較的詞

                        if (wordConpare.Contains("_NOT")) //判斷是否為經過負向處理的詞，則去除_NOT
                            wordConpare = wordConpare.Substring(0, wordConpare.Length - 4);

                        if (word == wordConpare)
                            RawTF++;

                    }
                    //計算TF
                    //article.GetTermArray()[i].Tfrequncy = RawTF;

                    article.GetTermArray()[i].TF = ((double)RawTF / (double)terms.Length);
                    //計算IDF
                    article.GetTermArray()[i].IDF = Math.Log10((double)ArticleCollection.Count / (double)terms[i].Dfrequency);

                    //MessageBox.Show(terms[i].GetWord() + "\n" + terms[i].TF + ": " + terms[i].Tfrequncy + "/" + terms.Length + "\n" + terms[i].IDF + ": " + ArticleCollection.Count.ToString() + "/" + terms[i].Dfrequency);
                    RawTF = 0;
                }
            }

            return ArticleCollection;
        }
    }
}
