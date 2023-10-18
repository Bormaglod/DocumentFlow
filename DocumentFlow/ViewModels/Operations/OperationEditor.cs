//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 25.07.2023
//-----------------------------------------------------------------------

using DocumentFlow.Controls;
using DocumentFlow.Controls.Events;
using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Models;
using DocumentFlow.Dialogs;
using DocumentFlow.Dialogs.Interfaces;
using DocumentFlow.Interfaces;
using DocumentFlow.Tools;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.ViewModels;

[Entity(typeof(Operation), RepositoryType = typeof(IOperationRepository))]
public partial class OperationEditor : EditorPanel, IOperationEditor, IDirectoryEditor
{
    private readonly IServiceProvider services;
    private readonly IPageManager pageManager;

    public OperationEditor(IServiceProvider services, IPageManager pageManager) : base(services)
    {
        InitializeComponent();

        this.services = services;
        this.pageManager = pageManager;
    }

    public Guid? ParentId
    {
        get => GetDirectory().ParentId;
        set => GetDirectory().ParentId = value;
    }

    protected Operation Operation { get; set; } = null!;

    protected override void DoBindingControls()
    {
        textCode.DataBindings.Add(nameof(textCode.TextValue), DataContext, nameof(Operation.Code), false, DataSourceUpdateMode.OnPropertyChanged);
        textName.DataBindings.Add(nameof(textName.TextValue), DataContext, nameof(Operation.ItemName), true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
        comboType.DataBindings.Add(nameof(comboType.SelectedItem), DataContext, nameof(Operation.TypeId), false, DataSourceUpdateMode.OnPropertyChanged);
        selectGroup.DataBindings.Add(nameof(selectGroup.SelectedItem), DataContext, nameof(Operation.ParentId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty);
        textProduced.DataBindings.Add(nameof(textProduced.IntegerValue), DataContext, nameof(Operation.Produced), true, DataSourceUpdateMode.OnPropertyChanged);
        textProdTime.DataBindings.Add(nameof(textProdTime.IntegerValue), DataContext, nameof(Operation.ProdTime), true, DataSourceUpdateMode.OnPropertyChanged);
        textRate.DataBindings.Add(nameof(textRate.IntegerValue), DataContext, nameof(Operation.ProductionRate), true, DataSourceUpdateMode.OnPropertyChanged);
        dateNorm.DataBindings.Add(nameof(dateNorm.DateTimeValue), DataContext, nameof(Operation.DateNorm), true, DataSourceUpdateMode.OnPropertyChanged, DateTime.MinValue);
        textSalary.DataBindings.Add(nameof(textSalary.DecimalValue), DataContext, nameof(Operation.Salary), false, DataSourceUpdateMode.OnPropertyChanged);
    }

    protected override void CreateDataSources()
    {
        selectGroup.DataSource = services.GetRequiredService<IOperationRepository>().GetOnlyFolders();
        comboType.DataSource = services.GetRequiredService<IOperationTypeRepository>().GetListExisting(callback: q => q.OrderBy("item_name"));
        gridGoods.DataSource = Operation.OperationGoods;
    }

    public override void RegisterNestedBrowsers()
    {
        EditorPage.RegisterNestedBrowser<IOperationUsageBrowser>();
    }

    private void ComboType_OpenButtonClick(object sender, DocumentSelectedEventArgs e)
    {
        pageManager.ShowAssociateEditor<IOperationTypeBrowser>(e.Document);
    }

    private bool EditOperationGoods(IDependentEntity? entity)
    {
        if (entity is OperationGoods goods)
        {
            var dialog = services.GetRequiredService<IDirectoryItemDialog>();
            var res = dialog.Get<Goods, IGoodsRepository>(goods.GoodsId);
            if (res != null)
            {
                goods.GoodsId = res.Id;
                goods.Code = res.Code;
                goods.ItemName = res.ItemName ?? string.Empty;

                return true;
            }
        }

        return false;
    }

    private void GridGoods_CreateRow(object sender, DependentEntitySelectEventArgs e)
    {
        var dialog = services.GetRequiredService<IDirectoryItemDialog>();
        var res = dialog.Get<Goods, IGoodsRepository>();
        if (res != null)
        {
            OperationGoods goods = new()
            {
                GoodsId = res.Id,
                Code = res.Code,
                ItemName = res.ItemName ?? string.Empty
            };

            e.DependentEntity = goods;
        }
        else
        {
            e.Accept = false;
        }
    }

    private void GridGoods_EditRow(object sender, DependentEntitySelectEventArgs e)
    {
        e.Accept = EditOperationGoods(e.DependentEntity);
    }
}
