//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 15.03.2019
// Time: 20:28
//-----------------------------------------------------------------------

namespace DocumentFlow.DataSchema.Types
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Newtonsoft.Json;
    using NHibernate;
    using DocumentFlow.DataSchema.Types.Core;

    [Tag("Label")]
    public class F_Label : EditorControl<Label>
    {
        [DefaultValue(false)]
        [JsonProperty("auto-size", DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool AutoSize { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [DefaultValue(ContentAlignment.TopLeft)]
        [JsonProperty("text-align", DefaultValueHandling = DefaultValueHandling.Populate)]
        public ContentAlignment TextAlign { get; set; }

        protected override void DefaultCreateControl(ISession session)
        {
            base.DefaultCreateControl(session);
            Control.Text = Text;
            Control.AutoSize = AutoSize;
            Control.TextAlign = TextAlign;
        }
    }
}
