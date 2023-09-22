//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 09.02.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Data.Models;

using Syncfusion.WinForms.DataGrid.Enums;

namespace DocumentFlow.Controls.Cards;

public partial class ApplicatorCard : UserControl, ICard
{
    private readonly IEquipmentRepository repository;

    public ApplicatorCard(IEquipmentRepository repository)
    {
        InitializeComponent();

        this.repository = repository;
    }

    public int Index => 5;

    public string Title => "Аппликаторы";

    Size ICard.Size => new(2, 1);

    public void RefreshCard()
    {
        gridApps.DataSource = repository.GetApplicators();
    }

    private void GridApps_AutoGeneratingColumn(object sender, Syncfusion.WinForms.DataGrid.Events.AutoGeneratingColumnArgs e)
    {
        switch (e.Column.MappingName)
        {
            case nameof(Applicator.ItemName):
                e.Column.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
                break;
            case nameof(Applicator.Commissioning):
                e.Column.Width = 120;
                break;
            case nameof(Applicator.TotalHits):
                e.Column.Width = 100;
                break;
            default:
                e.Cancel = true;
                break;
        }
    }
}
