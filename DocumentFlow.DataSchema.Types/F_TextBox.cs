//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 15.03.2019
// Time: 20:29
//-----------------------------------------------------------------------

namespace DocumentFlow.DataSchema.Types
{
    using System.ComponentModel;
    using Newtonsoft.Json;
    using NHibernate;
    using DocumentFlow.Core;
    using DocumentFlow.Controls;
    using DocumentFlow.DataSchema.Types.Core;

    [Tag("TextBox")]
    public class F_TextBox : BindingEditorControl<L_TextBox, string>
    {
        [DefaultValue(false)]
        [JsonProperty("multiline", DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool Multiline { get; set; }

        protected override void DefaultCreateControl(ISession session)
        {
            base.DefaultCreateControl(session);
            Control.Multiline = Multiline;
            if (Multiline)
            {
                Control.Height = Height;
            }

            Control.TextChanged += Control_TextChanged;
        }

        protected override object GetValue() => Control.Text.NullIfEmpty();

        protected override void SetValue(object value)
        {
            Control.Text = value == null ? DefaultValue : value.ToString();
        }

        protected override string GetDefaultValue() => string.Empty;

        private void Control_TextChanged(object sender, System.EventArgs e)
        {
            OnValueChanged();
        }
    }
}
