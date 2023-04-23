//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.01.2022
//
// Версия 2023.4.2
//  - создание элементов управления в конструкторе реализовано с 
//    помощью свойства EditorControls
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Infrastructure;

namespace DocumentFlow.Entities.Persons;

public class PersonEditor : Editor<Person>, IPersonEditor
{
    private const int headerWidth = 120;

    public PersonEditor(IPersonRepository repository, IPageManager pageManager) : base(repository, pageManager) 
    {
        EditorControls
            .AddTextBox(x => x.ItemName, "Фамилия И.О.", (text) =>
                text
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(250))
            .AddTextBox(x => x.Surname, "Фамилия", (text) =>
                text
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(200))
            .AddTextBox(x => x.FirstName, "Имя", (text) =>
                text
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(200))
            .AddTextBox(x => x.MiddleName, "Отчество", (text) =>
                text
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(200))
            .AddTextBox(x => x.Phone, "Телефон", (text) =>
                text
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(200))
            .AddTextBox(x => x.Email, "Эл. почта", (text) =>
                text
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(300));
    }
}