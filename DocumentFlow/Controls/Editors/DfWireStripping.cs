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
using DocumentFlow.Entities.Operations;
using DocumentFlow.Infrastructure.Controls;

using Syncfusion.Windows.Forms.Tools;

namespace DocumentFlow.Controls.Editors;

public enum StrippingPlace { Left, Right }

public partial class DfWireStripping : BaseControl, IBindingControl, IAccess
{
    public DfWireStripping(string header, StrippingPlace place, int headerWidth = default)
        : base(place.ToString())
    {
        InitializeComponent();
        SetLabelControl(label1, header, headerWidth);
        SetNestedControl(panel1);
    }

    public event EventHandler? ValueChanged;

    public bool ReadOnly
    {
        get => textBoxCleaning.ReadOnly;
        set
        {
            textBoxCleaning.ReadOnly = value;
            textBoxSweep.ReadOnly = value;
        }
    }

    public void ClearValue()
    {
        textBoxCleaning.DecimalValue = 0;
        textBoxSweep.IntegerValue = 0;
    }

    public object? Value
    {
        get => new Stripping() { Cleaning = textBoxCleaning.DecimalValue, Sweep = Convert.ToInt32(textBoxSweep.IntegerValue) };
        set
        {
            if (value is Stripping stripping)
            {
                (textBoxCleaning.DecimalValue, textBoxSweep.IntegerValue) = (stripping.Cleaning, stripping.Sweep);
            }
        }
    }

    private void TextBoxCleaning_DecimalValueChanged(object sender, EventArgs e) => ValueChanged?.Invoke(this, e);

    private void TextBoxSweep_IntegerValueChanged(object sender, EventArgs e) => ValueChanged?.Invoke(this, e);
}
