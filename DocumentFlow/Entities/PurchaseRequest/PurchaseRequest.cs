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
// Версия 2022.12.11
//  - добавлено перечисление PurchaseState, свойства state, state_name и
//    PurchaseState, методы StateNameFromValue и StateFromValue
// Версия 2022.12.17
//  - добавлено поле prepayment
//  - добавлены поля receipt_payment и delivery_amount
//  - изменился метод get для поля executed и оно теперь read-only
//  - поле paid стало bool? и read-only
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;

using Humanizer;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.Input.Enums;

using System.Globalization;

namespace DocumentFlow.Entities.PurchaseRequestLib;

public enum PurchaseState { NotActive, Active, Canceled, Completed }

[Description("Заявка")]
public class PurchaseRequest : ShipmentDocument
{
    public decimal cost_order { get; protected set; }
    public bool tax_payer { get; protected set; }
    public int tax { get; protected set; }
    public decimal tax_value { get; protected set; }
    public decimal full_cost { get; protected set; }
    public decimal prepayment { get; protected set; }
    public string? note { get; set; }
    public decimal receipt_payment { get; protected set; }
    public decimal delivery_amount { get; protected set; }
    public bool executed { get => delivery_amount > 0; }
    public bool? paid 
    { 
        get
        {
            if (prepayment + receipt_payment == 0)
            {
                return false;
            }

            if (prepayment + receipt_payment == delivery_amount)
            {
                return true;
            }

            return null;
        }
    }

    [EnumType("purchase_state")]
    [DataOperation(DataOperation.Add | DataOperation.Update)]
    public string state { get; set; } = "not active";
    public string state_name => StateNameFromValue(PurchaseState);

    [Exclude]
    public PurchaseState PurchaseState
    {
        get { return Enum.Parse<PurchaseState>(state.Dehumanize()); }
        set { state = StateFromValue(value); }
    }

    public static string StateNameFromValue(PurchaseState state) => state switch
    {
        PurchaseState.NotActive => "Создана",
        PurchaseState.Active => "В работе",
        PurchaseState.Canceled => "Отменена",
        PurchaseState.Completed => "Выполнена",
        _ => throw new NotImplementedException()
    };

    public static string StateFromValue(PurchaseState state) => state.ToString().Humanize(LetterCasing.LowerCase);

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
