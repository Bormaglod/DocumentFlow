//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 27.09.2023
//-----------------------------------------------------------------------

namespace DocumentFlow.Controls.Interfaces;

public interface IGroupColumn
{
    int Order { get; set; }
    string ColumnName { get; }
    string Name { get; }
    string Text { get; set; }
    string Description { get; set; }
}
