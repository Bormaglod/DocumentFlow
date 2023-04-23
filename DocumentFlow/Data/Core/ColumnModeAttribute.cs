//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 11.04.2023
//-----------------------------------------------------------------------

using Syncfusion.WinForms.DataGrid.Enums;

namespace DocumentFlow.Data.Core;

public enum ColumnFormat { Default, Currency, Progress }

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class ColumnModeAttribute : Attribute
{
    public AutoSizeColumnsMode AutoSizeColumnsMode { get; set; } = AutoSizeColumnsMode.None;
    public int Width { get; set; } = 0;
    public ColumnFormat Format { get; set; } = ColumnFormat.Default;
    public HorizontalAlignment Alignment { get; set; } = HorizontalAlignment.Left;
}
