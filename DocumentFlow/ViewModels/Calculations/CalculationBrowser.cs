//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 11.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Exceptions;
using DocumentFlow.Data.Models;
using DocumentFlow.Interfaces;

using Microsoft.Extensions.Configuration;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Styles;
using Syncfusion.WinForms.Input.Enums;

using System.ComponentModel;

using SE = Syncfusion.WinForms.DataGrid.Enums;
using SWF = System.Windows.Forms;

namespace DocumentFlow.ViewModels;

public class CalculationBrowser : BrowserPage<Calculation>, ICalculationBrowser
{
    private readonly IContextMenuItem stateApproved;
    private readonly IContextMenuItem stateExpired;
    private readonly ICalculationRepository repository;

    public CalculationBrowser(IServiceProvider services, IPageManager pageManager, ICalculationRepository repository, IConfiguration configuration) 
        : base(services, pageManager, repository, configuration) 
    {
        this.repository = repository;

        ToolBar.SmallIcons();

        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var code = CreateText(x => x.Code, "Код", width: 130);
        var state = CreateText(x => x.CalculationStateName, "Состояние");
        var approval = CreateDateTime(x => x.DateApproval, "Дата утв.", width: 100, format: "dd.MM.yyyy");
        var weight = CreateNumeric(x => x.Weight, "Вес, г", width: 80, decimalDigits: 3);
        var time = CreateNumeric(x => x.ProducedTime, "Время изг., с", width: 120, decimalDigits: 1);
        var stimul_payment = CreateNumeric(x => x.StimulPayment, "Стимул. выпл.", width: 100, decimalDigits: 2);
        var stimul_type_name = CreateText(x => x.StimulTypeName, "Способ начисления", width: 110);
        var cost_price = CreateCurrency(x => x.CostPrice, "Себестоимость", width: 130);
        var profit_percent = CreateNumeric(x => x.ProfitPercent, "Прибыль, %", width: 120, mode: FormatMode.Percent, decimalDigits: 2);
        var profit_value = CreateCurrency(x => x.ProfitValue, "Прибыль", width: 100);
        var price = CreateCurrency(x => x.Price, "Цена", width: 80);

        state.AutoSizeColumnsMode = SE.AutoSizeColumnsMode.Fill;
        weight.CellStyle.HorizontalAlignment = SWF.HorizontalAlignment.Right;
        time.CellStyle.HorizontalAlignment = SWF.HorizontalAlignment.Right;
        profit_percent.CellStyle.HorizontalAlignment = SWF.HorizontalAlignment.Center;
        stimul_payment.CellStyle.HorizontalAlignment = SWF.HorizontalAlignment.Right;

        time.AllowHeaderTextWrapping = true;
        stimul_payment.AllowHeaderTextWrapping = true;
        stimul_type_name.AllowHeaderTextWrapping = true;
        profit_percent.AllowHeaderTextWrapping = true;

        approval.AllowNull = true;
        approval.NullDisplayText = "Не установлена";

        AddColumns(new GridColumn[] { id, code, state, approval, weight, time, stimul_type_name, stimul_payment, cost_price, profit_percent, profit_value, price });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [code] = ListSortDirection.Ascending
        });

        stateApproved = ContextMenu.CreateItem("Утвердить", ApproveCalculation);
        stateExpired = ContextMenu.CreateItem("В архив", ArchiveCalculation);
        ContextMenu.AddItems(new IContextMenuItem[] { stateApproved, stateExpired });
    }

    protected override void BrowserCellStyle(Calculation document, string column, CellStyleInfo style)
    {
        if (column == nameof(Calculation.CalculationStateName))
        {
            style.TextColor = document.CalculationState switch
            {
                CalculationState.Prepare => Color.FromArgb(52, 101, 164),
                CalculationState.Approved => Color.FromArgb(18, 118, 34),
                CalculationState.Expired => Color.FromArgb(201, 33, 30),
                _ => throw new NotImplementedException()
            };
        }
    }

    protected override void DoContextRecordMenuOpening()
    {
        if (CurrentDocument != null) 
        {
            stateApproved.Visible = CurrentDocument.CalculationState == CalculationState.Prepare;
            stateExpired.Visible = CurrentDocument.CalculationState == CalculationState.Approved;
        }
    }

    private void ApproveCalculation(object? sender, EventArgs args)
    {
        if (CurrentDocument != null)
        {
            CurrentDocument.CalculationState = CalculationState.Approved;
            try
            {
                repository.Update(CurrentDocument);
                RefreshGrid();
            }
            catch (RepositoryException e)
            {
                MessageBox.Show(e.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private void ArchiveCalculation(object? sender, EventArgs args)
    {
        if (CurrentDocument != null)
        {
            CurrentDocument.CalculationState = CalculationState.Expired;
            try
            {
                repository.Update(CurrentDocument);
                RefreshGrid();
            }
            catch (RepositoryException e)
            {
                MessageBox.Show(e.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
