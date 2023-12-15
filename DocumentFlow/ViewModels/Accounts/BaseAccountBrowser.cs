//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Data.Interfaces.Repository;
using DocumentFlow.Data.Models;

using Microsoft.Extensions.Configuration;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.ViewModels;

public abstract class BaseAccountBrowser<T> : BrowserPage<T>
    where T : Account
{
    public BaseAccountBrowser(IServiceProvider services, IRepository<Guid, T> repository, IConfiguration configuration)
        : base(services, repository, configuration)
    {
        ToolBar.SmallIcons();

        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var name = CreateText(x => x.ItemName, "Наименование", width: 200, hidden: false);
        var account = CreateNumeric(x => x.AccountValue, "Расчётный счёт", width: 200, format: "000 00 000 0 0000 0000000");
        var bank = CreateText(x => x.BankName, "Банк");

        bank.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;

        AddColumns(new GridColumn[] { id, name, account, bank });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [name] = ListSortDirection.Ascending
        });
    }
}
