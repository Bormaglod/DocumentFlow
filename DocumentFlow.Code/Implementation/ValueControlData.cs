//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.01.2021
// Time: 18:33
//-----------------------------------------------------------------------

using System;
using System.Windows.Forms;

namespace DocumentFlow.Code.Implementation
{
    public class ValueControlData : ControlData, IValueControl, IDataName
    {
        public ValueControlData(Control control) : base(control) { }

        string IDataName.Name => ControlName;

        public object Value
        {
            get
            {
                if (Owner is IValuable edit)
                    return edit.Value;

                throw new Exception("Объект не реализует интерфейс IValuable. Присваивание значение не возможно.");
            }

            set
            {
                if (Owner is IValuable edit)
                    edit.Value = value;
            }
        }
    }
}
