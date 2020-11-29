//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.11.2020
// Time: 20:38
//-----------------------------------------------------------------------

using System;

namespace DocumentFlow.Code
{
    public interface IDataGrid : IControl, IPopulate
    {
        string Name { get; }
        IDataGrid CreateColumns(Action<IColumnCollection> createColumns);
        IDataGrid SetEditor(string headerText, IEditorCode editor, Action<object> checkValues = null);
    }
}
