//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 16.06.2022
//
// Версия 2022.12.18
//  - добавлен метод GetByContractor
// Версия 2022.12.29
//  - интерфейс теперь public
//  - добавлен второй параметр BalanceCategory в GetByContractor
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Data;

namespace DocumentFlow.Entities.Balances.Initial;

public interface IInitialBalanceContractorRepository : IDocumentRepository<InitialBalanceContractor>
{
    IReadOnlyList<InitialBalanceContractor> GetByContractor(Guid? contractorId, BalanceCategory category);
}
