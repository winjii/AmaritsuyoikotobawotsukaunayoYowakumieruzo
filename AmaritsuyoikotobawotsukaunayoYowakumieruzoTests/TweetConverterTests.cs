﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            string input = "試合を終えて家路へ向かうサッカー部員達。 疲れからか、不幸にも黒塗りの高級車に追突してしまう。後輩をかばいすべての責任を負った三浦に対し、 車の主、暴力団員谷岡が言い渡した示談の条件とは・・・。";
            IEnumerable<IWord> ret = TweetConverter.ParseSentence(input);
            Assert.Fail();
        }
    }
}