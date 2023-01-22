//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 13.11.2022
//
// Версия 2022.11.15
//  - метод UpdateMaterialRemaind перенесен в IBalanceMaterialRepository
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Data;

namespace DocumentFlow.Entities.Balances;

public interface IBalanceProductRepository<T> : IOwnedRepository<Guid, T>
    where T : BalanceProduct
{
    void ReacceptChangedDocument(T balance);
}
