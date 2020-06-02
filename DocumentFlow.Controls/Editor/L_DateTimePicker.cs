//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 22.12.2019
// Time: 14:07
//-----------------------------------------------------------------------

namespace DocumentFlow.Controls
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using DocumentFlow.DataSchema;

    public partial class L_DateTimePicker : UserControl, ILabeled, ISized
    {
        public L_DateTimePicker()
        {
            InitializeComponent();
            datePickerAdv.NullableValue = DateTime.MinValue;
        }

        string ILabeled.Text { get => label1.Text; set => label1.Text = value; }

        int ILabeled.Width { get => label1.Width; set => label1.Width = value; }

        int ILabeled.EditWidth { get => datePickerAdv.Width; set => datePickerAdv.Width = value; }

        bool ILabeled.AutoSize { get => label1.AutoSize; set => label1.AutoSize = value; }

        ContentAlignment ILabeled.TextAlign { get => label1.TextAlign; set => label1.TextAlign = value; }

        bool ILabeled.Visible
        {
            get => label1.Visible;
            set
            {
                label1.Visible = value;
                datePickerAdv.Dock = value ? DockStyle.Left : DockStyle.Top;
            }
        }

        void ISized.SetFullSize() => datePickerAdv.Dock = DockStyle.Fill;

        public string CustomFormat { get => datePickerAdv.CustomFormat; set => datePickerAdv.CustomFormat = value; }

        public DateTimePickerFormat Format { get => datePickerAdv.Format; set => datePickerAdv.Format = value; }

        public bool ShowCheckBox { get => datePickerAdv.ShowCheckBox; set => datePickerAdv.ShowCheckBox = value; }

        public DateTime Value { get => datePickerAdv.Value; set => datePickerAdv.Value = value; }
    }
}
