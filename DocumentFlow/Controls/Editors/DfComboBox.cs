//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 19.12.2019
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Events;
using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Data.Interfaces;

using Syncfusion.WinForms.Core.Enums;
using Syncfusion.WinForms.ListView.Events;

using System.ComponentModel;

namespace DocumentFlow.Controls.Editors;

[ToolboxItem(true)]
public partial class DfComboBox : DfControl, IAccess
{
    private Guid selectedItem;
    private bool enabledEditor = true;
    private bool showDeleteButton = true;
    private bool showOpenButton = true;
    private string displayMember = nameof(IDirectory.ItemName);

    private Guid? currentItem;

    public event EventHandler? DeleteButtonClick;
    public event EventHandler<DocumentSelectedEventArgs>? OpenButtonClick;
    public event EventHandler? SelectedItemChanged;
    public event EventHandler<DocumentSelectedEventArgs>? DocumentSelectedChanged;

    public DfComboBox()
    {
        InitializeComponent();
        SetNestedControl(panelEdit);

        comboBox.DisplayMember = displayMember;
        comboBox.ValueMember = nameof(IDirectory.Id);

        comboBox.DataBindings.Add(nameof(comboBox.SelectedValue), this, nameof(SelectedItem), true, DataSourceUpdateMode.OnPropertyChanged);
    }

    public bool EnabledEditor
    {
        get => enabledEditor;
        set
        {
            if (enabledEditor != value)
            {
                enabledEditor = value;
                comboBox.Enabled = value;
                buttonDelete.Enabled = value;
            }
        }
    }

    public bool ShowDeleteButton
    {
        get => showDeleteButton;
        set
        {
            if (showDeleteButton != value)
            {
                showDeleteButton = value;
                panelDelete.Visible = value;
            }
        }
    }

    public bool ShowOpenButton
    {
        get => showOpenButton;
        set
        {
            if (showOpenButton != value)
            {
                showOpenButton = value;
                panelOpen.Visible = value;
            }
        }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Guid SelectedItem
    {
        get => selectedItem;
        set
        {
            if (selectedItem != value)
            {
                selectedItem = value;
                OnSelectedItemChanged();
            }
        }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IEnumerable<IDirectory> DataSource
    {
        get => (IEnumerable<IDirectory>)comboBox.DataSource;
        set => comboBox.DataSource = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IDocumentInfo SelectedDocument
    {
        get => (IDocumentInfo)comboBox.SelectedItem;
    }

    public string DisplayMember
    {
        get => displayMember;
        set
        {
            displayMember = value;
            comboBox.DisplayMember = displayMember;
        }
    }

    public void Clear() => comboBox.DataSource = null;

    public void UpdateValue() => comboBox.DataBindings[nameof(comboBox.SelectedValue)].ReadValue();

    public void OnSelectedItemChanged() => SelectedItemChanged?.Invoke(this, EventArgs.Empty);

    private void ButtonDelete_Click(object sender, EventArgs e)
    {
        if (SelectedItem != Guid.Empty)
        {
            SelectedItem = Guid.Empty;
            comboBox.SelectedIndex = -1;
            DeleteButtonClick?.Invoke(this, EventArgs.Empty);
        }
    }

    private void ButtonOpen_Click(object sender, EventArgs e)
    {
        if (SelectedItem != Guid.Empty)
        {
            OpenButtonClick?.Invoke(this, new DocumentSelectedEventArgs(SelectedDocument));
        }
    }

    private void ComboBox_DropDownClosed(object sender, DropDownClosedEventArgs e)
    {
        if (e.DropDownCloseAction == PopupCloseAction.Done)
        {
            if (currentItem != SelectedItem)
            {
                DocumentSelectedChanged?.Invoke(this, new DocumentSelectedEventArgs(SelectedDocument));
            }
        }
    }

    private void ComboBox_DropDownOpened(object sender, EventArgs e)
    {
        currentItem = SelectedItem;
    }
}
