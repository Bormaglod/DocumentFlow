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

namespace DocumentFlow.Entities.OkpdtrLib;

public class OkpdtrEditor : Editor<Okpdtr>, IOkpdtrEditor
{
    private const int headerWidth = 200;

    public OkpdtrEditor(IOkpdtrRepository repository, IPageManager pageManager) : base(repository, pageManager) 
    {
        EditorControls
            .CreateTextBox(x => x.Code, "Код", (text) =>
                text
                    .SetHeaderWidth(headerWidth)
                    .DefaultAsValue())
            .CreateTextBox(x => x.ItemName, "Наименование", (text) =>
                text
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(400))
            .CreateTextBox(x => x.SignatoryName, "Наименование для договоров", (text) =>
                text
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(400));
    }
}