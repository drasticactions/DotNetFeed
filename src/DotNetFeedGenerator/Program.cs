// <copyright file="Program.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Text.Json;
using DotNetFeed.Models;
using DotNetFeed.Services;
using DotNetFeedGenerator;

Console.WriteLine($"DotNetFeedGenerator {AppSettings.CurrentVersion}");

var appDispatcher = new TestAppDispatcher();
var errorHandler = new TestErrorHandler();

var defaultDB = "AppDatabase.db3";

if (File.Exists(defaultDB))
{
    File.Delete(defaultDB);
}

var databaseService = new DatabaseService(defaultDB, errorHandler);

var databaseJson = await File.ReadAllTextAsync("database.json");
await databaseService.InitializeAsync();

var database = JsonSerializer.Deserialize<FeedFolderGen[]>(databaseJson);

var rssService = new RssFeedService(errorHandler);

foreach (var item in database!)
{
    if (string.IsNullOrEmpty(item.Folder))
    {
        continue;
    }

    var folder = new FeedFolder() { Name = item.Folder };
    await databaseService.UpsertFolderAsync(folder);
    foreach (var feed in item.Feeds)
    {
        var (feedItem, rssItems) = await rssService.ReadFeedAsync(new Uri(feed.FeedUrl));
        feedItem!.FolderId = folder.Id;
        await databaseService.UpsertFeedListItemAsync(feedItem);
        foreach (var rssItem in rssItems!)
        {
            rssItem.FeedListItemId = feedItem.Id;
        }

        await databaseService.UpsertFeedItemsAsync(rssItems);
    }
}

databaseService.Dispose();

class FeedFolderGen
{
    public string Folder { get; set; }

    public FeedItemGen[] Feeds { get; set; }
}

class FeedItemGen
{
    public string FeedUrl { get; set; }
}