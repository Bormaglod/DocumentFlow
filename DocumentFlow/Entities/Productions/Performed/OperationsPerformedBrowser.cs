//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 11.05.2022
//
// Версия 2022.9.9
//  - добавлен столбец double_rate
// Версия 2023.1.8
//  - в конструктор добавлен параметр settings
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//  - DocumentFlow.Settings.Infrastructure перемещено в DocumentFlow.Infrastructure.Settings
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Controls;
using DocumentFlow.Infrastructure.Data;
using DocumentFlow.Infrastructure.Settings;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.Productions.Performed;

public class OperationsPerformedBrowser : BaseOperationsPerformedBrowser, IOperationsPerformedBrowser
{
    public OperationsPerformedBrowser(IOperationsPerformedRepository repository, IPageManager pageManager, IDocumentFilter filter, IStandaloneSettings settings)
        : base(repository, pageManager, filter: filter, settings: settings)
    {
        AllowGrouping();

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

        CreateGroups()
            .Add(order_name)
            .Add(goods);

        operation.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
        quantity.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;

        AddColumns(new GridColumn[] { id, number, date, order_name, lot_name, goods, operation, employee, material, quantity, salary, double_rate });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [date] = ListSortDirection.Ascending
        });
    }
}
