//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 20.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls;
using DocumentFlow.Controls.Enums;
using DocumentFlow.Controls.Events;
using DocumentFlow.Data.Models;
using DocumentFlow.Interfaces;
using DocumentFlow.Tools;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.ViewModels;

[Entity(typeof(InitialBalanceGoods), RepositoryType = typeof(IInitialBalanceGoodsRepository))]
public partial class InitialBalanceGoodsEditor : EditorPanel, IInitialBalanceGoodsEditor
{
    private readonly IServiceProvider services;
    private readonly IPageManager pageManager;

    public InitialBalanceGoodsEditor(IServiceProvider services, IPageManager pageManager) : base(services)
    {
        InitializeComponent();

        this.services = services;
        this.pageManager = pageManager;

        InitialBalance.OrganizationId = services
            .GetRequiredService<IOrganizationRepository>()
            .GetMain()
            .Id;

        var columns = new Dictionary<string, string>
        {
            ["Code"] = "Артикул",
            ["ItemName"] = "Наименование"
        };

        selectGoods.SetColumns<Goods>(nameof(Goods.Code), columns);
    }

    protected InitialBalanceGoods InitialBalance { get; set; } = null!;

    protected override void AfterConstructData(ConstructDataMethod method)
    {
        textDocNumber.Enabled = InitialBalance.Id != Guid.Empty;
    }

    protected override void DoBindingControls()
    {
        textDocNumber.DataBindings.Add(nameof(textDocNumber.IntegerValue), DataContext, nameof(InitialBalanceGoods.DocumentNumber), true, DataSourceUpdateMode.OnPropertyChanged, 0);
        dateDocument.DataBindings.Add(nameof(dateDocument.DateTimeValue), DataContext, nameof(InitialBalanceGoods.DocumentDate), true, DataSourceUpdateMode.OnPropertyChanged);
        comboOrg.DataBindings.Add(nameof(comboOrg.SelectedItem), DataContext, nameof(InitialBalanceGoods.OrganizationId), false, DataSourceUpdateMode.OnPropertyChanged);
        selectGoods.DataBindings.Add(nameof(selectGoods.SelectedItem), DataContext, nameof(InitialBalanceGoods.ReferenceId), false, DataSourceUpdateMode.OnPropertyChanged);
        textAmount.DataBindings.Add(nameof(textAmount.DecimalValue), DataContext, nameof(InitialBalanceGoods.Amount), false, DataSourceUpdateMode.OnPropertyChanged);
        textOperationSumma.DataBindings.Add(nameof(textOperationSumma.DecimalValue), DataContext, nameof(InitialBalanceGoods.OperationSumma), false, DataSourceUpdateMode.OnPropertyChanged);
    }

    protected override void CreateDataSources()
    {
        comboOrg.DataSource = services.GetRequiredService<IOrganizationRepository>().GetList();
        selectGoods.DataSource = services.GetRequiredService<IGoodsRepository>().GetListExisting();
    }

    private void SelectGoods_OpenButtonClick(object sender, DocumentSelectedEventArgs e)
    {
        pageManager.ShowAssociateEditor<IGoodsBrowser>(e.Document);
    }
}
