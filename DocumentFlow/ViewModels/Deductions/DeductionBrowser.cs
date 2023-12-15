//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Data.Models;

using Microsoft.Extensions.Configuration;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.Input.Enums;

using System.ComponentModel;

namespace DocumentFlow.ViewModels;

public class DeductionBrowser : BrowserPage<Deduction>, IDeductionBrowser
{
    public DeductionBrowser(IServiceProvider services, IDeductionRepository repository, IConfiguration configuration) 
        : base(services, repository, configuration) 
    {
        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var name = CreateText(x => x.ItemName, "Наименование", hidden: false);
        var base_calc = CreateText(x => x.BaseCalcText, "База для начисления", width: 160);
        var percent = CreateNumeric(x => x.PercentValue, "Процент", width: 100, mode: FormatMode.Percent, decimalDigits: 2);
        var fix = CreateCurrency(x => x.FixValue, "Фикс. сумма", width: 100);

        name.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
        percent.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;

        AddColumns(new GridColumn[] { id, name, base_calc, percent, fix });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [name] = ListSortDirection.Ascending
        });
    }
}
