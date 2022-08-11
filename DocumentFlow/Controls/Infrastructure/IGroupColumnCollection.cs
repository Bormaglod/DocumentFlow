//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 20.11.2020
// Time: 18:21
//-----------------------------------------------------------------------

using Syncfusion.WinForms.DataGrid;

namespace DocumentFlow.Controls.Infrastructure;

public interface IGroupColumnCollection
{
    IGroupColumnCollection Add(GridColumn column);
}
