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
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Controls.Editors;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Entities.Accounts;
using DocumentFlow.Entities.Balances;
using DocumentFlow.Entities.Employees;
using DocumentFlow.Entities.OkopfLib;
using DocumentFlow.Entities.Persons;
using DocumentFlow.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Companies;

public class ContractorEditor : Editor<Contractor>, IContractorEditor
{
    private const int headerWidth = 190;
    private readonly DfChoice<SubjectsCivilLow> subject;
    private readonly DfMaskedTextBox<decimal> inn;
    private readonly DfMaskedTextBox<decimal> kpp;
    private readonly DfMaskedTextBox<decimal> ogrn;
    private readonly DfMaskedTextBox<decimal> okpo;
    private readonly DfComboBox<Okopf> okopf;
    private readonly DfComboBox<Account> account;
    private readonly DfDirectorySelectBox<Person> person_id;
    private readonly IContractorRepository contractorRepository;

    public ContractorEditor(IContractorRepository repository, IPageManager pageManager) : base(repository, pageManager) 
    {
        contractorRepository = repository;

        var code = new DfTextBox("code", "Наименование", headerWidth, 400) { DefaultAsNull = false };
        var parent_id = new DfDirectorySelectBox<Contractor>("parent_id", "Группа", headerWidth, 400) { Required = true, ShowOnlyFolder = true };
        var name = new DfTextBox("item_name", "Короткое наименование", headerWidth, 500);
        var full_name = new DfTextBox("full_name", "Полное наименование", headerWidth, 500) { Multiline = true, Height = 75 };
        subject = new DfChoice<SubjectsCivilLow>("SubjectCivilLow", "Субъект права", headerWidth, 200);
        inn = new DfMaskedTextBox<decimal>("inn", "ИНН", 190, 200);
        kpp = new DfMaskedTextBox<decimal>("kpp", "КПП", headerWidth, 200, mask: "#### ## ###");
        ogrn = new DfMaskedTextBox<decimal>("ogrn", "ОГРН", headerWidth, 200, mask: "# ## ## ## ##### #");
        okpo = new DfMaskedTextBox<decimal>("okpo", "ОКПО", headerWidth, 200, mask: "## ##### #");
        okopf = new DfComboBox<Okopf>("okopf_id", "ОКОПФ", headerWidth, 450)
        {
            OpenAction = (p) => pageManager.ShowEditor<IOkopfEditor, Okopf>(p)
        };
        person_id = new DfDirectorySelectBox<Person>("person_id", "Физ. лицо", headerWidth, 300);

        account = new DfComboBox<Account>("account_id", "Расчётный счёт", headerWidth, 450)
        {
            OpenAction = (p) => pageManager.ShowEditor<IAccountEditor, Account>(p)
        };

        subject.SetChoiceValues(Contractor.Subjects);
        subject.ManualValueChange += Subject_ManualValueChange;

        parent_id.SetDataSource(() => contractorRepository.GetOnlyFolders());
        okopf.SetDataSource(() => Services.Provider.GetService<IOkopfRepository>()!.GetAllValid(callback: q => q.OrderBy("item_name")));
        account.SetDataSource(() => Services.Provider.GetService<IAccountRepository>()!.GetByOwner(Document.account_id, Id));
        person_id.SetDataSource(() => Services.Provider.GetService<IPersonRepository>()!.GetAllValid(callback: q => q.OrderBy("item_name")));

        AddControls(new Control[]
        {
            code,
            parent_id,
            name,
            full_name,
            subject,
            inn,
            kpp,
            ogrn,
            okpo,
            okopf,
            account,
            person_id
        });
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

    protected override void DoAfterRefreshData() => UpdateVisibility(subject.ChoiceValue);

    private void Subject_ManualValueChange(object? sender, SelectedValueChanged<SubjectsCivilLow?> e) => UpdateVisibility(e.Value);

    private void UpdateVisibility(SubjectsCivilLow? subj)
    {
        bool? legal_entity = null;

        if (subj != null)
        {
            inn.Mask = subj == SubjectsCivilLow.Person ? "#### ##### #" : "#### ###### ##";
            legal_entity = subj == SubjectsCivilLow.LegalEntity;
        }

        kpp.Visible = legal_entity.HasValue && legal_entity.Value;
        ogrn.Visible = legal_entity.HasValue && legal_entity.Value;
        okpo.Visible = legal_entity.HasValue && legal_entity.Value;
        okopf.Visible = legal_entity.HasValue && legal_entity.Value;
        account.Visible = legal_entity.HasValue && legal_entity.Value;
        person_id.Visible = legal_entity.HasValue && !legal_entity.Value;
    }
}