//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.12.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Models;
using DocumentFlow.Interfaces;
using DocumentFlow.Settings;
using DocumentFlow.ViewModels;

using System.Globalization;

namespace DocumentFlow.Controls;

public partial class CardRow : UserControl
{
    private readonly Guid contractorId;
    private readonly IPageManager pageManager;

    public CardRow(IPageManager pageManager, CurrentBalanceContractor contractor, StartPageSettings settings)
    {
        InitializeComponent();

        this.pageManager = pageManager;

        Dock = DockStyle.Top;

        var customProvider = new NumberFormatInfo
        {
            NumberGroupSeparator = " ",
        };

        linkLabelContractor.Text = contractor.ContractorName;
        labelAmount.Text = contractor.Debt.ToString("#,###.00", customProvider);
        labelAmount.Width = settings.CardRowValueWidth;

        contractorId = contractor.Id;
    }

    private void LinkLabelContractor_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        pageManager.ShowAssociateEditor<IContractorBrowser>(contractorId);
    }
}
