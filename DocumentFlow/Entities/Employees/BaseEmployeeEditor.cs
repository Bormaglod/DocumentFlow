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
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
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
    private const int headerWidth = 120;

    public BaseEmployeeEditor(IRepository<Guid, T> repository, IPageManager pageManager) : base(repository, pageManager) 
    {
        EditorControls
            .AddTextBox(x => x.OwnerName, "Организация", text =>
                text
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(400)
                    .Disable())
            .AddDirectorySelectBox<Person>(x => x.PersonId, "Сотрудник", select =>
                select
                    .SetDataSource(GetPeople)
                    .EnableEditor<IPersonEditor>()
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(300))
            .AddDirectorySelectBox<Okpdtr>(x => x.PostId, "Должность", select =>
                select
                    .SetDataSource(GetPosts)
                    .EnableEditor<IOkpdtrEditor>()
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(300))
            .AddTextBox(x => x.Phone, "Телефон", text =>
                text
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(200))
            .AddTextBox(x => x.Email, "Эл. почта", text =>
                text
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(200))
            .AddChoice<JobRole>(x => x.JobRole, "Роль", choice =>
                choice
                    .SetChoiceValues(Employee.Roles)
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(150));
    }

    private IEnumerable<Person> GetPeople() => Services.Provider.GetService<IPersonRepository>()!.GetListExisting(callback: q => q.OrderBy("item_name"));

    private IEnumerable<Okpdtr> GetPosts() => Services.Provider.GetService<IOkpdtrRepository>()!.GetListExisting(callback: q => q.OrderBy("item_name"));
}