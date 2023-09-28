//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Data.Models;
using DocumentFlow.Interfaces;

using Microsoft.Extensions.Configuration;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.ViewModels;

public class CuttingBrowser : BrowserPage<Cutting>, ICuttingBrowser
{
    public CuttingBrowser(IServiceProvider services, IPageManager pageManager, ICuttingRepository repository, IConfiguration configuration, IBreadcrumb navigator) 
        : base(services, pageManager, repository, configuration, navigator: navigator)
    {
        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var code = CreateText(x => x.Code, "Код", width: 150, visible: false);
        var name = CreateText(x => x.ItemName, "Наименование", hidden: false);
        var produced = CreateNumeric(x => x.Produced, "Выработка", width: 100);
        var prod_time = CreateNumeric(x => x.ProdTime, "Время выработки, сек.", width: 100);
        var production_rate = CreateNumeric(x => x.ProductionRate, "Норма выработки, ед./час", width: 100);
        var segment_length = CreateNumeric(x => x.SegmentLength, "Длина резки", width: 100);
        var left_cleaning = CreateNumeric(x => x.LeftCleaning, "Длина зачистки слева", width: 100, visible: false, decimalDigits: 1);
        var left_sweep = CreateNumeric(x => x.LeftSweep, "Ширина окна слева", width: 100, visible: false);
        var right_cleaning = CreateNumeric(x => x.RightCleaning, "Длина зачистки справа", width: 100, visible: false, decimalDigits: 1);
        var right_sweep = CreateNumeric(x => x.RightSweep, "Ширина окна справа", width: 100, visible: false);
        var program_number = CreateNumeric(x => x.ProgramNumber, "Программа", width: 100);
        var salary = CreateNumeric(x => x.Salary, "Зар. плата", width: 100, decimalDigits: 4);
        var operation_using = CreateBoolean(x => x.OperationUsing, "Используется", 120);
        var date_norm = CreateDateTime(x => x.DateNorm, "Дата нормир.", 100, format: "dd.MM.yyyy", visible: false);

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

        AddColumns(new GridColumn[] { id, code, name, produced, prod_time, production_rate, segment_length, left_cleaning, left_sweep, right_cleaning, right_sweep, program_number, salary, date_norm, operation_using });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [name] = ListSortDirection.Ascending
        });

        CreateGrouping();
    }
}
