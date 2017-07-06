using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Configuration;

namespace AmaritsuyoikotobawotsukaunayoYowakumieruzo
{
    public class TweetConverter
    {
        //句点や読点などは、品詞「Other」として扱われる
        public static List<List<IWord>> ParseSentence(DistinctString sentence, out List<int> parentIndeces)
        {
            List<List<IWord>> chunks = new List<List<IWord>>();
            parentIndeces = new List<int>();

            if (string.IsNullOrEmpty(sentence.Str))
            {
                return chunks;
            }

            string appid = ConfigurationManager.AppSettings["yahooApiKey"];
            //string appid = ConfigurationManager.AppSettings["yahooApiKey"];
            var uri = "https://jlp.yahooapis.jp/DAService/V1/parse?appid=" + appid + "&sentence=" +
                      sentence.Str;
            var request = (HttpWebRequest)WebRequest.Create(uri);
            try
            {
                WebResponse response = request.GetResponse();
                var respStream = response.GetResponseStream();
                int cnt = 0;

                using (StreamReader sr = new StreamReader(respStream))
                {
                    string res = sr.ReadToEnd();
                    XmlDocument document = new XmlDocument();
                    document.LoadXml(res);
                    XmlElement root = document.DocumentElement;
                    XmlNodeList chunkList = root.GetElementsByTagName("Chunk");
                    for (int i = 0; i < chunkList.Count; i++)
                    {
                        parentIndeces.Add(-1);
                        chunks.Add(new List<IWord>());
                    }
                    foreach (XmlElement chunk in chunkList)
                    {
                        int id = int.Parse(chunk["Id"].InnerText);
                        parentIndeces[id] = int.Parse(chunk["Dependency"].InnerText);
                        XmlNodeList morphemList = chunk.GetElementsByTagName("Morphem");
                        foreach (XmlNode morphem in morphemList)
                        {
                            string surface = morphem["Surface"].InnerText;
                            IWord word = new Word(
                                new DistinctString(surface, sentence.Ids.GetRange(cnt, surface.Length)),
                                morphem["Reading"].InnerText,
                                morphem["POS"].InnerText);
                            cnt += surface.Length;
                            chunks[id].Add(word);
                        }
                    }
                    return chunks;
                }
            }
            catch (Exception e)
            {
                return new List<List<IWord>>();
            }

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
        
        public static DistinctString Convert(string tweet)
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

            DistinctString res = new DistinctString("");
            foreach (string sentence in sentences)
            {
                List<int> parentIndeces;
                List<List<IWord>> parsedSentence = ParseSentence(new DistinctString(sentence), out parentIndeces);
                //------形態素解析を使う処理(処理済み文字列はresに順次追加)

                //------
                foreach (IWord s in parsedSentence)
                {
                    res.Connect(s.ToKanji());
                }
            }
            return res;
        }
    }
}
