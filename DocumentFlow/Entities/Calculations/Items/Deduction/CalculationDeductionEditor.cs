//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Entities.Deductions;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Controls;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Calculations;

public class CalculationDeductionEditor : Editor<CalculationDeduction>, ICalculationDeductionEditor
{
    private const int headerWidth = 230;

    public CalculationDeductionEditor(ICalculationDeductionRepository repository, IPageManager pageManager) : base(repository, pageManager)
    {
        EditorControls
            .AddTextBox(x => x.CalculationName, "Калькуляция", (text) =>
                text
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(300)
                    .Disable())
            .AddComboBox<Deduction>(x => x.ItemId, "Удержание", (combo) =>
                combo
                    .ItemChanged(DeductionItemChanged)
                    .ItemSelected(DeductionItemSelected)
                    .SetDataSource(GetDeductions)
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(400))
            .AddCurrencyTextBox(x => x.Price, "Сумма с которой произв. удерж.", (box) =>
                box
                    .Disable()
                    .SetVisible(false)
                    .DefaultAsValue()
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(150))
            .AddPercentTextBox(x => x.Value, "Процент", (box) =>
                box
                    .SetPercentDecimalDigits(2)
                    .Disable()
                    .SetVisible(false)
                    .DefaultAsValue()
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(150))
            .AddCurrencyTextBox(x => x.ItemCost, "Сумма удержания", (box) =>
                box
                    .SetVisible(false)
                    .DefaultAsValue()
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(150));
    }

    private IEnumerable<Deduction> GetDeductions() => Services.Provider.GetService<IDeductionRepository>()!.GetListExisting();

    private void DeductionItemChanged(Deduction? deduction)
    {
        bool visible = false;
        bool is_not_person = false;
        if (deduction != null)
        {
            visible = true;
            is_not_person = deduction.BaseDeduction != BaseDeduction.Person;
        }

        EditorControls.GetControl<ICurrencyTextBoxControl>("Price").SetVisible(visible && is_not_person);
        EditorControls.GetControl<IPercentTextBoxControl>("Value").SetVisible(visible && is_not_person);

        var item_cost = EditorControls.GetControl<ICurrencyTextBoxControl>("ItemCost");
        item_cost.SetVisible(visible);

        if (visible)
        {
            item_cost.SetEnabled(!is_not_person);
        }
    }

    private void DeductionItemSelected(Deduction? deduction)
    {
        if (deduction == null)
        {
            return;
        }

        var price = EditorControls.GetControl<ICurrencyTextBoxControl>("Price");
        var percent = EditorControls.GetControl<IPercentTextBoxControl>("Value");
        var item_cost = EditorControls.GetControl<ICurrencyTextBoxControl>("ItemCost");

        if (deduction.BaseDeduction == BaseDeduction.Person)
        {
            price.NumericValue = deduction.Value;
            percent.NumericValue = 100m;
            item_cost.NumericValue = deduction.Value;
        }
        else
        {
            if (Document.OwnerId != null)
            {
                if (deduction.BaseDeduction == BaseDeduction.Salary)
                {
                    price.NumericValue = Services.Provider.GetService<ICalculationOperationRepository>()!.GetSumItemCost(Document.OwnerId.Value);
                }
                else
                {
                    price.NumericValue = Services.Provider.GetService<ICalculationMaterialRepository>()!.GetSumItemCost(Document.OwnerId.Value);
                }

                percent.NumericValue = deduction.Value;
            }

            item_cost.NumericValue = price.NumericValue * percent.NumericValue / 100;
        }
    }
}