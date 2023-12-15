//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 27.08.2023
//-----------------------------------------------------------------------

using CommunityToolkit.Mvvm.Messaging;

using DocumentFlow.Controls;
using DocumentFlow.Controls.Events;
using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Models;
using DocumentFlow.Messages;
using DocumentFlow.Tools;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.ViewModels;

[Entity(typeof(CalculationCutting), RepositoryType = typeof(ICalculationCuttingRepository))]
public partial class CalculationCuttingEditor : EditorPanel, ICalculationCuttingEditor, IDocumentEditor
{
    private readonly IServiceProvider services;

    public CalculationCuttingEditor(IServiceProvider services) : base(services)
    {
        InitializeComponent();

        this.services = services;
    }

    public Guid? OwnerId => Operation.OwnerId;

    public void SetOwner(IDocumentInfo owner)
    {
        Operation.OwnerId = owner.Id;
        if (owner is Calculation calculation)
        {
            Operation.CalculationName = calculation.Code;
        }
    }

    protected CalculationCutting Operation { get; set; } = null!;

    protected override void OnEntityPropertyChanged(string? propertyName)
    {
        if (propertyName == nameof(CalculationCutting.Code))
        {
            OnHeaderChanged();
        }
    }

    protected override void DoBindingControls()
    {
        textCalcName.DataBindings.Add(nameof(textCalcName.TextValue), DataContext, nameof(CalculationCutting.CalculationName));
        textCode.DataBindings.Add(nameof(textCode.TextValue), DataContext, nameof(CalculationCutting.Code), false, DataSourceUpdateMode.OnPropertyChanged);
        textName.DataBindings.Add(nameof(textName.TextValue), DataContext, nameof(CalculationCutting.ItemName), true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
        selectOperation.DataBindings.Add(nameof(selectOperation.SelectedItem), DataContext, nameof(CalculationCutting.ItemId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty);
        textPrice.DataBindings.Add(nameof(textPrice.DecimalValue), DataContext, nameof(CalculationCutting.Price), false, DataSourceUpdateMode.OnPropertyChanged);
        selectEquipment.DataBindings.Add(nameof(selectEquipment.SelectedItem), DataContext, nameof(CalculationCutting.EquipmentId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty);
        selectTool.DataBindings.Add(nameof(selectTool.SelectedItem), DataContext, nameof(CalculationCutting.ToolsId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty);
        selectMaterial.DataBindings.Add(nameof(selectMaterial.SelectedItem), DataContext, nameof(CalculationCutting.MaterialId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty);
        textAmount.DataBindings.Add(nameof(textAmount.DecimalValue), DataContext, nameof(CalculationCutting.MaterialAmount), false, DataSourceUpdateMode.OnPropertyChanged);
        textRepeat.DataBindings.Add(nameof(textRepeat.IntegerValue), DataContext, nameof(CalculationCutting.Repeats), true, DataSourceUpdateMode.OnPropertyChanged);
        textNote.DataBindings.Add(nameof(textNote.TextValue), DataContext, nameof(CalculationCutting.Note), true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
    }

    protected override void CreateDataSources()
    {
        selectOperation.DataSource = services.GetRequiredService<IOperationRepository>().GetListExisting(callback: q => q.OrderBy("item_name"));
        selectMaterial.DataSource = services.GetRequiredService<IMaterialRepository>().GetMaterials();
        selectEquipment.DataSource = services.GetRequiredService<IEquipmentRepository>().GetListExisting(callback: q => q.WhereFalse("is_tools"));
        selectTool.DataSource = services.GetRequiredService<IEquipmentRepository>().GetListExisting(callback: q => q.WhereTrue("is_tools"));
   }

    private void SelectOperation_OpenButtonClick(object sender, DocumentSelectedEventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new EntityEditorOpenMessage(typeof(IOperationEditor), e.Document));
    }

    private void SelectOperation_DocumentSelectedChanged(object sender, DocumentChangedEventArgs e)
    {
        Operation.Price = ((Operation)e.NewDocument).Salary;
    }

    private void SelectEquipment_OpenButtonClick(object sender, DocumentSelectedEventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new EntityEditorOpenMessage(typeof(IEquipmentEditor), e.Document));
    }

    private void SelectMaterial_OpenButtonClick(object sender, DocumentSelectedEventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new EntityEditorOpenMessage(typeof(IMaterialEditor), e.Document));
    }
}
