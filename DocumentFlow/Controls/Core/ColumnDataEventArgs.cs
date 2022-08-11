//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 19.01.2022
//-----------------------------------------------------------------------

using Syncfusion.WinForms.DataGrid;

namespace DocumentFlow.Controls.Core;
public class ColumnDataEventArgs : EventArgs
{
    public ColumnDataEventArgs(Columns columns) => Columns = columns;

    public Columns Columns { get; }
}
