//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 26.03.2020
// Time: 23:07
//-----------------------------------------------------------------------

namespace DocumentFlow.Controls.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Syncfusion.WinForms.DataGrid;
    using DocumentFlow.DataSchema;

    public class EnumFormatter : IDataGridFormatProvider
    {
        private IEnumerable<EnumValues> enums;

        public EnumFormatter(IEnumerable<EnumValues> enums)
        {
            this.enums = enums;
        }

        public object Format(string format, GridColumnBase gridColumn, object record, object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException((value == null) ? "format" : "args");
            }

            if (gridColumn is GridTextColumn)
            {
                int enum_value = Convert.ToInt32(value);
                EnumValues e = enums.FirstOrDefault(x => x.Id == enum_value);
                if (e != null)
                    return e.Caption;
            }

            return value.ToString();
        }


        public object GetFormat(Type formatType)
        {
            return this;
        }

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            throw new NotImplementedException();
        }
    }
}
