//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 23.03.2019
// Time: 22:06
//-----------------------------------------------------------------------

namespace DocumentFlow.DataSchema.Types
{
    using System;
    using DocumentFlow.Controls;
    using DocumentFlow.DataSchema.Types.Core;

    [Tag("CheckBox")]
    public class F_CheckBox : BindingEditorControl<L_CheckBox, bool>
    {
        protected override bool GetDefaultValue() => false;

        protected override object GetValue() => Control.BoolValue;

        protected override void SetValue(object value)
        {
            Control.BoolValue = value == null ? DefaultValue : Convert.ToBoolean(value);
        }
    }
}
