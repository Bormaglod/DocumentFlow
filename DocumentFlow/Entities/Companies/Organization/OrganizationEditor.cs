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
        EditorControls
            .AddTextBox(x => x.Code, "Наименование", (text) =>
                text
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(400)
                    .DefaultAsValue())
            .AddTextBox(x => x.ItemName, "Короткое наименование", (text) =>
                text
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(500))
            .AddTextBox(x => x.FullName, "Полное наименование", (text) =>
                text
                    .Multiline()
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(500))
            .AddMaskedTextBox<decimal>(x => x.Inn, "ИНН", (text) =>
                text
                    .SetMask("#### ##### #")
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(200))
            .AddMaskedTextBox<decimal>(x => x.Kpp, "КПП", (text) =>
                text
                    .SetMask("#### ## ###")
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(200))
            .AddMaskedTextBox<decimal>(x => x.Ogrn, "ОГРН", (text) =>
                text
                    .SetMask("# ## ## ## ##### #")
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(200))
            .AddMaskedTextBox<decimal>(x => x.Okpo, "ОКПО", (text) =>
                text
                    .SetMask("## ##### #")
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(200))
            .AddComboBox<Okopf>(x => x.OkopfId, "ОКОПФ", (combo) =>
                combo
                    .SetDataSource(GetOkopfs)
                    .EnableEditor<IOkopfEditor>()
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(450))
            .AddComboBox<OurAccount>(x => x.AccountId, "Расчётный счёт", (combo) =>
                combo
                    .SetDataSource(GetAccounts)
                    .EnableEditor<IOurAccountEditor>()
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(450))
            .AddTextBox(x => x.Address, "Адрес", (text) =>
                text
                    .Multiline()
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(500))
            .AddTextBox(x => x.Phone, "Телефон", (text) =>
                text
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(250))
            .AddTextBox(x => x.Email, "Эл. почта", (text) =>
                text
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(250))
            .AddToggleButton(x => x.DefaultOrg, "Основная организация", (toggle) => 
                toggle
                    .SetHeaderWidth(headerWidth));
    }

    protected override void RegisterNestedBrowsers()
    {
        base.RegisterNestedBrowsers();
        RegisterNestedBrowser<IOurAccountBrowser, OurAccount>();
        RegisterNestedBrowser<IOrgEmployeeBrowser, OurEmployee>();
    }

    private IEnumerable<Okopf> GetOkopfs() => Services.Provider.GetService<IOkopfRepository>()!.GetListExisting(callback: q => q.OrderBy("item_name"));

    private IEnumerable<OurAccount> GetAccounts() => Services.Provider.GetService<IOurAccountRepository>()!.GetByOwner(Document.AccountId, Id);
}