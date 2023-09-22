﻿//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 26.07.2023
//-----------------------------------------------------------------------

using DocumentFlow.Controls;
using DocumentFlow.Controls.Events;
using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Data.Models;
using DocumentFlow.Dialogs;
using DocumentFlow.Tools;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.ViewModels;

[Entity(typeof(ContractApplication), RepositoryType = typeof(IContractApplicationRepository))]
public partial class ContractApplicationEditor : EditorPanel, IContractApplicationEditor, IDocumentEditor
{
    private readonly IServiceProvider services;

    public ContractApplicationEditor(IServiceProvider services) : base(services)
    {
        InitializeComponent();

        this.services = services;
    }

    public Guid? OwnerId
    {
        get => App.OwnerId;
        set => App.OwnerId = value;
    }

    protected ContractApplication App { get; set; } = null!;

    protected override void OnEntityPropertyChanged(string? propertyName)
    {
        if (propertyName == nameof(ContractApplication.ItemName) || propertyName == nameof(ContractApplication.DocumentDate) || propertyName == nameof(ContractApplication.Code))
        {
            OnHeaderChanged();
        }
    }

    protected override void DoBindingControls()
    {
        textContract.DataBindings.Add(nameof(textContract.TextValue), DataContext, nameof(ContractApplication.ContractName), false, DataSourceUpdateMode.OnPropertyChanged);
        textNumber.DataBindings.Add(nameof(textNumber.TextValue), DataContext, nameof(ContractApplication.Code), false, DataSourceUpdateMode.OnPropertyChanged);
        textName.DataBindings.Add(nameof(textName.TextValue), DataContext, nameof(ContractApplication.ItemName), true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
        dateDocument.DataBindings.Add(nameof(dateDocument.DateTimeValue), DataContext, nameof(ContractApplication.DocumentDate), false, DataSourceUpdateMode.OnPropertyChanged);
        dateStart.DataBindings.Add(nameof(dateStart.DateTimeValue), DataContext, nameof(ContractApplication.DateStart), false, DataSourceUpdateMode.OnPropertyChanged);
        dateEnd.DataBindings.Add(nameof(dateEnd.DateTimeValue), DataContext, nameof(ContractApplication.DateEnd), true, DataSourceUpdateMode.OnPropertyChanged, DateTime.MinValue);
        textNote.DataBindings.Add(nameof(textNote.TextValue), DataContext, nameof(ContractApplication.Note), true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
    }

    protected override void CreateDataSources()
    {
        gridProducts.DataSource = App.PriceApprovals;
    }

    private void GridProducts_CreateRow(object sender, DependentEntitySelectEventArgs e)
    {
        var dialog = services.GetRequiredService<PriceApprovalDialog>();
        if (dialog.Create(out var price))
        {
            e.DependentEntity = price;
        }
        else
        {
            e.Accept = false;
        }
    }

    private void GridProducts_EditRow(object sender, DependentEntitySelectEventArgs e)
    {
        if (e.DependentEntity is PriceApproval priceApproval)
        {
            var dialog = services.GetRequiredService<PriceApprovalDialog>();
            if (dialog.Edit(priceApproval))
            {
                return;
            }
        }

        e.Accept = false;
    }
}