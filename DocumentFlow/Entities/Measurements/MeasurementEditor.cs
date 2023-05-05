//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.12.2021
//
// Версия 2023.4.2
//  - создание элементов управления в конструкторе реализовано с 
//    помощью свойства EditorControls
// Версия 2023.5.5
//  - уствановка SetHeaderWidth перенесена из элементов управления в
//    IControls
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Infrastructure;

namespace DocumentFlow.Entities.Measurements;

public class MeasurementEditor : Editor<Measurement>, IMeasurementEditor
{
    public MeasurementEditor(IMeasurementRepository repository, IPageManager pageManager) : base(repository, pageManager) 
    {
        EditorControls
            .SetHeaderWidth(140)
            .SetEditorWidth(400)
            .AddTextBox(x => x.Code, "Код", text => text
                .SetDefaultEditorWidth()
                .DefaultAsValue())
            .AddTextBox(x => x.ItemName, "Наименование")
            .AddTextBox(x => x.Abbreviation, "Сокр. наименование");
    }
}