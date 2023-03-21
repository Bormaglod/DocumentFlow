//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 31.01.2022
//
// Версия 2022.11.13
//  - в конструкторе изменени тип параметра repository на IBalanceProductRepository
//  - в контекстное меню добавлен пункт для перепроведения документа
//    изменившего остаток
//  - в контекстное меню добавлен пункт для пересчёта остатка
// Версия 2022.11.15
//  - пункт меню для пересчёта остатка переименован в пересчёт суммы остатка
//    и перенесен в BalanceMaterialBrowser
// Версия 2023.1.8
//  - в конструктор добавлен параметр settings
// Версия 2023.1.22
//  - DocumentFlow.Settings.Infrastructure перемещено в DocumentFlow.Infrastructure.Settings
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Settings;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.Balances;

public abstract class BalanceProductBrowser<T> : BalanceBrowser<T>
    where T : BalanceProduct
{
    public BalanceProductBrowser(IBalanceProductRepository<T> repository, IPageManager pageManager, IStandaloneSettings? settings = null) 
        : base(repository, pageManager, settings: settings) 
    {
        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var name = CreateText(x => x.document_type_name, "Документ", hidden: false);
        var doc_date = CreateDateTime(x => x.DocumentDate, "Дата", 150);
        var doc_number = CreateNumeric(x => x.DocumentNumber, "Номер", 100);
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
    }

    protected override string HeaderText => "Остатки";
}
