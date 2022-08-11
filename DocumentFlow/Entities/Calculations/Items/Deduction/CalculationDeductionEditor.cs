//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Editors;
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
        var name = new DfTextBox("calculation_name", "Калькуляция", headerWidth, 300) { Enabled = false };
        var deduction = new DfComboBox<Deduction>("item_id", "Удержание", headerWidth, 400);
        var price = new DfCurrencyTextBox("price", "Сумма с которой произв. удерж.", headerWidth, 150) { DefaultAsNull = false, Enabled = false, Visible = false };
        var percent = new DfPercentTextBox("value", "Процент", headerWidth, 150) { DefaultAsNull = false, PercentDecimalDigits = 2, Enabled = false, Visible = false };
        var item_cost = new DfCurrencyTextBox("item_cost", "Сумма удержания", headerWidth, 150) { DefaultAsNull = false, /*Enabled = false,*/ Visible = false };

        deduction.SetDataSource(() => Services.Provider.GetService<IDeductionRepository>()!.GetAllValid());
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
                    price.Value = e.Value.value;
                    percent.Value = 100m;
                    item_cost.Value = e.Value.value;
                }
                else
                {
                    if (Document.owner_id != null)
                    {
                        if (e.Value.BaseDeduction == BaseDeduction.Salary)
                        {
                            price.Value = Services.Provider.GetService<ICalculationOperationRepository>()!.GetSumItemCost(Document.owner_id.Value);
                        }
                        else
                        {
                            price.Value = Services.Provider.GetService<ICalculationMaterialRepository>()!.GetSumItemCost(Document.owner_id.Value);
                        }

                        percent.Value = e.Value.value;
                    }

                    item_cost.Value = price.NumericValue * percent.NumericValue / 100;
                }
            }
        };

        AddControls(new Control[]
        {
            name,
            deduction,
            price,
            percent,
            item_cost
        });
    }
}