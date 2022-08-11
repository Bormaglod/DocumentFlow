//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.11.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;

namespace DocumentFlow.Data.Core;

public class AccountingDocument : BaseDocument, IAccountingDocument
{
    public bool carried_out { get; protected set; }
    public bool re_carried_out { get; protected set; }
}
