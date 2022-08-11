//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 27.12.2019
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Infrastructure;
using DocumentFlow.Data;
using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Infrastructure;
using DocumentFlow.Settings.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow;

public static class Services
{
    private static IServiceProvider? serviceProvider;

    public static IServiceProvider Provider
    {
        get
        {
            if (serviceProvider == null)
            {
                ConfigureServices();
            }

            return serviceProvider ?? throw new Exception("Не удалось создать ServiceProvider");
        }
    }

    private static void ConfigureServices()
    {
        var services = new ServiceCollection()
            .Scan(scan => scan
                .FromEntryAssembly()
                    .AddClasses(classes => classes.AssignableTo<IDatabase>())
                        .AsMatchingInterface()
                        .WithSingletonLifetime()
                    .AddClasses(classes => classes.AssignableTo(typeof(IRepository<,>)))
                        .AsMatchingInterface()
                        .WithSingletonLifetime()
                    .AddClasses(classes => classes.AssignableTo(typeof(IBrowser<>)))
                        .AsMatchingInterface()
                        .WithTransientLifetime()
                    .AddClasses(classes => classes.AssignableTo(typeof(IEditor<,>)))
                        .AsMatchingInterface()
                        .WithTransientLifetime()
                    .AddClasses(classes => classes.AssignableTo<IBreadcrumb>())
                        .AsMatchingInterface()
                        .WithTransientLifetime()
                    .AddClasses(classes => classes.AssignableTo<IDateRangeFilter>())
                        .AsMatchingInterface()
                        .WithTransientLifetime()
                    .AddClasses(classes => classes.AssignableTo<IRowHeaderImage>())
                        .AsMatchingInterface()
                        .WithTransientLifetime()
                    .AddClasses(classes => classes.AssignableTo<ISettingsPage>())
                        .AsImplementedInterfaces()
                        .WithTransientLifetime()
                    .AddClasses(classes => classes.AssignableTo<ISettings>())
                        .AsMatchingInterface()
                        .WithTransientLifetime()
                );

        services.AddSingleton<IPageManager>(c => new PageManager(CurrentApplicationContext.Context.TabPages));

        serviceProvider = services.BuildServiceProvider();
    }
}
