//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.02.2023
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Data.Models;

using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.DataGrid.Events;

namespace DocumentFlow.Controls.Cards;

public partial class WageCard : UserControl, ICard
{
    private readonly IOurEmployeeRepository repository;

    public WageCard(IOurEmployeeRepository repository)
    {
        InitializeComponent();

        this.repository = repository;
    }

    public int Index => 4;

    public string Title => $"Зар. плата за {DateTime.Today:Y}";

    Size ICard.Size => new(3, 1);

    public void RefreshCard()
    {
        gridEmps.DataSource = repository.GetWages();
    }

    private void GridEmps_AutoGeneratingColumn(object sender, AutoGeneratingColumnArgs e)
    {
        switch (e.Column.MappingName)
        {
            case nameof(EmployeeWageBalance.EmployeeName):
                e.Column.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
                break;
            case nameof(EmployeeWageBalance.BeginingBalance):
            case nameof(EmployeeWageBalance.EndingBalance):
                e.Column.Width = 120;
                break;
            default:
                e.Column.Width = 80;
                break;
        }
    }
}