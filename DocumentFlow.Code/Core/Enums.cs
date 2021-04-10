//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 27.09.2020
// Time: 13:13
//-----------------------------------------------------------------------

namespace DocumentFlow.Code.Core
{
    public enum BrowserMode { Main, Dependent }

    public enum DataType { None, Directory, Document, Report }

    public enum IntegerLength { Int32, Int64 }

    public enum NumberFormatMode
    {
        //
        // Сводка:
        //     Displays numeric values.
        Numeric = 0,
        //
        // Сводка:
        //     Displays the percent value with percent symbol.
        Percent = 1,
        //
        // Сводка:
        //     Displays the value in currency format with currency symbol.
        Currency = 2
    }

    public enum GroupVerticalPosition
    {
        //
        // Сводка:
        //     Specifies the TableSummaryRow or UnboundRow is positioned at bottom.
        Bottom = 0,
        //
        // Сводка:
        //     Specifies the TableSummaryRow or UnboundRow is positioned at top.
        Top = 1
    }

    public enum RowSummaryType
    {
        //
        // Сводка:
        //     Specify the count aggregate for the summary column.
        CountAggregate = 0,
        //
        // Сводка:
        //     Specify the double value aggregate for the summary column.
        DoubleAggregate = 1,
        //
        // Сводка:
        //     Specify integer value aggregate for the summary column.
        Int32Aggregate = 2,
        //
        // Сводка:
        //     Specify custom aggregate for the summary column which implement Syncfusion.Data.ISummaryAggregate
        //     or Syncfusion.Data.ISummaryExpressionAggregate interface to delegate the summary
        //     computation.
        Custom = 3
    }

    public enum DataColumnType { Integer, Numeric, Text, Image, Memo, Boolean, Date, Enums, Combobox, Progress }

    public enum SizeColumnsMode
    {
        //
        // Сводка:
        //     No sizing. Default column width or defined width set to column.
        None = 0,
        //
        // Сводка:
        //     Calculates the width of column based on header and cell contents. So that header
        //     and cell content’s are not truncated.
        AllCells = 1,
        //
        // Сводка:
        //     Applies AutoSizeColumnsMode.AllCells width to all the columns except last column
        //     which is visible and the remaining width from total width of SfDataGrid is set
        //     to last column.
        LastColumnFill = 2,
        //
        // Сводка:
        //     Applies AutoSizeColumnsMode.AllCells width to all the columns except last column
        //     which is visible and sets the maximum between last column auto spacing width
        //     and remaining width to last column.
        AllCellsWithLastColumnFill = 3,
        //
        // Сводка:
        //     Calculates the width of column based on cell contents. So that cell content’s
        //     are not truncated.
        AllCellsExceptHeader = 4,
        //
        // Сводка:
        //     Calculates the width of column based on header content. So that header content
        //     is not truncated.
        ColumnHeader = 5,
        //
        // Сводка:
        //     Divides the total width equally for columns.
        Fill = 6
    }


    public enum ButtonIconSize { Small, Large }

    public enum DateTimeFormat
    {
        //
        // Сводка:
        //     System.Windows.Forms.DateTimePicker Элемент управления отображает значение даты
        //     и времени в длинном формате, установленные в операционной системе пользователя.
        Long = 1,
        //
        // Сводка:
        //     System.Windows.Forms.DateTimePicker Элемент управления отображает значение даты
        //     и времени в формате короткой даты, установленном операционной системой пользователя.
        Short = 2,
        //
        // Сводка:
        //     System.Windows.Forms.DateTimePicker Элемент управления отображает значение даты
        //     и времени в формате времени, настроенном в операционной системе пользователя.
        Time = 4,
        //
        // Сводка:
        //     System.Windows.Forms.DateTimePicker Элемент управления отображает значение даты
        //     и времени в пользовательском формате. Для получения дополнительной информации
        //     см. System.Windows.Forms.DateTimePicker.CustomFormat.
        Custom = 8
    }

    public enum CommandMethod { Sql, Embedded, UserDefined }
}
