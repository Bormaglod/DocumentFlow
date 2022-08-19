﻿//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 08.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Infrastructure;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.Wages;

public abstract class BillingDocumentBrowser<T> : Browser<T>
    where T : BillingDocument
{
    public BillingDocumentBrowser(IDocumentRepository<T> repository, IPageManager pageManager, IDocumentFilter filter)
        : base(repository, pageManager, filter: filter)
    {
        AllowGrouping();

        var id = CreateText(x => x.id, "Id", width: 180, visible: false);
        var date = CreateDateTime(x => x.document_date, "Дата", hidden: false, width: 150);
        var number = CreateNumeric(x => x.document_number, "Номер", width: 100);
        var billing_range = CreateText(x => x.billing_range, "Расчётный период", width: 160);
        var emps = CreateText(x => x.employee_names_text, "Сотрудники");

        emps.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;

        AddColumns(new GridColumn[] { id, date, number, billing_range, emps });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [billing_range] = ListSortDirection.Ascending
        });
    }
}