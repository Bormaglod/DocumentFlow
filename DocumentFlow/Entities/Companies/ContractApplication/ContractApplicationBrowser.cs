﻿//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 16.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Infrastructure;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Infrastructure;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.Companies;

public class ContractApplicationBrowser : Browser<ContractApplication>, IContractApplicationBrowser
{
    public ContractApplicationBrowser(IContractApplicationRepository repository, IPageManager pageManager) : base(repository, pageManager) 
    {
        Toolbar.IconSize = ButtonIconSize.Small;

        GridTextColumn id = CreateText(x => x.id, "Id", width: 180, visible: false);
        GridTextColumn code = CreateText(x => x.code, "Номер");
        GridDateTimeColumn document_date = CreateDateTime(x => x.document_date, "Дата", width: 150, format: "dd.MM.yyyy");
        GridTextColumn name = CreateText(x => x.item_name, "Наименование");
        GridDateTimeColumn date_start = CreateDateTime(x => x.date_start, "Дата начала", width: 150, format: "dd.MM.yyyy");
        GridDateTimeColumn date_end = CreateDateTime(x => x.date_end, "Дата окончания", width: 150, format: "dd.MM.yyyy");

        name.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;

        AddColumns(new GridColumn[] { id, code, document_date, name, date_start, date_end });

        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [document_date] = ListSortDirection.Ascending,
            [code] = ListSortDirection.Ascending
        });
    }

    protected override string HeaderText => "Приложения";
}