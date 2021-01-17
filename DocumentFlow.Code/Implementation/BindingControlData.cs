//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 10.10.2020
// Time: 19:47
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using DocumentFlow.Code.Core;

namespace DocumentFlow.Code.Implementation
{
    public class BindingControlData : ValueControlData, IBindingControl, IPopulate, IDataName
    {
        private bool populating;
        private object populateData;

        public BindingControlData(Control control) : base(control)
        {
            IBindingControl binding = this;
            binding.LabelWidth = 100;
            binding.ControlWidth = 100;
            binding.Visible = true;

            populating = false;

            if (control is IEditControl edit)
            {
                edit.ValueChanged += Edit_ValueChanged;
            }
        }

        public event EventHandler<ValueChangedEventArgs> ValueChanged;
        public event EventHandler AfterPopulation;

        string IDataName.Name => FieldName;

        public string LabelText
        {
            get
            {
                if (Owner is ILabelControl label)
                    return label.Text;

                return string.Empty;
            }

            set
            {
                if (Owner is ILabelControl label)
                    label.Text = value;
            }
        }

        public string FieldName { get; set; }

        public bool LabelAutoSize
        {
            get
            {
                if (Owner is ILabelControl label)
                    return label.AutoSize;

                return false;
            }

            set
            {
                if (Owner is ILabelControl label)
                    label.AutoSize = value;
            }
        }

        public ContentAlignment LabelTextAlignment
        {
            get
            {
                if (Owner is ILabelControl label)
                    return label.TextAlign;

                return ContentAlignment.MiddleLeft;
            }

            set
            {
                if (Owner is ILabelControl label)
                    label.TextAlign = value;
            }
        }

        public int LabelWidth
        {
            get
            {
                if (Owner is ILabelControl label)
                    return label.Width;

                return 0;
            }

            set
            {
                if (Owner is ILabelControl label)
                    label.Width = value;
            }
        }

        public int ControlWidth
        {
            get
            {
                if (Owner is IEditControl edit)
                    return edit.Width;

                return 0;
            }

            set
            {
                if (Owner is IEditControl edit)
                    edit.Width = value;
            }
        }

        public bool Nullable
        {
            get
            {
                if (Owner is IEditControl edit)
                    return edit.Nullable;

                throw new Exception("Объект не реализует интерфейс IEditControl. Присваивание значение не возможно.");
            }

            set
            {
                if (Owner is IEditControl edit)
                    edit.Nullable = value;
            }
        }

        internal IList<string> Locked { get; set; }

        public IPopulate AsPopulate() => this;

        public IBindingControl SetLabelAutoSize(bool autoSize)
        {
            LabelAutoSize = autoSize;
            return this;
        }

        public IBindingControl SetLabelTextAlignment(ContentAlignment contentAlignment)
        {
            LabelTextAlignment = contentAlignment;
            return this;
        }

        public IBindingControl SetLabelWidth(int labelWidth)
        {
            LabelWidth = labelWidth;
            return this;
        }

        public IBindingControl SetControlWidth(int controlWidth)
        {
            ControlWidth = controlWidth;
            return this;
        }

        public IBindingControl AsNullable()
        {
            Nullable = true;
            return this;
        }

        public IBindingControl AsRequired()
        {
            Nullable = false;
            return this;
        }

        public IBindingControl ValueChangedAction(EventHandler<ValueChangedEventArgs> valueChanged)
        {
            ValueChanged = valueChanged;
            return this;
        }

        public IControl AfterPopulationAction(EventHandler afterPopulation)
        {
            AfterPopulation = afterPopulation;
            return this;
        }

        public void DoAfterPopulation() => AfterPopulation?.Invoke(this, EventArgs.Empty);

        virtual public void Populate(IDbConnection connection, object data)
        {
            if (Owner is IEditControl edit)
            {
                populateData = data;
                populating = true;
                try
                {
                    PropertyInfo prop = data.GetType().GetProperty(FieldName);
                    if (prop == null)
                        throw new ArgumentNullException($"Предпринята попытка заполнить отсутствующее в объекте поле {FieldName}");

                    edit.Value = prop.GetValue(data);
                }
                finally
                {
                    populating = false;
                }
            }
        }

        public override string ToString()
        {
            string name = base.ToString();
            if (string.IsNullOrEmpty(name))
                return FieldName;

            return $"{FieldName} ({name})";
        }

        private void Edit_ValueChanged(object sender, EventArgs e)
        {
            if (sender is IEditControl edit)
            {
                if (!Locked.Contains(FieldName))
                {
                    Locked.Add(FieldName);
                    try
                    {
                        PropertyInfo prop = populateData.GetType().GetProperty(FieldName);
                        object value = edit.Value;
                        if (value != null && value.GetType() != prop.PropertyType)
                        {
                            Type type = global::System.Nullable.GetUnderlyingType(prop.PropertyType);
                            if (type == null)
                                value = Convert.ChangeType(value, prop.PropertyType);
                        }

                        prop.SetValue(populateData, value);

                        if (!populating)
                        {
                            ValueChanged?.Invoke(this, new ValueChangedEventArgs(edit.Value));
                        }
                    }
                    finally
                    {
                        Locked.Remove(FieldName);
                    }
                }
            }
        }
    }
}
