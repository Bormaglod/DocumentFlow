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

using DocumentFlow.Controls.Editors;
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
        var org = new DfTextBox("owner_name", "Организация", headerWidth, 400) { Enabled = false };
        var person = new DfDirectorySelectBox<Person>("person_id", "Сотрудник", headerWidth, 300)
        {
            OpenAction = (p) => pageManager.ShowEditor<IPersonEditor, Person>(p)
        };

        var post = new DfDirectorySelectBox<Okpdtr>("post_id", "Должность", headerWidth, 300)
        {
            OpenAction = (p) => pageManager.ShowEditor<IOkpdtrEditor, Okpdtr>(p)
        };

        var phone = new DfTextBox("phone", "Телефон", headerWidth, 200);
        var email = new DfTextBox("email", "Эл. почта", headerWidth, 200);
        var role = new DfChoice<JobRole>("JobRole", "Роль", headerWidth, 150);

        person.SetDataSource(() => Services.Provider.GetService<IPersonRepository>()!.GetAllValid(callback: q => q.OrderBy("item_name")));
        post.SetDataSource(() => Services.Provider.GetService<IOkpdtrRepository>()!.GetAllValid(callback: q => q.OrderBy("item_name")));
        role.SetChoiceValues(Employee.Roles);

        AddControls(new Control[]
        {
            org,
            person,
            post,
            phone,
            email,
            role
        });
    }
}