using DocumentFlow.Controls;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Interfaces.Repository;
using DocumentFlow.Data.Tools;
using DocumentFlow.Interfaces;
using DocumentFlow.ReportEngine;
using DocumentFlow.Settings;
using DocumentFlow.Tools;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            .Build();

        Application.Run(host.Services.GetRequiredService<CurrentApplicationContext>());
    }

    static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
    {
        services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
        services.Configure<LocalSettings>(configuration.GetSection("LocalSettings"));
        services.AddSingleton<CurrentApplicationContext>();
        services.AddSingleton<MainForm>();
        services.AddSingleton<LoginForm>();
        services.AddTransient<AboutForm>();
        services.AddSingleton<Navigator>();
        services.AddSingleton<IDockingManager>(x => x.GetRequiredService<MainForm>());
        services.AddSingleton<IHostApp>(x => x.GetRequiredService<MainForm>());
        services.AddSingleton<IPageManager, PageManager>();
        services.AddTransient<IBreadcrumb, Breadcrumb>();

        services.Scan(scan => scan
            .FromEntryAssembly()
                .AddClasses(classes => classes.AssignableTo<IPage>())
                    .AsMatchingInterface()
                    .WithTransientLifetime()
                .AddClasses(classes => classes.AssignableTo(typeof(IEditor)))
                    .AsMatchingInterface()
                    .WithTransientLifetime()
                .AddClasses(classes => classes.InNamespaces("DocumentFlow.Dialogs").WithAttribute<DialogAttribute>())
                    .AsSelf()
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