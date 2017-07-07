using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AmaritsuyoikotobawotsukaunayoYowakumieruzo
{
    class WeakClauseDeleter
    {

        //文節の品詞を判定
        private static Hinshi DetermineHinshi(List<IWord> clause)
        {
            foreach (IWord w in clause)
            {
                switch (w.GetHinshi().ToString())
                {
                    case "Doshi":
                        return Hinshi.Doshi;

                    case "Meishi":
                        return Hinshi.Meishi;

                    case "Keiyoushi":
                        return Hinshi.Keiyodoshi;
                }
            }
            return Hinshi.Other;
        }

        private static bool IsSubject(List<IWord> clause)
        {
            string[] keywords = new string[]
            {
                "は",
                "が",
            };

            foreach (IWord s in clause)
            {
                if (keywords.Contains(s.ToKanji().Str)) return true;
            }
            return false;
        }

        private static bool IsMeishiOnly(List<IWord> clause)
        {
            foreach (IWord s in clause)
            {
                if (s.GetHinshi() != Hinshi.Meishi && s.GetHinshi() != Hinshi.Other) return false;
            }
            return true;
        }


        private static bool IsNG(List<IWord> clause)
        {
            /*
            StreamReader sr = new StreamReader("NGWord.txt");
            string[] banList_ = sr.ReadToEnd().Split('\n');
            string[] banList = new string[banList_.Length - 1];
            Array.Copy(banList_, 0, banList, 0, banList.Length);

            for (int i = 0; i < banList.Length; i++)
            {
                banList[i] = banList[i].Substring(0, banList[i].Length - 1);
            }


            sr.Close();

            foreach (IWord s in clause)
            {

                if (banList.Contains(s.ToKanji().Str)) return true;
            }
            */
            return false;
        }


        private static void dfs(int idx, List<List<int>> tree, bool[] isDeleted)
        {
            isDeleted[idx] = true;
            foreach (int nextIdx in tree[idx])
            {
                dfs(nextIdx, tree, isDeleted);
            }
        }


        private static void DeleteInvolvedClause(int idx, List<List<int>> tree, bool[] isDeleted)
        {
            foreach (int nextIdx in tree[idx]) dfs(nextIdx, tree, isDeleted);
        }

        public static List<List<IWord>> Delete(List<List<IWord>> parsedSentence, List<int> parentIndeces)
        {
            /*
            foreach(List<IWord>list in parsedSentence)
            {
                string str = "";
                foreach(IWord s in list)
                {
                    str += s.ToKanji().Str;
                }
                System.Windows.MessageBox.Show(str + " " + DetermineHinshi(list).ToString());
            }*/

            List<List<int>> tree = new List<List<int>>();
            for (int i = 0; i < parsedSentence.Count(); i++) tree.Add(new List<int>());


            for (int i = 0; i < parentIndeces.Count(); i++)
            {
                if (parentIndeces[i] != -1)
                {
                    tree[parentIndeces[i]].Add(i);
                }
            }


            bool[] isDeleted = new bool[parsedSentence.Count()];


            //>>>ここで不要な文節を消す

            for (int i = 0; i < parsedSentence.Count(); i++)
            {
                if (IsSubject(parsedSentence[i]) || IsMeishiOnly(parsedSentence[i]))
                {
                    DeleteInvolvedClause(i, tree, isDeleted);
                }
            }


            for (int i = 0; i < parsedSentence.Count(); i++)
            {
                if (IsNG(parsedSentence[i]))
                {
                    MessageBox.Show("" + i);
                    dfs(i, tree, isDeleted);
                }
            }
            //<<<



            List<List<IWord>> res = new List<List<IWord>>();
            for (int i = 0; i < parsedSentence.Count(); i++)
            {
                if (!isDeleted[i])
                {
                    res.Add(parsedSentence[i]);
                }
            }

            return res;
        }
    }
}
