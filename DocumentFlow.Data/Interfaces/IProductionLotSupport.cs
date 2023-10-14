//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 14.10.2023
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Interfaces;

public interface IProductionLotSupport
{
    Guid? LotId { get; set; }
}
