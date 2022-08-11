//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Infrastructure;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.Productions.Returns;

public class ReturnMaterialsBrowser : Browser<ReturnMaterials>, IReturnMaterialsBrowser
{
    public ReturnMaterialsBrowser(IReturnMaterialsRepository repository, IPageManager pageManager, IDocumentFilter filter)
        : base(repository, pageManager, filter: filter)
    {
        AllowGrouping();

        var id = CreateText(x => x.id, "Id", width: 180, visible: false);
        var date = CreateDateTime(x => x.document_date, "Дата", hidden: false, width: 150);
        var number = CreateNumeric(x => x.document_number, "Номер", width: 100);
        var order_date = CreateDateTime(x => x.order_date, "Дата", width: 150);
        var order_number = CreateNumeric(x => x.order_number, "Номер", width: 100);
        var contractor = CreateText(x => x.contractor_name, "Контрагент");
        var contract = CreateText(x => x.contract_name, "Договор", width: 200, visible: false);

        contractor.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;

        AddColumns(new GridColumn[] { id, date, number, order_date, order_number, contractor, contract });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [date] = ListSortDirection.Ascending,
            [number] = ListSortDirection.Ascending
        });

        CreateStackedColumns("Заказ", new string[] { "order_date", "order_number" });
    }

    protected override string HeaderText => "Возврат материалов заказчику";
}
