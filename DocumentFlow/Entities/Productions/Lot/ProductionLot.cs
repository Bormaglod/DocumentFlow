//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.04.2022
//
// Версия 2022.8.28
//  - добавлены методы/свойства для поддержки поля state таблицы
//    production_lot
//  - добавлено свойство execute_percent
// Версия 2022.6.16
//  - Атрибут DataOperation заменен на AllowOperation
//
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;

using Humanizer;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.Input.Enums;

using System.Globalization;


namespace DocumentFlow.Entities.Productions.Lot;

public enum LotState { Created, Production, Completed }

[Description("Партия")]
public class ProductionLot : AccountingDocument
{
    private string state = "created";

    public Guid CalculationId { get; set; }
    public decimal Quantity { get; set; }
    public int OrderNumber { get; protected set; }
    public DateTime OrderDate { get; protected set; }
    public Guid GoodsId { get; protected set; }
    public string GoodsName { get; protected set; } = string.Empty;
    public string CalculationName { get; protected set; } = string.Empty;
    public int ExecutePercent { get; protected set; }

    [AllowOperation(DataOperation.Add | DataOperation.Update)]
    [EnumType("lot_state")]
    public string State 
    {
        get => state;
        set
        {
            state = value;
            NotifyPropertyChanged();
        }
    }

    public string StateName => StateNameFromValue(LotState);

    public LotState LotState => Enum.Parse<LotState>(State.Pascalize());

    public static string StateNameFromValue(LotState state) => state switch
    {
        LotState.Created => "Создана",
        LotState.Production => "В производстве",
        LotState.Completed => "Выполнена",
        _ => throw new NotImplementedException()
    };

    public static void CreateGridColumns(IList<GridColumn> columns)
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

        var goods = new GridTextColumn()
        {
            MappingName = "GoodsName",
            HeaderText = "Изделие"
        };

        NumberFormatInfo format = (NumberFormatInfo)Application.CurrentCulture.NumberFormat.Clone();
        format.NumberDecimalDigits = 0;

        var quantity = new GridNumericColumn()
        {
            MappingName = "Quantity",
            HeaderText = "Количество",
            FormatMode = FormatMode.Numeric,
            NumberFormatInfo = format,
            Width = 150
        };

        columns.Add(doc_date);
        columns.Add(doc_number);
        columns.Add(goods);
        columns.Add(quantity);

        goods.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
        quantity.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
    }
}
