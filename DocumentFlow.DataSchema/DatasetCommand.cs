//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.03.2020
// Time: 22:54
//-----------------------------------------------------------------------

namespace DocumentFlow.DataSchema
{
    using System.ComponentModel;
    using Newtonsoft.Json;

    public class DatasetCommand
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("select", Required = Required.Always)]
        public string Select { get; set; }

        [JsonProperty("select-id")]
        public string SelectByID { get; set; }

        [JsonProperty("insert")]
        public string Insert { get; set; }

        [JsonProperty("update")]
        public string Update { get; set; }

        [JsonProperty("delete")]
        public string Delete { get; set; }

        [DefaultValue(true)]
        [JsonProperty("generate-defaults", DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool GenerateDefaultValue { get; set; }

        public string InsertDefault(string name)
        {
            if (!string.IsNullOrEmpty(Name))
                name = Name;

            if (string.IsNullOrEmpty(Insert))
            {
                if (string.IsNullOrEmpty(name))
                    return null;
                else
                    return $"insert into {name} default values returning id";
            }
            else
                return Insert;
        }

        public string DeleteDefault(string name)
        {
            if (!string.IsNullOrEmpty(Name))
                name = Name;

            if (string.IsNullOrEmpty(Delete))
            {
                if (string.IsNullOrEmpty(name))
                    return null;
                else
                    return $"delete from {name} where id = :id";
            }
            else
                return Delete;
        }
    }
}
