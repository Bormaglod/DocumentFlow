//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 25.01.2022
//
// Версия 2023.7.23
//  - поле DateApproval было доступно для редактирования в архивном
//    состоянии калькуляции - исправлено.
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Controls;

using Syncfusion.Linq;

namespace DocumentFlow.Entities.Calculations;

public class CalculationEditor : Editor<Calculation>, ICalculationEditor
{
    private const int headerWidth = 170;

    public CalculationEditor(ICalculationRepository repository, IPageManager pageManager) : base(repository, pageManager)
    {
        EditorControls
            .AddState(x => x.CalculationState, "Состояние", (state) =>
                state
                    .StateChanged(CalculationStateChanged)
                    .SetHeaderWidth(headerWidth))
            .AddTextBox(x => x.GoodsName, "Продукция", (text) =>
                text
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(500)
                    .Disable())
            .AddTextBox(x => x.Code, "Код", (text) =>
                text
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(150)
                    .DefaultAsValue()
                    .Raise())
            .AddChoice<StimulatingValue>(x => x.StimulatingValue, "Способ", (choice) =>
                choice
                    .SetChoiceValues(Calculation.StimulatingValues)
                    .Required()
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(150)
                    .Raise())
            .AddNumericTextBox(x => x.StimulPayment, "Стимул. выплата", (box) =>
                box
                    .SetNumberDecimalDigits(2)
                    .SetHeaderWidth(headerWidth)
                    .DefaultAsValue()
                    .Raise())
            .AddCurrencyTextBox(x => x.CostPrice, "Себестоимость", (box) =>
                box
                    .SetHeaderWidth(headerWidth)
                    .DefaultAsValue()
                    .Disable())
            .AddPercentTextBox(x => x.ProfitPercent, "Рентабельность", (box) =>
                box
                    .SetPercentDecimalDigits(2)
                    .SetHeaderWidth(headerWidth)
                    .DefaultAsValue()
                    .Raise())
            .AddCurrencyTextBox(x => x.ProfitValue, "Прибыль", (box) =>
                box
                    .SetHeaderWidth(headerWidth)
                    .DefaultAsValue()
                    .Raise())
            .AddCurrencyTextBox(x => x.Price, "Цена", (box) =>
                box
                    .SetHeaderWidth(headerWidth)
                    .DefaultAsValue()
                    .Raise())
            .AddDateTimePicker(x => x.DateApproval, "Дата утверждения", (date) =>
                date
                    .NotRequired()
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(150)
                    .Raise())
            .AddTextBox(x => x.Note, "Полное наименование", (text) =>
                text
                    .Multiline()
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(500)
                    .Raise());

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

        SetNestedBrowserStatus(EditorControls.GetControl<IStateControl>().Current == CalculationState.Expired);
    }

    private void CalculationStateChanged(CalculationState state)
    {
        bool enable = state != CalculationState.Expired;
        EditorControls.GetControls<IControl>()
                      .Where(x => x.IsRaised)
                      .ForEach(x => x.SetEnabled(enable));
        SetNestedBrowserStatus(!enable);
    }
}