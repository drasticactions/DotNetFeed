// <copyright file="Program.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DA.UI.Services;
using DotNetFeed.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Dispatching;
using Microsoft.Windows.AppLifecycle;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetFeed;

/// <summary>
/// Main Program.
/// </summary>
internal class Program
{
    private const string AppKey = "7317742F-805D-4586-999A-9F971DFE1397";

    [STAThread]
    private static int Main(string[] args)
    {
        WinRT.ComWrappersSupport.InitializeComWrappers();
        bool isRedirect = DecideRedirection();
        if (!isRedirect)
        {
            Microsoft.UI.Xaml.Application.Start((p) =>
            {
                AppStart();
            });
        }

        return 0;
    }

    private static void AppStart()
    {
        var context = new DispatcherQueueSynchronizationContext(
                    DispatcherQueue.GetForCurrentThread());
        SynchronizationContext.SetSynchronizationContext(context);
        var originalDatabase = Path.Combine(Environment.CurrentDirectory, "AppDatabase.original.db3");
        var database = Path.Combine(Environment.CurrentDirectory, "AppDatabase.db3");
        if (!File.Exists(database))
        {
            if (!File.Exists(originalDatabase))
            {
                throw new FileNotFoundException("Database not found.", originalDatabase);
            }

            File.Copy(originalDatabase, database);
        }

        var services = new ServiceCollection();
        var appDispatcher = new AppDispatcher(DispatcherQueue.GetForCurrentThread());
        var errorHandler = new ErrorHandler();
        var databaseService = new DatabaseService(database, errorHandler, true);
        services.AddSingleton<IAppDispatcher>(appDispatcher);
        services.AddSingleton<IErrorHandler>(errorHandler);
        services.AddSingleton<DatabaseService>(databaseService);
        services.AddSingleton<IAsyncCommandFactory, AsyncCommandFactory>();

        var provider = services.BuildServiceProvider();
        new App(provider);
    }

    private static bool DecideRedirection()
    {
        bool isRedirect = false;
        AppActivationArguments args = AppInstance.GetCurrent().GetActivatedEventArgs();
        ExtendedActivationKind kind = args.Kind;
        AppInstance keyInstance = AppInstance.FindOrRegisterForKey(AppKey);

        if (keyInstance.IsCurrent)
        {
            keyInstance.Activated += OnActivated;
        }
        else
        {
            isRedirect = true;
            keyInstance.RedirectActivationToAsync(args).GetAwaiter().GetResult();
        }

        return isRedirect;
    }

    private static void OnActivated(object? sender, AppActivationArguments args)
    {
        ExtendedActivationKind kind = args.Kind;
    }
}