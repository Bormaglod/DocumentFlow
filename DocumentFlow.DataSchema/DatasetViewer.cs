//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 21.03.2020
// Time: 16:32
//-----------------------------------------------------------------------

namespace DocumentFlow.DataSchema
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using Newtonsoft.Json;

    public enum DataType { Directory, Document, Report }

    public enum DateRanges { None, FirstMonthDay, LastMonthDay, FirstQuarterDay, LastQuarterDay, FirstYearDay, LastYearDay, CurrentDay }

    public class DatasetViewer
    {
        /// <summary>
        /// SQL-запрос данные которого будут отображены в окне.
        /// </summary>
        [JsonProperty("dataset", Required = Required.Always)]
        public DatasetCommand Dataset { get; set; }

        /// <summary>
        /// Определяет возможность группировки данных. Если установлено значение true, то ркно будет содержать поле для
        /// группировки данных (в элемете <seealso cref="SfDataGrid"/> установлены свойства AllowGrouping и ShowGroupDropArea 
        /// в значение true).
        /// </summary>
        [DefaultValue(false)]
        [JsonProperty("allow-grouping", DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool AllowGrouping { get; set; }

        [DefaultValue(true)]
        [JsonProperty("allow-sorting", DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool AllowSorting { get; set; }

        [JsonProperty("columns")]
        public IList<DatasetColumn> Columns { get; set; }

        [JsonProperty("stacked-columns")]
        public IList<StackedColumnData> StackedColumns { get; set; } = new List<StackedColumnData>();

        [JsonProperty("groups")]
        public IList<string> Groups { get; set; }

        [JsonProperty("data-type", Required = Required.Always)]
        public DataType DataType { get; set; }

        [DefaultValue(true)]
        [JsonProperty("commandbar-visible", DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool CommandBarVisible { get; set; }

        [JsonProperty("double-click-command")]
        public string DoubleClickCommand { get; set; }

        [JsonProperty("toolbar")]
        public Toolbar Toolbar { get; set; }

        [JsonProperty("command-groups")]
        public IList<CommandGroup> CommandGroups { get; set; }

        [JsonProperty("command-visible")]
        public IList<string> CommandVisible { get; set; }

        [DefaultValue(DateRanges.FirstMonthDay)]
        [JsonProperty("from-date", DefaultValueHandling = DefaultValueHandling.Populate)]
        public DateRanges FromDate { get; set; }

        [DefaultValue(DateRanges.LastMonthDay)]
        [JsonProperty("to-date", DefaultValueHandling = DefaultValueHandling.Populate)]
        public DateRanges ToDate { get; set; }

        [JsonProperty("sorts")]
        public IList<ColumnSort> Sorts { get; set; }
    }
}
