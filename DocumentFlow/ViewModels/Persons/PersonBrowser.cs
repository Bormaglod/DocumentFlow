//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.12.2021
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Data.Models;
using DocumentFlow.Interfaces;

using Microsoft.Extensions.Configuration;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.ViewModels;

public class PersonBrowser : BrowserPage<Person>, IPersonBrowser
{
    public PersonBrowser(IServiceProvider services, IPageManager pageManager, IPersonRepository repository, IConfiguration configuration) 
        : base(services, pageManager, repository, configuration)
    {
        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var name = CreateText(x => x.ItemName, "Фамилия И.О.", hidden: false);
        var surname = CreateText(x => x.Surname, "Фамилия", width: 150);
        var first_name = CreateText(x => x.FirstName, "Имя", width: 100);
        var middle_name = CreateText(x => x.MiddleName, "Отчество", width: 150);
        var phone = CreateText(x => x.Phone, "Телефон", width: 150);
        var email = CreateText(x => x.Email, "Эл. почта", width: 200);

        name.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;

        AddColumns(new GridColumn[] { id, name, surname, first_name, middle_name, phone, email });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [name] = ListSortDirection.Ascending
        });
    }
}
