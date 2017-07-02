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
        public static IEnumerable<IWord> ParseSentence(string sentence)
        {
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
        }
    }
}
