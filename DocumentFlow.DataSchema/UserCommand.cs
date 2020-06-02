//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 12.04.2020
// Time: 16:19
//-----------------------------------------------------------------------

namespace DocumentFlow.DataSchema
{
    using System.ComponentModel;
    using Newtonsoft.Json;

    public enum CommandMethod { Sql, Embedded }

    public class UserCommand
    {
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [DefaultValue(CommandMethod.Sql)]
        [JsonProperty("method", DefaultValueHandling = DefaultValueHandling.Populate)]
        public CommandMethod Method { get; set; }

        [JsonProperty("command", Required = Required.Always)]
        public string Command { get; set; }

        /*[JsonProperty("parameters")]
        public IList<string> Parameters = new List<string>();

        [DefaultValue(false)]
        [JsonProperty("refresh", DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool Refresh { get; set; }*/

        [DefaultValue(false)]
        [JsonProperty("show-title", DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool ShowTitle { get; set; }

        [DefaultValue(false)]
        [JsonProperty("context-menu", DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool InsertInContextMenu { get; set; }

        [DefaultValue(true)]
        [JsonProperty("toolbar-button", DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool InsertInToolbar { get; set; }
    }
}
