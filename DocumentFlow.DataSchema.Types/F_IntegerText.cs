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

    [Tag("Integer")]
    public class F_IntegerText : BindingEditorControl<L_IntegerTextBox, int>
    {
        [DefaultValue(long.MinValue)]
        [JsonProperty("min", DefaultValueHandling = DefaultValueHandling.Populate)]
        public long MinValue { get; set; }

        [DefaultValue(long.MaxValue)]
        [JsonProperty("max", DefaultValueHandling = DefaultValueHandling.Populate)]
        public long MaxValue { get; set; }

        [DefaultValue(false)]
        [JsonProperty("allow-leading-zeros", DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool AllowLeadingZeros { get; set; }

        [DefaultValue(" ")]
        [JsonProperty("group-separator", DefaultValueHandling = DefaultValueHandling.Populate)]
        public string NumberGroupSeparator { get; set; }

        [JsonProperty("group-sizes")]
        public IList<int> NumberGroupSizes { get; set; } = new List<int>();

        protected override void DefaultCreateControl(ISession session)
        {
            base.DefaultCreateControl(session);
            Control.MinValue = MinValue;
            Control.MaxValue = MaxValue;
            Control.AllowLeadingZeros = AllowLeadingZeros;
            Control.NumberGroupSeparator = NumberGroupSeparator;
            Control.NumberGroupSizes = NumberGroupSizes.ToArray();
        }

        protected override int GetDefaultValue() => 0;

        protected override object GetValue() => Control.IntegerValue;

        protected override void SetValue(object value)
        {
            Control.IntegerValue = value == null ? DefaultValue : Convert.ToInt64(value);
        }
    }
}
