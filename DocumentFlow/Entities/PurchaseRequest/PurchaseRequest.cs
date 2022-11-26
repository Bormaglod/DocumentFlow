//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.02.2022
//
// Версия 2022.11.26
//  - добавлен метод CreateGridColumns
//  - добавлено поле executed возвращающее флаг налиличия поставок по
//    заказу
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.Input.Enums;

using System.Globalization;

namespace DocumentFlow.Entities.PurchaseRequestLib;

[Description("Заявка")]
public class PurchaseRequest : ShipmentDocument
{
    public decimal cost_order { get; protected set; }
    public bool tax_payer { get; protected set; }
    public int tax { get; protected set; }
    public decimal tax_value { get; protected set; }
    public decimal full_cost { get; protected set; }
    public decimal paid { get; protected set; }
    public string? note { get; set; }
    public bool executed { get; protected set; }

    public static void CreateGridColumns(Columns columns)
    {
        var doc_date = new GridDateTimeColumn()
        {
            MappingName = "document_date",
            HeaderText = "Дата",
            Pattern = DateTimePattern.LongDate,
            Width = 120
        };

        NumberFormatInfo numberFormat = (NumberFormatInfo)Application.CurrentCulture.NumberFormat.Clone();
        numberFormat.NumberDecimalDigits = 0;

        var doc_number = new GridNumericColumn()
        {
            MappingName = "document_number",
            HeaderText = "Номер",
            FormatMode = FormatMode.Numeric,
            NumberFormatInfo = numberFormat,
            Width = 80
        };

        var contractor = new GridTextColumn()
        {
            MappingName = "contractor_name",
            HeaderText = "Контрагент"
        };

        NumberFormatInfo costFormat = (NumberFormatInfo)Application.CurrentCulture.NumberFormat.Clone();
        costFormat.NumberDecimalDigits = 2;

        var full_cost = new GridNumericColumn()
        {
            MappingName = "full_cost",
            HeaderText = "Сумма заказа",
            FormatMode = FormatMode.Currency,
            NumberFormatInfo = costFormat,
            Width = 150
        };

        columns.Add(doc_date);
        columns.Add(doc_number);
        columns.Add(contractor);
        columns.Add(full_cost);

        contractor.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
        full_cost.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
    }
}
