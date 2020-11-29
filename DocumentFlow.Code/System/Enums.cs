//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 27.09.2020
// Time: 13:13
//-----------------------------------------------------------------------

namespace DocumentFlow.Code.System
{
    public enum BrowserMode { Main, Dependent }

    public enum DataType { None, Directory, Document, Report }

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

    public enum HorizontalAlignmentText
    {
        //
        // Сводка:
        //     Объект или текст выравнивается по левой части элемента управления.
        Left = 0,
        //
        // Сводка:
        //     Объект или текст выравнивается по правому краю элемента управления.
        Right = 1,
        //
        // Сводка:
        //     Объект или текст выравнивается по центру элемента управления.
        Center = 2
    }

    public enum ButtonDisplayStyle
    {
        //
        // Сводка:
        //     Указывает, что изображения и текста для отображения для этого System.Windows.Forms.ToolStripItem.
        None = 0,
        //
        // Сводка:
        //     Указывает, что только текст для отображения для этого System.Windows.Forms.ToolStripItem.
        Text = 1,
        //
        // Сводка:
        //     Указывает, что только изображения к просмотру для этого System.Windows.Forms.ToolStripItem.
        Image = 2,
        //
        // Сводка:
        //     Указывает, что изображение и текст для отображения для этого System.Windows.Forms.ToolStripItem.
        ImageAndText = 3
    }

    public enum ButtonIconSize { Small, Large }

    public enum SortDirection
    {
        //
        // Сводка:
        //     Сортировка по возрастанию.
        Ascending = 0,
        //
        // Сводка:
        //     Сортировка по убыванию.
        Descending = 1
    }

    public enum DockStyleControl
    {
        //
        // Сводка:
        //     Элемент управления не закреплен.
        None = 0,
        //
        // Сводка:
        //     Верхний край элемента управления присоединяется к верхней части содержащего его
        //     элемента управления.
        Top = 1,
        //
        // Сводка:
        //     Нижний край элемента управления закрепляется в нижней части содержащего его элемента
        //     управления.
        Bottom = 2,
        //
        // Сводка:
        //     Левый край элемента управления закрепляется левого края содержащего его элемента
        //     управления.
        Left = 3,
        //
        // Сводка:
        //     Правый край элемента управления закрепляется правого края содержащего его элемента
        //     управления.
        Right = 4,
        //
        // Сводка:
        //     Элемент управления края ко всем краям содержащего его элемента управления, а
        //     их размеры изменяются соответствующим образом.
        Fill = 5
    }

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
