using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;

namespace AndroidAnalyzer
{
    class ArticleModel
    {
        private LinkedList<Article> ArticleList = new LinkedList<Article>();
        public int Count;
       
        public ArticleModel(DataTable table, Form1 form)
        {
            for (int i = 0; i < table.Rows.Count;i++ )
            {
                form.UpdateStatus("正在建立ArticleModel...第(" + (i + 1) + "/" + table.Rows.Count + ")筆資料");

                Article article = new Article(table.Rows[i]["CommentId"].ToString(), table.Rows[i]["ContentCKIP"].ToString());

                ArticleList.AddLast(article);
            }

            Count = ArticleList.Count;
        }
        public LinkedList<Article> GetArticleList()
        {
            return ArticleList;
        }
        public void refresh()
        {
            Count = ArticleList.Count; 
        }
        public void Print()
        {
            foreach (Article article in ArticleList)
            {
                Term[] terms = article.GetTermArray(); //取出一篇文章的字詞陣列

                String output = article.GetArticleID();

                for (int j = 0; j < article.GetTermArray().Length; j++)
                {
                    String word = terms[j].GetWord();

                    if (word.Contains("_NOT")) //判斷是否為經過負向處理的詞，則去除_NOT再計算
                        word = word.Substring(0, word.Length - 4);

                    output += " " + word + "TF: " + terms[j].TF + "IDF: " + terms[j].IDF + "\n";
                    MessageBox.Show(output);
                }


            }

        }
    }
}
