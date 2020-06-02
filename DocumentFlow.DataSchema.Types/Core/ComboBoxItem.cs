//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 20.06.2019
// Time: 20:25
//-----------------------------------------------------------------------

namespace DocumentFlow.DataSchema.Types.Core
{
    using Newtonsoft.Json;

    public class ComboBoxItem
    {
        [JsonProperty("key")]
        public int Key { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        public override string ToString()
        {
            return Value;
        }
    }
}
