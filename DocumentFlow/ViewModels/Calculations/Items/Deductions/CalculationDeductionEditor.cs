//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 27.08.2023
//-----------------------------------------------------------------------

using DocumentFlow.Controls;
using DocumentFlow.Controls.Events;
using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Models;
using DocumentFlow.Interfaces;
using DocumentFlow.Tools;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.ViewModels;

[Entity(typeof(CalculationDeduction), RepositoryType = typeof(ICalculationDeductionRepository))]
public partial class CalculationDeductionEditor : EditorPanel, ICalculationDeductionEditor, IDocumentEditor
{
    private readonly IServiceProvider services;
    private readonly IPageManager pageManager;

    public CalculationDeductionEditor(IServiceProvider services, IPageManager pageManager) : base(services)
    {
        InitializeComponent();

        this.services = services;
        this.pageManager = pageManager;
    }

    public Guid? OwnerId => Deduction.OwnerId;

    public void SetOwner(IDocumentInfo owner)
    {
        Deduction.OwnerId = owner.Id;
        if (owner is Calculation calculation)
        {
            Deduction.CalculationName = calculation.Code;
        }
    }

    protected CalculationDeduction Deduction { get; set; } = null!;

    protected override void OnEntityPropertyChanged(string? propertyName)
    {
        if (propertyName == nameof(CalculationDeduction.ItemId))
        {
            OnHeaderChanged();
        }
    }

    protected override void DoBindingControls()
    {
        textCalcName.DataBindings.Add(nameof(textCalcName.TextValue), DataContext, nameof(CalculationDeduction.CalculationName));
        comboDeduction.DataBindings.Add(nameof(comboDeduction.SelectedItem), DataContext, nameof(CalculationDeduction.ItemId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty);
        textPrice.DataBindings.Add(nameof(textPrice.DecimalValue), DataContext, nameof(CalculationDeduction.Price), false, DataSourceUpdateMode.OnPropertyChanged);
        textPercent.DataBindings.Add(nameof(textPercent.PercentValue), DataContext, nameof(CalculationDeduction.Value), false, DataSourceUpdateMode.OnPropertyChanged);
        textItemCost.DataBindings.Add(nameof(textItemCost.DecimalValue), DataContext, nameof(CalculationDeduction.ItemCost), false, DataSourceUpdateMode.OnPropertyChanged);
    }

    protected override void CreateDataSources()
    {
        comboDeduction.DataSource = services
            .GetRequiredService<IDeductionRepository>()
            .GetListExisting()
            .Order()
            .ToList();
    }

    private void SelectOperation_OpenButtonClick(object sender, DocumentSelectedEventArgs e)
    {
        pageManager.ShowAssociateEditor<IOperationBrowser>(e.Document);
    }

    private void ComboDeduction_OpenButtonClick(object sender, DocumentSelectedEventArgs e)
    {
        pageManager.ShowAssociateEditor<IDeductionBrowser>(e.Document);
    }

    private void ComboDeduction_SelectedItemChanged(object sender, EventArgs e)
    {
        bool visible = false;
        bool is_not_person = false;

        if (comboDeduction.SelectedItem != Guid.Empty)
        {
            var d = (Deduction)comboDeduction.SelectedDocument;

            visible = true;
            is_not_person = d.BaseDeduction != BaseDeduction.Person;
        }

        textPrice.Visible = visible && is_not_person;
        textPercent.Visible = visible && is_not_person;
        textItemCost.Visible = visible;

        if (visible)
        {
            textItemCost.Enabled = !is_not_person;
        }
    }

    private void ComboDeduction_DocumentSelectedChanged(object sender, DocumentSelectedEventArgs e)
    {
        var d = (Deduction)e.Document;

        if (d.BaseDeduction == BaseDeduction.Person)
        {
            textPrice.DecimalValue = Deduction.Value;
            textPercent.PercentValue = 100m;
            textItemCost.DecimalValue = Deduction.Value;
        }
        else
        {
            if (Deduction.OwnerId != null)
            {
                if (d.BaseDeduction == BaseDeduction.Salary)
                {
                    textPrice.DecimalValue = services.GetRequiredService<ICalculationOperationRepository>().GetSumItemCost(Deduction.OwnerId.Value);
                }
                else
                {
                    textPrice.DecimalValue = services.GetRequiredService<ICalculationMaterialRepository>().GetSumItemCost(Deduction.OwnerId.Value);
                }

                textPercent.PercentValue = d.Value;
            }

            textItemCost.DecimalValue = textPrice.DecimalValue * textPercent.PercentValue / 100;
        }
    }
}
