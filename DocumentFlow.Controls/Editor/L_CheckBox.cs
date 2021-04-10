//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 19.12.2019
// Time: 18:54
//-----------------------------------------------------------------------

using System;
using System.Drawing;
using System.Windows.Forms;
using DocumentFlow.Code;

namespace DocumentFlow.Controls.Editor
{
    public partial class L_CheckBox : UserControl, ILabelControl, IEditControl
    {
        public L_CheckBox()
        {
            InitializeComponent();
            Nullable = false;
        }

        public event EventHandler ValueChanged;

        string ILabelControl.Text { get => label1.Text; set => label1.Text = value; }

        int ILabelControl.Width { get => label1.Width; set => label1.Width = value; }

        bool ILabelControl.AutoSize { get => label1.AutoSize; set => label1.AutoSize = value; }

        ContentAlignment ILabelControl.TextAlign { get => label1.TextAlign; set => label1.TextAlign = value; }
        
        bool ILabelControl.Visible { get => label1.Visible; set => label1.Visible = value; }

        int IEditControl.Width { get => checkBoxAdv1.Width; set => checkBoxAdv1.Width = value; }

        object IValuable.Value
        {
            get
            {
                if (Nullable && checkBoxAdv1.BoolValue == default)
                    return null;

                return checkBoxAdv1.BoolValue;
            }

            set => checkBoxAdv1.BoolValue = value != null && Convert.ToBoolean(value);
        }

        bool IEditControl.FitToSize { get; set; }

        public bool Nullable { get; set; }

        private void checkBoxAdv1_CheckedChanged(object sender, EventArgs e)
        {
            ValueChanged?.Invoke(this, e);
        }
    }
}
