using ItemSortingTool.Services;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ItemSortingTool;

internal static class Program
{
	/// <summary>
	///     The main entry point for the application.
	/// </summary>
	[STAThread]
    private static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();

        var services = new ServiceCollection();
        services.AddLogging(builder =>
        {
            builder.ClearProviders();
            builder.AddConsole();
            builder.SetMinimumLevel(LogLevel.Information);
        });

        services.AddSingleton<ItemRepository>();
        services.AddSingleton<CsvReaderService>();
        using ServiceProvider serviceProvider = services.BuildServiceProvider();

        var mainWindow = new MainWindow(
            serviceProvider.GetService<ItemRepository>(),
            serviceProvider.GetService<ILogger<MainWindow>>(),
            serviceProvider.GetService<CsvReaderService>()
        );

        Application.Run(mainWindow);
    }
}