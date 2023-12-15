//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 25.07.2023
//-----------------------------------------------------------------------

using DocumentFlow.Controls;
using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Data.Models;
using DocumentFlow.Tools;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.ViewModels;

[Entity(typeof(Cutting), RepositoryType = typeof(ICuttingRepository))]
public partial class CuttingEditor : EditorPanel, ICuttingEditor, IDirectoryEditor
{
    private readonly IServiceProvider services;

    public CuttingEditor(IServiceProvider services) : base(services)
    {
        InitializeComponent();

        this.services = services;
    }

    public Guid? ParentId
    {
        get => GetDirectory().ParentId;
        set => GetDirectory().ParentId = value;
    }

    protected Cutting Cutting { get; set; } = null!;

    protected override void DoBindingControls()
    {
        textCode.DataBindings.Add(nameof(textCode.TextValue), DataContext, nameof(Cutting.Code), false, DataSourceUpdateMode.OnPropertyChanged);
        textName.DataBindings.Add(nameof(textName.TextValue), DataContext, nameof(Cutting.ItemName), true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
        selectGroup.DataBindings.Add(nameof(selectGroup.SelectedItem), DataContext, nameof(Cutting.ParentId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty);
        textProduced.DataBindings.Add(nameof(textProduced.IntegerValue), DataContext, nameof(Cutting.Produced), true, DataSourceUpdateMode.OnPropertyChanged);
        textProdTime.DataBindings.Add(nameof(textProdTime.IntegerValue), DataContext, nameof(Cutting.ProdTime), true, DataSourceUpdateMode.OnPropertyChanged);
        textRate.DataBindings.Add(nameof(textRate.IntegerValue), DataContext, nameof(Cutting.ProductionRate), true, DataSourceUpdateMode.OnPropertyChanged);
        dateNorm.DataBindings.Add(nameof(dateNorm.DateTimeValue), DataContext, nameof(Cutting.DateNorm), true, DataSourceUpdateMode.OnPropertyChanged, DateTime.MinValue);
        textSalary.DataBindings.Add(nameof(textSalary.DecimalValue), DataContext, nameof(Cutting.Salary), false, DataSourceUpdateMode.OnPropertyChanged);
        choiceProgram.DataBindings.Add(nameof(choiceProgram.ChoiceValue), DataContext, nameof(Cutting.ProgramNumber), true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
        textLength.DataBindings.Add(nameof(textLength.IntegerValue), DataContext, nameof(Cutting.SegmentLength), true, DataSourceUpdateMode.OnPropertyChanged);
        textLeftCleaning.DataBindings.Add(nameof(textLeftCleaning.DecimalValue), DataContext, nameof(Cutting.LeftCleaning), true, DataSourceUpdateMode.OnPropertyChanged);
        textLeftSweep.DataBindings.Add(nameof(textLeftSweep.IntegerValue), DataContext, nameof(Cutting.LeftSweep), true, DataSourceUpdateMode.OnPropertyChanged);
        textRightCleaning.DataBindings.Add(nameof(textRightCleaning.DecimalValue), DataContext, nameof(Cutting.RightCleaning), true, DataSourceUpdateMode.OnPropertyChanged);
        textRightSweep.DataBindings.Add(nameof(textRightSweep.IntegerValue), DataContext, nameof(Cutting.RightSweep), true, DataSourceUpdateMode.OnPropertyChanged);
    }

    protected override void CreateDataSources()
    {
        var repository = services.GetRequiredService<ICuttingRepository>();
        choiceProgram.From(repository.GetAvailableProgram(Cutting.ProgramNumber));
        selectGroup.DataSource = repository.GetOnlyFolders();
    }

    public override void RegisterNestedBrowsers()
    {
        EditorPage.RegisterNestedBrowser<IOperationUsageBrowser>("Использование операции");
    }
}
