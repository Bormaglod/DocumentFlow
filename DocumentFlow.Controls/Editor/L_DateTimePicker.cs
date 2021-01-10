//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 21.10.2020
// Time: 22:31
//-----------------------------------------------------------------------

using System;
using System.Drawing;
using System.Windows.Forms;
using DocumentFlow.Code;

namespace DocumentFlow.Controls.Editor
{
    public partial class L_DateTimePicker : UserControl, ILabelControl, IEditControl
    {
        public L_DateTimePicker()
        {
            InitializeComponent();
            datePickerAdv.NullableValue = DateTime.MinValue;
        }

        public event EventHandler ValueChanged;

        string ILabelControl.Text { get => label1.Text; set => label1.Text = value; }

        int ILabelControl.Width { get => label1.Width; set => label1.Width = value; }

        bool ILabelControl.AutoSize { get => label1.AutoSize; set => label1.AutoSize = value; }

        ContentAlignment ILabelControl.TextAlign { get => label1.TextAlign; set => label1.TextAlign = value; }

        bool ILabelControl.Visible
        {
            get => label1.Visible;
            set
            {
                label1.Visible = value;
                datePickerAdv.Dock = value ? DockStyle.Left : DockStyle.Top;
            }
        }

        int IEditControl.Width { get => datePickerAdv.Width; set => datePickerAdv.Width = value; }

        object IValuable.Value
        {
            get
            { 
                if (!datePickerAdv.ShowCheckBox || datePickerAdv.Checked)
                    return datePickerAdv.Value;

                return null;
            }

            set
            {
                datePickerAdv.Value = value == null ? DateTime.Now : Convert.ToDateTime(value);
                datePickerAdv.Checked = datePickerAdv.ShowCheckBox && value != null;
            }
        }

        bool IEditControl.FitToSize
        {
            get => datePickerAdv.Dock == DockStyle.Fill;
            set => datePickerAdv.Dock = DockStyle.Fill;
        }

        public bool Nullable { get; set; }

        public string CustomFormat { get => datePickerAdv.CustomFormat; set => datePickerAdv.CustomFormat = value; }

        public DateTimePickerFormat Format { get => datePickerAdv.Format; set => datePickerAdv.Format = value; }

        public bool ShowCheckBox { get => datePickerAdv.ShowCheckBox; set => datePickerAdv.ShowCheckBox = value; }

        private void datePickerAdv_ValueChanged(object sender, EventArgs e)
        {
            ValueChanged?.Invoke(this, e);
        }

        private void datePickerAdv_CheckBoxCheckedChanged(object sender, EventArgs e)
        {
            ValueChanged?.Invoke(this, e);
        }
    }
}
