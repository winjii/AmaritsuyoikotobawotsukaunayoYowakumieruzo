using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AmaritsuyoikotobawotsukaunayoYowakumieruzo
{
    static class Util
    {
        public static bool IsHalfByRegex(char target)
        {
            return new Regex("^[\u0020-\u007E\uFF66-\uFF9F]+$").IsMatch(target.ToString());
        }
        public static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }
    }
}
