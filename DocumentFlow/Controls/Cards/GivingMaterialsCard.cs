//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.01.2023
//-----------------------------------------------------------------------

using DocumentFlow.Entities.Balances;
using DocumentFlow.Infrastructure.Controls;
using Syncfusion.WinForms.DataGrid.Events;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.WinForms.DataGrid.Enums;

namespace DocumentFlow.Controls.Cards;

public partial class GivingMaterialsCard : UserControl, ICard
{
    public GivingMaterialsCard()
    {
        InitializeComponent();
        RefreshCard();
    }

    public int Index => 3;

    public string Title => "Давальч. материал";

    Size ICard.Size => new(3, 1);

    public void RefreshCard()
    {
        var balance = Services.Provider.GetService<IBalanceProcessingRepository>();
        if (balance != null)
        {
            gridMaterials.DataSource = balance.GetRemainders();
        }
    }

    private void GridMaterials_AutoGeneratingColumn(object sender, AutoGeneratingColumnArgs e)
    {
        switch (e.Column.MappingName) 
        {
            case "material_name":
                e.Column.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
                break;
            case "contractor_name":
                e.Column.Width = 140;
                break;
            case "remainder":
                e.Column.Width = 80;
                break;
            default:
                e.Cancel = true;
                break;
        }
    }
}
