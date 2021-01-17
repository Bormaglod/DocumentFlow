//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.06.2019
// Time: 18:44
//-----------------------------------------------------------------------

using System;
using System.Text.Json.Serialization;

namespace DocumentFlow.Data
{
    public enum MessageDestination { Object, List }

    public class NotifyMessage
    {
        [JsonPropertyName("destination")]
        public MessageDestination Destination { get; set; }

        [JsonPropertyName("entity-id")]
        public Guid EntityId { get; set; }

        [JsonPropertyName("object-id")]
        public Guid ObjectId { get; set; }

        [JsonPropertyName("action")]
        public string Action { get; set; }
    }
}
