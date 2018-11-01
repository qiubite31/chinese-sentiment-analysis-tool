using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Windows.Forms;

namespace AndroidAnalyzer
{
    class CKIPSegmentation
    {
        public static String CKIPbyDetailPOS(String post)
        {
            //String post = "今天很開心，又找到一個可以改善的方法，真棒啊！！！哈哈，開心: )";
            post = "Submit=%B0e%A5X&query=" + post;

            Byte[] postByte = Encoding.GetEncoding("big5").GetBytes(post);  //轉成byte型態

            WebRequest Request = WebRequest.Create("http://sunlight.iis.sinica.edu.tw/cgi-bin/text.cgi"); //建立與ckip的伺服器連線
            Request.Method = "post";  //採post傳送資料
            Request.Timeout = 200000;
            Request.ContentLength = postByte.Length;

            Stream RequestStream = Request.GetRequestStream();  //建立請求串流
            RequestStream.Write(postByte, 0, postByte.Length);  //送出資料請求
            RequestStream.Close();

            Stream ResponseStream = Request.GetResponse().GetResponseStream();  //建立回應串流

            StreamReader ResponseSR = new StreamReader(ResponseStream);
            String responseURL = ResponseSR.ReadToEnd();  //讀取所有的回應串流
            //回傳格式 <HTML><HEAD><meta http-equiv=Content-Type content='text/html'; charset='UTF-8'>
            //<META HTTP-EQUIV="REFRESH" CONTENT="0.1; URL='/uwextract/pool/3815.html'"></HEAD><BODY></BODY></HTML>

            responseURL = responseURL.Substring(responseURL.IndexOf("/uwextract"), responseURL.IndexOf(".html") - responseURL.IndexOf("/uwextract"));//解析
            //解析結果為→uwextract/pool/324
            ResponseStream.Close();

            //送出回傳字詞與詞性Tag的請求url：http://sunlight.iis.sinica.edu.tw//uwextract/show.php?id=324&type=tag

            String GetTagURL = "http://sunlight.iis.sinica.edu.tw/uwextract/show.php?id=" + responseURL.Substring(16) + "&type=tag";

            WebRequest RequestTagResult = WebRequest.Create(GetTagURL);  //與斷詞結果的tag頁面建立連線
            RequestTagResult.Timeout = 15000;
            Stream ResponseStreamTag = RequestTagResult.GetResponse().GetResponseStream();  //取得結果
            StreamReader sr = new StreamReader(ResponseStreamTag, Encoding.Default, false);  //建立資料流讀取
            String ReturnResult = sr.ReadToEnd();
            //回傳格式 html><head><meta HTTP-EQUIV="Content-Type" CONTENT="text/html; charset=big5"></head><body><pre>　好(VH)</pre></body></html>

            ReturnResult = ReturnResult.Replace("-", ""); //去除-符號
            ReturnResult = ReturnResult.Replace("\n", ""); //去除斷行符號
            ReturnResult = ReturnResult.Replace("　", " "); //將全形空白轉為半形
            ReturnResult = ReturnResult.Substring(ReturnResult.IndexOf("<pre>"), ReturnResult.IndexOf("</pre>") - ReturnResult.IndexOf("<pre>"));
            ReturnResult = ReturnResult.Replace("<pre> ", ""); //去除剩餘tag
            ReturnResult = ReturnResult.Replace("<pre>", ""); //去除剩餘tag。當文章最後一個字為"連"，出現超"神奇"的無法回傳pos情況

            return ReturnResult;
        }
        public static String CKIPbySimplePOS(String post)
        {
            return post;
        }
    }
}
