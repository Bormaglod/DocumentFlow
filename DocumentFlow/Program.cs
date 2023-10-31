using DocumentFlow.Controls;
using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Interfaces.Repository;
using DocumentFlow.Data.Tools;
using DocumentFlow.Dialogs.Interfaces;
using DocumentFlow.Ghostscript.API;
using DocumentFlow.Interfaces;
using DocumentFlow.ReportEngine;
using DocumentFlow.Settings;
using DocumentFlow.Settings.Authentification;
using DocumentFlow.Tools;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Minio;

using NLog.Extensions.Logging;

using System.IO;

namespace DocumentFlow;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        try
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(File.ReadLines("license.txt").First());
        }
        catch (FileNotFoundException e)
        {
            MessageBox.Show($"не найден файл {e.FileName}. Выполнение программы невозможно.");
            return;
        }

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        Dapper.SqlMapper.AddTypeHandler(new DapperSqlDateOnlyTypeHandler());
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

        FastReport.Utils.RegisteredObjects.AddConnection(typeof(FastReport.Data.PostgresDataConnection));

        ApplicationConfiguration.Initialize();
        GhostScript.Initialize();
        ToastOperations.OnActivated();

        var localSettings = Path.Combine(
#if !DEBUG
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "Автоком",
            "settings",
#endif
            "appsettings.local.json"
        );

        var browserSettings = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "Автоком",
            "settings",
            "browsers"
        );

        var host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((context, builder) =>
            {
                builder.AddJsonFile("appsettings.auth.json");
                builder.AddJsonFile(localSettings, optional: true);
                if (Directory.Exists(browserSettings))
                {
                    foreach (var item in Directory.GetFiles(browserSettings, "*.json"))
                    {
                        builder.AddJsonFile(item, optional: true);
                    }
                }
            })
            .ConfigureServices((context, services) =>
            {
                ConfigureServices(context.Configuration, services);
            })
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.SetMinimumLevel(LogLevel.Trace);
                logging.AddNLog();
            })
            .Build();

        Application.Run(host.Services.GetRequiredService<CurrentApplicationContext>());
    }

    static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
    {
        services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
        services.Configure<LocalSettings>(configuration.GetSection("LocalSettings"));
        services.Configure<PostgresqlAuth>(configuration.GetSection("Authentification:Postgresql"));
        services.AddSingleton<CurrentApplicationContext>();
        services.AddSingleton<MainForm>();
        services.AddSingleton<LoginForm>();
        services.AddTransient<AboutForm>();
        services.AddSingleton<Navigator>();
        services.AddSingleton<IDockingManager>(x => x.GetRequiredService<MainForm>());
        services.AddSingleton<IHostApp>(x => x.GetRequiredService<MainForm>());
        services.AddSingleton<IPageManager, PageManager>();
        services.AddTransient<IBreadcrumb, Breadcrumb>();
        services.AddMinio(configureClient =>
        {
            var auth = new MinioAuth();
            configuration.GetSection("Authentification:Minio").Bind(auth);
            configureClient
                .WithEndpoint(auth.ToString())
                .WithCredentials(auth.AccessKey, auth.SecretKey)
                .WithSSL(false);
        });

        services.Scan(scan => scan
            .FromEntryAssembly()
                .AddClasses(classes => classes.AssignableTo<IPage>())
                    .AsMatchingInterface()
                    .WithTransientLifetime()
                .AddClasses(classes => classes.AssignableTo(typeof(IEditor)))
                    .AsMatchingInterface()
                    .WithTransientLifetime()
                .AddClasses(classes => classes.AssignableTo(typeof(IDialog)))
                    .AsMatchingInterface()
                    .WithTransientLifetime()
                .AddClasses(classes => classes.AssignableTo<IDatabase>())
                    .AsMatchingInterface()
                    .WithSingletonLifetime()
                .AddClasses(classes => classes.AssignableTo<IRowHeaderImage>())
                    .AsMatchingInterface()
                    .WithTransientLifetime()
                .AddClasses(classes => classes.AssignableTo<IFilter>())
                    .AsMatchingInterface()
                    .WithTransientLifetime()
                .AddClasses(classes => classes.AssignableTo(typeof(Report<>)))
                    .AsSelf()
                    .WithTransientLifetime()
                .AddClasses(classes => classes.AssignableTo<ICard>())
                    .AsImplementedInterfaces()
                    .WithTransientLifetime()
            .FromAssemblyOf<IDocumentInfo>()
                .AddClasses(classes => classes.AssignableTo(typeof(IRepository<,>)))
                    .AsMatchingInterface()
                    .WithSingletonLifetime()
        );
    }
}