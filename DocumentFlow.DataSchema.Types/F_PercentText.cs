//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.05.2019
// Time: 20:25
//-----------------------------------------------------------------------

namespace DocumentFlow.DataSchema.Types
{
    using System;
    using System.ComponentModel;
    using Newtonsoft.Json;
    using NHibernate;
    using DocumentFlow.Controls;
    using DocumentFlow.DataSchema.Types.Core;

    [Tag("Percent")]
    public class F_PercentText : BindingEditorControl<L_PercentTextBox, double>
    {
        [DefaultValue(0)]
        [JsonProperty("decimal-digits", DefaultValueHandling = DefaultValueHandling.Populate)]
        public int PercentDecimalDigits { get; set; }

        [DefaultValue(100)]
        [JsonProperty("max", DefaultValueHandling = DefaultValueHandling.Populate)]
        public double MaxValue { get; set; }

        [DefaultValue(-100)]
        [JsonProperty("min", DefaultValueHandling = DefaultValueHandling.Populate)]
        public double MinValue { get; set; }

        protected override void DefaultCreateControl(ISession session)
        {
            base.DefaultCreateControl(session);
            Control.PercentDecimalDigits = PercentDecimalDigits;
            Control.MaxValue = MaxValue;
            Control.MinValue = MinValue;
        }

        protected override double GetDefaultValue() => 0;

        protected override object GetValue() => Control.PercentValue;

        protected override void SetValue(object value)
        {
            Control.PercentValue = value == null ? DefaultValue : Convert.ToDouble(value);
        }
    }
}
