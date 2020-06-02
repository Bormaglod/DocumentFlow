//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 31.03.2020
// Time: 19:02
//-----------------------------------------------------------------------

namespace DocumentFlow.DataSchema
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class StackedColumnData
    {
        [JsonProperty("header", Required = Required.Always)]
        public string Header { get; set; }

        [JsonProperty("childs")]
        public IList<string> Childs { get; set; }
    }
}
