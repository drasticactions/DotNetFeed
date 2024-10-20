// <copyright file="TestErrorHandler.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DA.UI.Services;

namespace DotNetFeedGenerator;

/// <summary>
/// Test Error Handler.
/// </summary>
public class TestErrorHandler : IErrorHandler
{
    /// <inheritdoc/>
    public void HandleError(Exception ex)
    {
        throw ex;
    }
}