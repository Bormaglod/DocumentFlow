//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.12.2021
//
// Версия 2022.9.3
//  - удалены методы IsColumnVisible, IsAllowVisibilityColumn и IsVisible
// Версия 2023.1.8
//  - в конструктор добавлен параметр settings
// Версия 2023.1.22
//  - DocumentFlow.Settings.Infrastructure перемещено в DocumentFlow.Infrastructure.Settings
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Controls;
using DocumentFlow.Infrastructure.Settings;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.Companies;

public class ContractorBrowser : Browser<Contractor>, IContractorBrowser
{
    private readonly GridNumericColumn inn;

    public ContractorBrowser(IContractorRepository repository, IPageManager pageManager, IBreadcrumb navigator, IStandaloneSettings settings) 
        : base(repository, pageManager, navigator: navigator, settings: settings) 
    {
        inn = CreateNumeric(x => x.inn, "ИНН", width: 100);

        var id = CreateText(x => x.id, "Id", width: 180, visible: false);
        var code = CreateText(x => x.code, "Наименование", hidden: false);
        var name = CreateText(x => x.item_name, "Краткое наименование", width: 150, visible: false);
        var full_name = CreateText(x => x.full_name, "Полное наименование", width: 400);
        var kpp = CreateNumeric(x => x.kpp, "КПП", width: 100, format: "0000 00 000");
        var okpo = CreateNumeric(x => x.okpo, "ОКПО", width: 100, format: "00 00000 0");
        var ogrn = CreateNumeric(x => x.ogrn, "ОГРН", width: 120, format: "0 00 00 00 00000 0");
        var okopf = CreateText(x => x.okopf_name, "ОКОПФ", width: 300);

        code.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
        okopf.AllowGrouping = true;

        AddColumns(new GridColumn[] { id, code, name, full_name, inn, kpp, okpo, ogrn, okopf });

        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [code] = ListSortDirection.Ascending
        });
    }

    protected override string HeaderText => "Контрагенты";

    protected override void OnChangeParent()
    {
        base.OnChangeParent();
        if (RootId.HasValue)
        {
            inn.Format = RootId.Value == Contractor.CompanyGroup ? "0000 00000 0" : "0000 000000 00";
        }
    }
}
