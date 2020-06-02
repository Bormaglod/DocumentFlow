//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.12.2019
// Time: 16:37
//-----------------------------------------------------------------------

namespace DocumentFlow.DataSchema
{
    using System.Windows.Forms;
    using Newtonsoft.Json;

    abstract public class DatasetEditorControl<T, P> : BindingEditorControl<T, P> where T : Control, new()
    {
        [JsonProperty("dataset")]
        public string Dataset { get; set; }
    }
}
