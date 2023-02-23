//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.10.2021
//
// Версия 2023.1.22
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
// Версия 2023.2.23
//  - добавлен control labelSuffix
//  - добавлены свойства SuffixText и ShowSuffix
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Controls;

namespace DocumentFlow.Controls.Core;

abstract public partial class BaseNumericTextBox<T, C> : BaseControl, IBindingControl
    where T : struct, IComparable<T>
    where C : Control, new()
{
    private T numericValue;
    private readonly C textBox;
    private bool lockChangeValue = false;

    public BaseNumericTextBox(string property, string header, int headerWidth, int editorWidth = default) : base(property)
    {
        InitializeComponent();

        textBox = new C
        {
            Dock = DockStyle.Left,
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point)
        };

        Controls.Add(textBox);
        textBox.BringToFront();
        labelSuffix.BringToFront();

        SetNumericValue(default);

        Header = header;
        HeaderWidth = headerWidth;
        if (editorWidth == default)
        {
            EditorFitToSize = true;
        }
        else
        {
            EditorWidth = editorWidth;
        }
    }

    public event EventHandler? ValueChanged;

    public C TextBox => textBox;

    public string Header { get => label1.Text; set => label1.Text = value; }

    public int HeaderWidth { get => label1.Width; set => label1.Width = value; }

    public bool HeaderAutoSize { get => label1.AutoSize; set => label1.AutoSize = value; }

    public ContentAlignment HeaderTextAlign { get => label1.TextAlign; set => label1.TextAlign = value; }

    public bool HeaderVisible { get => label1.Visible; set => label1.Visible = value; }

    public int EditorWidth { get => textBox.Width; set => textBox.Width = value; }

    public bool EditorFitToSize
    {
        get => textBox.Dock == DockStyle.Fill;
        set => textBox.Dock = value ? DockStyle.Fill : textBox.Dock = DockStyle.Left;
    }

    public T? NumericValue { get => (T?)Value; set => Value = value; }

    public string SuffixText { get => labelSuffix.Text; set => labelSuffix.Text = value; }

    public bool ShowSuffix { get => labelSuffix.Visible; set => labelSuffix.Visible = value; }

    #region IBindingControl interface

    public object? Value 
    {
        get
        { 
            if (DefaultAsNull && (numericValue.CompareTo(default) == 0))
            {
                return null;
            }

            return numericValue;

        }

        set => SetNumericValue((T?)value);
    }

    #endregion

    abstract public void ClearValue();

    protected abstract void UpdateTextControl(T value);

    protected abstract T GetValueTextBox();

    private void OnValueChange() => ValueChanged?.Invoke(this, EventArgs.Empty);

    protected void UpdateNumericValue()
    {
        if (lockChangeValue)
        {
            return;
        }

        numericValue = GetValueTextBox();
        OnValueChange();
    }

    private void SetNumericValue(T? value)
    {
        if (value == null)
        {
            numericValue = default;
        }
        else
        {
            numericValue = value.Value;
        }

        lockChangeValue = true;
        try
        {
            UpdateTextControl(numericValue);
        }
        finally
        {
            lockChangeValue = false;
        }


        OnValueChange();
    }
}
