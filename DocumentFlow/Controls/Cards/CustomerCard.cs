//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.12.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Data.Models;
using DocumentFlow.Interfaces;
using DocumentFlow.Settings;

using Microsoft.Extensions.Options;

namespace DocumentFlow.Controls.Cards;

public partial class CustomerCard : UserControl, ICard
{
    private readonly IPageManager pageManager;
    private readonly IBalanceContractorRepository repository;
    private readonly StartPageSettings settings;

    public CustomerCard(IPageManager pageManager, IBalanceContractorRepository repository, IOptions<LocalSettings> options)
    {
        InitializeComponent();

        this.pageManager = pageManager;
        this.repository = repository;

        settings = options.Value.StartPage;
    }

    public int Index => 0;

    public string Title => "Покупатели";

    Size ICard.Size => new(1, 1);

    public void RefreshCard()
    {
        Controls.Clear();

        var customers = repository.GetCustomersDebt(6);
        int cnt = Math.Min(customers.Count, 6);
        for (int i = 0; i < cnt; i++)
        {
            CardRow row = new(pageManager, customers[i], settings);
            Controls.Add(row);

            row.BringToFront();
        }
    }
}
