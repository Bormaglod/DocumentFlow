//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 21.04.2019
// Time: 18:35
//-----------------------------------------------------------------------

namespace DocumentFlow.Controls
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;
    using Syncfusion.Windows.Forms.Tools;
    using DocumentFlow.Controls.Forms;
    using DocumentFlow.DataSchema;

    public enum SelectView { Tree, List, Folder, File }

    public partial class L_SelectBox : UserControl, ILabeled, ISized
    {
        private object selectedItem;
        private List<(object item, object parent, bool folder)> items;

        public L_SelectBox()
        {
            InitializeComponent();
            items = new List<(object, object, bool)>();
        }

        public event EventHandler<SelectBoxValueChanged> ValueChanged;

        public SelectView SelectView { get; set; }

        string ILabeled.Text { get => LabelText; set => LabelText = value; }

        int ILabeled.Width { get => LabelWidth; set => LabelWidth = value; }

        int ILabeled.EditWidth { get => EditControlWidth; set => EditControlWidth = value; }

        bool ILabeled.AutoSize { get => AutoSizeLabel; set => AutoSizeLabel = value; }

        ContentAlignment ILabeled.TextAlign { get => TextAlign; set => TextAlign = value; }

        bool ILabeled.Visible { get => ShowLabel; set => ShowLabel = value; }

        void ISized.SetFullSize() => panelEdit.Dock = DockStyle.Fill;

        public string LabelText { get => label1.Text; set => label1.Text = value; }

        public int LabelWidth { get => label1.Width; set => label1.Width = value; }

        public int EditControlWidth { get => panelEdit.Width; set => panelEdit.Width = value; }

        public bool AutoSizeLabel { get => label1.AutoSize; set => label1.AutoSize = value; }

        public ContentAlignment TextAlign { get => label1.TextAlign; set => label1.TextAlign = value; }

        public bool ShowLabel
        {
            get => label1.Visible;
            set
            {
                label1.Visible = value;
                panelEdit.Dock = value ? DockStyle.Left : DockStyle.Top;
            }
        }

        public object SelectedItem
        {
            get => selectedItem;

            set
            {
                if (selectedItem != value)
                {
                    selectedItem = value;
                    if (selectedItem == null)
                        textValue.Text = string.Empty;
                    else
                    {
                        switch (SelectView)
                        {
                            case SelectView.Tree:
                                textValue.Text = selectedItem.ToString();
                                break;
                            case SelectView.Folder:
                                textValue.Text = selectedItem.ToString();
                                break;
                            case SelectView.File:
                                textValue.Text = Path.GetFileName(selectedItem.ToString());
                                break;
                            default:
                                break;
                        }
                    }

                    OnValueChanged(selectedItem);
                }
            }
        }

        public IEnumerable Items => items.ConvertAll(x => x.item);

        public void Clear()
        {
            items.Clear();
        }

        public void AddItem(object item, object parent, bool IsFolder)
        {
            items.Add((item, parent, IsFolder));
        }

        private void AddItems(SelectBoxWindow window, TreeNodeAdv node, object parent)
        {
            foreach (var item in items.Where(x => x.parent == parent))
            {
                if (SelectView == SelectView.Folder && !item.folder)
                    continue;

                TreeNodeAdv n = window.AddItem(node, item.folder, item.item);
                AddItems(window, n, item.item);
            }
        }

        private void ShowWindow()
        {
            SelectBoxWindow window = new SelectBoxWindow(SelectView == SelectView.Folder);
            AddItems(window, null, null);
            window.ExpandAll();
            window.SelectedItem = SelectedItem;
            if (window.ShowDialog() == DialogResult.OK)
            {
                SelectedItem = window.SelectedItem;
            }
        }

        private void OnValueChanged(object selectedItem)
        {
            if (ValueChanged != null)
                ValueChanged.Invoke(this, new SelectBoxValueChanged(selectedItem));
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            SelectedItem = null;
            OnValueChanged(SelectedItem);
        }

        private void ButtonSelect_Click(object sender, EventArgs e)
        {
            switch (SelectView)
            {
                case SelectView.Tree:
                    ShowWindow();
                    break;
                case SelectView.Folder:
                    ShowWindow();
                    break;
                case SelectView.File:
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        SelectedItem = openFileDialog1.FileName;
                        OnValueChanged(SelectedItem);
                    }

                    break;
                default:
                    break;
            }
        }
    }
}
