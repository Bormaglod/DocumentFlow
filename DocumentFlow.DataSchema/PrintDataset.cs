//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 10.05.2020
// Time: 20:12
//-----------------------------------------------------------------------

namespace DocumentFlow.DataSchema
{
    using System.ComponentModel;
    using Newtonsoft.Json;

    public class PrintDataset
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("sql-query")]
        public string Query { get; set; }

        [DefaultValue(false)]
        [JsonProperty("unique-result", DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool UniqueResult { get; set; }
    }
}
