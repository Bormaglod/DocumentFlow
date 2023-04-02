﻿//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.12.2021
//
// Версия 2023.1.8
//  - в конструктор добавлен параметр settings
// Версия 2023.1.22
//  - DocumentFlow.Settings.Infrastructure перемещено в DocumentFlow.Infrastructure.Settings
// Версия 2023.4.2
//  - добавлен столбец "Наим. для договоров"
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Settings;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.OkpdtrLib;

public class OkpdtrBrowser : Browser<Okpdtr>, IOkpdtrBrowser
{
    public OkpdtrBrowser(IOkpdtrRepository repository, IPageManager pageManager, IStandaloneSettings settings) 
        : base(repository, pageManager, settings: settings) 
    {
        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var code = CreateText(x => x.Code, "Код", width: 110);
        var name = CreateText(x => x.ItemName, "Наименование", hidden: false);
        var sign = CreateText(x => x.SignatoryName, "Наим. для договоров", width: 500);

        name.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;

        AddColumns(new GridColumn[] { id, code, name, sign });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [code] = ListSortDirection.Ascending
        });
    }

    protected override string HeaderText => "ОКПДТР";
}
