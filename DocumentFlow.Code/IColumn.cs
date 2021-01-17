//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 27.09.2020
// Time: 19:08
//-----------------------------------------------------------------------

using System.Windows.Forms;
using DocumentFlow.Code.Core;

namespace DocumentFlow.Code
{
    public interface IColumn
    {
        string FieldName { get; }
        DataColumnType ColumnType { get; }
        double Width { get; set; }
        SizeColumnsMode AutoSizeColumnsMode { get; set; }
        bool Visible { get; set; }
        bool Visibility { get; set; }
        bool AllowResizing { get; set; }
        bool AllowGrouping { get; set; }
        HorizontalAlignment HorizontalAlignment { get; set; }
        string BackColor { get; set; }
        string TextColor { get; set; }
        string NegativeValueColor { get; set; }
        bool Hideable { get; set; }

        IColumn SetWidth(double width);
        IColumn SetVisible(bool visible);
        IColumn SetVisibility(bool visibility);
        IColumn SetAllowGrouping(bool allowGrouping);
        IColumn SetHideable(bool hideable);
        IColumn SetAutoSizeColumnsMode(SizeColumnsMode mode);
        IColumn SetHorizontalAlignment(HorizontalAlignment alignment);
        IColumn SetBackgroundColor(string color);
        IColumn SetTextColor(string color);
    }
}
