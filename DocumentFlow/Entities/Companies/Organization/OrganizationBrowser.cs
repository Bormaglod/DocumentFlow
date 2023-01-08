//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.12.2021
//
// Версия 2023.1.8
//  - в конструктор добавлен параметр settings
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Infrastructure;
using DocumentFlow.Settings.Infrastructure;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.Companies;

public class OrganizationBrowser : Browser<Organization>, IOrganizationBrowser
{
    public OrganizationBrowser(IOrganizationRepository repository, IPageManager pageManager, IStandaloneSettings settings) 
        : base(repository, pageManager, settings: settings) 
    {
        GridTextColumn id = CreateText(x => x.id, "Id", width: 180, visible: false);
        GridTextColumn code = CreateText(x => x.code, "Наименование", hidden: false);
        GridTextColumn name = CreateText(x => x.item_name, "Краткое наименование", width: 150, visible: false);
        GridTextColumn full_name = CreateText(x => x.full_name, "Полное наименование", width: 200, visible: false);
        GridNumericColumn inn = CreateNumeric(x => x.inn, "ИНН", width: 100, format: "0000 00000 0");
        GridNumericColumn kpp = CreateNumeric(x => x.kpp, "КПП", width: 100, format: "0000 00 000");
        GridNumericColumn okpo = CreateNumeric(x => x.okpo, "ОКПО", width: 100, visible: false, format: "00 00000 0");
        GridNumericColumn ogrn = CreateNumeric(x => x.ogrn, "ОГРН", width: 120, format: "0 00 00 00 00000 0");
        GridTextColumn okopf = CreateText(x => x.okopf_name, "ОКОПФ", width: 300);
        GridTextColumn address = CreateText(x => x.address, "Адрес", width: 250, visible: false);
        GridTextColumn phone = CreateText(x => x.phone, "Телефон", width: 150);
        GridTextColumn email = CreateText(x => x.email, "Эл. почта", width: 200);
        GridCheckBoxColumn default_org = CreateBoolean(x => x.default_org, "Основная", width: 100);

        code.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;

        AddColumns(new GridColumn[] { id, code, name, full_name, inn, kpp, okpo, ogrn, okopf, address, phone, email, default_org });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [code] = ListSortDirection.Ascending
        });
    }

    protected override string HeaderText => "Организация";
}
