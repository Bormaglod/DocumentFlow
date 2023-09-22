//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 24.08.2023
//-----------------------------------------------------------------------

using Syncfusion.WinForms.DataGrid;

namespace DocumentFlow.Controls.Events;

public class DocumentDialogColumnsEventArgs : EventArgs
{
    public DocumentDialogColumnsEventArgs(IList<GridColumn> columns) => Columns = columns;

    public IList<GridColumn> Columns { get; set; }
}