//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 18.03.2020
// Time: 21:39
//-----------------------------------------------------------------------

namespace DocumentFlow.DataSchema
{
    using Newtonsoft.Json;

    public class DatasetSchema
    {
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty("viewer")]
        public DatasetViewer Viewer { get; set; }

        [JsonProperty("editor")]
        public DatasetEditor Editor { get; set; }
    }
}
