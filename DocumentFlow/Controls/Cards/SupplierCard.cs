//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 31.12.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Data.Models;
using DocumentFlow.Settings;

using Microsoft.Extensions.Options;

namespace DocumentFlow.Controls.Cards;

public partial class SupplierCard : UserControl, ICard
{
    private readonly IBalanceContractorRepository repository;
    private readonly StartPageSettings settings;

    public SupplierCard(IBalanceContractorRepository repository, IOptions<LocalSettings> options)
    {
        InitializeComponent();

        this.repository = repository;

        settings = options.Value.StartPage;
    }

    public int Index => 1;

    public string Title => "Поставщики";

    Size ICard.Size => new(1, 1);

    public void RefreshCard()
    {
        Controls.Clear();

        var suppliers = repository.GetSuppliersDebt(6);
        int cnt = Math.Min(suppliers.Count, 6);
        for (int i = 0; i < cnt; i++)
        {
            CardRow row = new(suppliers[i], settings);
            Controls.Add(row);

            row.BringToFront();
        }
    }
}
