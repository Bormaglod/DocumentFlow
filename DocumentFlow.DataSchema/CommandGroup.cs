//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 12.04.2020
// Time: 16:40
//-----------------------------------------------------------------------

namespace DocumentFlow.DataSchema
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class CommandGroup
    {
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty("commands", Required = Required.Always)]
        public IList<UserCommand> Commands { get; set; }
    }
}
