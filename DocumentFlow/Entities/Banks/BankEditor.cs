//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.01.2022
//
// Версия 2023.4.2
//  - создание элементов управления в конструкторе реализовано с 
//    помощью свойства EditorControls
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Infrastructure;

namespace DocumentFlow.Entities.Banks;

public class BankEditor : Editor<Bank>, IBankEditor
{
    private const int headerWidth = 120;

    public BankEditor(IBankRepository repository, IPageManager pageManager) : base(repository, pageManager) 
    {
        EditorControls
            .CreateTextBox(x => x.ItemName, "Наименование", (text) =>
                text
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(400))
            .CreateMaskedTextBox<decimal>(x => x.Bik, "БИК", (text) =>
                text
                    .SetMask("## ## ## ###")
                    .SetHeaderWidth(headerWidth)
                    .DefaultAsValue())
            .CreateMaskedTextBox<decimal>(x => x.Account, "Корр. счёт", (text) =>
                text
                    .SetMask("### ## ### # ######## ###")
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(180)
                    .DefaultAsValue())
            .CreateTextBox(x => x.Town, "Город", (text) =>
                text
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(400)
                    .SetEditorWidth(180));
    }
}