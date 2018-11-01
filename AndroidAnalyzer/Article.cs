using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Collections;

namespace AndroidAnalyzer
{
    class Article
    {
        private String ArticleID;
        private Term[] Term;
        public int count;
        public int SentimentScore;
        public String TopicClass;

        public Article(String id, String CKIPContent)
        {
            SetArticleID(id);
            SetTermArray(CKIPContent);
            SentimentScore = 0;

            count = Term.Length;

        }
        private void SetArticleID(String id)
        {
            ArticleID = id;
        }
        private void SetTermArray(String CKIPContent)
        {
            String[] CkipContentList = Regex.Split(CKIPContent, " "); //將接收斷詞後的內容打成陣列
            ArrayList TermList = new ArrayList();  //儲存每一個Term物件

            for (int i = 0; i < CkipContentList.Length; i++)
            {
                String tmp = CkipContentList[i];

                int WordEnd = tmp.IndexOf("(") ;
                int SpeechStart = WordEnd +1;

                if ((tmp == "") || (tmp == "(FW)"))  //判斷空白內容
                    continue;
                if (WordEnd == 0)   //判斷詞是否為左刮號，是則加一位置
                {
                    WordEnd += 1;
                    SpeechStart += 1;
                }
                String word = tmp.Substring(0, WordEnd); //擷取字詞
                String speech = tmp.Substring(SpeechStart ,tmp.Length-SpeechStart-1);  //擷取OS
                //MessageBox.Show(word + "_" + speech);

                Term term = new Term(word,speech);
                TermList.Add(term);
            }

            Term = (Term[])TermList.ToArray(typeof(AndroidAnalyzer.Term));

        }
        public void ResetTermList(Term[] TermList)
        {
            this.Term = TermList;
            
        }
        public String GetArticleID()
        {
            return ArticleID;
        }
        public Term[] GetTermArray()
        {
            return Term;
        }
        public String GetCkipContent()
        {
            String returnString ="";

            for(int i = 0;i < Term.Length ; i++)
            {
                returnString += (Term[i].GetWord() + "(" + Term[i].GetSpeech() +") ");

            }
            return returnString;
        }

    }
}
