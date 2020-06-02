//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.04.2020
// Time: 16:58
//-----------------------------------------------------------------------

namespace DocumentFlow.DataSchema
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class ControlCondition
    {
        [JsonProperty("controls")]
        public IList<string> Controls { get; set; }

        [JsonProperty("enable")]
        public Condition Enable { get; set; }

        [JsonProperty("visible")]
        public Condition Visible { get; set; }
    }
}
