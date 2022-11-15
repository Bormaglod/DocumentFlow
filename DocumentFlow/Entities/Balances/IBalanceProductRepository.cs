//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 13.11.2022
//
// Версия 2022.11.15
//  - метод UpdateMaterialRemaind перенесен в IBalanceMaterialRepository
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;

namespace DocumentFlow.Entities.Balances;

public interface IBalanceProductRepository<T> : IOwnedRepository<Guid, T>
    where T : BalanceProduct
{
    void ReacceptChangedDocument(T balance);
}
