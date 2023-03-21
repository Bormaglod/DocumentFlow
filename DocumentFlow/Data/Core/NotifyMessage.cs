//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.06.2019
//
// Версия 2023.3.17
//  - перенесено из DocumentFlow.Data в DocumentFlow.Data.Core
//
//-----------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DocumentFlow.Data.Core
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
