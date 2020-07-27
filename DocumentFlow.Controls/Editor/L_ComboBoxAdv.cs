//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.03.2019
// Time: 23:18
//-----------------------------------------------------------------------

namespace DocumentFlow.Controls
{
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Windows.Forms;
    using Syncfusion.Windows.Forms.Tools;
    using DocumentFlow.DataSchema;

    public partial class L_ComboBoxAdv : UserControl, ILabeled, ISized
    {
        public L_ComboBoxAdv()
        {
            InitializeComponent();
        }

        string ILabeled.Text { get => label1.Text; set => label1.Text = value; }

        int ILabeled.Width { get => label1.Width; set => label1.Width = value; }

        int ILabeled.EditWidth { get => panelEdit.Width; set => panelEdit.Width = value; }

        bool ILabeled.AutoSize { get => label1.AutoSize; set => label1.AutoSize = value; }

        ContentAlignment ILabeled.TextAlign { get => label1.TextAlign; set => label1.TextAlign = value; }

        bool ILabeled.Visible
        {
            get => label1.Visible;
            set
            {
                label1.Visible = value;
                panelEdit.Dock = value ? DockStyle.Left : DockStyle.Top;
            }
        }

        void ISized.SetFullSize() => panelEdit.Dock = DockStyle.Fill;

        public event EventHandler<SelectedIndexChangingArgs> SelectedIndexChanging;

        public object SelectedItem
        {
            get { return comboBoxAdv1.SelectedItem; }
            set { comboBoxAdv1.SelectedItem = value; }
        }

        public IEnumerable Items => comboBoxAdv1.Items;


        public int AddItem(object item)
        {
            return comboBoxAdv1.Items.Add(item);
        }

        public void ClearItems() => comboBoxAdv1.Items.Clear();

        private void ToolButton1_Click(object sender, EventArgs e)
        {
            comboBoxAdv1.SelectedItem = null;
        }

        private void ComboBoxAdv1_SelectedIndexChanging(object sender, SelectedIndexChangingArgs e)
        {
            if (SelectedIndexChanging != null)
                SelectedIndexChanging.Invoke(this, e);
        }
    }
}
