//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 03.10.2021
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Controls.Infrastructure;

using Syncfusion.Windows.Forms.Tools;

namespace DocumentFlow.Controls.Editors;

public partial class DfToggleButton : BaseControl, IBindingControl, IAccess
{
    public DfToggleButton(string property, string header, int headerWidth) : base(property)
    {
        InitializeComponent();

        Header = header;
        HeaderWidth = headerWidth;
        PropertyName = property;
    }

    public event EventHandler? ValueChanged;

    public string Header { get => label1.Text; set => label1.Text = value; }

    public int HeaderWidth { get => label1.Width; set => label1.Width = value; }

    /// <summary>
    /// default = false
    /// </summary>
    public bool HeaderAutoSize { get => label1.AutoSize; set => label1.AutoSize = value; }

    /// <summary>
    /// default = ContentAlignment.TopLeft
    /// </summary>
    public ContentAlignment HeaderTextAlign { get => label1.TextAlign; set => label1.TextAlign = value; }

    /// <summary>
    /// default = true
    /// </summary>
    public bool HeaderVisible { get => label1.Visible; set => label1.Visible = value; }

    public int EditorWidth { get => toggleButton1.Width; set => toggleButton1.Width = value; }

    public bool ToggleValue
    {
        get => toggleButton1.ToggleState == ToggleButtonState.Active;
        set => toggleButton1.ToggleState = ((bool?)value ?? false) ? ToggleButtonState.Active : ToggleButtonState.Inactive;
    }

    public bool ReadOnly
    {
        get => !toggleButton1.Enabled;
        set => toggleButton1.Enabled = !value;
    }

    public object? Value
    {
        get => ToggleValue;
        set => ToggleValue = ((bool?)value ?? false);
    }

    public void ClearValue() => toggleButton1.ToggleState = ToggleButtonState.Inactive;

    private void ToggleButton1_ToggleStateChanged(object sender, ToggleStateChangedEventArgs e) => ValueChanged?.Invoke(this, e);
}
