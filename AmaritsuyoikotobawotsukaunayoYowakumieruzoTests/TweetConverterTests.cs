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
    public class TweetConverterTests
    {
        [TestMethod()]
        public void ParseSentenceTest()
        {
            string input = "うちの庭には二羽鶏がいます。";
            List<List<IWord>> chunks;
            List<int> parentIndeces;
            TweetConverter.ParseSentence(input, out chunks, out parentIndeces);
            Assert.Fail();
        }
    }
}