//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.09.2020
// Time: 19:48
//-----------------------------------------------------------------------

using System.Windows.Forms;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.Input.Enums;
using DocumentFlow.Code.Controls;
using DocumentFlow.Code.Core;
using DocumentFlow.Core;

namespace DocumentFlow.Code.Implementation
{
    public class GridNumericColumnData : GridColumnData, INumericColumn
    {
        public GridNumericColumnData(DataColumnType type, GridColumn column, ToolStripMenuItem toolStripItem) : base(type, column, toolStripItem) { }

        private GridNumericColumn NumericColumn => (GridNumericColumn)Owner;

        NumberFormatMode INumericColumn.FormatMode => EnumHelper.TransformEnum<NumberFormatMode, FormatMode>(NumericColumn.FormatMode);

        int INumericColumn.DecimalDigits
        {
            get => NumericColumn.NumberFormatInfo.NumberDecimalDigits;
            set => NumericColumn.NumberFormatInfo.NumberDecimalDigits = value;
        }

        string INumericColumn.Format 
        {
            get => Owner.Format;
            set => Owner.Format = value;
        }

        INumericColumn INumericColumn.SetDecimalDigits(int decimalDigits)
        {
            if (Owner is GridDecimalColumn)
            {
                NumericColumn.NumberFormatInfo.NumberDecimalDigits = decimalDigits;
                NumericColumn.NumberFormatInfo.PercentDecimalDigits = decimalDigits;
            }

            return this;
        }

        INumericColumn INumericColumn.SetGroupSizes(int[] groupSizes)
        {
            NumericColumn.NumberFormatInfo.NumberGroupSizes = groupSizes;
            NumericColumn.NumberFormatInfo.PercentGroupSizes = groupSizes;
            NumericColumn.NumberFormatInfo.CurrencyGroupSizes = groupSizes;
            return this;
        }

        INumericColumn INumericColumn.SetFormat(string format)
        {
            Owner.Format = format;
            return this;
        }
    }
}
