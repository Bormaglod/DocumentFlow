//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.01.2022
//
// Версия 2023.1.8
//  - в конструктор добавлен параметр settings
// Версия 2023.1.22
//  - DocumentFlow.Settings.Infrastructure перемещено в DocumentFlow.Infrastructure.Settings
// Версия 2023.2.7
//  - добавлен столбец "Ввод в экспл."
// Версия 2023.2.8
//  - добавлен столбец
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Settings;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.Equipments;

public class EquipmentBrowser : Browser<Equipment>, IEquipmentBrowser
{
    public EquipmentBrowser(IEquipmentRepository repository, IPageManager pageManager, IStandaloneSettings settings) 
        : base(repository, pageManager, settings: settings) 
    {
        var id = CreateText(x => x.id, "Id", width: 180, visible: false);
        var code = CreateText(x => x.code, "Код", width: 110);
        var name = CreateText(x => x.item_name, "Наименование", hidden: false);
        var serial = CreateText(x => x.serial_number, "Серийный номер", width: 100);
        var is_tools = CreateBoolean(x => x.is_tools, "Инструмент", width: 160);
        var commissioning = CreateDateTime(x => x.commissioning, "Ввод в экспл.", width: 150, format: "dd.MM.yyyy");
        var starting_hits = CreateNumeric(x => x.starting_hits, "Кол-во опрессовок", width: 100, visible: false);

        name.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;

        AddColumns(new GridColumn[] { id, code, name, serial, commissioning, starting_hits, is_tools });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [code] = ListSortDirection.Ascending
        });
    }

    protected override string HeaderText => "Оборудование";
}
