//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.01.2023
//
// Версия 2023.2.4
//  - из конструктора убран вызов RefreshCard
//
//-----------------------------------------------------------------------

using DocumentFlow.Entities.Balances;
using DocumentFlow.Infrastructure.Controls;

using Microsoft.Extensions.DependencyInjection;

using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.DataGrid.Events;

namespace DocumentFlow.Controls.Cards;

public partial class GivingMaterialsCard : UserControl, ICard
{
    public GivingMaterialsCard()
    {
        InitializeComponent();
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
            case "MaterialName":
                e.Column.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
                break;
            case "ContractorName":
                e.Column.Width = 140;
                break;
            case "Remainder":
                e.Column.Width = 80;
                break;
            default:
                e.Cancel = true;
                break;
        }
    }
}
