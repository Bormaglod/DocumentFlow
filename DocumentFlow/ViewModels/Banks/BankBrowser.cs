//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.12.2021
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Data.Models;

using Microsoft.Extensions.Configuration;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.ViewModels;

public class BankBrowser : BrowserPage<Bank>, IBankBrowser
{
    public BankBrowser(IServiceProvider services, IBankRepository repository, IConfiguration configuration)
        : base(services, repository, configuration)
    {
        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var name = CreateText(x => x.ItemName, "Наименование", hidden: false);
        var bik = CreateNumeric(x => x.Bik, "БИК", width: 100, format: "00 00 00 000");
        var account = CreateNumeric(x => x.Account, "Корр. счёт", width: 180, format: "000 00 000 0 00000000 000");
        var town = CreateText(x => x.Town, "Город", width: 200);

        name.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;

        AddColumns(new GridColumn[] { id, name, bik, account, town });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [name] = ListSortDirection.Ascending
        });
    }
}
