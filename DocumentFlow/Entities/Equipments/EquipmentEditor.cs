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
        AddControls(new Control[]
        {
            CreateTextBox(x => x.Code, "Код", headerWidth, 100, defaultAsNull: false),
            CreateTextBox(x => x.ItemName, "Наименование", headerWidth, 400),
            CreateTextBox(x => x.SerialNumber, "Серийный номер", headerWidth, 100),
            CreateDateTimePicker(x => x.Commissioning, "Ввод в экспл.", headerWidth, 150, required: false),
            CreateIntegerTextBox<int>(x => x.StartingHits, "Кол-во опрессовок", headerWidth, 150, defaultAsNull: true),
            CreateToggleButton(x => x.IsTools, "Это инструмент", headerWidth)
        });
    }
}