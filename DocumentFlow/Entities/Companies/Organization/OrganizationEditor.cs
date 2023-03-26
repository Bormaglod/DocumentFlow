//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.01.2022
//-----------------------------------------------------------------------

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
        AddControls(new Control[]
        {
            CreateTextBox(x => x.Code, "Наименование", headerWidth, 400, defaultAsNull: false),
            CreateTextBox(x => x.ItemName, "Короткое наименование", headerWidth, 500),
            CreateMultilineTextBox(x => x.FullName, "Полное наименование", headerWidth, 500),
            CreateMaskedTextBox<decimal>(x => x.Inn, "ИНН", headerWidth, 200, mask: "#### ##### #"),
            CreateMaskedTextBox<decimal>(x => x.Kpp, "КПП", headerWidth, 200, mask: "#### ## ###"),
            CreateMaskedTextBox<decimal>(x => x.Ogrn, "ОГРН", headerWidth, 200, mask: "# ## ## ## ##### #"),
            CreateMaskedTextBox<decimal>(x => x.Okpo, "ОКПО", headerWidth, 200, mask: "## ##### #"),
            CreateComboBox(x => x.OkopfId, "ОКОПФ", headerWidth, 450, data: GetOkopfs),
            CreateComboBox(x => x.AccountId, "Расчётный счёт", headerWidth, 450, data: GetAccounts),
            CreateMultilineTextBox(x => x.Address, "Адрес", headerWidth, 500),
            CreateTextBox(x => x.Phone, "Телефон", headerWidth, 250),
            CreateTextBox(x => x.Email, "Эл. почта", headerWidth, 250),
            CreateToggleButton(x => x.DefaultOrg, "Основная организация", headerWidth)
        });
    }

    protected override void RegisterNestedBrowsers()
    {
        base.RegisterNestedBrowsers();
        RegisterNestedBrowser<IOurAccountBrowser, OurAccount>();
        RegisterNestedBrowser<IOrgEmployeeBrowser, OurEmployee>();
    }

    private IEnumerable<Okopf> GetOkopfs() => Services.Provider.GetService<IOkopfRepository>()!.GetAllValid(callback: q => q.OrderBy("item_name"));

    private IEnumerable<OurAccount> GetAccounts() => Services.Provider.GetService<IOurAccountRepository>()!.GetByOwner(Document.AccountId, Id);
}