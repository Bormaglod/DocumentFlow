//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.12.2021
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Infrastructure;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.Persons;

public class PersonBrowser : Browser<Person>, IPersonBrowser
{
    public PersonBrowser(IPersonRepository repository, IPageManager pageManager) : base(repository, pageManager) 
    {
        GridTextColumn id = CreateText(x => x.id, "Id", width: 180, visible: false);
        GridTextColumn name = CreateText(x => x.item_name, "Фамилия И.О.", hidden: false);
        GridTextColumn surname = CreateText(x => x.surname, "Фамилия", width: 150);
        GridTextColumn first_name = CreateText(x => x.first_name, "Имя", width: 100);
        GridTextColumn middle_name = CreateText(x => x.middle_name, "Отчество", width: 150);
        GridTextColumn phone = CreateText(x => x.phone, "Телефон", width: 150);
        GridTextColumn email = CreateText(x => x.email, "Эл. почта", width: 200);

        name.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;

        AddColumns(new GridColumn[] { id, name, surname, first_name, middle_name, phone, email });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [name] = ListSortDirection.Ascending
        });
    }

    protected override string HeaderText => "Физ. лица";
}
