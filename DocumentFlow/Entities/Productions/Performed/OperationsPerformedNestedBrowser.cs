//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 12.06.2022
//
// Версия 2022.9.9
//  - добавлен столбец double_rate
// Версия 2023.1.8
//  - в конструктор добавлен параметр settings
// Версия 2023.1.22
//  - DocumentFlow.Settings.Infrastructure перемещено в DocumentFlow.Infrastructure.Settings
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Controls;
using DocumentFlow.Infrastructure.Settings;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.Productions.Performed;

public class OperationsPerformedNestedBrowser : BaseOperationsPerformedBrowser, IOperationsPerformedNestedBrowser
{
    public OperationsPerformedNestedBrowser(IOperationsPerformedRepository repository, IPageManager pageManager, IStandaloneSettings settings) 
        : base(repository, pageManager, settings: settings)
    {
        Toolbar.IconSize = ButtonIconSize.Small;

        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var date = CreateDateTime(x => x.DocumentDate, "Дата/время", hidden: false, width: 150);
        var operation = CreateText(x => x.operation_name, "Операция", hidden: false);
        var employee = CreateText(x => x.employee_name, "Исполнитель", width: 200);
        var material = CreateText(x => x.material_name, "Использованный материал", width: 270);
        var quantity = CreateNumeric(x => x.quantity, "Количество", width: 140, hidden: false, decimalDigits: 3);
        var salary = CreateCurrency(x => x.salary, "Зарплата", width: 120);
        var double_rate = CreateBoolean(x => x.double_rate, "Двойная оплата", width: 100);

        CreateSummaryRow(VerticalPosition.Bottom)
            .AsSummary(quantity)
            .AsSummary(salary, SummaryColumnFormat.Currency);

        operation.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
        quantity.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;

        AddColumns(new GridColumn[] { id, date, operation, employee, material, quantity, salary, double_rate });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [date] = ListSortDirection.Ascending
        });
    }
}
