//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 25.06.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Infrastructure;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.Products;

public class WireBrowser : Browser<Wire>, IWireBrowser
{
    public WireBrowser(IWireRepository repository, IPageManager pageManager) : base(repository, pageManager) 
    {
        var id = CreateText(x => x.id, "Id", width: 180, visible: false);
        var code = CreateText(x => x.code, "Код", width: 110);
        var name = CreateText(x => x.item_name, "Наименование", hidden: false);
        var wsize = CreateNumeric(x => x.wsize, "Сечение проводп", width: 100, decimalDigits: 2);

        name.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;

        AddColumns(new GridColumn[] { id, code, name, wsize });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [code] = ListSortDirection.Ascending
        });
    }

    protected override string HeaderText => "Типы проводов";
}
