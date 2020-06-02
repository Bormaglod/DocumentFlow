//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 19.12.2019
// Time: 19:40
//-----------------------------------------------------------------------

namespace DocumentFlow.Controls
{
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Windows.Forms;
    using Syncfusion.Windows.Forms.Tools;
    using DocumentFlow.DataSchema;

    public partial class L_ComboBox : UserControl, ILabeled, ISized
    {
        public L_ComboBox()
        {
            InitializeComponent();
        }

        public event EventHandler<SelectedIndexChangingArgs> SelectedIndexChanging;

        string ILabeled.Text { get => label1.Text; set => label1.Text = value; }

        int ILabeled.Width { get => label1.Width; set => label1.Width = value; }

        int ILabeled.EditWidth { get => comboBoxAdv1.Width; set => comboBoxAdv1.Width = value; }

        bool ILabeled.AutoSize { get => label1.AutoSize; set => label1.AutoSize = value; }

        ContentAlignment ILabeled.TextAlign { get => label1.TextAlign; set => label1.TextAlign = value; }

        bool ILabeled.Visible
        { 
            get => label1.Visible; 
            set 
            { 
                label1.Visible = value;
                comboBoxAdv1.Dock = value ? DockStyle.Left : DockStyle.Top;
            } 
        }

        void ISized.SetFullSize() => comboBoxAdv1.Dock = DockStyle.Fill;

        public object SelectedItem { get => comboBoxAdv1.SelectedItem; set => comboBoxAdv1.SelectedItem = value; }

        public IList Items { get => comboBoxAdv1.Items; }

        public void ClearItems() => comboBoxAdv1.Items.Clear();

        private void ComboBoxAdv1_SelectedIndexChanging(object sender, SelectedIndexChangingArgs e)
        {
            if (SelectedIndexChanging != null)
                SelectedIndexChanging.Invoke(this, e);
        }
    }
}
