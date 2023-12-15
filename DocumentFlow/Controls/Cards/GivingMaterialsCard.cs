﻿//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.01.2023
//-----------------------------------------------------------------------

using CommunityToolkit.Mvvm.Messaging;

using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Data.Models;
using DocumentFlow.Messages;
using DocumentFlow.ViewModels;

using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.DataGrid.Events;

namespace DocumentFlow.Controls.Cards;

public partial class GivingMaterialsCard : UserControl, ICard
{
    private readonly IBalanceProcessingRepository repository;

    public GivingMaterialsCard(IBalanceProcessingRepository repository)
    {
        InitializeComponent();

        this.repository = repository;

        gridMaterials.RecordContextMenu = contextRecordMenu;
    }

    public int Index => 3;

    public string Title => "Давальч. материал";

    Size ICard.Size => new(3, 1);

    public void RefreshCard()
    {
        gridMaterials.DataSource = repository.GetRemainders();
    }

    private void GridMaterials_AutoGeneratingColumn(object sender, AutoGeneratingColumnArgs e)
    {
        switch (e.Column.MappingName)
        {
            case nameof(BalanceProcessing.MaterialName):
                e.Column.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
                break;
            case nameof(BalanceProcessing.ContractorName):
                e.Column.Width = 140;
                break;
            case nameof(BalanceProcessing.Remainder):
                e.Column.Width = 80;
                break;
            default:
                e.Cancel = true;
                break;
        }
    }

    private void MenuOpenMaterial_Click(object sender, EventArgs e)
    {
        if (gridMaterials.CurrentItem is BalanceProcessing balance)
        {
            WeakReferenceMessenger.Default.Send(new EntityEditorOpenMessage(typeof(IMaterialEditor), balance.ReferenceId));
        }
    }

    private void MenuOpenContractor_Click(object sender, EventArgs e)
    {
        if (gridMaterials.CurrentItem is BalanceProcessing balance)
        {
            WeakReferenceMessenger.Default.Send(new EntityEditorOpenMessage(typeof(IContractorEditor), balance.ContractorId));
        }
    }
}
