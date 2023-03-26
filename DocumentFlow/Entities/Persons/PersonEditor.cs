//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.01.2022
//-----------------------------------------------------------------------

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
            CreateTextBox(x => x.ItemName, "Фамилия И.О.", headerWidth, 250),
            CreateTextBox(x => x.Surname, "Фамилия", headerWidth, 200),
            CreateTextBox(x => x.FirstName, "Имя", headerWidth, 200),
            CreateTextBox(x => x.MiddleName, "Отчество", headerWidth, 200),
            CreateTextBox(x => x.Phone, "Телефон", headerWidth, 200),
            CreateTextBox(x => x.Email, "Эл. почта", headerWidth, 300)
        });
    }
}