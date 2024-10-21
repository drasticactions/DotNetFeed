// <copyright file="SidebarViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DA.UI.Services;
using DA.UI.Tools;
using DA.UI.ViewModels;
using DotNetFeed.Models;
using DotNetFeed.Services;

namespace DotNetFeed.ViewModels;

public class SidebarViewModel : BaseViewModel
{
    private readonly DatabaseService _databaseService;

    public SidebarViewModel(DatabaseService databaseService, IAppDispatcher dispatcher, IErrorHandler errorHandler, IAsyncCommandFactory asyncCommandFactory)
    : base(dispatcher, errorHandler, asyncCommandFactory)
    {
        this._databaseService = databaseService;
        this.InitializeAsync().FireAndForgetSafeAsync(errorHandler);
    }

    public List<SidebarItem> SidebarItems { get; private set; } = new List<SidebarItem>();

    private Task InitializeAsync() =>
        this.PerformBusyAsyncTask(async (x, y, z) =>
        {
            var folders = await this._databaseService.GetFeedFoldersAsync();
            foreach (var folder in folders)
            {
                var feedItems = await this._databaseService.GetFeedListItemsForFolderAsync(folder.Id);
                var testItem = new SidebarItem(folder.Name, feedItems.ToList());
                this.SidebarItems.Add(testItem);
            }

            this.OnPropertyChanged(nameof(SidebarItems));
        });
}

public class SidebarItem : List<FeedListItem>
{
    public string Name { get; private set; }

    public SidebarItem(string name, List<FeedListItem> animals)
        : base(animals)
    {
        Name = name;
    }
}