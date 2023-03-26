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
        AddControls(new Control[]
        {
            CreateTextBox(x => x.CompanyName, "Контрагент", headerWidth, 120, enabled: false),
            CreateTextBox(x => x.ItemName, "Наименование", headerWidth, 500),
            CreateMaskedTextBox<decimal>(x => x.AccountValue, "Номер счета", headerWidth, 400, mask: "### ## ### # #### #######", defaultAsNull: false),
            CreateComboBox(x => x.BankId, "Банк", headerWidth, 400, data: GetBanks)
        });
    }

    private IEnumerable<Bank> GetBanks() => Services.Provider.GetService<IBankRepository>()!.GetAllValid(callback: q => q.OrderBy("item_name"));
}
