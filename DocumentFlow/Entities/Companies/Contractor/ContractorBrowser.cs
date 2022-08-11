//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.12.2021
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Infrastructure;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Infrastructure;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.Companies;

public class ContractorBrowser : Browser<Contractor>, IContractorBrowser
{
    private readonly GridNumericColumn inn;

    public ContractorBrowser(IContractorRepository repository, IPageManager pageManager, IBreadcrumb navigator) : base(repository, pageManager, navigator: navigator) 
    {
        inn = CreateNumeric(x => x.inn, "ИНН", width: 100);

        GridTextColumn id = CreateText(x => x.id, "Id", width: 180, visible: false);
        GridTextColumn code = CreateText(x => x.code, "Наименование", hidden: false);
        GridTextColumn name = CreateText(x => x.item_name, "Краткое наименование", width: 150, visible: false);
        GridTextColumn full_name = CreateText(x => x.full_name, "Полное наименование", width: 400);
        GridNumericColumn kpp = CreateNumeric(x => x.kpp, "КПП", width: 100, format: "0000 00 000");
        GridNumericColumn okpo = CreateNumeric(x => x.okpo, "ОКПО", width: 100, format: "00 00000 0");
        GridNumericColumn ogrn = CreateNumeric(x => x.ogrn, "ОГРН", width: 120, format: "0 00 00 00 00000 0");
        GridTextColumn okopf = CreateText(x => x.okopf_name, "ОКОПФ", width: 300);

        code.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
        okopf.AllowGrouping = true;

        AddColumns(new GridColumn[] { id, code, name, full_name, inn, kpp, okpo, ogrn, okopf });

        ChangeColumnsVisible(full_name, inn, kpp, okpo, ogrn, okopf);
        AllowColumnVisibility(name, full_name, inn, kpp, okpo, ogrn, okopf);

        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [code] = ListSortDirection.Ascending
        });
    }

    protected override string HeaderText => "Контрагенты";

    protected override bool? IsColumnVisible(GridColumn column) => IsVisible(column);

    protected override bool? IsAllowVisibilityColumn(GridColumn column) => IsVisible(column);

    protected override void OnChangeParent()
    {
        base.OnChangeParent();
        if (RootId.HasValue)
        {
            inn.Format = RootId.Value == Contractor.CompanyGroup ? "0000 00000 0" : "0000 000000 00";
        }
    }

    private bool IsVisible(GridColumn column)
    {
        bool res = RootId.HasValue;
        if (column != inn)
        {
            res = res && (RootId ?? Guid.Empty) == Contractor.CompanyGroup;
        }

        return res;
    }
}
