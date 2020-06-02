//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.04.2020
// Time: 16:55
//-----------------------------------------------------------------------

namespace DocumentFlow.DataSchema
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class Condition
    {
        [JsonProperty("states")]
        public IList<string> States { get; set; }

        [JsonProperty("if-equal")]
        public string ExpressionIfEqual { get; set; }

        [JsonProperty("if-equal-sql")]
        public string ExpressionIfEqualQuery { get; set; }
    }
}
