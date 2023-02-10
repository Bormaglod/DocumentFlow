//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 09.02.2022
//-----------------------------------------------------------------------

using DocumentFlow.Entities.Equipments;
using DocumentFlow.Infrastructure.Controls;

using Microsoft.Extensions.DependencyInjection;

using Syncfusion.WinForms.DataGrid.Enums;

namespace DocumentFlow.Controls.Cards;

public partial class ApplicatorCard : UserControl, ICard
{
    public ApplicatorCard()
    {
        InitializeComponent();
    }

    public int Index => 5;

    public string Title => $"Аппликаторы";

    Size ICard.Size => new(2, 1);

    public void RefreshCard()
    {
        var emp = Services.Provider.GetService<IEquipmentRepository>();
        if (emp != null)
        {
            gridApps.DataSource = emp.GetApplicators();
        }
    }

    private void GridApps_AutoGeneratingColumn(object sender, Syncfusion.WinForms.DataGrid.Events.AutoGeneratingColumnArgs e)
    {
        switch (e.Column.MappingName)
        {
            case "item_name":
                e.Column.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
                break;
            case "commissioning":
                e.Column.Width = 120;
                break;
            case "TotalHits":
                e.Column.Width = 100;
                break;
            default:
                e.Cancel = true;
                break;
        }
    }
}
