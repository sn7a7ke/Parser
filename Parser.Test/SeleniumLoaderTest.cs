﻿using AutoFixture;
using AutoFixture.NUnit3;
using HtmlAgilityPack;
using Moq;
using NUnit.Framework;
using Parser.Interfaces;
using SeleniumProvider;

namespace Parser.Test
{
    public class SeleniumLoaderTest
    {
        [Theory, AutoMoqData]
        public void GetPageResultIsEqual(
            string fSource,
            HtmlDocument fHtmlDocument,
            HtmlDocument htmlResult,
            Mock<IUrl> mUrl,
            [Frozen]Mock<IWebDriverProvider> mIWebDriverProvider)
        {
            // arrange
            htmlResult.LoadHtml(fSource);
            mIWebDriverProvider.Setup(p => p.Source).Returns(fSource);
            var fixture = new Fixture();
            fixture.Register(() => new SeleniumLoader(mIWebDriverProvider.Object));
            var provider = fixture.Create<SeleniumLoader>();

            // act
            var result = provider.GetPage(mUrl.Object);

            // assert
            mIWebDriverProvider.Verify(p => p.GoTo(mUrl.Object.Get(), null));
            mIWebDriverProvider.Verify(p => p.Source);
            Assert.AreEqual(htmlResult.Text, result.Text);
        }
    }
}
