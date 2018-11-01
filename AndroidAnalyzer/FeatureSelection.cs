using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace AndroidAnalyzer
{
    class FeatureSelection
    {
        public VectorSpaceModel VSM;

        public FeatureSelection(VectorSpaceModel VSM)
        {
            this.VSM = VSM;
        }
        public Dictionary<String, double> InformationGain(Form1 form)
        {
            int FeatureNum = VSM.FeatureCollection.Count;  //特徵的數量
            double ClassEntropy = 0.0;
            Dictionary<double, int> ClassDic = new Dictionary<double, int>();  //儲存所有類別與類別的各類別的資料量
            Dictionary<String, double> FeatureIGScore = new Dictionary<string, double>(); //裝所有算完IG的特徵詞和IG分數

            int ArticleNum = VSM.VSM.GetColumnDataCount(0);  //文章的總數

            for (int i = 0; i < ArticleNum; i++)  //總計所有文章的類別與各類別資料量
            {
                if(!ClassDic.ContainsKey((int)VSM.VSM.GetAt(i,FeatureNum + 1)))  //如果該類別沒出現過則加入
                    ClassDic.Add((int)VSM.VSM.GetAt(i, FeatureNum + 1), 1);
                else
                    ClassDic[(int)VSM.VSM.GetAt(i, FeatureNum + 1)]++;   //如該類別已出現，則作+1

            }
            foreach (KeyValuePair<double, int> item in ClassDic)  //計算類別的Entropy
            {
                double SingleClassEntropy = -((double)item.Value / (double)ArticleNum) * Math.Log((double)item.Value / (double)ArticleNum, 2);
                ClassEntropy += SingleClassEntropy;
            }
            form.UpdateMSG(" ");
            form.UpdateMSG("類別的Entropy:" + ClassEntropy.ToString());

            //類別與陣列位置的對照表。類別可能是1,2,3,4,999...等等，要對應到陣列位置0,1,2,3...
            Dictionary<int, int> Mapping = new Dictionary<int, int>();
            int change = -1;
            foreach (int index in ClassDic.Keys.ToList())
            {
                if (!Mapping.ContainsKey(index))
                    Mapping.Add(index, ++change);
            }
            //-------------------------------------

            for (int FeatureIndex = 1; FeatureIndex <= FeatureNum; FeatureIndex++)  //計算每個特徵的子集合資訊量
            {
                //紀錄每個特徵在各篇文章中，有出現和沒出現的次數
                double[,] Feature = new double[2, ClassDic.Keys.Count];  
 
                for (int ArticleIndex = 0; ArticleIndex < ArticleNum; ArticleIndex++)
                {
                    int Class = VSM.VSM.GetAt(ArticleIndex, FeatureIndex) > 0.0 ? 1 : 0;

                    if (ClassDic.ContainsKey(VSM.VSM.GetAt(ArticleIndex, FeatureNum + 1)))
                        Feature[Class, Mapping[(int)VSM.VSM.GetAt(ArticleIndex, FeatureNum + 1)]]++;
                }
                //----------------------------------------------

                double FeatureEntropyPart = 0.0;  //單一個特徵的資訊量的部份值
                double FeatureEntropy = 0.0;  //單一個特徵的資訊量

                //計算單一個特徵的資訊量
                for (int i = 0; i < Feature.GetLength(0); i++)
                {
                    double Dividby = 0.0;  //算熵的分母

                    for (int j = 0; j < Feature.GetLength(1); j++)  
                        Dividby += (double)Feature[i, j];

                    for (int j = 0; j < Feature.GetLength(1); j++)
                    {
                        //form.UpdateMSG("-(" + Feature[i, j].ToString() + "/" + Dividby.ToString() +") *" + "log2(" + Feature[i, j].ToString() + "/" + Dividby.ToString() +")");

                        double FeatureEntropyPartSingle;

                        if(Feature[i, j] == 0)  //資料完全分割會產生0，避免NaN所以直接給0
                            FeatureEntropyPartSingle = 0.0;
                        else
                            FeatureEntropyPartSingle = -(Feature[i, j] / Dividby) * (Math.Log(Feature[i, j], 2) - Math.Log(Dividby, 2));

                        FeatureEntropyPart += FeatureEntropyPartSingle;
                    }
                    //form.UpdateMSG("( " +  i.ToString() + " " + " ): " + FeatureEntropyPart.ToString());
                    FeatureEntropy += (Dividby / (double)ArticleNum) * FeatureEntropyPart;
                    FeatureEntropyPart = 0.0;
                }
                //form.UpdateMSG(FeatureEntropy.ToString());
                form.UpdateMSG("特徵詞: " + VSM.FeatureCollection[FeatureIndex-1].ToString() + " IG值: " + (ClassEntropy - FeatureEntropy).ToString());
                FeatureIGScore.Add(VSM.FeatureCollection[FeatureIndex-1].ToString(), (ClassEntropy - FeatureEntropy));
                //form.UpdateMSG("----------------------");
            }

            //MessageBox.Show(Path.GetDirectoryName(form.VSMPath) + "\\IGScore.csv");

            

            return FeatureIGScore;
        }
    }
}
