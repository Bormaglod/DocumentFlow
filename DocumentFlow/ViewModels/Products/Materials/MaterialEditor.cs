//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 09.07.2023
//-----------------------------------------------------------------------

using DocumentFlow.Controls;
using DocumentFlow.Controls.Editors;
using DocumentFlow.Controls.Events;
using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Models;
using DocumentFlow.Dialogs;
using DocumentFlow.Interfaces;
using DocumentFlow.Tools;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.ViewModels;

[Entity(typeof(Material), RepositoryType = typeof(IMaterialRepository))]
public partial class MaterialEditor : EditorPanel, IMaterialEditor, IDirectoryEditor
{
    private readonly IServiceProvider services;
    private readonly IPageManager pageManager;

    public MaterialEditor(IServiceProvider services, IPageManager pageManager) : base(services)
    {
        InitializeComponent();

        this.services = services;
        this.pageManager = pageManager;

        choiceMaterialKind.FromEnum<MaterialKind>(KeyGenerationMethod.PostgresEnumValue);
        choiceVat.FromEnum<VatRate>(KeyGenerationMethod.IntegerValue);
    }

    public Guid? ParentId
    {
        get => GetDirectory().ParentId;
        set => GetDirectory().ParentId = value;
    }

    public override void RegisterNestedBrowsers()
    {
        if (services.GetRequiredService<IMaterialRepository>().HasPrivilege(Privilege.Update))
        {
            EditorPage.RegisterNestedBrowser<IСustomerBrowser>();
            EditorPage.RegisterNestedBrowser<IBalanceMaterialBrowser>();
            EditorPage.RegisterNestedBrowser<IMaterialUsageBrowser>();
        }
    }

    protected Material Material { get; set; } = null!;

    protected override void DoBindingControls()
    {
        textCode.DataBindings.Add(nameof(textCode.TextValue), DataContext, nameof(Material.Code), false, DataSourceUpdateMode.OnPropertyChanged);
        textName.DataBindings.Add(nameof(textName.TextValue), DataContext, nameof(Material.ItemName), true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
        textDocName.DataBindings.Add(nameof(textDocName.TextValue), DataContext, nameof(Material.DocName), true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
        selectGroup.DataBindings.Add(nameof(selectGroup.SelectedItem), DataContext, nameof(Material.ParentId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty);
        choiceMaterialKind.DataBindings.Add(nameof(choiceMaterialKind.ChoiceValue), DataContext, nameof(Material.MaterialKind), false, DataSourceUpdateMode.OnPropertyChanged);
        textExtArticle.DataBindings.Add(nameof(textExtArticle.TextValue), DataContext, nameof(Material.ExtArticle), true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
        selectCross.DataBindings.Add(nameof(selectCross.SelectedItem), DataContext, nameof(Material.OwnerId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty);
        comboMeasurement.DataBindings.Add(nameof(comboMeasurement.SelectedItem), DataContext, nameof(Material.MeasurementId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty);
        textWeight.DataBindings.Add(nameof(textWeight.DecimalValue), DataContext, nameof(Material.Weight), true, DataSourceUpdateMode.OnPropertyChanged, 0m);
        textPrice.DataBindings.Add(nameof(textPrice.DecimalValue), DataContext, nameof(Material.Price), false, DataSourceUpdateMode.OnPropertyChanged);
        choiceVat.DataBindings.Add(nameof(choiceVat.ChoiceValue), DataContext, nameof(Material.Vat), true, DataSourceUpdateMode.OnPropertyChanged);
        textMinOrder.DataBindings.Add(nameof(textMinOrder.DecimalValue), DataContext, nameof(Material.MinOrder), true, DataSourceUpdateMode.OnPropertyChanged, 0m);
    }

    protected override void CreateDataSources()
    {
        var repository = services.GetRequiredService<IMaterialRepository>();

        selectGroup.DataSource = repository.GetOnlyFolders();

        comboMeasurement.DataSource = services
            .GetRequiredService<IMeasurementRepository>()
            .GetListExisting(callback: q => q.OrderBy("item_name"));

        gridParts.DataSource = Material.CompatibleParts;

        selectCross.DataSource = repository.GetCrossMaterials(Material.Id);
    }

    private bool EditCompatiblePart(IDependentEntity? entity)
    {
        if (entity is CompatiblePart part)
        {
            var dialog = services.GetRequiredService<DirectoryItemDialog>();
            var res = dialog.Get<Material, IMaterialRepository>(part.CompatibleId);
            if (res != null)
            {
                part.CompatibleId = res.Id;
                part.Code = res.Code;
                part.ItemName = res.ItemName ?? string.Empty;

                return true;
            }
        }

        return false;
    }

    private void SelectCross_OpenButtonClick(object sender, DocumentSelectedEventArgs e)
    {
        pageManager.ShowAssociateEditor<IMaterialBrowser>(e.Document);
    }

    private void GridParts_CreateRow(object sender, DependentEntitySelectEventArgs e)
    {
        var dialog = services.GetRequiredService<DirectoryItemDialog>();
        var res = dialog.Get<Material, IMaterialRepository>();
        if (res != null)
        {
            CompatiblePart part = new()
            {
                CompatibleId = res.Id,
                Code = res.Code,
                ItemName = res.ItemName ?? string.Empty
            };

            e.DependentEntity = part;
        }
        else
        {
            e.Accept = false;
        }
    }

    private void GridParts_EditRow(object sender, DependentEntitySelectEventArgs e)
    {
        e.Accept = EditCompatiblePart(e.DependentEntity);
    }
}
