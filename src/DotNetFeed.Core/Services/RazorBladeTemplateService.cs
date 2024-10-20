// <copyright file="RazorBladeTemplateService.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DotNetFeed.Models;
using DotNetFeed.Templates;

namespace DotNetFeed.Services;

/// <summary>
/// Razor Blade Template Service.
/// </summary>
public class RazorBladeTemplateService
{
    private IArticleParserService? articleParser;
    private BlankTemplate blankTemplate;

    /// <summary>
    /// Initializes a new instance of the <see cref="RazorBladeTemplateService"/> class.
    /// This uses the default template HTML.
    /// </summary>
    /// <param name="articleParser">Optional Article Parser.</param>
    public RazorBladeTemplateService(IArticleParserService? articleParser = default)
    {
        this.articleParser = articleParser;
        this.blankTemplate = new BlankTemplate();
    }

    /// <inheritdoc/>
    public Task<string> RenderBlankAsync(bool darkMode)
    {
        return this.blankTemplate.RenderAsync();
    }

    /// <inheritdoc/>
    public Task<string> RenderFeedItemAsync(FeedListItem feedListItem, FeedItem item)
    {
        var feedItemTemplate = new FeedItemTemplate() { FeedItem = item, FeedListItem = feedListItem};
        return feedItemTemplate.RenderAsync();
    }
}