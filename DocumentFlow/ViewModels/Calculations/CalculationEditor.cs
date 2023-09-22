//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 23.07.2023
//-----------------------------------------------------------------------

using DocumentFlow.Controls;
using DocumentFlow.Controls.Editors;
using DocumentFlow.Controls.Enums;
using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Models;
using DocumentFlow.Tools;

namespace DocumentFlow.ViewModels;

[Entity(typeof(Calculation), RepositoryType = typeof(ICalculationRepository))]
public partial class CalculationEditor : EditorPanel, ICalculationEditor, IDocumentEditor
{
    public CalculationEditor(IServiceProvider services) : base(services)
    {
        InitializeComponent();

        choiceType.FromEnum<StimulatingValue>(KeyGenerationMethod.PostgresEnumValue);
    }

    public Guid? OwnerId 
    { 
        get => Calculation.OwnerId; 
        set => Calculation.OwnerId = value; 
    }

    protected Calculation Calculation { get; set; } = null!;

    public override void RegisterNestedBrowsers()
    {
        EditorPage.RegisterNestedBrowser<ICalculationCuttingBrowser>();
        EditorPage.RegisterNestedBrowser<ICalculationOperationBrowser>();
        EditorPage.RegisterNestedBrowser<ICalculationMaterialBrowser>();
        EditorPage.RegisterNestedBrowser<ICalculationDeductionBrowser>();
    }

    protected override void RegisterReports()
    {
        EditorPage.RegisterReport<ProcessMapReport>();
        EditorPage.RegisterReport<SpecificationReport>();
    }

    protected override void OnEntityPropertyChanged(string? propertyName)
    {
        if (propertyName == nameof(Calculation.Code))
        {
            OnHeaderChanged();
        }
    }

    protected override void DoBindingControls()
    {
        textState.DataBindings.Add(nameof(textState.TextValue), DataContext, nameof(Calculation.CalculationStateName));
        textGoods.DataBindings.Add(nameof(textGoods.TextValue), DataContext, nameof(Calculation.GoodsName));
        textCode.DataBindings.Add(nameof(textCode.TextValue), DataContext, nameof(Calculation.Code), false, DataSourceUpdateMode.OnPropertyChanged);
        choiceType.DataBindings.Add(nameof(choiceType.ChoiceValue), DataContext, nameof(Calculation.StimulType), false, DataSourceUpdateMode.OnPropertyChanged);
        textStimul.DataBindings.Add(nameof(textStimul.DecimalValue), DataContext, nameof(Calculation.StimulPayment), false, DataSourceUpdateMode.OnPropertyChanged);
        textCost.DataBindings.Add(nameof(textCost.DecimalValue), DataContext, nameof(Calculation.CostPrice), false, DataSourceUpdateMode.OnPropertyChanged);
        textPercent.DataBindings.Add(nameof(textPercent.PercentValue), DataContext, nameof(Calculation.ProfitPercent), false, DataSourceUpdateMode.OnPropertyChanged);
        textProfit.DataBindings.Add(nameof(textProfit.DecimalValue), DataContext, nameof(Calculation.ProfitValue), false, DataSourceUpdateMode.OnPropertyChanged);
        textPrice.DataBindings.Add(nameof(textPrice.DecimalValue), DataContext, nameof(Calculation.Price), false, DataSourceUpdateMode.OnPropertyChanged);
        dateApproval.DataBindings.Add(nameof(dateApproval.DateTimeValue), DataContext, nameof(Calculation.DateApproval), true, DataSourceUpdateMode.OnPropertyChanged, DateTime.MinValue);
        textNote.DataBindings.Add(nameof(textNote.TextValue), DataContext, nameof(Calculation.Note), true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
    }

    protected override void AfterConstructData(ConstructDataMethod method)
    {
        Enabled = Calculation.CalculationState != CalculationState.Expired;
    }
}
