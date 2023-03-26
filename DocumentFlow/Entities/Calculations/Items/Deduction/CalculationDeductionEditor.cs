//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Entities.Deductions;
using DocumentFlow.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Calculations;

public class CalculationDeductionEditor : Editor<CalculationDeduction>, ICalculationDeductionEditor
{
    private const int headerWidth = 230;

    public CalculationDeductionEditor(ICalculationDeductionRepository repository, IPageManager pageManager) : base(repository, pageManager)
    {
        var deduction = CreateComboBox(x => x.OwnerId, "Удержание", headerWidth, 400, data: GetDeductions);
        var price = CreateCurrencyTextBox(x => x.Price, "Сумма с которой произв. удерж.", headerWidth, 150, enabled: false, visible: false, defaultAsNull: false);
        var percent = CreatePercentTextBox(x => x.Value, "Процент", headerWidth, 150, enabled: false, visible: false, defaultAsNull: false, digits: 2);
        var item_cost = CreateCurrencyTextBox(x => x.ItemCost, "Сумма удержания", headerWidth, 150, visible: false, defaultAsNull: false);

        deduction.ValueChanged += (s, e) =>
        {
            bool visible = false;
            bool is_not_person = false;
            if (e.Value != null)
            {
                visible = true;
                is_not_person = e.Value.BaseDeduction != BaseDeduction.Person;
            }

            price.Visible = visible && is_not_person;
            percent.Visible = visible && is_not_person;
            item_cost.Visible = visible;

            if (visible)
            {
                item_cost.Enabled = !is_not_person;
            }
        };

        deduction.ManualValueChange += (s, e) =>
        {
            if (e.Value != null)
            {
                if (e.Value.BaseDeduction == BaseDeduction.Person)
                {
                    price.Value = e.Value.Value;
                    percent.Value = 100m;
                    item_cost.Value = e.Value.Value;
                }
                else
                {
                    if (Document.OwnerId != null)
                    {
                        if (e.Value.BaseDeduction == BaseDeduction.Salary)
                        {
                            price.Value = Services.Provider.GetService<ICalculationOperationRepository>()!.GetSumItemCost(Document.OwnerId.Value);
                        }
                        else
                        {
                            price.Value = Services.Provider.GetService<ICalculationMaterialRepository>()!.GetSumItemCost(Document.OwnerId.Value);
                        }

                        percent.Value = e.Value.Value;
                    }

                    item_cost.Value = price.NumericValue * percent.NumericValue / 100;
                }
            }
        };

        AddControls(new Control[]
        {
            CreateTextBox(x => x.CalculationName, "Калькуляция", headerWidth, 300, enabled: false),
            deduction,
            price,
            percent,
            item_cost
        });
    }

    private IEnumerable<Deduction> GetDeductions() => Services.Provider.GetService<IDeductionRepository>()!.GetAllValid();
}