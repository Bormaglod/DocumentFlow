//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 03.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Models;
using DocumentFlow.Interfaces;

using Microsoft.Extensions.Configuration;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DocumentFlow.ViewModels;

public class BalanceProcessingBrowser : BalanceBrowser<BalanceProcessing>, IBalanceProcessingBrowser
{
    public BalanceProcessingBrowser(IServiceProvider services, IPageManager pageManager, IBalanceProcessingRepository repository, IConfiguration configuration) 
        : base(services, pageManager, repository, configuration) 
    {
        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var name = CreateText(x => x.DocumentTypeName, "Документ", hidden: false);
        var doc_date = CreateDateTime(x => x.DocumentDate, "Дата", 150);
        var doc_number = CreateNumeric(x => x.DocumentNumber, "Номер", 100);
        var material = CreateText(x => x.MaterialName, "Материал", 200, visible: false);
        var income = CreateNumeric(x => x.Income, "Приход", 130, decimalDigits: 3);
        var expense = CreateNumeric(x => x.Expense, "Расход", 130, decimalDigits: 3);
        var remainder = CreateNumeric(x => x.Remainder, "Остаток", 130, decimalDigits: 3);

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

        CreateGrouping()
            .Add(material)
            .Register(doc_date, "DateByDay", "По дням", (string ColumnName, object o) =>
            {
                var op = (BalanceProcessing)o;
                if (op.DocumentDate.HasValue)
                {
                    return op.DocumentDate.Value.ToShortDateString();
                }

                return "NONE";
            });

        AllowSorting = false;
    }
}
