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
using DocumentFlow.Code.System;

namespace DocumentFlow.Code.Implementation
{
    public class BindingControlData: ControlData, IBindingControl, IPopulate
    {
        private bool populating;
        private object populateData;
        private readonly List<string> locked = new List<string>();

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

        /*public bool Nullable
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
        }*/

        public object Value 
        { 
            get
            {
                if (Owner is IEditControl edit)
                    return edit.Value;

                throw new Exception("Объект не реализует интерфейс IEditControl. Присваивание значение не возможно.");
            }

            set
            {
                if (Owner is IEditControl edit)
                    edit.Value = value;
            }
        }

        public IPopulate AsPopulateControl() => this;

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

        /*public IBindingControl AsNullable()
        {
            Nullable = true;
            return this;
        }

        public IBindingControl AsNotNullable()
        {
            Nullable = false;
            return this;
        }*/

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

        public void DoAfterPopulation()
        {
            AfterPopulation?.Invoke(this, EventArgs.Empty);
        }

        virtual public void Populate(IDbConnection connection, object data)
        {
            if (Owner is IEditControl edit)
            {
                populateData = data;
                populating = true;
                try
                {
                    PropertyInfo prop = data.GetType().GetProperty(FieldName);
                    edit.Value = prop.GetValue(data);
                }
                finally
                {
                    populating = false;
                }
            }
        }

        private void Edit_ValueChanged(object sender, EventArgs e)
        {
            if (populating)
            {
                return;
            }

            if (sender is IEditControl edit)
            {
                if (!locked.Contains(FieldName))
                {
                    locked.Add(FieldName);
                    try
                    {
                        PropertyInfo prop = populateData.GetType().GetProperty(FieldName);
                        prop.SetValue(populateData, edit.Value);

                        ValueChanged?.Invoke(this, new ValueChangedEventArgs(edit.Value));
                    }
                    finally
                    {
                        locked.Remove(FieldName);
                    }
                }
            }
        }
    }
}
