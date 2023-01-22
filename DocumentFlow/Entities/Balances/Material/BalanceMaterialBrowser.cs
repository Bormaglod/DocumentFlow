//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 31.01.2022
//
// Версия 2022.11.15
//  - добавлен пункт меню для пересчёта суммы остатка
// Версия 2023.1.8
//  - в конструктор добавлен параметр settings
// Версия 2023.1.22
//  - DocumentFlow.Settings.Infrastructure перемещено в DocumentFlow.Infrastructure.Settings
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Settings;

namespace DocumentFlow.Entities.Balances;

public class BalanceMaterialBrowser : BalanceProductBrowser<BalanceMaterial>, IBalanceMaterialBrowser
{
    public BalanceMaterialBrowser(IBalanceMaterialRepository repository, IPageManager pageManager, IStandaloneSettings settings) 
        : base(repository, pageManager, settings: settings) 
    {
        ContextMenu.Add("Пересчитать сумму остатка", (_) =>
        {
            if (CurrentDocument != null)
            {
                repository.UpdateMaterialRemaind(CurrentDocument);
            }
        }, false);
    }
}
