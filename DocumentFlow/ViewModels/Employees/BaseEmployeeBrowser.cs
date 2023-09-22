//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Data.Models;
using DocumentFlow.Data.Interfaces.Repository;
using DocumentFlow.Interfaces;

using Microsoft.Extensions.Configuration;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.ViewModels;

public abstract class BaseEmployeeBrowser<T> : BrowserPage<T>
    where T : Employee
{
    protected BaseEmployeeBrowser(IServiceProvider services, IPageManager pageManager, IRepository<Guid, T> repository, IConfiguration configuration) 
        : base(services, pageManager, repository, configuration)
    {
        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var name = CreateText(x => x.ItemName, "Сотрудник", hidden: false);
        var post_name = CreateText(x => x.PostName, "Должность", width: 300);
        var phone = CreateText(x => x.Phone, "Телефон", width: 250, visible: false);
        var email = CreateText(x => x.Email, "Эл. почта", width: 250);
        var j_role = CreateText(x => x.EmployeeRoleName, "Роль", width: 150, visible: false);

        name.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;

        AddColumns(new GridColumn[] { id, name, post_name, phone, email, j_role });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [name] = ListSortDirection.Ascending
        });
    }
}
