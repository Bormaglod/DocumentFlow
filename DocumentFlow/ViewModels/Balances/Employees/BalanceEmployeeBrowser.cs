﻿//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Models;
using DocumentFlow.Interfaces;

using Microsoft.Extensions.Configuration;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.ViewModels;

public class BalanceEmployeeBrowser : BalanceBrowser<BalanceEmployee>, IBalanceEmployeeBrowser
{
    public BalanceEmployeeBrowser(IServiceProvider services, IPageManager pageManager, IBalanceEmployeeRepository repository, IConfiguration configuration) 
        : base(services, pageManager, repository, configuration) 
    {
        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var name = CreateText(x => x.DocumentTypeName, "Документ", hidden: false);
        var doc_date = CreateDateTime(x => x.DocumentDate, "Дата", 150);
        var doc_number = CreateNumeric(x => x.DocumentNumber, "Номер", 100);
        var c_debt = CreateCurrency(x => x.EmployeeDebt, "Долг сотрудника.", 150);
        var o_debt = CreateCurrency(x => x.OrganizationDebt, "Наш долг", 130);
        var debt = CreateCurrency(x => x.Debt, "Текущий долг", 130);

        name.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
        c_debt.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
        o_debt.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
        debt.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;

        debt.CellStyle.BackColor = ColorTranslator.FromHtml("#FFC873");
        debt.HeaderStyle.BackColor = ColorTranslator.FromHtml("#FFC873");

        AddColumns(new GridColumn[] { id, name, doc_date, doc_number, c_debt, o_debt, debt });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [doc_date] = ListSortDirection.Descending
        });

        AllowSorting = false;
    }
}