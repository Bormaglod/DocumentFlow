//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.06.2019
// Time: 18:44
//-----------------------------------------------------------------------

using System;
using Newtonsoft.Json;

namespace DocumentFlow.Data.Core
{
    public enum MessageDestination { Object, List }

    public class NotifyMessage
    {
        [JsonProperty("destination")]
        public MessageDestination Destination { get; set; }

        [JsonProperty("entity-id")]
        public Guid EntityId { get; set; }

        [JsonProperty("object-id")]
        public Guid ObjectId { get; set; }

        [JsonProperty("action")]
        public string Action { get; set; }
    }
}
