//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.08.2022
//
// Версия 2023.1.28
//  - добавлен метод CreateGridColumns
//
//-----------------------------------------------------------------------

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.Input.Enums;

using System.Globalization;

namespace DocumentFlow.Entities.Wages;

public class BasePayroll : BillingDocument
{
    /// <summary>
    /// Возвращает сумму заработной платы расчитанной в 1с.
    /// </summary>
    public decimal Wage { get; protected set; }

    public static void CreateGridColumns(IList<GridColumn> columns)
    {
        var billing_range = new GridTextColumn()
        {
            MappingName = nameof(BillingRange),
            HeaderText = "Расчётный период",
            Width = 150
        };

        var employee_names_text = new GridTextColumn()
        {
            MappingName = nameof(EmployeeNamesText),
            HeaderText = "Сотрудники"
        };

        NumberFormatInfo costFormat = (NumberFormatInfo)Application.CurrentCulture.NumberFormat.Clone();
        costFormat.NumberDecimalDigits = 2;

        var wage = new GridNumericColumn()
        {
            MappingName = nameof(Wage),
            HeaderText = "Сумма",
            FormatMode = FormatMode.Currency,
            NumberFormatInfo = costFormat,
            Width = 150
        };

        columns.Add(billing_range);
        columns.Add(employee_names_text);
        columns.Add(wage);

        employee_names_text.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
        wage.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
    }
}
