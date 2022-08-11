//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 12.06.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Infrastructure;
using DocumentFlow.Infrastructure;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.Productions.Performed;

public class OperationsPerformedNestedBrowser : BaseOperationsPerformedBrowser, IOperationsPerformedNestedBrowser
{
    public OperationsPerformedNestedBrowser(IOperationsPerformedRepository repository, IPageManager pageManager) : base(repository, pageManager)
    {
        Toolbar.IconSize = ButtonIconSize.Small;

        var id = CreateText(x => x.id, "Id", width: 180, visible: false);
        var date = CreateDateTime(x => x.document_date, "Дата/время", hidden: false, width: 150);
        var operation = CreateText(x => x.operation_name, "Операция", hidden: false);
        var employee = CreateText(x => x.employee_name, "Исполнитель", width: 200);
        var material = CreateText(x => x.material_name, "Использованный материал", width: 270);
        var quantity = CreateNumeric(x => x.quantity, "Количество", width: 140, hidden: false, decimalDigits: 3);
        var salary = CreateCurrency(x => x.salary, "Зарплата", width: 120);

        CreateSummaryRow(VerticalPosition.Bottom)
            .AsSummary(quantity)
            .AsSummary(salary, SummaryColumnFormat.Currency);

        operation.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
        quantity.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;

        AddColumns(new GridColumn[] { id, date, operation, employee, material, quantity, salary });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [date] = ListSortDirection.Ascending
        });
    }
}
