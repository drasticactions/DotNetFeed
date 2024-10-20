// <copyright file="FeedListItemExtensions.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Web;
using DotNetFeed.Models;

namespace DotNetFeed.Tools;

/// <summary>
/// FeedListItem Extensions.
/// </summary>
public static class FeedListItemExtensions
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FeedListItem"/> class.
    /// </summary>
    /// <param name="feed"><see cref="Sagara.FeedReader.Feed"/>.</param>
    /// <param name="feedUri">Original Feed Uri.</param>
    /// <returns><see cref="FeedListItem"/>.</returns>
    public static FeedListItem ToFeedListItem(this Sagara.FeedReader.Feed feed, string feedUri)
    {
        return new FeedListItem()
        {
            Name = feed.Title,
            Uri = new Uri(feedUri),
            Link = feed.Link,
            ImageUri = string.IsNullOrEmpty(feed.ImageUrl) ? null : new Uri(feed.ImageUrl),
            Description = feed.Description,
            Language = feed.Language,
            LastUpdatedDate = feed.LastUpdatedDate,
            LastUpdatedDateString = feed.LastUpdatedDateString,
            FeedType = Models.FeedType.Rss,
        };
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FeedItem"/> class.
    /// </summary>
    /// <param name="item"><see cref="Sagara.FeedReader.FeedReader.FeedItem"/>.</param>
    /// <param name="feedListItem"><see cref="FeedListItem"/>.</param>
    /// <param name="imageUrl">Image Url.</param>
    /// <returns><see cref="Core.FeedItem"/>.</returns>
    public static FeedItem ToFeedItem(this Sagara.FeedReader.FeedItem item, FeedListItem feedListItem, string? imageUrl = "")
    {
        return new FeedItem()
        {
            RssId = item.Id,
            FeedListItemId = feedListItem.Id,
            Title = item.Title,
            Link = item.Link,
            Description = item.Description,
            PublishingDate = item.PublishingDate,
            Author = item.Author,
            Content = item.Content,
            ImageUrl = imageUrl,
        };
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FeedListItem"/> class.
    /// </summary>
    /// <param name="feed"><see cref="Sagara.FeedReader.Feed"/>.</param>
    /// <param name="oldItem">Original Feed Uri.</param>
    /// <returns><see cref="FeedListItem"/>.</returns>
    public static FeedListItem Update(this Sagara.FeedReader.Feed feed, FeedListItem oldItem)
    {
        oldItem.Name = feed.Title;
        oldItem.Link = feed.Link;
        oldItem.ImageUri = string.IsNullOrEmpty(feed.ImageUrl) ? null : new Uri(feed.ImageUrl);
        oldItem.Description = feed.Description;
        oldItem.Language = feed.Language;
        oldItem.LastUpdatedDate = feed.LastUpdatedDate;
        oldItem.FeedType = FeedType.Rss;
        return oldItem;
    }

    private static async Task<byte[]?> GetFaviconFromUriAsync(this HttpClient client, string uri)
    => await GetByteArrayAsync(client, new Uri(uri));

    private static async Task<byte[]?> GetFaviconFromUriAsync(this HttpClient client, Uri uri)
        => await GetByteArrayAsync(client, new Uri($"{uri.Scheme}://{uri.Host}/favicon.ico"));

    private static async Task<byte[]?> GetByteArrayAsync(this HttpClient client, Uri uri)
    {
        using HttpResponseMessage response = await client.GetAsync(uri);

        if (!response.IsSuccessStatusCode)
        {
            throw new ArgumentException("Could not get image");
        }

        return await response.Content.ReadAsByteArrayAsync();
    }
}