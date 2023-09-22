//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 21.04.2019
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Events;
using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Data.Interfaces;

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace DocumentFlow.Controls.Editors;

[ToolboxItem(false)]
public partial class DfSelectBox : DfControl, IAccess
{
    private IDocumentInfo? selectedItem;
    private bool enabledEditor = true;
    private bool showDeleteButton = true;
    private bool showOpenButton = true;

    public event EventHandler? DeleteButtonClick;
    public event EventHandler<DocumentSelectedEventArgs>? OpenButtonClick;
    public event EventHandler? SelectedItemChanged;
    public event EventHandler<DocumentSelectedEventArgs>? UserDocumentModified;

    public DfSelectBox()
    {
        InitializeComponent();
        SetNestedControl(panelEdit);
    }

    public bool EnabledEditor
    {
        get => enabledEditor;
        set
        {
            if (value != enabledEditor) 
            { 
                enabledEditor = value;
                textValue.Enabled = value;
                buttonSelect.Enabled = value;
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
        get => selectedItem?.Id ?? Guid.Empty;
        set
        {
            Guid current = selectedItem?.Id ?? Guid.Empty;
            if (current != value)
            {
                if (value == Guid.Empty)
                {
                    selectedItem = null;
                }
                else
                {
                    selectedItem = GetDocument(value);
                }

                OnSelectedItemChanged();
            }
        }
    }

    public IDocumentInfo SelectedDocument => selectedItem ?? throw new ArgumentNullException(nameof(SelectedDocument));

    protected virtual IDocumentInfo? GetDocument(Guid id) => null;

    protected virtual bool SelectItem([MaybeNullWhen(false)] out IDocumentInfo documentInfo) => throw new NotImplementedException();

    private void OnSelectedItemChanged()
    {
        SelectedItemChanged?.Invoke(this, EventArgs.Empty);

        textValue.Text = selectedItem?.ToString() ?? string.Empty;
    }

    private void ButtonDelete_Click(object sender, EventArgs e)
    {
        if (selectedItem != null)
        {
            textValue.Text = string.Empty;
            selectedItem = null;
            OnSelectedItemChanged();
            DeleteButtonClick?.Invoke(this, EventArgs.Empty);
        }
    }

    private void ButtonSelect_Click(object sender, EventArgs e)
    {
        if (SelectItem(out var documentInfo))
        {
            selectedItem = documentInfo;
            OnSelectedItemChanged();
            UserDocumentModified?.Invoke(this, new DocumentSelectedEventArgs(selectedItem));
        }
    }

    private void ButtonOpen_Click(object sender, EventArgs e)
    {
        if (selectedItem != null)
        {
            OpenButtonClick?.Invoke(this, new DocumentSelectedEventArgs(selectedItem));
        }
    }
}
