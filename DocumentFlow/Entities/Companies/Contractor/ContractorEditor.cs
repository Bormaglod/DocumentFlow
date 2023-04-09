//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.01.2022
//
// Версия 2022.8.29
//  - расширены возможности полей okopf и account (добавлены кнопки для
//    редактирования выбранных значений)
// Версия 2023.1.25
//  - добавлено поле subject и person_id
//  - изменение видимости полей сдлано с привязкой к полю subject, а не
//    parent_id
// Версия 2023.4.2
//  - создание элементов управления в конструкторе реализовано с 
//    помощью свойства EditorControls
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Entities.Accounts;
using DocumentFlow.Entities.Balances;
using DocumentFlow.Entities.Employees;
using DocumentFlow.Entities.OkopfLib;
using DocumentFlow.Entities.Persons;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Controls;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Companies;

public class ContractorEditor : Editor<Contractor>, IContractorEditor
{
    private const int headerWidth = 190;
    private const string tag = "Subjects";

    public ContractorEditor(IContractorRepository repository, IPageManager pageManager) : base(repository, pageManager) 
    {
        EditorControls
            .CreateTextBox(x => x.Code, "Наименование", (text) =>
                text
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(400)
                    .DefaultAsValue())
            .CreateDirectorySelectBox<Contractor>(x => x.ParentId, "Группа", (select) =>
                select
                    .Required()
                    .ShowOnlyFolder()
                    .SetDataSource(repository.GetOnlyFolders)
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(400))
            .CreateTextBox(x => x.ItemName, "Короткое наименование", (text) =>
                text
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(500))
            .CreateTextBox(x => x.FullName, "Полное наименование", (text) =>
                text
                    .Multiline()
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(500))
            .CreateChoice<SubjectsCivilLow>(x => x.SubjectCivilLow, "Субъект права", (choice) =>
                choice
                    .SetChoiceValues(Contractor.Subjects)
                    .ManualValueChanged(UpdateVisibility)
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(200))
            .CreateMaskedTextBox<decimal>(x => x.Inn, "ИНН", (text) =>
                text
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(200))
            .CreateMaskedTextBox<decimal>(x => x.Kpp, "КПП", (text) =>
                text
                    .SetMask("#### ## ###")
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(200)
                    .SetTag(tag))
            .CreateMaskedTextBox<decimal>(x => x.Ogrn, "ОГРН", (text) =>
                text
                    .SetMask("# ## ## ## ##### #")
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(200)
                    .SetTag(tag))
            .CreateMaskedTextBox<decimal>(x => x.Okpo, "ОКПО", (text) =>
                text
                    .SetMask("## ##### #")
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(200)
                    .SetTag(tag))
            .CreateComboBox<Okopf>(x => x.OkopfId, "ОКОПФ", (combo) =>
                combo
                    .SetDataSource(GetOkopfs)
                    .EnableEditor<IOkopfEditor>()
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(450)
                    .SetTag(tag))
            .CreateDirectorySelectBox<Person>(x => x.PersonId, "Физ. лицо", (select) =>
                select
                    .SetDataSource(GetPeople)
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(300)
                    .SetTag(tag))
            .CreateComboBox<Account>(x => x.AccountId, "Расчётный счёт", (combo) =>
                combo
                    .SetDataSource(GetAccounts)
                    .EnableEditor<IAccountEditor>()
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(450)
                    .SetTag(tag));
    }

    protected override void RegisterNestedBrowsers()
    {
        base.RegisterNestedBrowsers();
        RegisterNestedBrowser<IBalanceContractorBrowser, BalanceContractor>();
        RegisterNestedBrowser<IBalanceProcessingBrowser, BalanceProcessing>();
        RegisterNestedBrowser<IContractBrowser, Contract>();
        RegisterNestedBrowser<IAccountBrowser, Account>();
        RegisterNestedBrowser<IEmployeeBrowser, Employee>();
    }

    protected override void DoAfterRefreshData() => UpdateVisibility(EditorControls.GetControl<IChoiceControl<SubjectsCivilLow>>("SubjectCivilLow").Value);

    private void UpdateVisibility(SubjectsCivilLow? subj)
    {
        bool? legal_entity = null;

        if (subj != null)
        {
            var inn = EditorControls.GetControl<IMaskedTextBoxControl<decimal>>("Inn");
            inn.SetMask(subj == SubjectsCivilLow.Person ? "#### ##### #" : "#### ###### ##");
            legal_entity = subj == SubjectsCivilLow.LegalEntity;
        }

        foreach (var control in EditorControls.GetControls<IControl>().Where(x => x.Tag == tag))
        {
            control.SetVisible(legal_entity.HasValue && legal_entity.Value);
        }
    }

    private IEnumerable<Okopf> GetOkopfs() => Services.Provider.GetService<IOkopfRepository>()!.GetAllValid(callback: q => q.OrderBy("item_name"));

    private IEnumerable<Account> GetAccounts() => Services.Provider.GetService<IAccountRepository>()!.GetByOwner(Document.AccountId, Id);

    private IEnumerable<Person> GetPeople() => Services.Provider.GetService<IPersonRepository>()!.GetAllValid(callback: q => q.OrderBy("item_name"));
}