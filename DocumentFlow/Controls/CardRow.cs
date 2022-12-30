//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.12.2022
//-----------------------------------------------------------------------

using DocumentFlow.Entities.Balances;

using System.Globalization;

namespace DocumentFlow.Controls;

public partial class CardRow : UserControl
{
    public CardRow(ContractorDebt contractor)
    {
        InitializeComponent();

        Dock = DockStyle.Top;

        var customProvider = new NumberFormatInfo
        {
            NumberGroupSeparator = " ",
        };

        linkLabelContractor.Text = contractor.contractor_name;
        labelAmount.Text = contractor.debt.ToString("#,###.##", customProvider);
    }
}
