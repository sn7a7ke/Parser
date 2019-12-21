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
            mILoader.Setup(l => l.GetPage(mUrl.Object, null)).Returns(fHtmlDocument);

            var mIParser = new Mock<IParser<string>>();
            mIParser.SetupSet(p => p.Document = fHtmlDocument);
            mIParser.Setup(p => p.Parse()).Returns(fResult);

            Executor.Loader = mILoader.Object;
            var executor = new Executor();
            // act
            var result = executor.Process(mUrl.Object, mIParser.Object);
            // assert
            mILoader.Verify(l => l.GetPage(mUrl.Object, null));
            mIParser.Verify(p => p.Parse());
            Assert.AreEqual(fResult, result);
        }
    }
}
