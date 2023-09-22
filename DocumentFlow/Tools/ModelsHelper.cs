//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 25.08.2023
//-----------------------------------------------------------------------

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.Input.Enums;
using DocumentFlow.Data.Models;
using System.Globalization;
using DocumentFlow.Data;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DocumentFlow.Tools;

public static class ModelsHelper
{
    public static void CreateProductinOrderColumns(IList<GridColumn> columns)
    {
        CreateDefaultDocumentColumns(columns);

        var contractor = new GridTextColumn()
        {
            MappingName = nameof(ProductionOrder.ContractorName),
            HeaderText = "Контрагент"
        };

        NumberFormatInfo costFormat = (NumberFormatInfo)Application.CurrentCulture.NumberFormat.Clone();
        costFormat.NumberDecimalDigits = 2;

        var full_cost = new GridNumericColumn()
        {
            MappingName = nameof(ProductionOrder.FullCost),
            HeaderText = "Сумма заказа",
            FormatMode = FormatMode.Currency,
            NumberFormatInfo = costFormat,
            Width = 150
        };

        columns.Add(contractor);
        columns.Add(full_cost);

        contractor.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
        full_cost.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
    }

    public static void CreatePurchaseRequestColumns(IList<GridColumn> columns)
    {
        CreateDefaultDocumentColumns(columns);

        var contract = new GridTextColumn()
        {
            MappingName = nameof(PurchaseRequest.ContractName),
            HeaderText = "Договор"
        };

        NumberFormatInfo costFormat = (NumberFormatInfo)Application.CurrentCulture.NumberFormat.Clone();
        costFormat.NumberDecimalDigits = 2;

        var full_cost = new GridNumericColumn()
        {
            MappingName = nameof(PurchaseRequest.FullCost),
            HeaderText = "Сумма заказа",
            FormatMode = FormatMode.Currency,
            NumberFormatInfo = costFormat,
            Width = 150
        };

        columns.Add(contract);
        columns.Add(full_cost);

        contract.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
        full_cost.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
    }

    public static void CreateProductionLotColumns(IList<GridColumn> columns, bool freeQuantityVisible)
    {
        CreateDefaultDocumentColumns(columns);

        var goods = new GridTextColumn()
        {
            MappingName = nameof(ProductionLot.GoodsName),
            HeaderText = "Изделие"
        };

        NumberFormatInfo format = (NumberFormatInfo)Application.CurrentCulture.NumberFormat.Clone();
        format.NumberDecimalDigits = 0;

        var quantity = new GridNumericColumn()
        {
            MappingName = nameof(ProductionLot.Quantity),
            HeaderText = "Количество",
            FormatMode = FormatMode.Numeric,
            NumberFormatInfo = format,
            Width = 100
        };

        
        columns.Add(goods);
        columns.Add(quantity);

        if (freeQuantityVisible)
        {
            var freeQuantity = new GridNumericColumn()
            {
                MappingName = nameof(ProductionLot.FreeQuantity),
                HeaderText = "Остаток",
                FormatMode = FormatMode.Numeric,
                NumberFormatInfo = format,
                Width = 100
            };

            columns.Add(freeQuantity);
        }

        goods.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
        quantity.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
    }

    public static void CreatePayrollColumns(IList<GridColumn> columns)
    {
        var billing_range = new GridTextColumn()
        {
            MappingName = nameof(BillingRange),
            HeaderText = "Расчётный период",
            Width = 150
        };

        var employee_names_text = new GridTextColumn()
        {
            MappingName = nameof(BasePayroll.EmployeeNamesText),
            HeaderText = "Сотрудники"
        };

        NumberFormatInfo costFormat = (NumberFormatInfo)Application.CurrentCulture.NumberFormat.Clone();
        costFormat.NumberDecimalDigits = 2;

        var wage = new GridNumericColumn()
        {
            MappingName = nameof(BasePayroll.Wage),
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

    private static void CreateDefaultDocumentColumns(IList<GridColumn> columns)
    {
        var doc_date = new GridDateTimeColumn()
        {
            MappingName = nameof(BaseDocument.DocumentDate),
            HeaderText = "Дата",
            Pattern = DateTimePattern.LongDate,
            Width = 120
        };

        NumberFormatInfo numberFormat = (NumberFormatInfo)Application.CurrentCulture.NumberFormat.Clone();
        numberFormat.NumberDecimalDigits = 0;

        var doc_number = new GridNumericColumn()
        {
            MappingName = nameof(BaseDocument.DocumentNumber),
            HeaderText = "Номер",
            FormatMode = FormatMode.Numeric,
            NumberFormatInfo = numberFormat,
            Width = 80
        };

        columns.Add(doc_date);
        columns.Add(doc_number);
    }
}
