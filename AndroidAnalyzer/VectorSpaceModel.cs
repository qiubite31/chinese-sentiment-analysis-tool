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
    class VectorSpaceModel
    {
        public SparseMatrix<double> VSM;
        public List<String> FeatureCollection;
        public List<String> IDCollection;

        public VectorSpaceModel()
        {

        }
        public void SetFeatureCollection(List<String> List)
        {
            FeatureCollection = List;
        }
        public void SetFeatureCollection(Dictionary<String,Term> Dic)
        {
            FeatureCollection = Dic.Keys.ToList();
 
        }
        public void SetFeatureCollection(String SelectedFeaturePath)
        {
            String[] FeatureArray;

            StreamReader reader = File.OpenText(SelectedFeaturePath);
            String SelectedFeature = reader.ReadLine();

            FeatureArray = Regex.Split(SelectedFeature, ","); //將特徵詞集分開打成陣列

            FeatureCollection = FeatureArray.ToList();

            FeatureCollection.Remove("ID");
            FeatureCollection.Remove("CLASS");

        }
        public void SetIDCollection(List<String> List)
        {
            IDCollection = List;
        }
        public SparseMatrix<double> BuildVSMbyCSV(Form1 form, String path)
        {
            VSM = new SparseMatrix<double>();

            StreamReader reader = File.OpenText(path);
            String Lines = reader.ReadLine(); //跳過特徵

            int RowCount = 0;
            while(Lines != null)
            {
                Lines = reader.ReadLine();

                String[] ValueList ;

                if (Lines != null)
                {
                    ValueList = Regex.Split(Lines, ","); //打成陣列

                    for (int ColumnCount = 0; ColumnCount < ValueList.Length ; ColumnCount++)
                        VSM.SetAt(RowCount, ColumnCount, double.Parse(ValueList[ColumnCount]));           
                }
                RowCount++;
            }
            return VSM;
        }
        public SparseMatrix<double> BuildVSM(Form1 form, ArticleModel ArticleCollection)
        {
            VSM = new SparseMatrix<double>();  //建立稀疏矩陣

            int ArticleCount = 0; //跑文章數

            foreach (Article article in ArticleCollection.GetArticleList())  //所有的數章
            {
                form.UpdateStatus("正在建立向量空間模型...第(" + (ArticleCount + 1) + "/" + ArticleCollection.Count + ")筆資料");

                VSM.SetAt(ArticleCount, 0, double.Parse(article.GetArticleID()));  //寫入ID

                for (int FeatureIndex = 0; FeatureIndex < FeatureCollection.Count; FeatureIndex++)  //一篇文章共要寫入的個特徵詞
                {
                    Term[] terms = article.GetTermArray();  //每篇文章的詞陣列

                    //將文章的詞陣列打成Dictionary，使查詢速度為O(1)，加快速度。用詞為key，Term物件為Value
                    Dictionary<String, Term> TermDic = new Dictionary<string,Term>();

                    for (int i = 0; i < terms.Length; i++)
                    {
                        if(!TermDic.ContainsKey(terms[i].GetWord()))
                            TermDic.Add(terms[i].GetWord(), terms[i]);
                    }
                    //==================================

                    //如果該特徵詞有出現在文章中，則寫入目前特徵詞的tfidf，否則寫入0
                    if(TermDic.ContainsKey(FeatureCollection[FeatureIndex]))
                        VSM.SetAt(ArticleCount, FeatureIndex + 1, TermDic[FeatureCollection[FeatureIndex]].TF * TermDic[FeatureCollection[FeatureIndex]].IDF);
                        //VSM.SetAt(ArticleCount, FeatureIndex + 1, 1.0);
                    else
                        VSM.SetAt(ArticleCount, FeatureIndex + 1, 0.0);
                }
                ArticleCount++;
            }
            return VSM;
        }
        public void VSMTranspose(Form1 form)
        {
            SparseMatrix<double> VSM_New = new SparseMatrix<double>();

            form.UpdateStatus("正在轉置向量空間模型...");

            List<String> ArticleID = new List<String>();

            for (int i = 0; i < VSM.GetColumnDataCount(0); i++)  //將原本的ID寫入到新矩陣的變數位置(第一行轉為第一列)
            {
                ArticleID.Add(VSM.GetAt(i,0).ToString());   //將原本的ID作紀錄，之後要轉為變數
                //VSM_New.SetAt(0,i,VSM.GetAt(i,0));
            }
            for (int i = 0; i < FeatureCollection.Count; i++)  //輸出變數對照表
            {
                form.UpdateMSG(FeatureCollection[i] + " " + (i+1).ToString());

                VSM_New.SetAt(i,0,(double)(i+1));
            }

            for (int ArticleIndex = 0; ArticleIndex < VSM.GetColumnDataCount(0); ArticleIndex++)
            {
                for (int FeatureIndex = 1; FeatureIndex <= FeatureCollection.Count; FeatureIndex++)
                {

                    VSM_New.SetAt(FeatureIndex -1 ,ArticleIndex + 1,VSM.GetAt(ArticleIndex,FeatureIndex));
                }
            }

            this.VSM = VSM_New;
            this.FeatureCollection = ArticleID;

        }

        public void ClassTag(ArticleModel ArticleCollection, int ClassType, Form1 form)
        {
            int VSMLength = FeatureCollection.Count;  //特徵詞的總數量
            Dictionary<String, int> TopicMapping = null;  


            if (ClassType == 2) //如果選擇輸出議題類別，則先建立議題類別對照字典
            {
                TopicMapping = new Dictionary<String, int>();
                //類資料庫選出所有不重複的議題類別
                DataTable TopicTable = Form1.AccessOperate.SelectQuery("SELECT DISTINCT Topic FROM TopicTerm", "TopicTerm").Tables["TopicTerm"];

                //將議題類別加入Dic，並輸出議題類別與編號的對照表
                form.UpdateMSG("議題類別與編號對照：");
                for (int i = 0; i < TopicTable.Rows.Count; i++)
                {
                    TopicMapping.Add(TopicTable.Rows[i]["Topic"].ToString(), i);
                    form.UpdateMSG(TopicTable.Rows[i]["Topic"].ToString() + " " + i.ToString());
                }
            }

            int count = 0; //文章計算
            foreach (Article article in ArticleCollection.GetArticleList()) //將每篇文章標記類別
            {
                form.UpdateStatus("正在補齊文章類別...第(" + (count + 1) + "/" + ArticleCollection.Count + ")篇文章");

                if (ClassType == 0)  //不補齊類別
                    break;
                if (ClassType == 1)  //補齊情感類別
                {
                    if (article.SentimentScore > 0)
                        VSM.SetAt(count, VSMLength + 1, 0.0);  //假設特徵有5個，一筆資料共有7格(0-6)，0為ID，故類別要塞在第5+1格位置
                    else if (article.SentimentScore < 0)
                        VSM.SetAt(count, VSMLength + 1, 1.0);
                    else
                        VSM.SetAt(count, VSMLength + 1, 2.0);
                }
                if (ClassType == 2)//補齊議題類別
                {
                    if (article.TopicClass == null)
                    {
                        MessageBox.Show("尚未計算文章議題類別！");
                        break;
                    }

                    if (TopicMapping.ContainsKey(article.TopicClass))
                        VSM.SetAt(count, VSMLength + 1, TopicMapping[article.TopicClass]);  //寫入該議題的類別的類別編號
                    else
                        VSM.SetAt(count, VSMLength + 1, 9999.0);
                }
                count++;
            }
        }

    }

}
