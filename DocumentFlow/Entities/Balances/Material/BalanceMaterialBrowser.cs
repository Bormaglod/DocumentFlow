//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 31.01.2022
//
// Версия 2022.11.15
//  - добавлен пункт меню для пересчёта суммы остатка
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure;

namespace DocumentFlow.Entities.Balances;

public class BalanceMaterialBrowser : BalanceProductBrowser<BalanceMaterial>, IBalanceMaterialBrowser
{
    public BalanceMaterialBrowser(IBalanceMaterialRepository repository, IPageManager pageManager) : base(repository, pageManager) 
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
