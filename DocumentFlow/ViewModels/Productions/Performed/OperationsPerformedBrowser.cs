//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 11.05.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Enums;
using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Models;
using DocumentFlow.Interfaces;

using Microsoft.Extensions.Configuration;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.ViewModels;

public class OperationsPerformedBrowser : BaseOperationsPerformedBrowser, IOperationsPerformedBrowser
{
    public OperationsPerformedBrowser(IServiceProvider services, IPageManager pageManager, IOperationsPerformedRepository repository, IConfiguration configuration, IDocumentFilter filter)
        : base(services, pageManager, repository, configuration, filter: filter)
    {
        filter.SetDateRange(DateRange.CurrentDay);

        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var number = CreateNumeric(x => x.DocumentNumber, "Номер", width: 80, visible: false);
        var date = CreateDateTime(x => x.DocumentDate, "Дата/время", hidden: false, width: 150);
        var order_name = CreateText(x => x.OrderName, "Заказ", width: 150, visible: false);
        var lot_name = CreateText(x => x.LotName, "Партия", width: 150, visible: false);
        var goods = CreateText(x => x.GoodsName, "Изделие", width: 200, visible: false);
        var operation = CreateText(x => x.OperationName, "Операция", hidden: false);
        var employee = CreateText(x => x.EmployeeName, "Исполнитель", width: 200);
        var material = CreateText(x => x.MaterialName, "Использованный материал", width: 270);
        var quantity = CreateNumeric(x => x.Quantity, "Количество", width: 140, hidden: false, decimalDigits: 3);
        var salary = CreateCurrency(x => x.Salary, "Зарплата", width: 120);
        var double_rate = CreateBoolean(x => x.DoubleRate, "Двойная оплата", width: 100);

        CreateSummaryRow(VerticalPosition.Bottom, true)
            .AsSummary(quantity)
            .AsSummary(salary, SummaryColumnFormat.Currency);

        operation.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.Fill;
        quantity.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Right;

        AddColumns(new GridColumn[] { id, number, date, order_name, lot_name, goods, operation, employee, material, quantity, salary, double_rate });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [date] = ListSortDirection.Ascending
        });

        CreateGrouping()
            .Add(order_name)
            .Add(goods)
            .Register(date, "DateByDay", "По дням", (string ColumnName, object o) =>
            {
                var op = (OperationsPerformed)o;
                if (op.DocumentDate.HasValue)
                {
                    return op.DocumentDate.Value.ToShortDateString();
                }

                return "NONE";
            });
    }
}
