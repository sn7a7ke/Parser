using AutoFixture;
using AutoFixture.NUnit3;
using HtmlAgilityPack;
using Moq;
using NUnit.Framework;
using OpenQA.Selenium;
using Parser.Interfaces;

namespace Parser.Test
{
    public class SeleniumLoaderTest
    {
        [Theory, AutoMoqData]
        public void GetPageResultIsEqual(
            string fSource,
            HtmlDocument fHtmlDocument,
            HtmlDocument htmlResult,
            [Frozen]Mock<IUrl> mUrl,
            [Frozen]Mock<IWebDriver> mIWebDriver)
        {
            //arrange
            htmlResult.LoadHtml(fSource);
            mIWebDriver.Setup(p => p.PageSource).Returns(fSource);
            var fixture = new Fixture();
            fixture.Register(() => new SeleniumLoader(mIWebDriver.Object));
            var provider = fixture.Create<SeleniumLoader>();

            // act
            provider.GetPage(mUrl.Object);
            var result = provider.Document;

            // assert
            mIWebDriver.VerifyGet(p => p.PageSource);
            Assert.AreEqual(htmlResult.Text, result.Text);
        }
    }
}
