//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Data.Models;

using Microsoft.Extensions.Configuration;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.ViewModels;

public class OperationTypeBrowser : BrowserPage<OperationType>, IOperationTypeBrowser
{
    public OperationTypeBrowser(IServiceProvider services, IOperationTypeRepository repository, IConfiguration configuration) 
        : base(services, repository, configuration) 
    {
        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var name = CreateText(x => x.ItemName, "Наименование", hidden: false);
        var salary = CreateCurrency(x => x.Salary, "Расценка, руб./час", width: 200);

        name.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;

        AddColumns(new GridColumn[] { id, name, salary });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [name] = ListSortDirection.Ascending
        });
    }
}
