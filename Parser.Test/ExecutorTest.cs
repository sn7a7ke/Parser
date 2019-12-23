using AutoFixture.NUnit3;
using HtmlAgilityPack;
using Moq;
using NUnit.Framework;
using Parser.Interfaces;

namespace Parser.Test
{
    [TestFixture]
    public class ExecutorTest
    {
        [Test]
        [Theory, AutoMoqData]
        public void RunResultIsEqual(
            string fResult,
            HtmlDocument fHtmlDocument,
            Mock<IUrl> mUrl,
            [Frozen]Mock<ILoader> mILoader,
            [Frozen]Mock<IParser<string>> mIParser,
            Executor executor)
        {
            // arrange
            mILoader.Setup(l => l.GetPage(mUrl.Object, null)).Returns(fHtmlDocument);
            mIParser.Setup(p => p.Parse()).Returns(fResult);
            Executor.Loader = mILoader.Object;

            // act
            var result = executor.Process(mUrl.Object, mIParser.Object);
            
            // assert
            mILoader.Verify(l => l.GetPage(mUrl.Object, null));
            mIParser.Verify(p => p.Parse());
            Assert.AreEqual(fResult, result);
        }
    }
}
