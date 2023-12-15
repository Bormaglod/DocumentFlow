//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.12.2021
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Data.Models;

using Microsoft.Extensions.Configuration;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.ViewModels;

public class OkopfBrowser : BrowserPage<Okopf>, IOkopfBrowser
{
    public OkopfBrowser(IServiceProvider services, IOkopfRepository repository, IConfiguration configuration) 
        : base(services, repository, configuration) 
    {
        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var code = CreateText(x => x.Code, "Код", width: 110);
        var name = CreateText(x => x.ItemName, "Наименование", hidden: false);

        name.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;

        AddColumns(new GridColumn[] { id, code, name });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [code] = ListSortDirection.Ascending
        });
    }
}
