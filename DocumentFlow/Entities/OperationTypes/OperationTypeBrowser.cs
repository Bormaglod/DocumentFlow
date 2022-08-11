//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Infrastructure;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.OperationTypes;

public class OperationTypeBrowser : Browser<OperationType>, IOperationTypeBrowser
{
    public OperationTypeBrowser(IOperationTypeRepository repository, IPageManager pageManager) : base(repository, pageManager) 
    {
        GridTextColumn id = CreateText(x => x.id, "Id", width: 180, visible: false);
        GridTextColumn name = CreateText(x => x.item_name, "Наименование", hidden: false);
        GridNumericColumn salary = CreateCurrency(x => x.salary, "Расценка, руб./час", width: 200);

        name.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;

        AddColumns(new GridColumn[] { id, name, salary });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [name] = ListSortDirection.Ascending
        });
    }

    protected override string HeaderText => "Виды операций";
}
