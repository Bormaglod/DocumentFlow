//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.12.2019
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Interfaces;

using System.ComponentModel;

namespace DocumentFlow.Controls.Editors;

[ToolboxItem(true)]
public partial class DfTextBox : DfControl, IAccess
{
    private bool enabledEditor = true;
    private bool multiline = false;
    private string text = string.Empty;

    public event EventHandler? TextValueChanged;

    public DfTextBox()
    {
        InitializeComponent();
        SetNestedControl(textBoxExt);

        textBoxExt.DataBindings.Add(nameof(textBoxExt.Text), this, nameof(TextValue), false, DataSourceUpdateMode.OnPropertyChanged);
    }

    public bool EnabledEditor
    {
        get => enabledEditor;
        set
        {
            if (enabledEditor != value) 
            { 
                enabledEditor = value;
                textBoxExt.Enabled = value;
            }
        }
    }

    public bool Multiline
    {
        get => multiline;
        set
        {
            if (multiline != value) 
            { 
                multiline = value;
                textBoxExt.Multiline = value;
                textBoxExt.ScrollBars = value ? ScrollBars.Vertical : ScrollBars.None;
            }
        }
    }

    public string TextValue
    {
        get => text;
        set
        {
            if (text != value)
            {
                text = value;
                TextValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
