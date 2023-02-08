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

using DocumentFlow.Controls.Editors;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Infrastructure;

namespace DocumentFlow.Entities.Equipments;

public class EquipmentEditor : Editor<Equipment>, IEquipmentEditor
{
    private const int headerWidth = 140;

    public EquipmentEditor(IEquipmentRepository repository, IPageManager pageManager) : base(repository, pageManager) 
    {
        var code = new DfTextBox("code", "Код", headerWidth, 100) { DefaultAsNull = false };
        var name = new DfTextBox("item_name", "Наименование", headerWidth, 400);
        var serial = new DfTextBox("serial_number", "Серийный номер", headerWidth, 100);
        var is_tools = new DfToggleButton("is_tools", "Это инструмент", headerWidth);
        var commissioning = new DfDateTimePicker("commissioning", "Ввод в экспл.", headerWidth, 150) { Required = false };
        var starting_hits = new DfIntegerTextBox<int>("starting_hits", "Кол-во опрессовок", headerWidth, 150) { DefaultAsNull = true };

        AddControls(new Control[]
        {
            code,
            name,
            serial,
            commissioning,
            starting_hits,
            is_tools
        });
    }
}