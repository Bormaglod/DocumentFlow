//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 31.01.2022
//
// Версия 2022.12.30
//  - добавлен метод GetCustomersDebt
// Версия 2022.12.30
//  - добавлен метод GetSuppliersDebt
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Data;

namespace DocumentFlow.Entities.Balances;

public interface IBalanceContractorRepository : IOwnedRepository<Guid, BalanceContractor>
{
    IReadOnlyList<ContractorDebt> GetCustomersDebt(int limit = 0);
    IReadOnlyList<ContractorDebt> GetSuppliersDebt(int limit = 0);
}
