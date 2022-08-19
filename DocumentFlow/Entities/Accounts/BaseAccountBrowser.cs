﻿//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Infrastructure;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Infrastructure;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.Accounts;

public abstract class BaseAccountBrowser<T> : Browser<T>
    where T : Account
{
    public BaseAccountBrowser(IRepository<Guid, T> repository, IPageManager pageManager) : base(repository, pageManager)
    {
        Toolbar.IconSize = ButtonIconSize.Small;

        var id = CreateText(x => x.id, "Id", width: 180, visible: false);
        var name = CreateText(x => x.item_name, "Наименование", width: 200, hidden: false);
        var account = CreateNumeric(x => x.account_value, "Расчётный счёт", width: 200, format: "000 00 000 0 0000 0000000");
        var bank = CreateText(x => x.bank_name, "Банк");

        bank.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;

        AddColumns(new GridColumn[] { id, name, account, bank });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [name] = ListSortDirection.Ascending
        });
    }
}