//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.12.2021
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Infrastructure;

public interface IAccountingDocument : IBaseDocument
{
    bool carried_out { get; }
    bool re_carried_out { get; }
}
