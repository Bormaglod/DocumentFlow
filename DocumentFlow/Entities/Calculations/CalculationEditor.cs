//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 25.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Editors;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Infrastructure;

namespace DocumentFlow.Entities.Calculations;

public class CalculationEditor : Editor<Calculation>, ICalculationEditor
{
    private const int headerWidth = 170;

    private readonly DfState state;

    public CalculationEditor(ICalculationRepository repository, IPageManager pageManager) : base(repository, pageManager)
    {
        state = new DfState("CalculationState", "Состояние", headerWidth);
        var goods_name = new DfTextBox("goods_name", "Продукция", headerWidth, 500) { Enabled = false };
        var code = new DfTextBox("code", "Код", headerWidth, 150) { DefaultAsNull = false };
        var stimul_type = new DfChoice<StimulatingValue>("StimulatingValue", "Способ", headerWidth, 150) { Required = true };
        var stimul_payment = new DfNumericTextBox("stimul_payment", "Стимул. выплата", headerWidth, 100) { DefaultAsNull = false, NumberDecimalDigits = 2 };
        var cost_price = new DfCurrencyTextBox("cost_price", "Себестоимость", headerWidth, 100) { DefaultAsNull = false, Enabled = false };
        var profit_percent = new DfPercentTextBox("profit_percent", "Рентабельность", headerWidth, 100) { DefaultAsNull = false, PercentDecimalDigits = 2 };
        var profit_value = new DfCurrencyTextBox("profit_value", "Прибыль", headerWidth, 100) { DefaultAsNull = false };
        var price = new DfCurrencyTextBox("price", "Цена", headerWidth, 100) { DefaultAsNull = false };
        var approval = new DfDateTimePicker("date_approval", "Дата утверждения", headerWidth, 150) { Required = false };
        var note = new DfTextBox("note", "Полное наименование", headerWidth, 500) { Multiline = true, Height = 75 };

        state.ValueChanged += (s, e) =>
        {
            bool enable = state.StateValue != CalculationState.Expired;
            code.Enabled = enable;
            profit_percent.Enabled = enable;
            profit_value.Enabled = enable;
            price.Enabled = enable;
            note.Enabled = enable;
            stimul_type.Enabled = enable;
            stimul_payment.Enabled = enable;
            SetNestedBrowserStatus(!enable);
        };

        stimul_type.SetChoiceValues(Calculation.StimulatingValues);

        AddControls(new Control[]
        {
            state,
            goods_name,
            code,
            stimul_type,
            stimul_payment,
            cost_price,
            profit_percent,
            profit_value,
            price,
            approval,
            note
        });

        RegisterReport(new ProcessMapReport());
        RegisterReport(new SpecificationReport());
    }

    protected override void DoAfterRefreshData()
    {
        base.DoAfterRefreshData();
        RegisterNestedBrowser<ICalculationCuttingBrowser, CalculationCutting>();
        RegisterNestedBrowser<ICalculationOperationBrowser, CalculationOperation>();
        RegisterNestedBrowser<ICalculationMaterialBrowser, CalculationMaterial>();
        RegisterNestedBrowser<ICalculationDeductionBrowser, CalculationDeduction>();

        SetNestedBrowserStatus(state.StateValue == CalculationState.Expired);
    }
}