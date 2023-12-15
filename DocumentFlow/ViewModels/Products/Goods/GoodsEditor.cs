//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 23.07.2023
//-----------------------------------------------------------------------

using CommunityToolkit.Mvvm.Messaging;

using DocumentFlow.Controls;
using DocumentFlow.Controls.Editors;
using DocumentFlow.Controls.Events;
using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Models;
using DocumentFlow.Dialogs.Interfaces;
using DocumentFlow.Messages;
using DocumentFlow.Tools;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.ViewModels;

[Entity(typeof(Goods), RepositoryType = typeof(IGoodsRepository))]
public partial class GoodsEditor : EditorPanel, IGoodsEditor, IDirectoryEditor
{
    private readonly IServiceProvider services;

    public GoodsEditor(IServiceProvider services) : base(services)
    {
        InitializeComponent();

        this.services = services;

        choiceVat.FromEnum<VatRate>(KeyGenerationMethod.IntegerValue);
    }

    public Guid? ParentId
    {
        get => GetDirectory().ParentId;
        set => GetDirectory().ParentId = value;
    }

    public override void RegisterNestedBrowsers()
    {
        if (services.GetRequiredService<IGoodsRepository>().HasPrivilege(Privilege.Update))
        {
            EditorPage.RegisterNestedBrowser<ICalculationBrowser>("Калькуляции");
            EditorPage.RegisterNestedBrowser<IBalanceGoodsBrowser>("Остатки");
        }
    }

    protected Goods Goods { get; set; } = null!;

    protected override void DoBindingControls()
    {
        textCode.DataBindings.Add(nameof(textCode.TextValue), DataContext, nameof(Goods.Code), false, DataSourceUpdateMode.OnPropertyChanged);
        textName.DataBindings.Add(nameof(textName.TextValue), DataContext, nameof(Goods.ItemName), true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
        textDocName.DataBindings.Add(nameof(textDocName.TextValue), DataContext, nameof(Goods.DocName), true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
        selectGroup.DataBindings.Add(nameof(selectGroup.SelectedItem), DataContext, nameof(Goods.ParentId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty);
        comboMeasurement.DataBindings.Add(nameof(comboMeasurement.SelectedItem), DataContext, nameof(Goods.MeasurementId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty);
        textWeight.DataBindings.Add(nameof(textWeight.DecimalValue), DataContext, nameof(Goods.Weight), true, DataSourceUpdateMode.OnPropertyChanged, 0m);
        textPrice.DataBindings.Add(nameof(textPrice.DecimalValue), DataContext, nameof(Goods.Price), false, DataSourceUpdateMode.OnPropertyChanged);
        choiceVat.DataBindings.Add(nameof(choiceVat.ChoiceValue), DataContext, nameof(Goods.Vat), true, DataSourceUpdateMode.OnPropertyChanged);
        comboCalculation.DataBindings.Add(nameof(comboCalculation.SelectedItem), DataContext, nameof(Goods.CalculationId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty);
        toggleService.DataBindings.Add(nameof(toggleService.ToggleValue), DataContext, nameof(Goods.IsService), false, DataSourceUpdateMode.OnPropertyChanged);
        textNote.DataBindings.Add(nameof(textNote.TextValue), DataContext, nameof(Goods.Note), true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
    }

    protected override void CreateDataSources()
    {
        selectGroup.DataSource = services
            .GetRequiredService<IGoodsRepository>()
            .GetOnlyFolders();

        comboMeasurement.DataSource = services
            .GetRequiredService<IMeasurementRepository>()
            .GetListExisting(callback: q => q.OrderBy("item_name"));

        comboCalculation.DataSource = services
            .GetRequiredService<ICalculationRepository>()
            .GetApproved(Goods);
    }

    private void ComboCalculation_OpenButtonClick(object sender, DocumentSelectedEventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new EntityEditorOpenMessage(typeof(ICalculationEditor), e.Document));
    }

    private void ComboMeasurement_OpenButtonClick(object sender, DocumentSelectedEventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new EntityEditorOpenMessage(typeof(IMeasurementEditor), e.Document));
    }

    private void ToggleService_ToggleValueChanged(object sender, EventArgs e)
    {
        textWeight.Visible = !toggleService.ToggleValue;
    }

    private void ButtonChangeCode_Click(object sender, EventArgs e)
    {
        var dialog = services.GetRequiredService<ICodeGeneratorDialog>();
        try
        {
            if (dialog.Get(Goods.Code, out var code))
            {
                Goods.Code = code;
            }
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
