//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.12.2022
//
// Версия 2022.12.31
//  - добавлен метод LinkLabelContractor_LinkClicked
//
//-----------------------------------------------------------------------

using DocumentFlow.Entities.Balances;
using DocumentFlow.Entities.Companies;
using DocumentFlow.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

using System.Globalization;

namespace DocumentFlow.Controls;

public partial class CardRow : UserControl
{
    private readonly Guid contractorId;

    public CardRow(ContractorDebt contractor)
    {
        InitializeComponent();

        Dock = DockStyle.Top;

        var customProvider = new NumberFormatInfo
        {
            NumberGroupSeparator = " ",
        };

        linkLabelContractor.Text = contractor.contractor_name;
        labelAmount.Text = contractor.debt.ToString("#,###.00", customProvider);

        contractorId = contractor.id;
    }

    private void LinkLabelContractor_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        Services.Provider.GetService<IPageManager>()?.ShowEditor<IContractorEditor>(contractorId);
    }
}
