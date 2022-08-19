//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 16.01.2022
//
// Версия 2022.8.19
//  - добавлен метод GetCurrents
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;

namespace DocumentFlow.Entities.Companies;

public interface IContractApplicationRepository : IOwnedRepository<Guid, ContractApplication>
{
    IReadOnlyList<ContractApplication> GetCurrents(Contract contract);
}
