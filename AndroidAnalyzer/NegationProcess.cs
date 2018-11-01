using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace AndroidAnalyzer
{
    class NegationProcess
    {
        private String[] NegationList;
        
        public NegationProcess(String NegationPath)
        {
            NegationList = ReadFile.ReadOneRowCSV(NegationPath);

        }
        public void NegationDetect(ArticleModel ArticleCollection, int interval,  Form1 form)
        { 
            int count = 0;

            foreach (Article article in ArticleCollection.GetArticleList())  //讀取ArticleCollection裡每一篇文章
            {
                form.UpdateStatus("正在處理負向詞...第(" + (count + 1) + "/" + ArticleCollection.Count + ")筆資料");
                count++;

                Term[] term = article.GetTermArray();  //讀取存所有字的陣列
                ArrayList NegationLocation = new ArrayList();  //用ArrayList來儲存一篇文章內，所有出現否定詞的位置

                for (int i = 0; i < article.GetTermArray().Length; i++)  //讀取每篇文章的字
                {
                    for (int j = 0; j < NegationList.Length; j++)  //將文章中每一個詞，和否定詞集作比較
                    {
                        String ConparWord = term[i].GetWord();  
                        String NegationWord = NegationList[j];

                        if (CheckNegation(ConparWord, NegationWord))  //判定文章中的詞是不是否定詞
                        {
                            NegationLocation.Add(i);  //如果是否定詞，就將該出現否定詞的位置加入ArrayList記錄
                            break;
                        }
                    }
                }
                if (NegationLocation.Count != 0)  //判斷確定有找到否定詞
                {
                    if(interval == 0)
                        VerbDetectForward(article, NegationLocation);  //將有偵測到否定詞的文章和存否定詞位置的ArrayList作處理
                    else
                        VerbDetectInterval(article, NegationLocation, interval);
                }
            }            
        }
        public Boolean CheckNegation(String ConparWord, String NegationWord)
        {
            if (ConparWord == NegationWord)
            {
                return true;
            }
            else
                return false;
        }
        public void VerbDetectForward(Article article, ArrayList NegationLocation)
        {
            Term[] term = article.GetTermArray();

            for (int i = 0; i < term.Length; i++)  //偵測文章中的動詞位置
            {
                int NegationCount = 0;  //記錄動詞前修飾的否定詞數量

                if((term[i].GetSpeech() == "Vi") || (term[i].GetSpeech() == "Vt")) //判斷詞性是否為動詞
                {
                    for (int j = i-1; j >= 0; j--) //從動詞往前找有多少個否定詞
                    {
                        if (NegationLocation.Contains(j))
                            NegationCount++;  //如該位置有出現在儲存否定詞位置的ArrayList中，則修飾否定詞數量+1
                        else
                            break;
                    }
                    if (NegationCount % 2 != 0)  //判斷否定詞是奇數還是偶數
                    {
                        term[i] = new Term(term[i].GetWord() + "_NOT", term[i].GetSpeech());  //當否定詞是奇數時，將動詞加上_NOT  
                    }
                }
            }
            //將更新後的資料update到資料庫裡
            //Form1.AccessOperate.UpdateQuery("UPDATE AppComment SET ContentCKIP = \'" + article.GetCkipContent() + "\' WHERE CommentId = " + article.GetArticleID());
        }
        public void VerbDetectInterval(Article article, ArrayList NegationLocation, int interval)
        {
            Term[] term = article.GetTermArray();

            for (int i = 0; i < term.Length; i++)  //偵測文章中的動詞位置
            {
                int NegationCount = 0;  //記錄區間內的否定詞數量
                int ForNegCount = 0;  //前區間內的否定詞數量
                int BakNegCount = 0;  //後區間內的否定詞數量

                if ((term[i].GetSpeech() == "Vi") || (term[i].GetSpeech() == "Vt")) //判斷詞性是否為動詞
                {
                    //NegationCount = CountNegInterval(term, i, NegationLocation, i - interval, i + interval);
                    int interval_H = i + interval < term.Length ? i + interval : term.Length - 1;
                    int interval_L = i - interval > 0 ? i - interval : 0;


                    for (int j = i + 1; j <= interval_H; j++)  //向前區間找尋
                    {
                        if (NegationLocation.Contains(j))
                        {
                            NegationCount++;
                            BakNegCount++;
                        }
                    }
                    for(int j = i - 1; j >= interval_L; j--)  //向後區間找尋
                    {
                        if (NegationLocation.Contains(j))
                        {
                            NegationCount++;
                            ForNegCount++;
                        }
                    }
                    /*
                    if (BakNegCount == 1 && ForNegCount == 1)
                        NegationCount = NegationCount;

                    if (NegationCount > 2)
                        NegationCount = NegationCount;
                    */

                    //判斷前後區間的負向詞為奇數或是偶數
                    Boolean ForNegCheck = ForNegCount % 2 != 0 ? false : true; 
                    Boolean BakNegCheck = BakNegCount % 2 != 0 ? false : true;

                    if(!ForNegCheck || !BakNegCheck)
                        term[i] = new Term(term[i].GetWord() + "_NOT", term[i].GetSpeech());  //當否定詞是奇數時，將動詞加上_NOT  

                    //if (NegationCount % 2 != 0)  //判斷否定詞是奇數還是偶數
                    //{
                      //  term[i] = new Term(term[i].GetWord() + "_NOT", term[i].GetSpeech());  //當否定詞是奇數時，將動詞加上_NOT  
                    //}

                }


            }
        }
    }
}
