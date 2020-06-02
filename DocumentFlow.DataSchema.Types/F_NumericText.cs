//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 17.03.2019
// Time: 17:53
//-----------------------------------------------------------------------

namespace DocumentFlow.DataSchema.Types
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using Newtonsoft.Json;
    using NHibernate;
    using DocumentFlow.Controls;
    using DocumentFlow.DataSchema.Types.Core;

    [Tag("Numeric")]
    public class F_NumericText : BindingEditorControl<L_NumericTextBox, decimal>
    {
        [DefaultValue(2)]
        [JsonProperty("decimal-digits", DefaultValueHandling = DefaultValueHandling.Populate)]
        public int NumberDecimalDigits { get; set; }

        [DefaultValue(",")]
        [JsonProperty("decimal-separator", DefaultValueHandling = DefaultValueHandling.Populate)]
        public string NumberDecimalSeparator { get; set; }

        [DefaultValue(" ")]
        [JsonProperty("group-separator", DefaultValueHandling = DefaultValueHandling.Populate)]
        public string NumberGroupSeparator { get; set; }

        [JsonProperty("group-sizes")]
        public IList<int> NumberGroupSizes { get; set; } = new List<int>();

        protected override void DefaultCreateControl(ISession session)
        {
            base.DefaultCreateControl(session);
            Control.NumberDecimalDigits = NumberDecimalDigits;
            Control.NumberDecimalSeparator = NumberDecimalSeparator;
            Control.NumberGroupSeparator = NumberGroupSeparator;
            Control.NumberGroupSizes = NumberGroupSizes.ToArray();
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
