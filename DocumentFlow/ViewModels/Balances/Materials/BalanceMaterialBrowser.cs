//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 31.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Models;

using Microsoft.Extensions.Configuration;

namespace DocumentFlow.ViewModels;

public class BalanceMaterialBrowser : BalanceProductBrowser<BalanceMaterial>, IBalanceMaterialBrowser
{
    public BalanceMaterialBrowser(IServiceProvider services, IBalanceMaterialRepository repository, IConfiguration configuration) 
        : base(services, repository, configuration) 
    {
        ContextMenu.Add("Пересчитать сумму остатка", (s, e) =>
        {
            if (CurrentDocument != null)
            {
                repository.UpdateMaterialRemaind(CurrentDocument);
            }
        });
    }
}
