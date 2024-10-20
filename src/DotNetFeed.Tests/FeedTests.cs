// <copyright file="FeedTests.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DotNetFeed.Services;

namespace DotNetFeed.Tests;

[TestClass]
public sealed class FeedTests
{
    private readonly TestAppDispatcher appDispatcher = new();
    private readonly TestErrorHandler errorHandler = new();

    /// <summary>
    /// Test the RssFeedService.
    /// </summary>
    /// <param name="rssUri">Rss Uri.</param>
    /// <returns>Task.</returns>
    [TestMethod]
    [DataRow("https://devblogs.microsoft.com/landing")]
    [DataRow("https://devblogs.microsoft.com/visualstudio/feed/")]
    [DataRow("https://code.visualstudio.com/feed.xml")]
    [DataRow("https://devblogs.microsoft.com/develop-from-the-cloud/feed/")]
    [DataRow("https://devblogs.microsoft.com/devops/feed/")]
    [DataRow("https://blogs.windows.com/windowsdeveloper/feed")]
    [DataRow("https://devblogs.microsoft.com/premier-developer/feed/")]
    [DataRow("https://devblogs.microsoft.com/ise/feed/")]
    [DataRow("https://devblogs.microsoft.com/engineering-at-microsoft/feed/")]
    [DataRow("https://devblogs.microsoft.com/azure-sdk/feed/")]
    [DataRow("https://devblogs.microsoft.com/commandline/feed/")]
    [DataRow("https://devblogs.microsoft.com/performance-diagnostics/feed/")]
    [DataRow("https://devblogs.microsoft.com/i18n/feed/")]
    [DataRow("https://devblogs.microsoft.com/react-native/feed/")]
    [DataRow("https://devblogs.microsoft.com/directx/feed/")]
    [DataRow("https://devblogs.microsoft.com/openapi/feed/")]
    [DataRow("https://devblogs.microsoft.com/surface-duo/feed/")]
    [DataRow("https://devblogs.microsoft.com/windowsai/feed/")]
    [DataRow("https://devblogs.microsoft.com/cppblog/feed/")]
    [DataRow("https://devblogs.microsoft.com/dotnet/feed/")]
    [DataRow("https://devblogs.microsoft.com/typescript/feed")]
    [DataRow("https://devblogs.microsoft.com/powershell-community/feed")]
    [DataRow("https://devblogs.microsoft.com/powershell/feed")]
    [DataRow("https://devblogs.microsoft.com/python/feed")]
    [DataRow("https://devblogs.microsoft.com/visualstudio/feed/")]
    [DataRow("https://devblogs.microsoft.com/java/feed")]
    [DataRow("https://devblogs.microsoft.com/java-ch/feed")]
    [DataRow("https://devblogs.microsoft.com/dotnet/category/dotnet-aspire/feed")]
    [DataRow("https://devblogs.microsoft.com/dotnet/category/maui/feed")]
    [DataRow("https://devblogs.microsoft.com/dotnet/category/blazor/feed")]
    [DataRow("https://devblogs.microsoft.com/dotnet/category/ai/feed")]
    [DataRow("https://devblogs.microsoft.com/dotnet/category/entity-framework/feed")]
    [DataRow("https://devblogs.microsoft.com/dotnet/category/maintenance-and-updates/feed")]
    [DataRow("https://devblogs.microsoft.com/dotnet-ch/feed")]
    [DataRow("https://devblogs.microsoft.com/ifdef-windows/feed")]
    [DataRow("https://devblogs.microsoft.com/azuregov/feed")]
    [DataRow("https://blogs.bing.com/Engineering-Blog/feed")]
    [DataRow("https://blogs.windows.com/msedgedev/feed")]
    [DataRow("https://azure.microsoft.com/blog/feed")]
    [DataRow("https://devblogs.microsoft.com/microsoft365dev/feed")]
    [DataRow("https://devblogs.microsoft.com/identity/feed")]
    [DataRow("https://devblogs.microsoft.com/oldnewthing/feed")]
    [DataRow("https://devblogs.microsoft.com/powerplatform/feed")]
    [DataRow("https://devblogs.microsoft.com/windows-music-dev/feed")]
    public async Task RssFeedServiceTest(string rssUri)
    {
        using var client = new HttpClient();
        var rssFeedService = new RssFeedService(this.errorHandler, client);
        Uri? uri = null;
        if (!Uri.TryCreate(rssUri, UriKind.Absolute, out uri))
        {
            Assert.Fail($"rssUri {rssUri} is not valid");
        }

        var (feedItem, feedItemList) = await rssFeedService.ReadFeedAsync(uri);
        Assert.IsNotNull(feedItem);
        Assert.IsNotNull(feedItemList);
        Assert.IsTrue(feedItemList.Any());
        Assert.IsTrue(!string.IsNullOrEmpty(feedItem.Name));
    }
}
