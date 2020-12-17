//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.09.2020
// Time: 17:40
//-----------------------------------------------------------------------

using System.Drawing;
using System.Windows.Forms;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using DocumentFlow.Code.System;
using DocumentFlow.Core;

namespace DocumentFlow.Code.Implementation
{
    public class GridColumnData : IColumn
    {
        private readonly DataColumnType columnType;
        private readonly GridColumn owner;
        private bool hideable;
        private bool visible;
        private bool visibility;

        public GridColumnData(DataColumnType type, GridColumn column, ToolStripMenuItem toolStripItem)
        {
            columnType = type;
            owner = column;
            MenuItem = toolStripItem;

            column.Visible = true;
            column.AllowResizing = true;
            column.AllowGrouping = false;

            hideable = true;
            visible = true;
            visibility = true;
        }

        public ToolStripMenuItem MenuItem { get; }

        protected GridColumn Owner => owner;

        string IColumn.FieldName => owner.MappingName;

        DataColumnType IColumn.ColumnType => columnType;

        double IColumn.Width
        {
            get => owner.Width;
            set => owner.Width = value;
        }

        SizeColumnsMode IColumn.AutoSizeColumnsMode
        {
            get => EnumHelper.TransformEnum<SizeColumnsMode, AutoSizeColumnsMode>(owner.AutoSizeColumnsMode);
            set => owner.AutoSizeColumnsMode = EnumHelper.TransformEnum<AutoSizeColumnsMode, SizeColumnsMode>(value);
        }

        bool IColumn.Visible
        {
            get => visible;
            set
            {
                visible = value;
                owner.Visible = value;
                MenuItem.Checked = value;
            }
        }

        bool IColumn.Visibility
        {
            get => visibility;
            set
            {
                visibility = value;
                if (visibility)
                {
                    owner.Visible = visible;
                    MenuItem.Visible = visibility;
                }
                else
                {
                    owner.Visible = false;
                    MenuItem.Visible = false;
                }
            }
        }

        bool IColumn.AllowResizing
        {
            get => owner.AllowResizing;
            set => owner.AllowResizing = value;
        }

        bool IColumn.AllowGrouping
        {
            get => owner.AllowGrouping;
            set => owner.AllowGrouping = value;
        }

        HorizontalAlignment IColumn.HorizontalAlignment
        {
            get => owner.CellStyle.HorizontalAlignment;
            set => owner.CellStyle.HorizontalAlignment = value;
        }

        string IColumn.BackColor
        {
            get => ColorTranslator.ToHtml(owner.CellStyle.BackColor);
            set
            {
                owner.CellStyle.BackColor = ColorTranslator.FromHtml(value);
                owner.HeaderStyle.BackColor = ColorTranslator.FromHtml(value);
            }
        }

        string IColumn.TextColor
        {
            get => ColorTranslator.ToHtml(owner.CellStyle.TextColor);
            set
            {
                owner.CellStyle.TextColor = ColorTranslator.FromHtml(value);
                owner.HeaderStyle.TextColor = ColorTranslator.FromHtml(value);
            }
        }

        string IColumn.NegativeValueColor { get; set; }

        bool IColumn.Hideable
        {
            get => hideable;
            set
            {
                hideable = value;
                MenuItem.Enabled = value;
            }
        }

        IColumn IColumn.SetWidth(double width)
        {
            IColumn column = this;
            column.Width = width;
            return column;
        }

        IColumn IColumn.SetVisible(bool visible)
        {
            IColumn column = this;
            column.Visible = visible;
            return column;
        }

        IColumn IColumn.SetVisibility(bool visibility)
        {
            IColumn column = this;
            column.Visibility = visibility;
            return column;
        }

        IColumn IColumn.SetAllowGrouping(bool allowGrouping)
        {
            IColumn column = this;
            column.AllowGrouping = allowGrouping;
            return column;
        }

        IColumn IColumn.SetHideable(bool hideable)
        {
            IColumn column = this;
            column.Hideable = hideable;
            return column;
        }

        IColumn IColumn.SetAutoSizeColumnsMode(SizeColumnsMode mode)
        {
            IColumn column = this;
            column.AutoSizeColumnsMode = mode;
            return column;
        }

        IColumn IColumn.SetHorizontalAlignment(HorizontalAlignment alignment)
        {
            IColumn column = this;
            column.HorizontalAlignment = alignment;
            return column;
        }

        IColumn IColumn.SetBackgroundColor(string color)
        {
            IColumn column = this;
            column.BackColor = color;
            return column;
        }

        IColumn IColumn.SetTextColor(string color)
        {
            IColumn column = this;
            column.TextColor = color;
            return column;
        }
    }
}
