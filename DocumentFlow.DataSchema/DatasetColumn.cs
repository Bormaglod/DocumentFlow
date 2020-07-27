//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 21.03.2020
// Time: 16:35
//-----------------------------------------------------------------------

namespace DocumentFlow.DataSchema
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows.Forms;
    using Newtonsoft.Json;
    using Syncfusion.WinForms.Input.Enums;

    public enum DatasetColumnType { Integer, Numeric, Text, Image, Memo, Boolean, Date, Enums, Combobox, Progress }

    public class DatasetColumn
    {
        [JsonProperty("datafield", Required = Required.Always)]
        public string DataField { get; set; }

        [JsonProperty("text", Required = Required.Always)]
        public string Text { get; set; }

        [JsonProperty("type", Required = Required.Always)]
        public DatasetColumnType Type { get; set; }

        /// <summary>
        /// Значение определяет ширину колонки, если значение <seealso cref="AutoSize"/> равно false, 
        /// иначе это значение игнорируется.
        /// </summary>
        [DefaultValue(100)]
        [JsonProperty("width", DefaultValueHandling = DefaultValueHandling.Populate)]
        public int Width { get; set; }

        [DefaultValue(true)]
        [JsonProperty("hideable", DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool Hideable { get; set; }

        [DefaultValue(true)]
        [JsonProperty("visible", DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool Visible { get; set; }

        [DefaultValue(true)]
        [JsonProperty("resizable", DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool Resizable { get; set; }

        /// <summary>
        /// Если значение свойства равно true, то ширина колонки автоматически расширяется до 
        /// максимальных размеров, а значение свойства <seealso cref="Width"/> - игнорируется.
        /// </summary>
        [DefaultValue(false)]
        [JsonProperty("auto-size", DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool AutoSize { get; set; }

        [DefaultValue(false)]
        [JsonProperty("allow-grouping", DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool AllowGrouping { get; set; }

        [DefaultValue(HorizontalAlignment.Left)]
        [JsonProperty("horizontal-align", DefaultValueHandling = DefaultValueHandling.Populate)]
        public HorizontalAlignment HorizontalAlignment { get; set; }

        [DefaultValue(0)]
        [JsonProperty("digits", DefaultValueHandling = DefaultValueHandling.Populate)]
        public int DecimalDigits { get; set; }

        [DefaultValue(FormatMode.Numeric)]
        [JsonProperty("format-mode", DefaultValueHandling = DefaultValueHandling.Populate)]
        public FormatMode FormatMode { get; set; }

        [DefaultValue("dd.MM.yyyy HH:mm:ss")]
        [JsonProperty("date-format", DefaultValueHandling = DefaultValueHandling.Populate)]
        public string DateFormat { get; set; }

        [JsonProperty("values")]
        public List<EnumValues> ColumnEnums { get; set; }

        [JsonProperty("summaries")]
        public Summaries Summaries { get; set; }

        [JsonProperty("negative-value-color")]
        public string NegativeValueColor { get; set; }

        [JsonProperty("text-color")]
        public string TextColor { get; set; }

        [JsonProperty("background-color")]
        public string BackColor { get; set; }

        [JsonIgnore]
        public string SummaryFormat
        {
            get
            {
                string format = Summaries.Aggregate.ToString();
                if (Type == DatasetColumnType.Integer || Type == DatasetColumnType.Numeric)
                {
                    if (FormatMode == FormatMode.Currency)
                    {
                        format += ":c";
                    }
                    else
                        if (Type == DatasetColumnType.Numeric)
                    {
                        format += $":f{DecimalDigits}";
                    }
                }

                return format;
            }
        }

        public override string ToString()
        {
            return $"{DataField} ({Text}): {Type}";
        }
    }
}
