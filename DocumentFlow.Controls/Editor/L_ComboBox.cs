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

namespace DocumentFlow.Controls.Editor
{
    public partial class L_ComboBox : UserControl, ILabelControl, IEditControl
    {
        public L_ComboBox()
        {
            InitializeComponent();
            Nullable = true;
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
                comboBoxAdv1.Dock = value ? DockStyle.Left : DockStyle.Top;
            }
        }

        int IEditControl.Width { get => comboBoxAdv1.Width; set => comboBoxAdv1.Width = value; }

        object IEditControl.Value 
        {
            get
            {
                if (comboBoxAdv1.SelectedItem is IIdentifier selectedItem)
                {
                    if (Nullable)
                    {
                        return selectedItem?.id;
                    }
                    else if (selectedItem != null)
                    {
                        return selectedItem.id;
                    }
                    else
                        throw new ArgumentNullException($"Значение поля {((ILabelControl)this).Text} не может быть пустым.");
                }

                return null;
            }

            set
            {
                if (value is Guid id)
                {
                    IIdentifier identifier = comboBoxAdv1.Items.OfType<IIdentifier>().FirstOrDefault(x => x.id == id);
                    comboBoxAdv1.SelectedItem = identifier;
                    OnValueChanged();
                    return;
                }

                ClearCurrent();
            }
        }

        bool IEditControl.FitToSize
        {
            get => comboBoxAdv1.Dock == DockStyle.Fill;
            set => comboBoxAdv1.Dock = DockStyle.Fill;
        }

        public bool Nullable { get; set; }

        public void ClearCurrent()
        {
            comboBoxAdv1.SelectedItem = null;
            OnValueChanged();
        }

        public void AddItems(IEnumerable<IIdentifier> addingItems)
        {
            comboBoxAdv1.Items.Clear();
            comboBoxAdv1.Items.AddRange(addingItems.ToArray());
        }

        private void OnValueChanged()
        {
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }

        private void ComboBoxAdv1_SelectedIndexChanging(object sender, SelectedIndexChangingArgs e)
        {
            OnValueChanged();
        }
    }
}
