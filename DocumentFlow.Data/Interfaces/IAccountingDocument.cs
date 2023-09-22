//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.12.2021
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Interfaces;

public interface IAccountingDocument : IBaseDocument
{
    bool CarriedOut { get; }
    bool ReCarriedOut { get; }
}
