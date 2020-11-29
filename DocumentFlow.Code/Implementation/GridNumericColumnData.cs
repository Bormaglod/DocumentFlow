//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.09.2020
// Time: 19:48
//-----------------------------------------------------------------------

using System.Windows.Forms;
using Syncfusion.WinForms.DataGrid;
using DocumentFlow.Code.System;

namespace DocumentFlow.Code.Implementation
{
    public class GridNumericColumnData : GridColumnData, INumericColumn
    {
        private readonly NumberFormatMode formatMode;
        private readonly int decimalDigits;

        public GridNumericColumnData(DataColumnType type, NumberFormatMode formatMode, int decimalDigits, GridColumn column, ToolStripMenuItem toolStripItem) : base(type, column, toolStripItem)
        {
            this.formatMode = formatMode;
            this.decimalDigits = decimalDigits;
        }

        NumberFormatMode INumericColumn.FormatMode => formatMode;

        int INumericColumn.DecimalDigits => decimalDigits;
    }
}
