﻿//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 31.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Models;

using Microsoft.Extensions.Configuration;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.ViewModels;

public abstract class BalanceProductBrowser<T> : BalanceBrowser<T>
    where T : BalanceProduct
{
    public BalanceProductBrowser(IServiceProvider services, IBalanceProductRepository<T> repository, IConfiguration configuration)
        : base(services, repository, configuration) 
    {
        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var name = CreateText(x => x.DocumentTypeName, "Документ", hidden: false);
        var doc_date = CreateDateTime(x => x.DocumentDate, "Дата", 150);
        var doc_number = CreateNumeric(x => x.DocumentNumber, "Номер", 100);
        var operation_summa = CreateCurrency(x => x.OperationSumma, "Сумма", width: 100);
        var income = CreateNumeric(x => x.Income, "Приход", 130, decimalDigits: 3);
        var expense = CreateNumeric(x => x.Expense, "Расход", 130, decimalDigits: 3);
        var remainder = CreateNumeric(x => x.Remainder, "Остаток (кол-во)", 130, decimalDigits: 3);
        var monetary = CreateCurrency(x => x.MonetaryBalance, "Остаток (сумма)", 130);

        name.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
        income.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
        expense.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
        remainder.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;

        remainder.CellStyle.BackColor = ColorTranslator.FromHtml("#DAE5F5");
        remainder.HeaderStyle.BackColor = ColorTranslator.FromHtml("#DAE5F5");

        monetary.CellStyle.BackColor = ColorTranslator.FromHtml("#FFC873");
        monetary.HeaderStyle.BackColor = ColorTranslator.FromHtml("#FFC873");

        AddColumns(new GridColumn[] { id, name, doc_date, doc_number, operation_summa, income, expense, remainder, monetary });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [doc_date] = ListSortDirection.Descending
        });

        AllowSorting = false;


        ContextMenu
            .AddSeparator()
            .Add("Перепровести документ изменивший остаток", (s, e) =>
            {
                if (CurrentDocument != null)
                {
                    repository.ReacceptChangedDocument(CurrentDocument);
                }
            });
    }
}
