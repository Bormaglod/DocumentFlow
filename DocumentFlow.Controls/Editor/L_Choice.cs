//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 19.12.2019
// Time: 19:40
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Syncfusion.Windows.Forms.Tools;
using DocumentFlow.Code;
using DocumentFlow.Data;
using DocumentFlow.Data.Base;

namespace DocumentFlow.Controls.Editor
{
    public partial class L_Choice : UserControl, ILabelControl, IEditControl
    {
        private bool nullable;

        public L_Choice()
        {
            InitializeComponent();
            nullable = true;
        }

        public L_Choice(IDictionary<int, string> keyValues) : this()
        {
            foreach (int item in keyValues.Keys)
            {
                comboBoxAdv1.Items.Add(new ChoiceDataItem() { id = item, name = keyValues[item] });
            }
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
                panelEdit.Dock = value ? DockStyle.Left : DockStyle.Top;
            }
        }

        int IEditControl.Width { get => panelEdit.Width; set => panelEdit.Width = value; }

        object IValuable.Value 
        {
            get
            {
                if (comboBoxAdv1.SelectedItem != null && comboBoxAdv1.SelectedItem is IIdentifier<int> selectedItem)
                {
                    return selectedItem.id;
                }

                if (Nullable)
                {
                    return null;
                }
                else
                {
                    throw new ArgumentNullException($"Значение поля {((ILabelControl)this).Text} не может быть пустым.");
                }
            }

            set
            {
                if (value is int id)
                {
                    IIdentifier<int> identifier = comboBoxAdv1.Items.OfType<IIdentifier<int>>().FirstOrDefault(x => x.id == id);
                    comboBoxAdv1.SelectedItem = identifier;
                }
                else
                {
                    comboBoxAdv1.SelectedItem = null;
                }

                OnValueChanged();
            }
        }

        bool IEditControl.FitToSize
        {
            get => panelEdit.Dock == DockStyle.Fill;
            set => panelEdit.Dock = DockStyle.Fill;
        }

        public bool Nullable
        {
            get { return nullable; }
            set
            {
                nullable = value;
                buttonDelete.Visible = nullable;
                panelSeparator1.Visible = nullable;
            }
        }

        public void AddItems(IEnumerable<IIdentifier> addingItems)
        {
            comboBoxAdv1.Items.Clear();
            comboBoxAdv1.Items.AddRange(addingItems.ToArray());
        }

        private void OnValueChanged() => ValueChanged?.Invoke(this, EventArgs.Empty);

        private void ComboBoxAdv1_SelectedIndexChanging(object sender, SelectedIndexChangingArgs e) => OnValueChanged();

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            comboBoxAdv1.SelectedItem = null;
            OnValueChanged();
        }
    }
}
