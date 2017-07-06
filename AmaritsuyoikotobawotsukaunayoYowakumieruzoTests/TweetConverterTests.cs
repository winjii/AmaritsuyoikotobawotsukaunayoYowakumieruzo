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
            DistinctString input = new DistinctString("うちの庭には二羽鶏がいます。");
            List<List<IWord>> chunks;
            List<int> parentIndeces;
            chunks = TweetConverter.ParseSentence(input, out parentIndeces);
            Assert.Fail();
        }
    }
}