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

using DocumentFlow.Data;
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
    public decimal CostOrder { get; protected set; }
    public bool TaxPayer { get; protected set; }
    public int Tax { get; protected set; }
    public decimal TaxValue { get; protected set; }
    public decimal FullCost { get; protected set; }
    public decimal Prepayment { get; protected set; }
    public string? Note { get; set; }
    public decimal ReceiptPayment { get; protected set; }
    public decimal DeliveryAmount { get; protected set; }
    public bool Executed { get => DeliveryAmount > 0; }
    public bool? Paid 
    { 
        get
        {
            if (Prepayment + ReceiptPayment == 0)
            {
                return false;
            }

            if (Prepayment + ReceiptPayment == DeliveryAmount)
            {
                return true;
            }

            return null;
        }
    }

    [EnumType("purchase_state")]
    [DataOperation(DataOperation.Add | DataOperation.Update)]
    public string State { get; set; } = "not active";
    public string StateName => StateNameFromValue(PurchaseState);

    [Exclude]
    public PurchaseState PurchaseState
    {
        get { return Enum.Parse<PurchaseState>(State.Dehumanize()); }
        set { State = StateFromValue(value); }
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
            MappingName = "DocumentDate",
            HeaderText = "Дата",
            Pattern = DateTimePattern.LongDate,
            Width = 120
        };

        NumberFormatInfo numberFormat = (NumberFormatInfo)Application.CurrentCulture.NumberFormat.Clone();
        numberFormat.NumberDecimalDigits = 0;

        var doc_number = new GridNumericColumn()
        {
            MappingName = "DocumentNumber",
            HeaderText = "Номер",
            FormatMode = FormatMode.Numeric,
            NumberFormatInfo = numberFormat,
            Width = 80
        };

        var contractor = new GridTextColumn()
        {
            MappingName = "ContractorName",
            HeaderText = "Контрагент"
        };

        NumberFormatInfo costFormat = (NumberFormatInfo)Application.CurrentCulture.NumberFormat.Clone();
        costFormat.NumberDecimalDigits = 2;

        var full_cost = new GridNumericColumn()
        {
            MappingName = "FullCost",
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
