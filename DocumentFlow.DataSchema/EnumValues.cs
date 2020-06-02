//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 26.03.2020
// Time: 23:09
//-----------------------------------------------------------------------

namespace DocumentFlow.DataSchema
{
    using Newtonsoft.Json;

    public class EnumValues
    {
        [JsonProperty("id", Required = Required.Always)]
        public int Id { get; set; }

        [JsonProperty("caption", Required = Required.Always)]
        public string Caption { get; set; }
    }
}
