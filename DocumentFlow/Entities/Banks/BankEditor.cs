//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Editors;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Infrastructure;

namespace DocumentFlow.Entities.Banks;

public class BankEditor : Editor<Bank>, IBankEditor
{
    private const int headerWidth = 120;

    public BankEditor(IBankRepository repository, IPageManager pageManager) : base(repository, pageManager) 
    {
        var name = new DfTextBox("item_name", "Наименование", headerWidth, 400);
        var bik = new DfMaskedTextBox<decimal>("bik", "БИК", headerWidth, 100, mask: "## ## ## ###") { DefaultAsNull = false };
        var account = new DfMaskedTextBox<decimal>("account", "Корр. счёт", headerWidth, 180, mask: "### ## ### # ######## ###") { DefaultAsNull = false };
        var town = new DfTextBox("town", "Город", headerWidth, 180);

        AddControls(new Control[]
        {
            name,
            bik,
            account,
            town
        });
    }
}