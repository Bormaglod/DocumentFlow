//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.01.2022
//
// Версия 2023.1.8
//  - в конструктор добавлен параметр settings
// Версия 2022.1.19
//  - добавлена колонка "Оплата"
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//  - DocumentFlow.Settings.Infrastructure перемещено в DocumentFlow.Infrastructure.Settings
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Data;
using DocumentFlow.Infrastructure.Settings;

using Syncfusion.WinForms.DataGrid;

namespace DocumentFlow.Entities.Waybills;

public class WaybillSaleBrowser : WaybillBrowser<WaybillSale>, IWaybillSaleBrowser
{
    public WaybillSaleBrowser(IWaybillSaleRepository repository, IPageManager pageManager, IDocumentFilter filter, IStandaloneSettings settings)
        : base(repository, pageManager, filter: filter, settings: settings)
    {
        var payment_exists = CreateBoolean(x => x.payment_exists, "Оплата", 100);
        
        AddColumns(new GridColumn[] { payment_exists });
    }

    protected override string HeaderText => "Реализация";
}
