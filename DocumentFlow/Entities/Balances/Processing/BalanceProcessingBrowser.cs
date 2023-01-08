//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 03.07.2022
//
// Версия 2023.1.8
//  - в конструктор добавлен параметр settings
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure;
using DocumentFlow.Settings.Infrastructure;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.Balances;

public class BalanceProcessingBrowser : BalanceBrowser<BalanceProcessing>, IBalanceProcessingBrowser
{
    public BalanceProcessingBrowser(IBalanceProcessingRepository repository, IPageManager pageManager, IStandaloneSettings settings) 
        : base(repository, pageManager, settings: settings) 
    {
        AllowGrouping();

        var id = CreateText(x => x.id, "Id", width: 180, visible: false);
        var name = CreateText(x => x.document_type_name, "Документ", hidden: false);
        var doc_date = CreateDateTime(x => x.document_date, "Дата", 150);
        var doc_number = CreateNumeric(x => x.document_number, "Номер", 100);
        var material = CreateText(x => x.material_name, "Материал", 200, visible: false);
        var income = CreateNumeric(x => x.income, "Приход", 130, decimalDigits: 3);
        var expense = CreateNumeric(x => x.expense, "Расход", 130, decimalDigits: 3);
        var remainder = CreateNumeric(x => x.remainder, "Остаток", 130, decimalDigits: 3);

        name.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
        income.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
        expense.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
        remainder.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;

        remainder.CellStyle.BackColor = ColorTranslator.FromHtml("#DAE5F5");
        remainder.HeaderStyle.BackColor = ColorTranslator.FromHtml("#DAE5F5");

        AddColumns(new GridColumn[] { id, name, doc_date, doc_number, material, income, expense, remainder });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [doc_date] = ListSortDirection.Descending
        });

        CreateGroups()
            .Add(material);

        AllowSorting = false;
    }

    protected override string HeaderText => "Остатки дав. сырья";
}
