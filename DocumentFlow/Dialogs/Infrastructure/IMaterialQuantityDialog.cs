//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 16.04.2023
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Dialogs;
using DocumentFlow.Entities.Productions.Returns;

namespace DocumentFlow.Dialogs.Infrastructure;

public interface IMaterialQuantityDialog : IDataGridDialog<ReturnMaterialsRows> { }