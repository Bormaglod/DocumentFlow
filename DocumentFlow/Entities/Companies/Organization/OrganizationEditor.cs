//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Editors;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Entities.Accounts;
using DocumentFlow.Entities.Employees;
using DocumentFlow.Entities.OkopfLib;
using DocumentFlow.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Companies;

public class OrganizationEditor : Editor<Organization>, IOrganizationEditor
{
    private const int headerWidth = 190;

    public OrganizationEditor(IOrganizationRepository repository, IPageManager pageManager) : base(repository, pageManager) 
    {
        var code = new DfTextBox("code", "Наименование", headerWidth, 400) { DefaultAsNull = false };
        var name = new DfTextBox("item_name", "Короткое наименование", headerWidth, 500);
        var full_name = new DfTextBox("full_name", "Полное наименование", headerWidth, 500) { Multiline = true, Height = 75 };
        var inn = new DfMaskedTextBox<decimal>("inn", "ИНН", headerWidth, 200, mask: "#### ##### #");
        var kpp = new DfMaskedTextBox<decimal>("kpp", "КПП", headerWidth, 200, mask: "#### ## ###");
        var ogrn = new DfMaskedTextBox<decimal>("ogrn", "ОГРН", headerWidth, 200, mask: "# ## ## ## ##### #");
        var okpo = new DfMaskedTextBox<decimal>("okpo", "ОКПО", headerWidth, 200, mask: "## ##### #");
        var okopf = new DfComboBox<Okopf>("okopf_id", "ОКОПФ", headerWidth, 450);
        var account = new DfComboBox<OurAccount>("account_id", "Расчётный счёт", headerWidth, 450);
        var address = new DfTextBox("address", "Адрес", headerWidth, 500) { Multiline = true, Height = 75 };
        var phone = new DfTextBox("phone", "Телефон", headerWidth, 250);
        var email = new DfTextBox("email", "Эл. почта", headerWidth, 250);
        var default_org = new DfToggleButton("default_org", "Основная организация", headerWidth);

        okopf.SetDataSource(() => Services.Provider.GetService<IOkopfRepository>()!.GetAllValid(callback: q => q.OrderBy("item_name")));

        account.SetDataSource(() => Services.Provider.GetService<IOurAccountRepository>()!.GetByOwner(Document.account_id, Id));

        AddControls(new Control[]
        {
            code,
            name,
            full_name,
            inn,
            kpp,
            ogrn,
            okpo,
            okopf,
            account,
            address,
            phone,
            email,
            default_org
        });
    }

    protected override void RegisterNestedBrowsers()
    {
        base.RegisterNestedBrowsers();
        RegisterNestedBrowser<IOurAccountBrowser, OurAccount>();
        RegisterNestedBrowser<IOrgEmployeeBrowser, OurEmployee>();
    }
}