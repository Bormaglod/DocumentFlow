//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Editors;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Entities.Accounts;
using DocumentFlow.Entities.Balances;
using DocumentFlow.Entities.Employees;
using DocumentFlow.Entities.OkopfLib;
using DocumentFlow.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Companies;

public class ContractorEditor : Editor<Contractor>, IContractorEditor
{
    private const int headerWidth = 190;
    private readonly DfMaskedTextBox<decimal> inn;
    private readonly DfDirectorySelectBox<Contractor> parent_id;
    private readonly DfMaskedTextBox<decimal> kpp;
    private readonly DfMaskedTextBox<decimal> ogrn;
    private readonly DfMaskedTextBox<decimal> okpo;
    private readonly DfComboBox<Okopf> okopf;
    private readonly DfComboBox<Account> account;
    private readonly IContractorRepository contractorRepository;

    public ContractorEditor(IContractorRepository repository, IPageManager pageManager) : base(repository, pageManager) 
    {
        contractorRepository = repository;

        var code = new DfTextBox("code", "Наименование", headerWidth, 400) { DefaultAsNull = false };
        parent_id = new DfDirectorySelectBox<Contractor>("parent_id", "Группа", headerWidth, 400) { Required = true, ShowOnlyFolder = true };
        var name = new DfTextBox("item_name", "Короткое наименование", headerWidth, 500);
        var full_name = new DfTextBox("full_name", "Полное наименование", headerWidth, 500) { Multiline = true, Height = 75 };
        inn = new DfMaskedTextBox<decimal>("inn", "ИНН", 190, 200);
        kpp = new DfMaskedTextBox<decimal>("kpp", "КПП", headerWidth, 200, mask: "#### ## ###");
        ogrn = new DfMaskedTextBox<decimal>("ogrn", "ОГРН", headerWidth, 200, mask: "# ## ## ## ##### #");
        okpo = new DfMaskedTextBox<decimal>("okpo", "ОКПО", headerWidth, 200, mask: "## ##### #");
        okopf = new DfComboBox<Okopf>("okopf_id", "ОКОПФ", headerWidth, 450);
        account = new DfComboBox<Account>("account_id", "Расчётный счёт", headerWidth, 450);

        parent_id.SetDataSource(() => contractorRepository.GetOnlyFolders());
        parent_id.ValueChanged += Parent_id_ValueChanged;

        okopf.SetDataSource(() => Services.Provider.GetService<IOkopfRepository>()!.GetAllValid(callback: q => q.OrderBy("item_name")));

        account.SetDataSource(() => Services.Provider.GetService<IAccountRepository>()!.GetByOwner(Document.account_id, Id));

        AddControls(new Control[]
        {
            code,
            parent_id,
            name,
            full_name,
            inn,
            kpp,
            ogrn,
            okpo,
            okopf,
            account
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

    protected override void DoAfterRefreshData()
    {
        parent_id.Value = DefaultParentId ?? Contractor.CompanyGroup;
        if (parent_id.SelectedItem != null)
        {
            Guid? root = contractorRepository.GetRoot(parent_id.SelectedItem.id);
            inn.Mask = root == Contractor.CompanyGroup ? "#### ##### #" : "#### ###### ##";

            UpdateVisibility(root == Contractor.CompanyGroup);
        }
    }

    private void Parent_id_ValueChanged(object? sender, EventArgs e)
    {
        if (parent_id.SelectedItem != null)
        {
            Guid? root = contractorRepository.GetRoot(parent_id.SelectedItem.id);

            inn.Mask = root == Contractor.CompanyGroup ? "#### ##### #" : "#### ###### ##";

            UpdateVisibility(root == Contractor.CompanyGroup);
        }
    }

    private void UpdateVisibility(bool visible)
    {
        kpp.Visible = visible;
        ogrn.Visible = visible;
        okpo.Visible = visible;
        okopf.Visible = visible;
        account.Visible = visible;
    }
}