//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 24.10.2021
//
// Версия 2023.1.22
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Entities.Calculations;
using DocumentFlow.Infrastructure.Controls;

namespace DocumentFlow.Controls.Editors;

public partial class DfState : BaseControl, IBindingControl, IAccess
{
    private CalculationState state;

    public DfState(string property, string header, int headerWidth) : base(property)
    {
        InitializeComponent();

        Header = header;
        HeaderWidth = headerWidth;
        PropertyName = property;
    }

    public event EventHandler? ValueChanged;

    public string Header { get => labelState.Text; set => labelState.Text = value; }

    public int HeaderWidth { get => labelState.Width; set => labelState.Width = value; }

    public CalculationState StateValue { get => state; }

    public bool ReadOnly
    {
        get => !buttonAction.Enabled;
        set => buttonAction.Enabled = !value;
    }

    public object? Value
    {
        get => state;
        set
        {
            if (value == null)
            {
                throw new ArgumentNullException($"{Header}: Value нельзя присвоить значение null.");
            }

            state = (CalculationState)value;
            labelStateValue.Text = Calculation.StateNameFromValue(state);
            labelStateValue.ForeColor = state switch
            {
                CalculationState.Prepare => Color.FromArgb(52, 101, 164),
                CalculationState.Approved => Color.FromArgb(18, 118, 34),
                CalculationState.Expired => Color.FromArgb(201, 33, 30),
                _ => throw new NotImplementedException()
            };

            buttonAction.Visible = state != CalculationState.Expired;
            buttonAction.Text = state == CalculationState.Prepare ? "Утвердить" : "В архив";
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public void ClearValue() => Value = CalculationState.Prepare;

    private void ButtonAction_Click(object sender, EventArgs e) => Value = state == CalculationState.Prepare ? CalculationState.Approved : CalculationState.Expired;
}
