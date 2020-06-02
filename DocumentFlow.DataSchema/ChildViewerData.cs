//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 15.04.2020
// Time: 19:29
//-----------------------------------------------------------------------

namespace DocumentFlow.DataSchema
{
    using Newtonsoft.Json;

    public class ChildViewerData
    {
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty("visible")]
        public string Visible { get; set; }
    }
}
