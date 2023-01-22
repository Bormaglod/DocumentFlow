//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.01.2022
//
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Editors;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Entities.Banks;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Data;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Accounts;

public abstract class BaseAccountEditor<T> : Editor<T>
    where T : Account, IDocumentInfo, new()
{
    private const int headerWidth = 120;

    public BaseAccountEditor(IRepository<Guid, T> repository, IPageManager pageManager) : base(repository, pageManager) 
    {
        var company = new DfTextBox("company_name", "Контрагент", headerWidth, 120) { Enabled = false };
        var name = new DfTextBox("item_name", "Наименование", headerWidth, 500);
        var account = new DfMaskedTextBox<decimal>("account_value", "Номер счета", headerWidth, 400, mask: "### ## ### # #### #######") { DefaultAsNull = false };
        var bank = new DfComboBox<Bank>("bank_id", "Банк", headerWidth, 400);
        
        bank.SetDataSource(() => Services.Provider.GetService<IBankRepository>()!.GetAllValid(callback: q => q.OrderBy("item_name")));

        AddControls(new Control[]
        {
            company,
            name,
            account,
            bank
        });
    }
}
