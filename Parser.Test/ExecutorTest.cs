using System;
using AutoFixture;
using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Parser.Interfaces;

namespace Parser.Test
{
    [TestClass]
    public class ExecutorTest
    {
        [TestMethod]
        public void RunResultIsEqual()
        {
            // arrange
            var fixture = new Fixture();
            var fResult = fixture.Create<string>();
            var fHtmlDocument = fixture.Create<HtmlDocument>();

            var mUrl = new Mock<IUrl>();
            var mILoader = new Mock<ILoader>();
            mILoader.Setup(l => l.GetPage(mUrl.Object)).Returns(fHtmlDocument);
            mILoader.Setup(l => l.GetPage(mUrl.Object, "some text")).Returns(fHtmlDocument);

            var mIParser = new Mock<IParser<string>>();
            mIParser.Setup(p => p.Parse(fHtmlDocument)).Returns(fResult);

            var executor = new Executor<string>(mILoader.Object, mIParser.Object);
            // act
            var result = executor.Run(mUrl.Object);
            // assert
            mILoader.Verify(l => l.GetPage(mUrl.Object));
            mIParser.Verify(p => p.Parse(fHtmlDocument));
            Assert.AreEqual(fResult, result);            
        }
    }
}
