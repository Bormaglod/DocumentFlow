//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 27.12.2019
//
// Версия 2022.12.6
//  - добавлен интерфейс IBalanceContractorFilter
// Версия 2022.12.21
//  - добавлен интерфейс ICreationBased
// Версия 2022.12.31
//  - добавлены интерфейсы IStartPage и ICard
// Версия 2023.1.8
//  - интерфейс ISettings переименован в IAppSettings
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//  - DocumentFlow.Settings.Infrastructure перемещено в DocumentFlow.Infrastructure.Settings
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
// Версия 2023.4.2
//  - добавлен интерфейс IControls<>
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Controls;
using DocumentFlow.Infrastructure.Data;
using DocumentFlow.Infrastructure.Dialogs;
using DocumentFlow.Infrastructure.Settings;

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

    public static void ConfigureServices()
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
                    .AddClasses(classes => classes.AssignableTo(typeof(IControls<>)))
                        .AsMatchingInterface()
                        .WithTransientLifetime()
                    .AddClasses(classes => classes.AssignableTo<IBreadcrumb>())
                        .AsMatchingInterface()
                        .WithTransientLifetime()
                    .AddClasses(classes => classes.AssignableTo<IDateRangeFilter>())
                        .AsMatchingInterface()
                        .WithTransientLifetime()
                    .AddClasses(classes => classes.AssignableTo<IBalanceContractorFilter>())
                        .AsMatchingInterface()
                        .WithTransientLifetime()
                    .AddClasses(classes => classes.AssignableTo<IRowHeaderImage>())
                        .AsMatchingInterface()
                        .WithTransientLifetime()
                    .AddClasses(classes => classes.AssignableTo<ISettingsPage>())
                        .AsImplementedInterfaces()
                        .WithTransientLifetime()
                    .AddClasses(classes => classes.AssignableTo<IAppSettings>())
                        .AsMatchingInterface()
                        .WithTransientLifetime()
                    .AddClasses(classes => classes.AssignableTo<ICreationBased>())
                        .AsImplementedInterfaces()
                        .WithTransientLifetime()
                    .AddClasses(classes => classes.AssignableTo<IStartPage>())
                        .AsMatchingInterface()
                        .WithTransientLifetime()
                    .AddClasses(classes => classes.AssignableTo<ICard>())
                        .AsImplementedInterfaces()
                        .WithTransientLifetime()
                    .AddClasses(classes => classes.AssignableTo(typeof(IDataGridDialog<>)))
                        .AsMatchingInterface()
                        .WithTransientLifetime()
                );

        services.AddSingleton<IPageManager>(c => new PageManager(CurrentApplicationContext.Context.TabPages));

        serviceProvider = services.BuildServiceProvider();
    }
}
