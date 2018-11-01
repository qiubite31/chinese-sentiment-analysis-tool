using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Forms;
using System.Collections;
using System.Data;

namespace AndroidAnalyzer
{
    class ChineseWordNet
    {
        private XmlDocument LMFFile;
        private Form1 form;

        public ChineseWordNet(String LMFPath)
        {
            SetLMF(LMFPath);
        }
        private void SetLMF(String LMFPath)
        {
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.Load(LMFPath);
            LMFFile = XmlDoc;
        }
        public void SetForm(Form1 form)
        {
            this.form = form;
        }
        public Dictionary<String,String> SearchTerm(Dictionary<String,String> SeedTerm)
        {
            Dictionary<String, String> SenseidRelation = new Dictionary<string, string>();  //建立一個要回傳且包含所有senseid的dictionary

            foreach (KeyValuePair<String, String> term in SeedTerm.ToArray())
            {
                Dictionary<String, String> SenseidTmp = new Dictionary<string, string>();  //建立一個只包含單一個字詞之相關senseid的dictionary

                SenseidTmp = SearchLexicalEntryNode(term, SenseidTmp); //先搜尋LexicalEntry階層的節點，找出該詞的所有詞義

                //if (SenseidTmp.Count > 0)  //避免該字詞沒有沿伸詞義
                    SenseidTmp = SearchSynsetEntryNode(SenseidTmp);  //再將從LexicalEntry找到的所有詞義，再去找與該詞義有相反相關的其他詞義

                foreach (KeyValuePair<String, String> senseid in SenseidTmp)  //將每個詞的senseid加入到要回傳的senseidList
                {
                    if(!SenseidRelation.ContainsKey(senseid.Key))  //如果要回傳所有senseid集合裡沒有存在該senseid，則直接加入
                        SenseidRelation.Add(senseid.Key, senseid.Value);
                    else  //如果要回傳所有senseid集合裡有存在該senseid，則判斷詞義傾向是否相反
                    {
                        String ExistSenseValue = SenseidRelation[senseid.Key];  //取出已存在的senseid之相同義或相反義
                        String ExistDirection = ExistSenseValue.Substring(ExistSenseValue.IndexOf("synonym")) == "synonym" ? "synonym" : "antonym";
                        String NewSenseValue = senseid.Value;     //取出新的senseid之相同義或相反義
                        String NewDirection = NewSenseValue.Substring(NewSenseValue.IndexOf("synonym")) == "synonym" ? "synonym" : "antonym";

                        if (ExistDirection != NewDirection)  //判斷新的與已存在的senseid是否具有相同的同義/反義關係
                            form.UpdateMSG("詞義出現衝突: " + senseid.Key + " " + senseid.Value); 
                        //MessageBox.Show("詞義出現衝突: " + senseid.Key + " " + senseid.Value);
                    }

                    
                    

                }
            }
            
            return SenseidRelation;
        }
        private Dictionary<String,String> SearchLexicalEntryNode(KeyValuePair<String,String> Term, Dictionary<String,String> SenseidCollection)
        {
            //查WordNet中，字詞包含多少個不同詞義，並取出所有詞義的CWN_id
            XmlNodeList LexicalEntryNodeLists = LMFFile.SelectNodes("LexicalResource/Lexicon/LexicalEntry"); //取得LexicalEntry階層的結點

            //取出WordNet中包含的指定詞
            foreach (XmlNode Node in LexicalEntryNodeLists) //查詢LexicalEntry中每一個節點
            {
                XmlNode LemmaNode = Node.ChildNodes[0]; //取得Lemma節點
                XmlNode SenseNode = Node.ChildNodes[1]; //取得Sense節點

                if (LemmaNode.Attributes.GetNamedItem("writtenForm").Value == Term.Key) //判斷Lemma節點中，是否有要查詢的字
                {
                    //MessageBox.Show(SenseNode.Attributes.GetNamedItem("id").Value + " " + SenseNode.Attributes.GetNamedItem("synset").Value);
                    String syset = SenseNode.Attributes.GetNamedItem("synset").Value;

                    String pos = syset.Substring(16);
                    if (pos != "v" && pos != "a")  //如果該詞義並非動詞或是形容詞，則不儲存
                        continue;

                    if (Term.Value.Contains("Seed") || Term.Value.Contains("Vi") || Term.Value.Contains("Vt"))
                    {
                        if (!SenseidCollection.ContainsKey(syset.Substring(7, 8)))
                            SenseidCollection.Add(syset.Substring(7, 8), Term.Key + "_" + Term.Value + "_" + "synonym");  //種子詞的Value為PosSeed與NegSeed，而這些詞所找到的詞義皆加上同義synonym，以便後續判斷
                        else
                        {
                            form.UpdateMSG("詞義出現重複:　" + syset.Substring(7, 8) + " " + Term.Key + "_" + Term.Value + "_" + "synonym");
                            //MessageBox.Show("詞義出現重複:　" + syset.Substring(7, 8) + " " + Term.Key + "_" + Term.Value + "_" + "synonym");
                        }

                    }
                }

            }
            return SenseidCollection;
        }
        private Dictionary<String, String> SearchSynsetEntryNode(Dictionary<String, String> SenseidRelationDic)
        {
            //XML Format
            //LexcalEntry---Lemma、Sense
            //Synset     ---Definition、SynsetRelation、MonolingualExternalRefs

            //Function Task:
            //將從LexcalEntry的Sense中，找到的所有CWN_id，對應到Synset，並看SynsetRelation有沒有與之相關的詞義，有則全抓出來

            //---------將從LexcalEntry的Sense中，找到的所有CWN_id放進CWNid
            int count = 0;
            String[] CWNid = new String[SenseidRelationDic.Count];
            foreach (KeyValuePair<String, String> term in SenseidRelationDic)
            {
                CWNid[count] = term.Key;
                count++;
            }
            //----------------------------------

            XmlNodeList SynsetEntryNodeLists = LMFFile.SelectNodes("LexicalResource/Lexicon/Synset");

            int idIndex = 0;

            for (int i = 0; i < CWNid.Length; i++)  //在Synse的節點中找到所有CWNid的關係詞義
            {
                foreach (XmlNode Node in SynsetEntryNodeLists) //找到指定的CWNid的Synset節點
                {
                    XmlNode DefNode = Node.FirstChild; //取出詞義定義Definition節點
                    XmlNodeList RelasNode;

                    if (Node.Attributes.GetNamedItem("id").Value == "zho-16-" + CWNid[i] + "-v") //如果CWN_id等於該Synset節點，接著尋找其相關的衍生詞義
                    {
                        if (Node.ChildNodes.Count != 1) //若該詞義只有包含定義，沒有與其他詞彙的關係，則跳過，有則取出SyssetRelsation節點
                            RelasNode = Node.ChildNodes[1].SelectNodes("SynsetRelation");
                        else
                            break;

                        foreach (XmlNode subNode in RelasNode)  //抓出所有不重複的詞義
                        {
                            String synset = subNode.Attributes.GetNamedItem("target").Value;  //取出目標CWN_id
                            String relType = subNode.Attributes.GetNamedItem("relType").Value; //取出目標詞義與原詞義的關係

                            String pos = synset.Substring(16);  //取出詞義的詞性
                            String rel = relType.Substring(4);
                            if (pos != "v" && pos != "a")  //如果該詞義並非動詞或是形容詞，則不儲存
                                continue;
                            if (rel != "antonym" && rel != "synonym")  //如果該詞義不是原詞義的相同義或相反義則不儲存
                                continue;

                            if (!SenseidRelationDic.ContainsKey(synset.Substring(7, 8)))  //如果已有這個詞義則不再加入
                                SenseidRelationDic.Add(synset.Substring(7, 8), CWNid[idIndex] + "_" + synset.Substring(7, 8) + "_" + relType);
                            else
                            {
                                if (!SenseidRelationDic[synset.Substring(7, 8)].Contains(rel))  //判斷是如有相反詞義的衝突
                                    form.UpdateMSG("詞義衝突於Senseid: " + synset.Substring(7, 8) + "目前為" + rel + "與已存在相反");
                                    //MessageBox.Show("詞義衝突於Senseid: " + synset.Substring(7, 8) + "目前為" + rel + "與已存在相反" );
                            }
                                
                        }
                        break;  //找到目標詞義且存完所有的相關詞義則直接離開
                    }
                }
            }

            return SenseidRelationDic;
        }
        public DataSet QueryCWN(String senseid, String target)
        {
            String QueryTarget = "cwn_" + target;  //指定要查詢的資料表 (cwn_synonym或是cwn_antonym)

            DataSet TermData = Form1.CWNOperate.SelectQuery("SELECT * FROM " + QueryTarget + " WHERE cwn_id = '" + senseid + "'", QueryTarget);

            return TermData;
        }

    }
}
