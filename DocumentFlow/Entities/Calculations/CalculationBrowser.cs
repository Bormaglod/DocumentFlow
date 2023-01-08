//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 11.01.2022
//
// Версия 2023.1.8
//  - в конструктор добавлен параметр settings
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Infrastructure;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Infrastructure;
using DocumentFlow.Settings.Infrastructure;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.DataGrid.Styles;
using Syncfusion.WinForms.Input.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.Calculations;

public class CalculationBrowser : Browser<Calculation>, ICalculationBrowser
{
    public CalculationBrowser(ICalculationRepository repository, IPageManager pageManager, IStandaloneSettings settings) 
        : base(repository, pageManager, settings: settings) 
    {
        Toolbar.IconSize = ButtonIconSize.Small;

        var id = CreateText(x => x.id, "Id", width: 180, visible: false);
        var code = CreateText(x => x.code, "Код", width: 130);
        var state = CreateText(x => x.state_name, "Состояние");
        var approval = CreateDateTime(x => x.date_approval, "Дата утв.", width: 100, format: "dd.MM.yyyy");
        var weight = CreateNumeric(x => x.weight, "Вес, г", width: 80, decimalDigits: 3);
        var time = CreateNumeric(x => x.produced_time, "Время изг., с", width: 120, decimalDigits: 1);
        var stimul_payment = CreateNumeric(x => x.stimul_payment, "Стимул. выпл.", width: 100, decimalDigits: 2);
        var stimul_type_name = CreateText(x => x.stimul_type_name, "Способ начисления", width: 110);
        var cost_price = CreateCurrency(x => x.cost_price, "Себестоимость", width: 130);
        var profit_percent = CreateNumeric(x => x.profit_percent, "Прибыль, %", width: 120, mode: FormatMode.Percent, decimalDigits: 2);
        var profit_value = CreateCurrency(x => x.profit_value, "Прибыль", width: 100);
        var price = CreateCurrency(x => x.price, "Цена", width: 80);

        state.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
        weight.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
        time.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
        profit_percent.CellStyle.HorizontalAlignment = HorizontalAlignment.Center;
        stimul_payment.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;

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
    }

    protected override string HeaderText => "Калькуляции";

    protected override void BrowserCellStyle(Calculation document, string column, CellStyleInfo style)
    {
        if (column == "state_name")
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
}
