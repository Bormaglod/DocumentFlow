//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Editors;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Infrastructure;

namespace DocumentFlow.Entities.Persons;

public class PersonEditor : Editor<Person>, IPersonEditor
{
    private const int headerWidth = 120;

    public PersonEditor(IPersonRepository repository, IPageManager pageManager) : base(repository, pageManager) 
    {
        AddControls(new Control[]
        {
            new DfTextBox("item_name", "Фамилия И.О.", headerWidth, 250),
            new DfTextBox("surname", "Фамилия", headerWidth, 200),
            new DfTextBox("first_name", "Имя", headerWidth, 200),
            new DfTextBox("middle_name", "Отчество", headerWidth, 200),
            new DfTextBox("phone", "Телефон", headerWidth, 200),
            new DfTextBox("email", "Эл. почта", headerWidth, 300)
        });
    }
}