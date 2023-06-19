//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.01.2022
//
// Версия 2023.1.8
//  - в конструктор добавлен параметр settings
// Версия 2023.1.22
//  - DocumentFlow.Settings.Infrastructure перемещено в
//    DocumentFlow.Infrastructure.Settings
// Версия 2023.6.19
//  - столбец JRole заменён на EmployeeRoleName
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Settings;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.Employees;

public class OurEmployeeBrowser : Browser<OurEmployee>, IOurEmployeeBrowser
{
    public OurEmployeeBrowser(IOurEmployeeRepository repository, IPageManager pageManager, IStandaloneSettings settings) 
        : base(repository, pageManager, settings: settings) 
    {
        AllowGrouping();

        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var name = CreateText(x => x.ItemName, "Сотрудник", hidden: false);
        var post_name = CreateText(x => x.PostName, "Должность", width: 300);
        var org_name = CreateText(x => x.OwnerName, "Организация", width: 200);
        var phone = CreateText(x => x.Phone, "Телефон", width: 250, visible: false);
        var email = CreateText(x => x.Email, "Эл. почта", width: 250);
        var j_role = CreateText(x => x.EmployeeRoleName, "Роль", width: 150, visible: false);

        name.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;

        AddColumns(new GridColumn[] { id, name, post_name, org_name, phone, email, j_role });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [name] = ListSortDirection.Ascending
        });
    }

    protected override string HeaderText => "Сотрудники";
}
