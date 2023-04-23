//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 17.04.2023
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Data;
using DocumentFlow.Infrastructure.Dialogs;

namespace DocumentFlow.Dialogs.Infrastructure;

public interface IWageEmployeeDialog<T> : IDataGridDialog<T>
    where T : IEntity<long>, new()
{ 
}