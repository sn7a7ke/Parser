using AutoFixture;
using AutoFixture.NUnit3;
using HtmlAgilityPack;
using Moq;
using NUnit.Framework;
using Parser.Interfaces;
using SeleniumProvider;

namespace Parser.Test
{
    [TestFixture]
    public class SeleniumLoaderTest
    {
        [Test]
        [Theory, AutoMoqData]
        public void GetPageResultIsEqual(
            string fSource,
            HtmlDocument fHtmlDocument,
            HtmlDocument htmlResult,
            Mock<IUrl> mUrl,
            [Frozen]Mock<IWebDriverProvider> mIWebDriverProvider,
            NestedProvider nestedProvider)
        {
            // arrange
            htmlResult.LoadHtml(fSource);
            mIWebDriverProvider.Setup(p => p.Source).Returns(fSource);

            // act
            var result = nestedProvider.GetPage(mUrl.Object);

            // assert
            mIWebDriverProvider.Verify(p => p.GoTo(mUrl.Object.Get(), null));
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
