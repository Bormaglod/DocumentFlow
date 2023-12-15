//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.12.2021
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Data.Models;

using Microsoft.Extensions.Configuration;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.ViewModels;

public class ContractorBrowser : BrowserPage<Contractor>, IContractorBrowser
{
    private readonly IContractorRepository repository;
    private readonly GridNumericColumn inn;

    public ContractorBrowser(IServiceProvider services, IContractorRepository repository, IConfiguration configuration, IBreadcrumb navigator)
        : base(services, repository, configuration, navigator: navigator)
    {
        this.repository = repository;

        inn = CreateNumeric(x => x.Inn, "ИНН", width: 100);

        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var code = CreateText(x => x.Code, "Наименование", hidden: false);
        var name = CreateText(x => x.ItemName, "Краткое наименование", width: 150, visible: false);
        var full_name = CreateText(x => x.FullName, "Полное наименование", width: 400);
        var kpp = CreateNumeric(x => x.Kpp, "КПП", width: 100, format: "0000 00 000");
        var okpo = CreateNumeric(x => x.Okpo, "ОКПО", width: 100, format: "00 00000 0");
        var ogrn = CreateNumeric(x => x.Ogrn, "ОГРН", width: 120, format: "0 00 00 00 00000 0");
        var okopf = CreateText(x => x.OkopfName, "ОКОПФ", width: 300);

        code.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
        okopf.AllowGrouping = true;

        AddColumns(new GridColumn[] { id, code, name, full_name, inn, kpp, okpo, ogrn, okopf });

        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [code] = ListSortDirection.Ascending
        });
    }

    protected override void OnChangeParent()
    {
        base.OnChangeParent();
        if (ParentId != null) 
        {
            inn.Format = repository.GetRoot(ParentId.Value) == Contractor.CompanyGroup ? "0000 00000 0" : "0000 000000 00";
        }
        else
        {
            inn.Format = string.Empty;
        }
    }
}
