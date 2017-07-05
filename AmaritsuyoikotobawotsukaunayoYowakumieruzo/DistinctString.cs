using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmaritsuyoikotobawotsukaunayoYowakumieruzo
{
    public class DistinctString
    {
        public string Str { get; private set; }
        public List<int> Ids { get; private set; }
        private DistinctString(string str, IEnumerable<int> ids)
        {
            Str = str;
            Ids = ids.ToList();
        }
        public DistinctString(string str)
        {
            Str = str;
            Ids = new List<int>(str.Count());
            for (int i = 0; i < str.Count(); i++) Ids.Add(i);
        }
        public DistinctString Delete(int index, int count)
        {
            string str = Str.Substring(0, index) + Str.Substring(index + count);
            List<int> ids = Enumerable.Concat(
                Ids.GetRange(0, index),
                Ids.GetRange(index + count, Ids.Count - index - count)
                ).ToList();
            return new DistinctString(str, ids);
        }
        public DistinctString Replace(int index, int count, string replaced)
        {
            string str = Str.Substring(0, index) + replaced + Str.Substring(index + count);
            List<int> ids = Enumerable.Concat(
                Ids.GetRange(0, index),
                Enumerable.Concat(
                    Enumerable.Repeat(-1, replaced.Count()),
                    Ids.GetRange(index + count, Ids.Count - index - count)
                )).ToList();
            return new DistinctString(str, ids);
        }
    }
}
