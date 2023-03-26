//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.12.2021
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

namespace DocumentFlow.Entities.Persons;

public class PersonBrowser : Browser<Person>, IPersonBrowser
{
    public PersonBrowser(IPersonRepository repository, IPageManager pageManager, IStandaloneSettings settings) 
        : base(repository, pageManager, settings: settings) 
    {
        GridTextColumn id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        GridTextColumn name = CreateText(x => x.ItemName, "Фамилия И.О.", hidden: false);
        GridTextColumn surname = CreateText(x => x.Surname, "Фамилия", width: 150);
        GridTextColumn first_name = CreateText(x => x.FirstName, "Имя", width: 100);
        GridTextColumn middle_name = CreateText(x => x.MiddleName, "Отчество", width: 150);
        GridTextColumn phone = CreateText(x => x.Phone, "Телефон", width: 150);
        GridTextColumn email = CreateText(x => x.Email, "Эл. почта", width: 200);

        name.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;

        AddColumns(new GridColumn[] { id, name, surname, first_name, middle_name, phone, email });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [name] = ListSortDirection.Ascending
        });
    }

    protected override string HeaderText => "Физ. лица";
}
