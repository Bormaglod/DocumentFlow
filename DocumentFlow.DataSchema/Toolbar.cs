//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 21.03.2020
// Time: 16:32
//-----------------------------------------------------------------------

namespace DocumentFlow.DataSchema
{
    using System.ComponentModel;
    using System.Windows.Forms;
    using Newtonsoft.Json;

    public enum ButtonIconSize { Small, Large }

    public class Toolbar
    {
        [DefaultValue(ToolStripItemDisplayStyle.ImageAndText)]
        [JsonProperty("button-style", DefaultValueHandling = DefaultValueHandling.Populate)]
        public ToolStripItemDisplayStyle ButtonStyle { get; set; }

        [DefaultValue(ButtonIconSize.Large)]
        [JsonProperty("icon-size", DefaultValueHandling = DefaultValueHandling.Populate)]
        public ButtonIconSize IconSize { get; set; }
    }
}
