//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 31.01.2022
//
// Версия 2022.11.13
//  - в конструкторе изменени тип параметра repository на IBalanceProductRepository
//  - в контекстное меню добавлен пункт для перепроведения документа
//    изменившего остаток
//  - в контекстное меню добавлен пункт для пересчёта остатока
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure;
using DocumentFlow.Properties;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.Balances;

public abstract class BalanceProductBrowser<T> : BalanceBrowser<T>
    where T : BalanceProduct
{
    public BalanceProductBrowser(IBalanceProductRepository<T> repository, IPageManager pageManager) : base(repository, pageManager) 
    {
        var id = CreateText(x => x.id, "Id", width: 180, visible: false);
        var name = CreateText(x => x.document_type_name, "Документ", hidden: false);
        var doc_date = CreateDateTime(x => x.document_date, "Дата", 150);
        var doc_number = CreateNumeric(x => x.document_number, "Номер", 100);
        var operation_summa = CreateCurrency(x => x.operation_summa, "Сумма", width: 100);
        var income = CreateNumeric(x => x.income, "Приход", 130, decimalDigits: 3);
        var expense = CreateNumeric(x => x.expense, "Расход", 130, decimalDigits: 3);
        var remainder = CreateNumeric(x => x.remainder, "Остаток (кол-во)", 130, decimalDigits: 3);
        var monetary = CreateCurrency(x => x.monetary_balance, "Остаток (сумма)", 130);

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

        ContextMenu.Add("Перепровести документ изменивший остаток", (_) =>
        {
            if (CurrentDocument != null)
            {
                repository.ReacceptChangedDocument(CurrentDocument);
            }
        });

        ContextMenu.Add("Пересчитать остатки", (_) =>
        {
            if (CurrentDocument != null)
            {
                repository.UpdateMaterialRemaind(CurrentDocument);
            }
        }, false);
    }

    protected override string HeaderText => "Остатки";
}
