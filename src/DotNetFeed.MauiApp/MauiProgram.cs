using DA.UI.Tools;
using DotNetFeed.Services;
using Microsoft.Extensions.Logging;

namespace DotNetFeed;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "AppDatabase.db3");
		if (!File.Exists(databasePath))
		{
			var file = FileSystem.OpenAppPackageFileAsync("AppDatabase.db3").Result;
			using (var writeStream = File.Create(databasePath))
			{
				file.CopyTo(writeStream);
			}
		}
		var appDispatcher = new AppDispatcher();
		var errorHandler = new ErrorHandler();
		var databaseService = new DatabaseService(databasePath, errorHandler);
		databaseService.InitializeAsync().FireAndForgetSafeAsync(errorHandler);
		var builder = MauiApp.CreateBuilder();
		builder.Services.AddSingleton<AppDispatcher>(appDispatcher);
		builder.Services.AddSingleton<ErrorHandler>(errorHandler);
		builder.Services.AddSingleton<DatabaseService>(databaseService);
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
