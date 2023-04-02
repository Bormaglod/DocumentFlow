//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.01.2022
//
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
// Версия 2023.4.2
//  - создание элементов управления в конструкторе реализовано с 
//    помощью свойства EditorControls
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
        EditorControls
            .CreateTextBox(x => x.CompanyName, "Контрагент", (textBox) =>
                textBox
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(120)
                    .Disable())
            .CreateTextBox(x => x.ItemName, "Наименование", (textBox) =>
                textBox
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(500))
            .CreateMaskedTextBox<decimal>(x => x.AccountValue, "Номер счета", (textBox) => 
                textBox
                    .SetMask("### ## ### # #### #######")
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(400)
                    .DefaultAsValue())
            .CreateComboBox<Bank>(x => x.BankId, "Банк", (combo) => 
                combo
                    .SetDataSource(GetBanks)
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(400));
    }

    private IEnumerable<Bank> GetBanks() => Services.Provider.GetService<IBankRepository>()!.GetAllValid(callback: q => q.OrderBy("item_name"));
}
