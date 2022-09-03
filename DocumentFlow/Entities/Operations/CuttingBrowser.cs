//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.01.2022
//
// Версия 2022.9.3
//  - удалены методы IsColumnVisible и IsAllowVisibilityColumn
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Infrastructure;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Infrastructure;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.Operations;

public class CuttingBrowser : Browser<Cutting>, ICuttingBrowser
{
    public CuttingBrowser(ICuttingRepository repository, IPageManager pageManager, IBreadcrumb navigator) : base(repository, pageManager, navigator: navigator)
    {
        AllowGrouping();

        var id = CreateText(x => x.id, "Id", width: 180, visible: false);
        var code = CreateText(x => x.code, "Код", width: 150, visible: false);
        var name = CreateText(x => x.item_name, "Наименование", hidden: false);
        var produced = CreateNumeric(x => x.produced, "Выработка", width: 100, visible: false);
        var prod_time = CreateNumeric(x => x.prod_time, "Время выработки, сек.", width: 100, visible: false);
        var production_rate = CreateNumeric(x => x.production_rate, "Норма выработки, ед./час", width: 100, visible: false);
        var segment_length = CreateNumeric(x => x.segment_length, "Длина резки", width: 100, visible: false);
        var left_cleaning = CreateNumeric(x => x.left_cleaning, "Длина зачистки слева", width: 100, visible: false, decimalDigits: 1);
        var left_sweep = CreateNumeric(x => x.left_sweep, "Ширина окна слева", width: 100, visible: false);
        var right_cleaning = CreateNumeric(x => x.right_cleaning, "Длина зачистки справа", width: 100, visible: false, decimalDigits: 1);
        var right_sweep = CreateNumeric(x => x.right_sweep, "Ширина окна справа", width: 100, visible: false);
        var program_number = CreateNumeric(x => x.program_number, "Программа", width: 100, visible: false);
        var salary = CreateNumeric(x => x.salary, "Зар. плата", width: 100, visible: false, decimalDigits: 4);
        var operation_using = CreateBoolean(x => x.operation_using, "Используется", 120);

        name.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
        produced.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
        prod_time.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
        production_rate.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
        segment_length.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
        left_cleaning.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
        left_sweep.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
        right_cleaning.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
        right_sweep.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
        program_number.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
        salary.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;

        AddColumns(new GridColumn[] { id, code, name, produced, prod_time, production_rate, segment_length, left_cleaning, left_sweep, right_cleaning, right_sweep, program_number, salary, operation_using });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [name] = ListSortDirection.Ascending
        });
    }

    protected override string HeaderText => "Резка";
}
