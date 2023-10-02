//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.07.2023
//-----------------------------------------------------------------------

using DocumentFlow.Controls;
using DocumentFlow.Controls.Events;
using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Data.Models;
using DocumentFlow.Interfaces;
using DocumentFlow.Tools;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.ViewModels;

[Entity(typeof(CalculationOperation), RepositoryType = typeof(ICalculationOperationRepository))]
public partial class CalculationOperationEditor : EditorPanel, ICalculationOperationEditor, IDocumentEditor
{
    private readonly IServiceProvider services;
    private readonly IPageManager pageManager;

    public CalculationOperationEditor(IServiceProvider services, IPageManager pageManager) : base(services)
    {
        InitializeComponent();

        this.services = services;
        this.pageManager = pageManager;
    }

    public Guid? OwnerId
    {
        get => Operation.OwnerId;
        set => Operation.OwnerId = value;
    }

    protected CalculationOperation Operation { get; set; } = null!;

    protected override void OnEntityPropertyChanged(string? propertyName)
    {
        if (propertyName == nameof(CalculationOperation.Code))
        {
            OnHeaderChanged();
        }
    }

    protected override void DoBindingControls()
    {
        textCalcName.DataBindings.Add(nameof(textCalcName.TextValue), DataContext, nameof(CalculationOperation.CalculationName));
        textCode.DataBindings.Add(nameof(textCode.TextValue), DataContext, nameof(CalculationOperation.Code), false, DataSourceUpdateMode.OnPropertyChanged);
        textName.DataBindings.Add(nameof(textName.TextValue), DataContext, nameof(CalculationOperation.ItemName), true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
        selectOperation.DataBindings.Add(nameof(selectOperation.SelectedItem), DataContext, nameof(CalculationOperation.ItemId), false, DataSourceUpdateMode.OnPropertyChanged);
        textPrice.DataBindings.Add(nameof(textPrice.DecimalValue), DataContext, nameof(CalculationOperation.Price), false, DataSourceUpdateMode.OnPropertyChanged);
        selectEquipment.DataBindings.Add(nameof(selectEquipment.SelectedItem), DataContext, nameof(CalculationOperation.EquipmentId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty);
        selectTool.DataBindings.Add(nameof(selectTool.SelectedItem), DataContext, nameof(CalculationOperation.ToolsId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty);
        selectMaterial.DataBindings.Add(nameof(selectMaterial.SelectedItem), DataContext, nameof(CalculationOperation.MaterialId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty);
        textAmount.DataBindings.Add(nameof(textAmount.DecimalValue), DataContext, nameof(CalculationOperation.MaterialAmount), false, DataSourceUpdateMode.OnPropertyChanged);
        textRepeat.DataBindings.Add(nameof(textRepeat.IntegerValue), DataContext, nameof(CalculationOperation.Repeats), true, DataSourceUpdateMode.OnPropertyChanged);
        selectPrevious.DataBindings.Add(nameof(selectPrevious.SelectedItems), DataContext, nameof(CalculationOperation.PreviousOperation), true, DataSourceUpdateMode.OnPropertyChanged, Array.Empty<string>());
        textNote.DataBindings.Add(nameof(textNote.TextValue), DataContext, nameof(CalculationOperation.Note), true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
    }

    protected override void CreateDataSources()
    {
        selectOperation.DataSource = services.GetRequiredService<IOperationRepository>().GetListExisting(callback: q => q.OrderBy("item_name"));
        selectMaterial.DataSource = services.GetRequiredService<IMaterialRepository>().GetMaterials();
        selectEquipment.DataSource = services.GetRequiredService<IEquipmentRepository>().GetListExisting(callback: q => q.WhereFalse("is_tools"));
        selectTool.DataSource = services.GetRequiredService<IEquipmentRepository>().GetListExisting(callback: q => q.WhereTrue("is_tools"));
        selectPrevious.DataSource = services.GetRequiredService<ICalculationOperationRepository>().GetCodes(Operation);
    }

    private void SelectOperation_OpenButtonClick(object sender, DocumentSelectedEventArgs e)
    {
        pageManager.ShowAssociateEditor<IOperationBrowser>(e.Document);
    }

    private void SelectOperation_DocumentSelectedChanged(object sender, DocumentChangedEventArgs e)
    {
        Operation.Price = ((Operation)e.NewDocument).Salary;
    }

    private void SelectEquipment_OpenButtonClick(object sender, DocumentSelectedEventArgs e)
    {
        pageManager.ShowAssociateEditor<IEquipmentBrowser>(e.Document);
    }

    private void SelectTool_OpenButtonClick(object sender, DocumentSelectedEventArgs e)
    {
        pageManager.ShowAssociateEditor<IEquipmentBrowser>(e.Document);
    }

    private void SelectMaterial_OpenButtonClick(object sender, DocumentSelectedEventArgs e)
    {
        pageManager.ShowAssociateEditor<IMaterialBrowser>(e.Document);
    }
}
