﻿//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 21.04.2019
// Time: 18:35
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DocumentFlow.Code;
using DocumentFlow.Controls.Editor.Forms;
using DocumentFlow.Data.Core;

namespace DocumentFlow.Controls.Editor
{
    public partial class L_SelectBox : UserControl, ILabelControl, IEditControl
    {
        private readonly List<IIdentifier> items = new();
        private IIdentifier selectedItem;

        public L_SelectBox()
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
                panelEdit.Dock = value ? DockStyle.Left : DockStyle.Top;
            }
        }

        int IEditControl.Width { get => panelEdit.Width; set => panelEdit.Width = value; }

        object IValuable.Value
        {
            get
            {
                if (Nullable)
                {
                    if (selectedItem == null)
                    {
                        return null;
                    }
                    else
                    {
                        return new Guid(selectedItem.oid.ToString());
                    }
                }
                else if (selectedItem != null)
                {
                    return selectedItem.oid;
                }
                else
                    throw new ArgumentNullException($"Значение поля {((ILabelControl)this).Text} не может быть пустым.");
            }

            set
            {
                if (value is Guid id)
                {
                    selectedItem = items.FirstOrDefault(x => (x.oid is Guid guid) && guid == id);
                    textValue.Text = selectedItem?.ToString();
                    OnValueChanged();
                }
                else if (value == null)
                {
                    ClearCurrent();
                }
                else
                    throw new ArgumentException($"Значение поля {((ILabelControl)this).Text} должно быть идентификатором типа GUID");
            }
        }

        bool IEditControl.FitToSize
        {
            get => panelEdit.Dock == DockStyle.Fill;
            set => panelEdit.Dock = DockStyle.Fill;
        }

        public bool Nullable { get; set; }

        public bool ShowOnlyFolder { get; set; }

        public void ClearCurrent()
        {
            textValue.Text = string.Empty;
            selectedItem = null;

            OnValueChanged();
        }

        public void AddItems(IEnumerable<IIdentifier> addingItems)
        {
            items.Clear();
            items.AddRange(addingItems);
        }

        private void OnValueChanged() => ValueChanged?.Invoke(this, EventArgs.Empty);

        private void ButtonDelete_Click(object sender, EventArgs e) => ClearCurrent();

        private void ButtonSelect_Click(object sender, EventArgs e)
        {
            SelectBoxWindow window = new(ShowOnlyFolder);
            window.AddItems(items);
            window.SelectedItem = selectedItem;
            if (window.ShowDialog() == DialogResult.OK)
            {
                selectedItem = window.SelectedItem;
                textValue.Text = selectedItem.ToString();

                OnValueChanged();
            }
        }
    }
}
