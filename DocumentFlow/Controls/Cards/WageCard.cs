//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.02.2023
//-----------------------------------------------------------------------

using DocumentFlow.Entities.Employees;
using DocumentFlow.Infrastructure.Controls;

using Microsoft.Extensions.DependencyInjection;

using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.DataGrid.Events;

namespace DocumentFlow.Controls.Cards;

public partial class WageCard : UserControl, ICard
{
    public WageCard()
    {
        InitializeComponent();
    }

    public int Index => 4;

    public string Title => $"Зар. плата за {DateTime.Today:Y}";

    Size ICard.Size => new(3, 1);

    public void RefreshCard()
    {
        var emp = Services.Provider.GetService<IOurEmployeeRepository>();
        if (emp != null)
        {
            gridEmps.DataSource = emp.GetWages();
        }
    }

    private void GridEmps_AutoGeneratingColumn(object sender, AutoGeneratingColumnArgs e)
    {
        switch (e.Column.MappingName)
        {
            case "employee_name":
                e.Column.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
                break;
            case "begining_balance":
            case "ending_balance":
                e.Column.Width = 120;
                break;
            default:
                e.Column.Width = 80;
                break;
        }
    }
}