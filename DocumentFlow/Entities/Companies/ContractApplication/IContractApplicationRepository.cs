//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 16.01.2022
//
// Версия 2022.8.19
//  - добавлен метод GetCurrents
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Data;

namespace DocumentFlow.Entities.Companies;

public interface IContractApplicationRepository : IOwnedRepository<Guid, ContractApplication>
{
    IReadOnlyList<ContractApplication> GetCurrents(Contract contract);
}
