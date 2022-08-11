﻿//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Infrastructure;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.Input.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.Deductions;

public class DeductionBrowser : Browser<Deduction>, IDeductionBrowser
{
    public DeductionBrowser(IDeductionRepository repository, IPageManager pageManager) : base(repository, pageManager) 
    {
        GridTextColumn id = CreateText(x => x.id, "Id", width: 180, visible: false);
        GridTextColumn name = CreateText(x => x.item_name, "Наименование", hidden: false);
        GridTextColumn base_calc = CreateText(x => x.base_calc_text, "База для начисления", width: 160);
        GridNumericColumn percent = CreateNumeric(x => x.percent_value, "Процент", width: 100, mode: FormatMode.Percent, decimalDigits: 2);
        GridNumericColumn fix = CreateCurrency(x => x.fix_value, "Фикс. сумма", width: 100);

        name.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
        percent.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;

        AddColumns(new GridColumn[] { id, name, base_calc, percent, fix });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [name] = ListSortDirection.Ascending
        });
    }

    protected override string HeaderText => "Удержания";
}
