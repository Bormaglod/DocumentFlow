//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.12.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Infrastructure;
using DocumentFlow.Entities.Balances;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Controls.Cards;

public partial class CustomerCard : UserControl, ICard
{
    public CustomerCard()
    {
        InitializeComponent();

        var balance = Services.Provider.GetService<IBalanceContractorRepository>();
        if (balance != null )
        {
            var customers = balance!.GetCustomersDebt(6);
            for (int i = 0; i < 6; i++)
            {
                CardRow row = new(customers[i]);
                Controls.Add(row);
                
                row.BringToFront();
            }
        }
    }

    public string Title => "Покупатели";
}
