//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 13.06.2019
// Time: 21:05
//-----------------------------------------------------------------------

namespace DocumentFlow.DataSchema.Types
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;
    using Newtonsoft.Json;
    using NHibernate;
    using DocumentFlow.Controls;
    using DocumentFlow.DataSchema.Types.Core;

    [Tag("DateTime")]
    public class F_DateTimePicker : BindingEditorControl<L_DateTimePicker, DateTime>
    {
        [DefaultValue(false)]
        [JsonProperty("showcheck", DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool ShowCheckBox { get; set; }

        [DefaultValue(DateTimePickerFormat.Custom)]
        [JsonProperty("format", DefaultValueHandling = DefaultValueHandling.Populate)]
        public DateTimePickerFormat Format { get; set; }

        [DefaultValue("dd.MM.yyyy HH:mm:ss")]
        [JsonProperty("customformat", DefaultValueHandling = DefaultValueHandling.Populate)]
        public string CustomFormat { get; set; }

        protected override void DefaultCreateControl(ISession session)
        {
            base.DefaultCreateControl(session);
            Control.CustomFormat = CustomFormat;
            Control.Format = Format;
            Control.ShowCheckBox = ShowCheckBox;
        }

        protected override DateTime GetDefaultValue() => DateTime.Now;

        protected override object GetValue() => Control.Value;

        protected override void SetValue(object value)
        {
            Control.Value = value == null ? DefaultValue : Convert.ToDateTime(value);
        }
    }
}
