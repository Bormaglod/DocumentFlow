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
        state = CreateState(x => x.CalculationState, "Состояние", headerWidth);
        var goods_name = CreateTextBox(x => x.GoodsName, "Продукция", headerWidth, 500, enabled: false);
        var code = CreateTextBox(x => x.Code, "Код", headerWidth, 150, defaultAsNull: false);
        var stimul_type = CreateChoice(x => x.StimulatingValue, "Способ", headerWidth, 150, required: true, choices: Calculation.StimulatingValues);
        var stimul_payment = CreateNumericTextBox(x => x.StimulPayment, "Стимул. выплата", headerWidth, 100, defaultAsNull: false, digits: 2);
        var cost_price = CreateCurrencyTextBox(x => x.CostPrice, "Себестоимость", headerWidth, 100, defaultAsNull: false, enabled: false);
        var profit_percent = CreatePercentTextBox(x => x.ProfitPercent, "Рентабельность", headerWidth, 100, defaultAsNull: false, digits: 2);
        var profit_value = CreateCurrencyTextBox(x => x.ProfitValue, "Прибыль", headerWidth, 100, defaultAsNull: false);
        var price = CreateCurrencyTextBox(x => x.Price, "Цена", headerWidth, 100, defaultAsNull: false);
        var approval = CreateDateTimePicker(x => x.DateApproval, "Дата утверждения", headerWidth, 150, required: false);
        var note = CreateMultilineTextBox(x => x.Note, "Полное наименование", headerWidth, 500);

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