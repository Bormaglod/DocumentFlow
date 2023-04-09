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
// Версия 2023.4.8
//  - добавлено наследование от INumericTextBox
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Controls;

using Syncfusion.Windows.Forms.Tools;

namespace DocumentFlow.Controls.Core;

abstract public partial class BaseNumericTextBox<T, C> : BaseControl, IBindingControl, IBaseNumericTextBoxControl<T>
    where T : struct, IComparable<T>
    where C : TextBoxExt, new()
{
    private T numericValue;
    private readonly C textBox;
    private bool lockChangeValue = false;

    public BaseNumericTextBox(string property, string header, int headerWidth = default, int editorWidth = default) : base(property)
    {
        InitializeComponent();

        textBox = new C
        {
            Dock = DockStyle.Left,
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point)
        };

        SetLabelControl(label1, header, headerWidth);
        SetNestedControl(textBox, editorWidth);

        Controls.Add(textBox);
        textBox.BringToFront();
        labelSuffix.BringToFront();

        SetNumericValue(default);
    }

    public event EventHandler? ValueChanged;

    public C TextBox => textBox;

    public T? NumericValue { get => (T?)Value; set => Value = value; }

    public string SuffixText { get => labelSuffix.Text; set => labelSuffix.Text = value; }

    public bool ShowSuffix { get => labelSuffix.Visible; set => labelSuffix.Visible = value; }

    public bool ReadOnly
    {
        get => TextBox.ReadOnly;
        set => TextBox.ReadOnly = value;
    }

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

    #region IBaseNumericTextBoxControl interface

    IBaseNumericTextBoxControl<T> IBaseNumericTextBoxControl<T>.ReadOnly()
    {
        ReadOnly = true;
        return this;
    }

    #endregion
}
