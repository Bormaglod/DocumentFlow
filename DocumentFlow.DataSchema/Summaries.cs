//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 26.07.2020
// Time: 17:26
//-----------------------------------------------------------------------

namespace DocumentFlow.DataSchema
{
    using System.ComponentModel;
    using Newtonsoft.Json;

    public enum Aggregate { None, Count, Max, Min, Average, Sum }

    public enum SummaryView { Table, Group, Both }

    public class Summaries
    {
        [DefaultValue(Aggregate.None)]
        [JsonProperty("aggregate", DefaultValueHandling = DefaultValueHandling.Populate)]
        public Aggregate Aggregate { get; set; }

        [JsonProperty("aggregate-title")]
        public string AggregateTitle { get; set; }

        [DefaultValue(SummaryView.Table)]
        [JsonProperty("summary-view", DefaultValueHandling = DefaultValueHandling.Populate)]
        public SummaryView SummaryView { get; set; }
    }
}
