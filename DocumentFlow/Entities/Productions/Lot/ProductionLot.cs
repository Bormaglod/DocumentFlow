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
    private string _state = "created";

    public Guid calculation_id { get; set; }
    public decimal quantity { get; set; }
    public int order_number { get; protected set; }
    public DateTime order_date { get; protected set; }
    public Guid goods_id { get; protected set; }
    public string goods_name { get; protected set; } = string.Empty;
    public string calculation_name { get; protected set; } = string.Empty;
    public int execute_percent { get; protected set; }

    [DataOperation(DataOperation.Add | DataOperation.Update)]
    [EnumType("lot_state")]
    public string state 
    {
        get => _state;
        set
        {
            _state = value;
            NotifyPropertyChanged();
        }
    }

    public string state_name => StateNameFromValue(LotState);

    public LotState LotState => Enum.Parse<LotState>(state.Pascalize());

    public static string StateNameFromValue(LotState state) => state switch
    {
        LotState.Created => "Создана",
        LotState.Production => "В производстве",
        LotState.Completed => "Выполнена",
        _ => throw new NotImplementedException()
    };

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

        var goods = new GridTextColumn()
        {
            MappingName = "goods_name",
            HeaderText = "Изделие"
        };

        NumberFormatInfo format = (NumberFormatInfo)Application.CurrentCulture.NumberFormat.Clone();
        format.NumberDecimalDigits = 0;

        var quantity = new GridNumericColumn()
        {
            MappingName = "quantity",
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
