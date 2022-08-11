//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 26.06.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Editors;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Infrastructure;

namespace DocumentFlow.Entities.Products;

public class WireEditor : Editor<Wire>, IWireEditor
{
    private const int headerWidth = 140;

    public WireEditor(IWireRepository repository, IPageManager pageManager) : base(repository, pageManager) 
    {
        var code = new DfTextBox("code", "Код", headerWidth, 100) { DefaultAsNull = false };
        var name = new DfTextBox("item_name", "Наименование", headerWidth, 400);
        var wsize = new DfNumericTextBox("wsize", "Сечение", headerWidth, 100) { NumberDecimalDigits = 2 };
        
        AddControls(new Control[]
        {
            code, 
            name, 
            wsize
        });
    }
}