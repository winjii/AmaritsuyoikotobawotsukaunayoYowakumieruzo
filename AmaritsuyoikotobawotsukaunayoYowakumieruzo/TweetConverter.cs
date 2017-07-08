using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;
using System.Configuration;
namespace AmaritsuyoikotobawotsukaunayoYowakumieruzo
{
    public class TweetConverter
    {
        static string[] source =
        {
            "かもしれない",
            "かもしれません",
            "と思う",
            "と思った",
            "と思いました",
            "良い",
            "いい",
            "あまり",
            "個人的",
            "気がする",
            "感じがする",
            "そう",
            "欲しい",
            "なのでは",
            "うぃーん",
            "分からない",
            "つらい",
            "死にたい",
            "づらい",
            "終わり",
            "だろう"
        };
        static string[] target =
        {
            "だ",
            "です",
            "",
            "",
            "",
            "絶対良い",
            "絶対いい",
            "",
            "絶対的",
            "事実がある",
            "事実がある",
            "る",
            "なければならない",
            "なのだ",
            "うぃーんﾋﾞｰﾄﾋﾞｰﾄひるどwwwwwwうっくっくwwwwwwえいえいえt(←いずらいt)いえいwwwwらて",
            "完全に理解できた",
            "世の中が最悪だ",
            "殺す",
            "やすい",
            "らてまるたに奴隷にされました",
            "だ"
        };

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

        public static DistinctString Convert(DistinctString tweet)
        {
            //------形態素解析を使わない処理

            {
                //----------区切る前の処理
                //文字列の追加 -> rebuilder.ReserveAddition()
                //文字列の削除 -> rebuilder.ReserveDeletion()
                StringRebuilder rebuilder = new StringRebuilder(tweet);

                //TODO: >>>>>>>>>>>>>>>お願いまるた<<<<<<<<<<<<<<<<<<<

                tweet = rebuilder.Rebuild();
            }

            //----------区切る処理
            DistinctString[] sentences = tweet.Split(new Char[]{
                '、',    //、
                '。',    //。
                '\n',    //改行
                ',',     //,
                '.',     //.
                '　',    //全角空白
                ' ' ,    //半角空白
                '?',
                '？',
                '!',
                '！',
                '「',
                '」'
            });
            //----------

            {
                //----------区切った後の処理
                for (int i = 0; i < sentences.Length; i++)
                {
                    StringRebuilder rebuilder = new StringRebuilder(sentences[i]);

                    //TODO: >>>>>>>>>>>>>>>お願いまるた<<<<<<<<<<<<<<<<<<<
                    
                    for (int j = 0; j < sentences[i].Str.Length; j++)
                    {
                        for (int b = 0; b < source.Length; b++)
                        {
                            if (source[b].Length + j <= sentences[i].Str.Length && sentences[i].Str.Substring(j, source[b].Length) == source[b])
                            {
                                rebuilder.ReserveDeletion(j, source[b].Length);
                                rebuilder.ReserveAddition(j, target[b]);
                            }
                        }
                    }

                    for (int j = 1; j < sentences[i].Str.Length; j++)
                    {
                        //5000兆円欲しい
                        if (sentences[i].Str[j] == '円')
                        {
                            int index = j - 1;
                            for (int k = j - 1; k >= 0; k--)
                            {
                                if (!Util.IsHalfByRegex(sentences[i].Str[k])) break;
                                index = k;
                            }
                            rebuilder.ReserveDeletion(index, j - index);
                            rebuilder.ReserveAddition(index, "5000兆");
                        }
                    }

                    sentences[i] = rebuilder.Rebuild();
                }

            }

            //------

            DistinctString res = new DistinctString("");
            foreach (DistinctString sentence in sentences)
            {
                List<int> parentIndeces;
                List<List<IWord>> parsedSentence = ParseSentence(sentence, out parentIndeces);
                //------形態素解析を使う処理(処理済み文字列はresに順次追加)
                //Wordの削除 -> 普通に parsedSentence:List から削除していい
                //Wordの追加 -> new Word(string)を parsedSentence 任意の場所に追加

                //TODO: >>>>>>>>>>>>>>>お願いまるた<<<<<<<<<<<<<<<<<<<
                parsedSentence = WeakClauseDeleter.Delete(parsedSentence, parentIndeces);

                //------
                foreach (List<IWord> list in parsedSentence)
                {
                    foreach (IWord s in list)
                    {
                        res.Connect(s.ToKanji());
                    }
                }
            }
            return res;
        }
    }
}
