//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 21.03.2019
// Time: 22:44
//-----------------------------------------------------------------------

using System;
using System.Globalization;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.Input.Enums;

namespace DocumentFlow.Code.Controls
{
    public class GridDecimalColumn : GridNumericColumn
    {
        protected override object GetFormattedValue(object record, object value)
        {
            NumberFormatInfo provider = NumberFormatInfo ?? CultureInfo.CurrentUICulture.NumberFormat;
            if (value == null || DBNull.Value.Equals(value))
            {
                if (AllowNull)
                    return value;

                return string.Empty;
            }

            if (decimal.TryParse(value.ToString(), out decimal result))
            {
                if (result < decimal.MinValue)
                {
                    result = decimal.MinValue;
                }

                if (result > decimal.MaxValue)
                {
                    result = decimal.MaxValue;
                }

                if (FormatMode == FormatMode.Currency)
                {
                    return string.Format(provider, "{0:c}", result);
                }

                if (FormatMode == FormatMode.Percent)
                {
                    return string.Format(provider, "{0:P}", result / 100m);
                }

                return string.Format(provider, "{0:n}", result);
            }

            return value;
        }
    }
}
