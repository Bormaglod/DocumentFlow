//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 10.05.2020
// Time: 20:06
//-----------------------------------------------------------------------

namespace DocumentFlow.DataSchema
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class PrintDatasets
    {
        [JsonProperty("title", Required = Required.Always)]
        public string QueryTitle { get; set; }

        [JsonProperty("datasets")]
        public IList<PrintDataset> Datasets { get; set; }
    }
}
