﻿//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Infrastructure;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.Equipments;

public class EquipmentBrowser : Browser<Equipment>, IEquipmentBrowser
{
    public EquipmentBrowser(IEquipmentRepository repository, IPageManager pageManager) : base(repository, pageManager) 
    {
        GridTextColumn id = CreateText(x => x.id, "Id", width: 180, visible: false);
        GridTextColumn code = CreateText(x => x.code, "Код", width: 110);
        GridTextColumn name = CreateText(x => x.item_name, "Наименование", hidden: false);
        GridTextColumn serial = CreateText(x => x.serial_number, "Серийный номер", width: 100);
        GridCheckBoxColumn is_tools = CreateBoolean(x => x.is_tools, "Инструмент", width: 160);

        name.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;

        AddColumns(new GridColumn[] { id, code, name, serial, is_tools });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [code] = ListSortDirection.Ascending
        });
    }

    protected override string HeaderText => "Оборудование";
}