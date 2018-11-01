using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace AndroidAnalyzer
{
    class OutputFile
    {
        public static void Output2ColumnCSV(Dictionary<String,String> dic, String path, String name,String Disc, Form1 form)
        {
            StreamWriter writer = new StreamWriter(path + "\\" + name, false, System.Text.Encoding.UTF8);

            int count = 0;
            foreach (KeyValuePair<String, String> item in dic)
            {
                form.UpdateStatus("正在建立" + Disc + "...第(" + (++count) + "/" + dic.Count + ")筆資料");

                writer.WriteLine(item.Key + "," + item.Value);
            }
            writer.Close();
 
        }
        public static void OutputArticleTable(Form1 form, String path, ArticleModel ArticleCollection)
        {
            int count = 0;

            if (File.Exists(path + "\\ArticleTable.csv"))
                File.Delete(path + "\\ArticleTable.csv");

            StreamWriter writer = new StreamWriter(path + "\\ArticleTable.csv", false, System.Text.Encoding.UTF8);

            foreach (Article article in ArticleCollection.GetArticleList())
            {
                form.UpdateStatus("正在建立文章表...第(" + (count + 1) + "/" + ArticleCollection.Count + ")筆資料");
                count++;

                Term[] term = article.GetTermArray(); //取出一篇文章的字詞陣列

                String WriteContent = article.GetArticleID();

                for (int j = 0; j < article.GetTermArray().Length; j++)
                {
                    String word = term[j].GetWord();

                    if (word.Contains("_NOT")) //判斷是否為經過負向處理的詞，則去除_NOT再計算
                    {

                        word = word.Substring(0, word.Length - 4);
                    }

                    WriteContent += "," + word;
                }

                writer.WriteLine(WriteContent);

            }
            writer.Close();
            form.UpdateStatus("文章表csv檔輸出完畢！");
        }
        public static void OutputTermFrenquency(Form1 form, String path, Dictionary<String,Term> TermDic)
        {
            int count = 0;

            if (File.Exists(path + "\\TermFrenquency.csv"))
                File.Delete(path + "\\TermFrenquency.csv");

            StreamWriter writer = new StreamWriter(path + "\\TermFrenquency.csv", false, System.Text.Encoding.UTF8);

            writer.WriteLine("Term,TF,DF");

            foreach (var term in TermDic)
            {
                form.UpdateStatus("正在輸出字詞頻率計算結果...第(" + (count + 1) + "/" + TermDic.Count + ")個詞");
                count++;

                writer.WriteLine(term.Key + "," + term.Value.Tfrequncy + "," + term.Value.Dfrequency);

            }
            writer.Close();
            form.UpdateStatus("字詞頻率csv檔輸出完畢！");
        }
        public static void OutputVSMbySparseMatrix(Form1 form, List<String> FeatureCollection, String OutputPath, SparseMatrix<double> VSM)
        {
            if (File.Exists(OutputPath + "\\VectorSpaceModel.csv"))
                File.Delete(OutputPath + "\\VectorSpaceModel.csv");

            StreamWriter writer = new StreamWriter(OutputPath + "\\VectorSpaceModel.csv", false, System.Text.Encoding.UTF8);

            FeatureCollection.Remove("ID");
            FeatureCollection.Remove("CLASS");


            writer.Write("ID,");
            foreach(String Feature in FeatureCollection)
                writer.Write(Feature + ",");
            writer.Write("CLASS");

            for (int ArticleIndex = 0; ArticleIndex < VSM.GetColumnDataCount(0); ArticleIndex++)
            {
                form.UpdateStatus("正在輸出向量空間模型...第(" + (ArticleIndex + 1) + "/" + VSM.GetColumnDataCount(0) + ")篇文章");

                if ((int)VSM.GetAt(ArticleIndex, FeatureCollection.Count + 1) == 2)
                    continue;

                writer.Write("\n");

                int t = FeatureCollection.Count;
                for (int FeatureIndex = 0; FeatureIndex < FeatureCollection.Count + 2; FeatureIndex++)
                {
                    if (FeatureIndex == 0)
                        writer.Write((int)VSM.GetAt(ArticleIndex, FeatureIndex));
                    else
                        writer.Write("," + double.Parse(VSM.GetAt(ArticleIndex, FeatureIndex).ToString("0.0000")));
                }
            }
            writer.Close();
            form.UpdateStatus("向量空間模型csv檔輸出完畢！");
        }
        public static void OutputFeatureIGScore(Form1 form, Dictionary<String,double> FeatureIGScore,String OutputPath)
        {
            //輸出IG分數檔案
            int count = 0;

            if (File.Exists(OutputPath + "\\IGScore.csv"))
                File.Delete(OutputPath + "\\IGScore.csv");

            StreamWriter writer = new StreamWriter(OutputPath + "\\IGScore.csv", false, System.Text.Encoding.UTF8);

            foreach (KeyValuePair<String, double> feature in FeatureIGScore)
            {
                form.UpdateStatus("正在輸出IG分數...第(" + (++count) + "/" + FeatureIGScore.Count + ")個詞");
                writer.WriteLine(feature.Key.ToString() + "," + feature.Value.ToString("0.0000"));
            }
            writer.Close();

            form.UpdateStatus("特徵詞的IG分數表輸出完成！");
        }
        public static void OutputMatrixCSV(SparseMatrix<double> Matrix, List<String> ID, List<String> Feature,String path,String name, String Desc, Form1 form)
        {
            StreamWriter writer = new StreamWriter(path + "\\" + name, false, System.Text.Encoding.UTF8);

            for (int FeatureIndex = 0; FeatureIndex < Feature.Count; FeatureIndex++) //寫入特徵欄
            {
                writer.Write("," + Feature[FeatureIndex]);
            }

            for (int MatrixIndex = 0; MatrixIndex < ID.Count; MatrixIndex++)
            {
                form.UpdateStatus("正在輸出" + Desc + "...第(" + (MatrixIndex + 1) + "/" + Feature.Count + ")筆資料...");

                writer.Write("\n");
                writer.Write(ID[MatrixIndex]);
                
                for (int FeatureIndex = 0; FeatureIndex < Feature.Count; FeatureIndex++)
                {
                    writer.Write("," + double.Parse(Matrix.GetAt(MatrixIndex, FeatureIndex).ToString("0.0000")));
                }
            }
            writer.Close();
            form.UpdateStatus( Desc + "csv檔輸出完畢！");
        }
    }
}
