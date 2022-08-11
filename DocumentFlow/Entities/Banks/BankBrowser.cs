//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.12.2021
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Infrastructure;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.Banks;

public class BankBrowser : Browser<Bank>, IBankBrowser
{
    public BankBrowser(IBankRepository repository, IPageManager pageManager) : base(repository, pageManager)
    {
        GridTextColumn id = CreateText(x => x.id, "Id", width: 180, visible: false);
        GridTextColumn name = CreateText(x => x.item_name, "Наименование", hidden: false);
        GridNumericColumn bik = CreateNumeric(x => x.bik, "БИК", width: 100, format: "00 00 00 000");
        GridNumericColumn account = CreateNumeric(x => x.account, "Корр. счёт", width: 180, format: "000 00 000 0 00000000 000");
        GridTextColumn town = CreateText(x => x.town, "Город", width: 200);

        name.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;

        AddColumns(new GridColumn[] { id, name, bik, account, town });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [name] = ListSortDirection.Ascending
        });
    }

    protected override string HeaderText => "Банки";
}
