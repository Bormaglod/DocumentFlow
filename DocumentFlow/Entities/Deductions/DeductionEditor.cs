//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 16.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Entities.Persons;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Controls;

using Microsoft.Extensions.DependencyInjection;

using Syncfusion.Linq;

namespace DocumentFlow.Entities.Deductions;

public class DeductionEditor : Editor<Deduction>, IDeductionEditor
{
    private const int headerWidth = 170;

    public DeductionEditor(IDeductionRepository repository, IPageManager pageManager) : base(repository, pageManager) 
    {
        EditorControls
            .AddTextBox(x => x.ItemName, "Наименование", (text) =>
                text
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(400))
            .AddChoice<BaseDeduction>(x => x.BaseDeduction, "База для начисления", (choice) =>
                choice
                    .ChoiceChanged(BaseDeductionChanged)
                    .SetChoiceValues(Deduction.BaseDeductions)
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(300))
            .AddComboBox<Person>(x => x.PersonId, "Получатель фикс. суммы", (combo) =>
                combo
                    .SetDataSource(GetPeople)
                    .EnableEditor<IPersonEditor>()
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(300)
                    .Raise(1))
            .AddPercentTextBox(x => x.Value, "Процент от базы", (text) =>
                text
                    .SetHeaderWidth(headerWidth)
                    .DefaultAsValue()
                    .Raise(2))
            .AddCurrencyTextBox(x => x.Value, "Сумма", (text) =>
                text
                    .SetHeaderWidth(headerWidth)
                    .DefaultAsValue()
                    .Raise(1));
    }

    private void BaseDeductionChanged(BaseDeduction? deduction)
    {
        BaseDeduction baseDeduction = deduction == null ? BaseDeduction.NotDefined : deduction.Value;
        EditorControls.GetControls<IControl>()
                      .Where(c => c.IsRaised)
                      .ForEach(c => c.SetVisible(c.GetRaisedGroup() == 1 ? baseDeduction == BaseDeduction.Person : baseDeduction != BaseDeduction.Person));
    }

    private IEnumerable<Person> GetPeople() => Services.Provider.GetService<IPersonRepository>()!.GetListExisting(callback: q => q.OrderBy("item_name"));
}