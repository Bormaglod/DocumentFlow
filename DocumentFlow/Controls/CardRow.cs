//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.12.2022
//-----------------------------------------------------------------------

using CommunityToolkit.Mvvm.Messaging;

using DocumentFlow.Data.Models;
using DocumentFlow.Messages;
using DocumentFlow.Settings;
using DocumentFlow.ViewModels;

using System.Globalization;

namespace DocumentFlow.Controls;

public partial class CardRow : UserControl
{
    private readonly Guid contractorId;

    public CardRow(CurrentBalanceContractor contractor, StartPageSettings settings)
    {
        InitializeComponent();

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
        WeakReferenceMessenger.Default.Send(new EntityEditorOpenMessage(typeof(IContractorEditor), contractorId));
    }
}
