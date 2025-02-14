// <copyright file="RssCacheFeedUpdate.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace DotNetFeed.Models;

/// <summary>
/// Rss Cache Feed Update.
/// </summary>
public class RssCacheFeedUpdate
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RssCacheFeedUpdate"/> class.
    /// </summary>
    /// <param name="feedsCompleted">Number of Feeds Completed.</param>
    /// <param name="totalFeeds">Total Feeds.</param>
    /// <param name="lastUpdated">Last Feed Updated.</param>
    public RssCacheFeedUpdate(int feedsCompleted, int totalFeeds, FeedListItem? lastUpdated = default)
    {
        this.LastUpdated = lastUpdated;
        this.TotalFeeds = totalFeeds;
        this.FeedsCompleted = feedsCompleted;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RssCacheFeedUpdate"/> class.
    /// </summary>
    public RssCacheFeedUpdate()
    {
        this.FireRefresh = true;
    }

    /// <summary>
    /// Gets the last feed update.
    /// </summary>
    public FeedListItem? LastUpdated { get; }

    /// <summary>
    /// Gets the total number of feeds.
    /// </summary>
    public int TotalFeeds { get; }

    /// <summary>
    /// Gets the number of feeds completed.
    /// </summary>
    public int FeedsCompleted { get; }

    /// <summary>
    /// Gets a value indicating whether the update is done.
    /// </summary>
    public bool IsDone => this.FeedsCompleted >= this.TotalFeeds;

    /// <summary>
    /// Gets a value indicating whether to fire a refresh.
    /// </summary>
    public bool FireRefresh { get; }
}