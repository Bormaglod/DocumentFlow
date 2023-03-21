//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 08.08.2022
//
// Версия 2023.1.8
//  - в конструктор добавлен параметр settings
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//  - DocumentFlow.Settings.Infrastructure перемещено в DocumentFlow.Infrastructure.Settings
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Infrastructure.Data;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Settings;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.Wages;

public abstract class BillingDocumentBrowser<T> : Browser<T>
    where T : BillingDocument
{
    public BillingDocumentBrowser(IDocumentRepository<T> repository, IPageManager pageManager, IDocumentFilter? filter = null, IStandaloneSettings? settings = null)
        : base(repository, pageManager, filter: filter, settings: settings)
    {
        AllowGrouping();

        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var date = CreateDateTime(x => x.DocumentDate, "Дата", hidden: false, width: 150);
        var number = CreateNumeric(x => x.DocumentNumber, "Номер", width: 100);
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
