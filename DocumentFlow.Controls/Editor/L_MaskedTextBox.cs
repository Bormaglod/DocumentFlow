//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 19.12.2019
// Time: 09:00
//-----------------------------------------------------------------------

using System;
using System.Drawing;
using System.Windows.Forms;
using DocumentFlow.Code;

namespace DocumentFlow.Controls.Editor
{
    public partial class L_MaskedTextBox<T> : UserControl, ILabelControl, IEditControl where T: IComparable<T>
    {
        public L_MaskedTextBox()
        {
            InitializeComponent();
            Nullable = false;
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
                maskedEditBox1.Dock = value ? DockStyle.Left : DockStyle.Top;
            }
        }

        int IEditControl.Width { get => maskedEditBox1.Width; set => maskedEditBox1.Width = value; }

        object IValuable.Value
        {
            get
            {
                try
                {
                    T valueData = (T)Convert.ChangeType(maskedEditBox1.Text, typeof(T));
                    if (Nullable && valueData.CompareTo(default) == 0)
                        return null;

                    return valueData;
                }
                catch (Exception)
                {
                    if (Nullable)
                        return null;
                    
                    return default(T);
                }
            }

            set => maskedEditBox1.Text = value == null ? string.Empty : value.ToString();
        }

        bool IEditControl.FitToSize
        {
            get => maskedEditBox1.Dock == DockStyle.Fill;
            set => maskedEditBox1.Dock = DockStyle.Fill;
        }

        public string Mask
        {
            get => maskedEditBox1.Mask;
            set => maskedEditBox1.Mask = value;
        }

        public char PromptCharacter
        {
            get => maskedEditBox1.PromptCharacter;
            set => maskedEditBox1.PromptCharacter = value;
        }

        public bool Nullable { get; set; }

        private void OnValueChanged()
        {
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }

        private void maskedEditBox1_TextChanged(object sender, EventArgs e)
        {
            OnValueChanged();
        }
    }
}
