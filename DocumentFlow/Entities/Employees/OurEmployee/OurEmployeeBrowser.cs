//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.01.2022
//
// Версия 2023.1.8
//  - в конструктор добавлен параметр settings
// Версия 2023.1.22
//  - DocumentFlow.Settings.Infrastructure перемещено в DocumentFlow.Infrastructure.Settings
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

        GridTextColumn id = CreateText(x => x.id, "Id", width: 180, visible: false);
        GridTextColumn name = CreateText(x => x.item_name, "Сотрудник", hidden: false);
        GridTextColumn post_name = CreateText(x => x.post_name, "Должность", width: 300);
        GridTextColumn org_name = CreateText(x => x.owner_name, "Организация", width: 200);
        GridTextColumn phone = CreateText(x => x.phone, "Телефон", width: 250, visible: false);
        GridTextColumn email = CreateText(x => x.email, "Эл. почта", width: 250);
        GridTextColumn j_role = CreateText(x => x.j_role, "Роль", width: 150, visible: false);

        name.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;

        AddColumns(new GridColumn[] { id, name, post_name, org_name, phone, email, j_role });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [name] = ListSortDirection.Ascending
        });
    }

    protected override string HeaderText => "Сотрудники";
}
