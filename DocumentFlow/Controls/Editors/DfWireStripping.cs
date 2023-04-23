//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 12.01.2022
//
// Версия 2023.1.22
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Infrastructure.Controls;
using DocumentFlow.Infrastructure.Controls.Core;

namespace DocumentFlow.Controls.Editors;

public partial class DfWireStripping : BaseControl, IBindingControl, IAccess, IWireStrippingControl
{
    private ControlValueChanged<Stripping>? valueChanged;

    public DfWireStripping(string property, string header, int headerWidth = default)
        : base(property)
    {
        InitializeComponent();
        SetLabelControl(label1, header, headerWidth);
        SetNestedControl(panel1);
    }

    public bool ReadOnly
    {
        get => textBoxCleaning.ReadOnly;
        set
        {
            textBoxCleaning.ReadOnly = value;
            textBoxSweep.ReadOnly = value;
        }
    }

    public void ClearSelectedValue()
    {
        textBoxCleaning.DecimalValue = 0;
        textBoxSweep.IntegerValue = 0;
    }

    public object? Value
    {
        get => Stripping;
        set
        {
            if (value is Stripping stripping)
            {
                (textBoxCleaning.DecimalValue, textBoxSweep.IntegerValue) = (stripping.Cleaning, stripping.Sweep);
            }
        }
    }

    private Stripping Stripping => new() { Cleaning = textBoxCleaning.DecimalValue, Sweep = Convert.ToInt32(textBoxSweep.IntegerValue) };

    private void TextBoxCleaning_DecimalValueChanged(object sender, EventArgs e) => valueChanged?.Invoke(Stripping);

    private void TextBoxSweep_IntegerValueChanged(object sender, EventArgs e) => valueChanged?.Invoke(Stripping);

    #region IWireStrippingControl interface

    IWireStrippingControl IWireStrippingControl.StrippingChanged(ControlValueChanged<Stripping> action)
    {
        valueChanged = action;
        return this;
    }

    #endregion
}
