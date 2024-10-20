// <copyright file="AppDispatcher.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DA.UI.Services;

namespace DotNetFeed.Services;

/// <summary>
/// App Dispatcher.
/// </summary>
public class AppDispatcher : IAppDispatcher
{
    /// <inheritdoc/>
    public bool Dispatch(Action action)
    {
        return Microsoft.Maui.Controls.Application.Current!.Dispatcher.Dispatch(action);
    }
}