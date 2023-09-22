//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 16.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces.Repository;

namespace DocumentFlow.Data.Models;

public interface IContractApplicationRepository : IOwnedRepository<Guid, ContractApplication>
{
    IReadOnlyList<ContractApplication> GetCurrents(Contract contract);
}
