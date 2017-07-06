using Microsoft.VisualStudio.TestTools.UnitTesting;
using AmaritsuyoikotobawotsukaunayoYowakumieruzo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmaritsuyoikotobawotsukaunayoYowakumieruzo.Tests
{
    [TestClass()]
    public class DistinctStringTests
    {
        /*
        [TestMethod()]
        public void DeleteTest()
        {
            DistinctString dstr = new DistinctString("0123456789");
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; i + j < 10; j++)
                {
                    string expected = "";
                    for (int k = 0; k < i; k++) expected += k.ToString();
                    for (int k = i + j; k < 10; k++) expected += k.ToString();
                    DistinctString ret = dstr.Delete(i, j);
                    Console.WriteLine(expected + " " + ret.Str);
                    Assert.AreEqual(expected, ret.Str);
                }
            }
        }
        */

        /*
        [TestMethod()]
        public void ReplaceTest()
        {
            Random rand = new Random();
            DistinctString dstr = new DistinctString("0123456789");
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; i + j < 10; j++)
                {
                    string expectedStr = "";
                    List<int> expectedIds = new List<int>();
                    for (int k = 0; k < i; k++) { expectedStr += k.ToString(); expectedIds.Add(k); }
                    IEnumerable<Char> randChars = (new string('a', rand.Next(15))).Select((c, p) => (char)(c + rand.Next(26)));
                    string replaced = string.Join("", randChars);
                    expectedStr += replaced;
                    for (int k = 0; k < replaced.Length; k++) expectedIds.Add(-1);
                    for (int k = i + j; k < 10; k++) { expectedStr += k.ToString(); expectedIds.Add(k); }
                    DistinctString ret = dstr.Replace(i, j, replaced);
                    Console.WriteLine(expectedStr + " " + ret.Str);
                    Console.WriteLine("\t" + string.Join(",", expectedIds) + "," + string.Join(" ", ret.Ids));
                    Assert.AreEqual(expectedStr, ret.Str);
                    Assert.IsTrue(expectedIds.SequenceEqual(ret.Ids));
                }
            }
        }
        */
    }
}