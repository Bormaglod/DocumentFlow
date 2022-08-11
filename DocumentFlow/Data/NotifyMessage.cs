//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.06.2019
//-----------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DocumentFlow.Data
{
    public enum MessageDestination { Object, List }

    public enum MessageAction { Refresh, Add, Delete }

    public class NotifyMessage
    {
        [JsonPropertyName("destination")]
        public MessageDestination Destination { get; set; }

        [JsonPropertyName("entity-name")]
        public string? EntityName { get; set; }

        [JsonPropertyName("object-id")]
        public Guid ObjectId { get; set; }

        [JsonPropertyName("action")]
        public MessageAction Action { get; set; }
    }
}
