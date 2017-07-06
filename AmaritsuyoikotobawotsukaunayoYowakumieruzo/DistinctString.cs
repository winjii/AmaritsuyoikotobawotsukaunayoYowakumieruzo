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
        public DistinctString(string str, IEnumerable<int> ids)
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
        public DistinctString[] Split(char[] separator)
        {
            string[] strings = Str.Split(separator);
            DistinctString[] res = new DistinctString[strings.Length];
            int cnt = 0;
            for (int i = 0; i < strings.Length; i++)
            {
                res[i] = new DistinctString(strings[i], Ids.GetRange(cnt, strings[i].Length));
                cnt += strings[i].Length;
            }
            return res;
        }
        public void Connect(DistinctString ds)
        {
            Str += ds.Str;
            Ids.AddRange(ds.Ids);
        }
    }
}
