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

public class OrganizationBrowser : BrowserPage<Organization>, IOrganizationBrowser
{
    public OrganizationBrowser(IServiceProvider services, IPageManager pageManager, IOrganizationRepository repository, IConfiguration configuration) 
        : base(services, pageManager, repository, configuration)
    {
        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var code = CreateText(x => x.Code, "Наименование", hidden: false);
        var name = CreateText(x => x.ItemName, "Краткое наименование", width: 150, visible: false);
        var full_name = CreateText(x => x.FullName, "Полное наименование", width: 200, visible: false);
        var inn = CreateNumeric(x => x.Inn, "ИНН", width: 100, format: "0000 00000 0");
        var kpp = CreateNumeric(x => x.Kpp, "КПП", width: 100, format: "0000 00 000");
        var okpo = CreateNumeric(x => x.Okpo, "ОКПО", width: 100, visible: false, format: "00 00000 0");
        var ogrn = CreateNumeric(x => x.Ogrn, "ОГРН", width: 120, format: "0 00 00 00 00000 0");
        var okopf = CreateText(x => x.OkopfName, "ОКОПФ", width: 300);
        var address = CreateText(x => x.Address, "Адрес", width: 250, visible: false);
        var phone = CreateText(x => x.Phone, "Телефон", width: 150);
        var email = CreateText(x => x.Email, "Эл. почта", width: 200);
        var default_org = CreateBoolean(x => x.DefaultOrg, "Основная", width: 100);

        code.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;

        AddColumns(new GridColumn[] { id, code, name, full_name, inn, kpp, okpo, ogrn, okopf, address, phone, email, default_org });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [code] = ListSortDirection.Ascending
        });
    }
}
