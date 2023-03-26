//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Infrastructure;

namespace DocumentFlow.Entities.Banks;

public class BankEditor : Editor<Bank>, IBankEditor
{
    private const int headerWidth = 120;

    public BankEditor(IBankRepository repository, IPageManager pageManager) : base(repository, pageManager) 
    {
        AddControls(new Control[]
        {
            CreateTextBox(x => x.ItemName, "Наименование", headerWidth, 400),
            CreateMaskedTextBox<decimal>(x => x.Bik, "БИК", headerWidth, 100, mask: "## ## ## ###", defaultAsNull: false),
            CreateMaskedTextBox<decimal>(x => x.Account, "Корр. счёт", headerWidth, 180, mask: "### ## ### # ######## ###", defaultAsNull: false),
            CreateTextBox(x => x.Town, "Город", headerWidth, 180)
        });
    }
}