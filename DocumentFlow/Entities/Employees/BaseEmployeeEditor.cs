//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 20.01.2022
//
// Версия 2022.8.29
//  - расширены возможности полей person и post (добавлены кнопки для
//    редактирования выбранных значений)
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в
//    DocumentFlow.Infrastructure.Data
// Версия 2023.6.19
//  - поле JobRole заменено на EmployeeRole
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Entities.OkpdtrLib;
using DocumentFlow.Entities.Persons;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Data;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Employees;

public class BaseEmployeeEditor<T> : Editor<T>
    where T : Employee, IDocumentInfo, new()
{
    public BaseEmployeeEditor(IRepository<Guid, T> repository, IPageManager pageManager) : base(repository, pageManager) 
    {
        EditorControls
            .SetHeaderWidth(120)
            .AddTextBox(x => x.OwnerName, "Организация", text => text
                .SetEditorWidth(400)
                .Disable())
            .AddDirectorySelectBox<Person>(x => x.PersonId, "Сотрудник", select => select
                .SetDataSource(GetPeople)
                .EnableEditor<IPersonEditor>()
                .SetEditorWidth(300))
            .AddDirectorySelectBox<Okpdtr>(x => x.PostId, "Должность", select => select
                .SetDataSource(GetPosts)
                .EnableEditor<IOkpdtrEditor>()
                .SetEditorWidth(300))
            .AddTextBox(x => x.Phone, "Телефон", text => text
                .SetEditorWidth(200))
            .AddTextBox(x => x.Email, "Эл. почта", text => text
                .SetEditorWidth(200))
            .AddChoice<EmployeeRole>(x => x.EmployeeRole, "Роль", choice => choice
                .SetChoiceValues(Employee.Roles)
                .SetEditorWidth(150));
    }

    private IEnumerable<Person> GetPeople() => Services.Provider.GetService<IPersonRepository>()!.GetListExisting(callback: q => q.OrderBy("item_name"));

    private IEnumerable<Okpdtr> GetPosts() => Services.Provider.GetService<IOkpdtrRepository>()!.GetListExisting(callback: q => q.OrderBy("item_name"));
}