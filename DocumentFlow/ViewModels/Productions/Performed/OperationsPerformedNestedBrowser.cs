//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 12.06.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Enums;
using DocumentFlow.Data.Models;

using Microsoft.Extensions.Configuration;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.ViewModels;

public class OperationsPerformedNestedBrowser : BaseOperationsPerformedBrowser, IOperationsPerformedNestedBrowser
{
    public OperationsPerformedNestedBrowser(IServiceProvider services, IOperationsPerformedRepository repository, IConfiguration configuration) 
        : base(services, repository, configuration)
    {
        ToolBar.SmallIcons();

        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var number = CreateNumeric(x => x.DocumentNumber, "Номер", width: 80, visible: false);
        var date = CreateDateTime(x => x.DocumentDate, "Дата/время", hidden: false, width: 150);
        var operation = CreateText(x => x.OperationName, "Операция", hidden: false);
        var employee = CreateText(x => x.EmployeeName, "Исполнитель", width: 200);
        var material = CreateText(x => x.MaterialName, "Использованный материал", width: 270);
        var quantity = CreateNumeric(x => x.Quantity, "Количество", width: 140, hidden: false, decimalDigits: 3);
        var salary = CreateCurrency(x => x.Salary, "Зарплата", width: 120);
        var double_rate = CreateBoolean(x => x.DoubleRate, "Двойная оплата", width: 100);

        CreateSummaryRow(VerticalPosition.Bottom)
            .AsSummary(quantity)
            .AsSummary(salary, SummaryColumnFormat.Currency);

        operation.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
        quantity.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;

        AddColumns(new GridColumn[] { id, number, date, operation, employee, material, quantity, salary, double_rate });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [date] = ListSortDirection.Ascending
        });
    }
}
