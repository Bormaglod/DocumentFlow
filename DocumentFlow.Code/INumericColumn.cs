//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 03.12.2020
// Time: 23:35
//-----------------------------------------------------------------------

using DocumentFlow.Code.Core;

namespace DocumentFlow.Code
{
    public interface INumericColumn : IColumn
    {
        NumberFormatMode FormatMode { get; }
        int DecimalDigits { get; set; }
        string Format { get; set; }
        INumericColumn SetDecimalDigits(int decimalDigits);
        INumericColumn SetGroupSizes(int[] groupSizes);
        INumericColumn SetFormat(string format);
    }
}
