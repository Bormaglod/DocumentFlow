//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 16.06.2022
//
// Версия 2022.12.18
//  - добавлен метод GetByContractor
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;

namespace DocumentFlow.Entities.Balances.Initial;

internal interface IInitialBalanceContractorRepository : IDocumentRepository<InitialBalanceContractor>
{
    IReadOnlyList<InitialBalanceContractor> GetByContractor(Guid? contractorId);
}
