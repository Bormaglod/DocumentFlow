//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 11.01.2022
//
// Версия 2023.2.7
//  - добавлено поле "Ввод в экспл."
// Версия 2023.2.8
//  - добавлено поле "Кол-во опрессовок"
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Infrastructure;

namespace DocumentFlow.Entities.Equipments;

public class EquipmentEditor : Editor<Equipment>, IEquipmentEditor
{
    private const int headerWidth = 140;

    public EquipmentEditor(IEquipmentRepository repository, IPageManager pageManager) : base(repository, pageManager) 
    {
        EditorControls
            .AddTextBox(x => x.Code, "Код", text =>
                text
                    .SetHeaderWidth(headerWidth)
                    .DefaultAsValue())
            .AddTextBox(x => x.ItemName, "Наименование", text =>
                text
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(400))
            .AddTextBox(x => x.SerialNumber, "Серийный номер", text =>
                text
                    .SetHeaderWidth(headerWidth))
            .AddDateTimePicker(x => x.Commissioning, "Ввод в экспл.", date =>
                date
                    .NotRequired()
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(150))
            .AddIntergerTextBox<int>(x => x.StartingHits, "Кол-во опрессовок", text =>
                text
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(150)
                    .DefaultAsValue())
            .AddToggleButton(x => x.IsTools, "Это инструмент", toggle => toggle.SetHeaderWidth(headerWidth));
    }
}