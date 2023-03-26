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
        AddControls(new Control[]
        {
            CreateTextBox(x => x.OwnerName, "Организация", headerWidth, 400, enabled: false),
            CreateDirectorySelectBox<Person, IPersonEditor>(x => x.PersonId, "Сотрудник", headerWidth, 300, data: GetPeople),
            CreateDirectorySelectBox<Okpdtr, IOkpdtrEditor>(x => x.PostId, "Должность", headerWidth, 300, data: GetPosts),
            CreateTextBox(x => x.Phone, "Телефон", headerWidth, 200),
            CreateTextBox(x => x.Email, "Эл. почта", headerWidth, 200),
            CreateChoice(x => x.JobRole, "Роль", headerWidth, 150, choices: Employee.Roles)
        });
    }

    private IEnumerable<Person> GetPeople() => Services.Provider.GetService<IPersonRepository>()!.GetAllValid(callback: q => q.OrderBy("item_name"));

    private IEnumerable<Okpdtr> GetPosts() => Services.Provider.GetService<IOkpdtrRepository>()!.GetAllValid(callback: q => q.OrderBy("item_name"));
}