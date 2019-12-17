using AutoFixture;
using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Parser.Interfaces;
using SeleniumProvider;

namespace Parser.Test
{
    [TestClass]
    public class SeleniumLoaderTest
    {
        [TestMethod]
        public void GetPageResultIsEqual()
        {
            // arrange
            var fixture = new Fixture();
            var fSource = fixture.Create<string>();
            var fHtmlDocument = fixture.Create<HtmlDocument>();
            var htmlResult = new HtmlDocument();
            htmlResult.LoadHtml(fSource);

            var mUrl = new Mock<IUrl>();
            var mIWebDriverProvider = new Mock<IWebDriverProvider>();
            mIWebDriverProvider.Setup(p => p.Source).Returns(fSource);

            var nestedProvider = new NestedProvider(mIWebDriverProvider.Object);
            // act
            var result = nestedProvider.GetPage(mUrl.Object);
            // assert
            mIWebDriverProvider.Verify(p => p.Source);
            Assert.AreEqual(htmlResult.Text, result.Text);
        }

        public class NestedProvider : SeleniumLoader
        {
            public NestedProvider(IWebDriverProvider provider) : base(provider)
            {
            }
        }
    }
}
