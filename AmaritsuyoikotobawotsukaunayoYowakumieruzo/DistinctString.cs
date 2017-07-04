using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmaritsuyoikotobawotsukaunayoYowakumieruzo
{
    class DistinctString
    {
        public string Str { get; private set; }
        public List<int> Ids { get; private set; }
        public DistinctString(string str)
        {
            Str = str;
            Ids = new List<int>(str.Count());
            for (int i = 0; i < str.Count(); i++) Ids.Add(i);
        }
        public void Delete(int index, int count)
        {
            Str = Str.Substring(0, index) + Str.Substring(index + count);
            Ids = (List<int>)Enumerable.Concat(
                Ids.GetRange(0, index),
                Ids.GetRange(index + count, Ids.Count - index - count));
        }
        public void Replace(int index, int count, string target)
        {
            Str = Str.Substring(0, index) + target + Str.Substring(index + count);
            Ids = (List<int>)Enumerable.Concat(
                Ids.GetRange(0, index),
                Enumerable.Concat(
                    Enumerable.Repeat(-1, target.Count()),
                    Ids.GetRange(index + count, Ids.Count - index - count)
                ));
        }
    }
}
