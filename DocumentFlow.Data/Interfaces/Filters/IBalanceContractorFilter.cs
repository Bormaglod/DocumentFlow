//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.12.2022
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Interfaces.Filters;

public interface IBalanceContractorFilter : IFilter
{
    Guid? ContractIdentifier { get; set; }
}
