using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmaritsuyoikotobawotsukaunayoYowakumieruzo
{
    class StringRebuilder
    {
        public DistinctString DistinctStr { get; }
        private bool[] isDeleted;
        private List<string>[] addends;
        public StringRebuilder(DistinctString ds)
        {
            DistinctStr = ds;
            isDeleted = new bool[ds.Str.Length];
            addends = new List<string>[ds.Str.Length + 1];
            for (int i = 0; i < ds.Str.Length + 1; i++)
            {
                addends[i] = new List<string>();
            }
        }
        public void ReserveDeletion(int index, int count)
        {
            for (int i = 0; i < count; i++)
            {
                isDeleted[index + i] = true;
            }
        }
        public void ReserveAddition(int index, string addend)
        {
            addends[index].Add(addend);
        }
        public DistinctString Rebuild()
        {
            List<char> res = new List<char>();
            List<int> originalIndex = new List<int>();
            originalIndex = new List<int>();
            for (int i = 0; i <= DistinctStr.Str.Length; i++)
            {
                addends[i].Reverse();
                foreach (string addend in addends[i])
                {
                    foreach (char c in addend)
                    {
                        originalIndex.Add(-1);
                        res.Add(c);
                    }
                }
                if (i == DistinctStr.Str.Length || isDeleted[i]) continue;
                originalIndex.Add(i);
            }
            return new DistinctString(string.Join("", res), originalIndex);
        }
    }
}
