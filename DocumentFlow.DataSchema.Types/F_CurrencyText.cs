//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 23.03.2019
// Time: 21:23
//-----------------------------------------------------------------------

namespace DocumentFlow.DataSchema.Types
{
    using System;
    using NHibernate;
    using DocumentFlow.Controls;
    using DocumentFlow.DataSchema.Types.Core;

    [Tag("Currency")]
    public class F_CurrencyText : BindingEditorControl<L_CurrencyTextBox, decimal>
    {
        protected override void DefaultCreateControl(ISession session)
        {
            base.DefaultCreateControl(session);
            Control.DecimalValueChanged += Control_DecimalValueChanged;
        }

        protected override decimal GetDefaultValue() => 0;

        protected override object GetValue() => Control.DecimalValue;

        protected override void SetValue(object value)
        {
            Control.DecimalValue = value == null ? DefaultValue : Convert.ToDecimal(value);
        }

        private void Control_DecimalValueChanged(object sender, EventArgs e)
        {
            OnValueChanged();
        }
    }
}
