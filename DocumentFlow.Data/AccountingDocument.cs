//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.11.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;

namespace DocumentFlow.Data;

public abstract class AccountingDocument : BaseDocument, IAccountingDocument
{
    public bool CarriedOut { get; protected set; }
    public bool ReCarriedOut { get; protected set; }
}
