//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 16.01.2022
//-----------------------------------------------------------------------

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
        var name = CreateTextBox(x => x.ItemName, "Наименование", headerWidth, 400);
        var base_calc = CreateChoice(x => x.BaseDeduction, "База для начисления", headerWidth, 300, choices: Deduction.BaseDeductions);
        var person = CreateComboBox(x => x.PersonId, "Получатель фикс. суммы", headerWidth, 300, data: GetPeople);
        var value_percent = CreatePercentTextBox(x => x.Value, "Процент от базы", headerWidth, 100, defaultAsNull: false);
        var value_fix = CreateCurrencyTextBox(x => x.Value, "Процент от базы", headerWidth, 100, defaultAsNull: false);

        base_calc.ValueChanged += (sender, e) =>
        {
            BaseDeduction baseDeduction = e.Value == null ? BaseDeduction.NotDefined : e.Value.Value;
            person.Visible = baseDeduction == BaseDeduction.Person;
            value_fix.Visible = baseDeduction == BaseDeduction.Person;
            value_percent.Visible = baseDeduction != BaseDeduction.Person;
        };

        AddControls(new Control[]
        {
            name,
            base_calc,
            person,
            value_percent,
            value_fix
        });
    }

    private IEnumerable<Person> GetPeople() => Services.Provider.GetService<IPersonRepository>()!.GetAllValid(callback: q => q.OrderBy("item_name"));
}