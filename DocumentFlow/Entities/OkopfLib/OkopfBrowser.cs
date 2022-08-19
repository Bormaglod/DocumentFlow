﻿//-----------------------------------------------------------------------
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

namespace DocumentFlow.Entities.OkopfLib;

public class OkopfBrowser : Browser<Okopf>, IOkopfBrowser
{
    public OkopfBrowser(IOkopfRepository repository, IPageManager pageManager) : base(repository, pageManager) 
    {
        GridTextColumn id = CreateText(x => x.id, "Id", width: 180, visible: false);
        GridTextColumn code = CreateText(x => x.code, "Код", width: 110);
        GridTextColumn name = CreateText(x => x.item_name, "Наименование", hidden: false);

        name.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;

        AddColumns(new GridColumn[] { id, code, name });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [code] = ListSortDirection.Ascending
        });
    }

    protected override string HeaderText => "ОКОПФ";
}