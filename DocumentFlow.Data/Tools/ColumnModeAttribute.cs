//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 11.04.2023
//-----------------------------------------------------------------------

using DocumentFlow.Data.Enums;

namespace DocumentFlow.Data.Tools;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class ColumnModeAttribute : Attribute
{
    public AutoSizeColumnsMode AutoSizeColumnsMode { get; set; } = AutoSizeColumnsMode.None;
    public int Width { get; set; } = 0;
    public ColumnFormat Format { get; set; } = ColumnFormat.Default;
    public HorizontalAlignment Alignment { get; set; } = HorizontalAlignment.Left;
    public int DecimalDigits { get; set; } = -1;
}
