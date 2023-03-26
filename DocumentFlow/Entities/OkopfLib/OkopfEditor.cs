//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Infrastructure;

namespace DocumentFlow.Entities.OkopfLib;

public class OkopfEditor : Editor<Okopf>, IOkopfEditor
{
    private const int headerWidth = 140;

    public OkopfEditor(IOkopfRepository repository, IPageManager pageManager) : base(repository, pageManager) 
    {
        AddControls(new Control[]
        {
            CreateTextBox(x => x.Code, "Код", headerWidth, 100, defaultAsNull: false),
            CreateTextBox(x => x.ItemName, "Наименование", headerWidth, 400)
        });
    }
}