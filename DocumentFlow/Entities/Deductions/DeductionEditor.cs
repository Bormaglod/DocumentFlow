//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 16.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Editors;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Entities.Persons;
using DocumentFlow.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Deductions;

public class DeductionEditor : Editor<Deduction>, IDeductionEditor
{
    private const int headerWidth = 170;

    public DeductionEditor(IDeductionRepository repository, IPageManager pageManager) : base(repository, pageManager) 
    {
        var name = new DfTextBox("item_name", "Наименование", headerWidth, 400);
        var base_calc = new DfChoice<BaseDeduction>("BaseDeduction", "База для начисления", headerWidth, 300);
        var person = new DfComboBox<Person>("person_id", "Получатель фикс. суммы", headerWidth, 300);
        var value_percent = new DfPercentTextBox("value", "Процент от базы", headerWidth, 100) { DefaultAsNull = false };
        var value_fix = new DfCurrencyTextBox("value", "Процент от базы", headerWidth, 100) { DefaultAsNull = false };

        base_calc.SetChoiceValues(Deduction.BaseDeductions);
        base_calc.ValueChanged += (sender, e) =>
        {
            BaseDeduction baseDeduction = e.Value == null ? BaseDeduction.NotDefined : e.Value.Value;
            person.Visible = baseDeduction == BaseDeduction.Person;
            value_fix.Visible = baseDeduction == BaseDeduction.Person;
            value_percent.Visible = baseDeduction != BaseDeduction.Person;
        };

        person.SetDataSource(() => Services.Provider.GetService<IPersonRepository>()!.GetAllValid(callback: q => q.OrderBy("item_name")));

        AddControls(new Control[]
        {
            name,
            base_calc,
            person,
            value_percent,
            value_fix
        });
    }
}