using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AmaritsuyoikotobawotsukaunayoYowakumieruzo
{
    public class TweetConverter
    {
        //句点や読点などは、品詞「Other」として扱われる
        public static void ParseSentence(string sentence, out List<List<IWord>> words, out List<int> parentIndeces)
        {
            throw new NotImplementedException();
            /*
            if (string.IsNullOrEmpty(sentence))
            {
                return new List<IWord>();
            }
            
            string appid = ConfigurationManager.AppSettings["yahooApiKey"];
            //string appid = ConfigurationManager.AppSettings["yahooApiKey"];
            var uri = "http://jlp.yahooapis.jp/MAService/V1/parse?appid=" + appid + "&sentence=" +
                      sentence + "&results=ma";
            var request = (HttpWebRequest)WebRequest.Create(uri);
            try
            {
                WebResponse response = request.GetResponse();
                var respStream = response.GetResponseStream();

                using (var sr = new StreamReader(respStream))
                {
                    var res = sr.ReadToEnd();
                    XmlDocument document = new XmlDocument();
                    document.LoadXml(res);
                    var root = document.DocumentElement;
                    var wordList = root["ma_result"].GetElementsByTagName("word");

                    var wordsResult = new List<IWord>();
                    for (var i = 0; i < wordList.Count; ++i)
                    {
                        var wordNode = wordList[i];
                        var word = new Word(wordNode["surface"].InnerText,
                            wordNode["reading"].InnerText, wordNode["pos"].InnerText);
                        wordsResult.Add(word);
                    }

                    return wordsResult;
                }
            }
            catch (Exception e)
            {
                return null;
            }
            */
        }
        
        public static string Convert(string tweet)
        {
            //------形態素解析を使わない処理
            //----------区切る前の処理

            //----------

            //----------区切る処理
            string[] sentences = tweet.Split(new Char[]{
                '、',    //、
                '。',    //。
                '\n',    //改行
                ',',     //,
                '.',     //.
                '　',    //全角空白
                ' '      //半角空白
            });
            //----------

            //----------区切った後の処理

            //----------

            //------

            string res = "";
            foreach (string sentence in sentences)
            {
                List<List<IWord>> parsedSentence;
                List<int> parentIndeces;
                ParseSentence(sentence, out parsedSentence, out parentIndeces);
                //------形態素解析を使う処理(処理済み文字列はresに順次追加)

                //------
                res += parsedSentence.ToString();
            }
            return res;
        }
    }
}
