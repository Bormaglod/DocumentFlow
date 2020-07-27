//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 15.03.2019
// Time: 20:29
//-----------------------------------------------------------------------

namespace DocumentFlow.DataSchema
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;
    using Newtonsoft.Json;
    using NHibernate;

    public class DataExpression : IDataExpresssion
    {
        [JsonProperty("destination")]
        public string Destination { get; set; }

        [JsonProperty("expression")]
        public string Expression { get; set; }

        [JsonProperty("sql-expression")]
        public string SQLExpression { get; set; }
    }

    abstract public class BindingEditorControl<T, P> : EditorControl<T>, IBindingEditorControl, IPopulated, IEditorExpression where T : Control, new()
    {
        private bool populating;

        object IBindingEditorControl.Value
        {
            get => GetValue();
            set => SetValue((P)value);
        }

        object IBindingEditorControl.DefaultValue => DefaultValue;

        Type IBindingEditorControl.ValueType => typeof(P);

        void IPopulated.Populate(ISession session, IDictionary row, IDictionary<string, Type> types, int status)
        {
            populating = true;
            try
            {
                PreparePopulate(session, row, types, status);
                InternalPopulate(session, row);
            }
            finally
            {
                populating = false;
            }
        }

        IList<IDataExpresssion> IEditorExpression.Expressions => Expressions.OfType<IDataExpresssion>().ToList();

        [JsonProperty("datafield")]
        public string DataField { get; set; }

        [JsonProperty("data-type")]
        public string DataType { get; set; }

        [DefaultValue(true)]
        [JsonProperty("data-nullable", DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool DataNullable { get; set; }

        [JsonProperty("expressions")]
        public IList<DataExpression> Expressions { get; set; } = new List<DataExpression>();

        [DefaultValue(100)]
        [JsonProperty("label-width", DefaultValueHandling = DefaultValueHandling.Populate)]
        public int LabelWidth { get; set; }

        [DefaultValue(100)]
        [JsonProperty("edit-width", DefaultValueHandling = DefaultValueHandling.Populate)]
        public int EditWidth { get; set; }

        [DefaultValue(false)]
        [JsonProperty("edit-full-size", DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool EditFullSize { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [DefaultValue(false)]
        [JsonProperty("auto-size-label", DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool AutoSizeLabel { get; set; }

        [DefaultValue(ContentAlignment.TopLeft)]
        [JsonProperty("text-align-label", DefaultValueHandling = DefaultValueHandling.Populate)]
        public ContentAlignment TextAlign { get; set; }

        [DefaultValue(true)]
        [JsonProperty("show-label", DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool ShowLabel { get; set; }

        [JsonIgnore]
        public Type DataTypeValue 
        { 
            get
            {
                if (string.IsNullOrEmpty(DataType))
                    return null;

                string type = DataType;
                if (DataNullable && DataType != "string")
                    type = $"{type}?";

                return Type.GetType(type);
            }
        }

        [JsonIgnore]
        protected P Value
        {
            get => (P)GetValue();
            set => SetValue(value);
        }

        [JsonIgnore]
        protected P DefaultValue => GetDefaultValue();

        public event EventHandler ValueChanged;

        protected override void DefaultCreateControl(ISession session)
        {
            base.DefaultCreateControl(session);
            if (Control is ILabeled labeled)
            {
                labeled.Text = Text;
                labeled.AutoSize = AutoSizeLabel;
                labeled.Width = LabelWidth;
                labeled.TextAlign = TextAlign;
                labeled.EditWidth = EditWidth;
                labeled.Visible = ShowLabel;
            }

            if (EditFullSize)
            {
                if (Control is ISized sized)
                {
                    sized.SetFullSize();
                }
            }

            Value = DefaultValue;
        }

        protected abstract object GetValue();

        protected abstract void SetValue(object value);

        protected abstract P GetDefaultValue();

        protected virtual void PreparePopulate(ISession session, IDictionary row, IDictionary<string, Type> types, int status) { }

        protected virtual void InternalPopulate(ISession session, IDictionary row)
        {
            if (row == null)
            {
                Value = DefaultValue;
            }
            else
            {
                if (!row.Contains(DataField))
                    return;

                SetValue(row[DataField]);
            }
        }

        protected virtual void SetFullSize() { }

        protected virtual void OnValueChanged()
        {
            if (ValueChanged != null && !populating)
                ValueChanged.Invoke(this, EventArgs.Empty);
        }

        protected override string GetName() => DataField;
    }
}
