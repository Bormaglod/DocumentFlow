//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 20.11.2020
//-----------------------------------------------------------------------

using Syncfusion.WinForms.DataGrid;

namespace DocumentFlow.Controls.Interfaces;

public interface IGroupColumnCollection
{
    IReadOnlyList<IGroupColumn> AvailableGroups { get; }
    IReadOnlyList<IGroupColumn> SelectedGroups { get; }
    IGroupColumnCollection Add(GridColumn column);
    IGroupColumnCollection Add(string name);
    IGroupColumnCollection Register(GridColumn column, string name, string description, Func<string, object, object> keySelector);
}
