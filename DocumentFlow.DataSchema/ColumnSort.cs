//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.06.2020
// Time: 20:33
//-----------------------------------------------------------------------

namespace DocumentFlow.DataSchema
{
    using System.ComponentModel;
    using Newtonsoft.Json;

    public class ColumnSort
    {
        [JsonProperty("name", Required = Required.Always)]
        public string ColumnName { get; set; }

        [DefaultValue(ListSortDirection.Ascending)]
        [JsonProperty("direction", DefaultValueHandling = DefaultValueHandling.Populate)]
        public ListSortDirection SortDirection { get; set; }
    }
}
