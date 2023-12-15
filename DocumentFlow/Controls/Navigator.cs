//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 10.06.2023
//-----------------------------------------------------------------------

using CommunityToolkit.Mvvm.Messaging;

using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Messages;
using DocumentFlow.ViewModels;

using Microsoft.Extensions.DependencyInjection;

using Syncfusion.Windows.Forms.Tools;

namespace DocumentFlow.Controls;

public partial class Navigator : UserControl
{
    private readonly IServiceProvider services;

    public Navigator(IServiceProvider services)
    {
        InitializeComponent();

        this.services = services;

        CreateDocumentItem<IPurchaseRequestBrowser>("Заявка на приобретение материалов");
        
        var warehouse = CreateDocumentItem("Склад");
        CreateTreeMenuItem<IInitialBalanceMaterialBrowser>(warehouse, "Нач. остатки (материалы)");
        CreateTreeMenuItem<IInitialBalanceGoodsBrowser>(warehouse, "Нач. остатки (продукция)");
        CreateTreeMenuItem<IAdjustingBalancesBrowser>(warehouse, "Корректировка остатков");
        CreateTreeMenuItem<IBalanceSheetBrowser>(warehouse, "Материальный отчёт");

        var production = CreateDocumentItem("Производство");
        CreateTreeMenuItem<IProductionOrderBrowser>(production, "Заказ на изготовление");
        CreateTreeMenuItem<IProductionLotBrowser>(production, "Партии");
        CreateTreeMenuItem<IOperationsPerformedBrowser>(production, "Выполненные работы");
        CreateTreeMenuItem<IFinishedGoodsBrowser>(production, "Готовая продукция");
        CreateTreeMenuItem<IWaybillProcessingBrowser>(production, "Поступление в переработку");
        CreateTreeMenuItem<IReturnMaterialsBrowser>(production, "Возврат материалов заказчику");

        var settlements = CreateDocumentItem("Расчёты с контрагентами");
        CreateTreeMenuItem<IInitialBalanceContractorBrowser>(settlements, "Нач. остатки");
        CreateTreeMenuItem<IPaymentOrderBrowser>(settlements, "Платежи");

        CreateTreeMenuItem<IWaybillReceiptBrowser>(treeMenuDocument, "Поступление");
        CreateTreeMenuItem<IWaybillSaleBrowser>(treeMenuDocument, "Реализация");

        var salary = CreateDocumentItem("Зар. плата");
        CreateTreeMenuItem<IInitialBalanceEmployeeBrowser>(salary, "Нач. остатки");
        CreateTreeMenuItem<IWage1cBrowser>(salary, "Зар. плата 1С");
        CreateTreeMenuItem<IGrossPayrollBrowser>(salary, "Начисление зар. платы");
        CreateTreeMenuItem<IPayrollBrowser>(salary, "Платёжная ведомость");
        CreateTreeMenuItem<IPayrollPaymentBrowser>(salary, "Выплата зар. платы");

        CreateDictionaryItem<IMeasurementBrowser>("Единицы измерения");
        CreateDictionaryItem<IOkopfBrowser>("ОКОПФ");
        CreateDictionaryItem<IOkpdtrBrowser>("ОКПДТР");
        CreateDictionaryItem<IBankBrowser>("Банки");
        CreateDictionaryItem<IPersonBrowser>("Физ. лица");
        CreateDictionaryItem<IContractorBrowser>("Контрагенты");
        CreateDictionaryItem<IOrganizationBrowser>("Организации");
        CreateDictionaryItem<IEmployeeBrowser>("Сотрудники");

        var assortment = CreateDictionaryItem("Номенклатура");
        CreateTreeMenuItem<IWireBrowser>(assortment, "Типы проводов");
        CreateTreeMenuItem<IMaterialBrowser>(assortment, "Материалы");
        CreateTreeMenuItem<IGoodsBrowser>(assortment, "Продукция");

        CreateDictionaryItem<IOperationTypeBrowser>("Виды производственных операций");

        var prod_opers = CreateDictionaryItem("Производственные операции");
        CreateTreeMenuItem<IOperationBrowser>(prod_opers, "Операция");
        CreateTreeMenuItem<ICuttingBrowser>(prod_opers, "Резка");

        CreateDictionaryItem<IDeductionBrowser>("Удержания");
        CreateDictionaryItem<IEquipmentBrowser>("Оборудование");
    }

    private TreeMenuItem CreateDocumentItem(string text) => CreateTreeMenuItem(treeMenuDocument, text);
    
    private TreeMenuItem CreateDocumentItem<B>(string text) where B : IBrowserPage => CreateTreeMenuItem<B>(treeMenuDocument, text);

    private TreeMenuItem CreateDictionaryItem(string text) => CreateTreeMenuItem(treeMenuDictionary, text);

    private TreeMenuItem CreateDictionaryItem<B>(string text) where B : IBrowserPage => CreateTreeMenuItem<B>(treeMenuDictionary, text);

    private TreeMenuItem CreateTreeMenuItem<B>(TreeMenuItem parent, string text) where B : IBrowserPage
    {
        var item = CreateTreeMenuItem(parent, text);

        item.Tag = typeof(B);
        item.Click += AddonItem_Click;

        return item;
    }

    private static TreeMenuItem CreateTreeMenuItem(TreeMenuItem parent, string text)
    {
        TreeMenuItem item = new()
        {
            Text = text,
            BackColor = parent.BackColor,
            ForeColor = parent.ForeColor,
            ItemBackColor = parent.ItemBackColor,
            ItemHoverColor = parent.ItemHoverColor,
            SelectedColor = parent.SelectedColor,
            SelectedItemForeColor = parent.SelectedItemForeColor
        };

        parent.Items.Add(item);

        return item;
    }

    private void AddonItem_Click(object? sender, EventArgs e)
    {
        if (sender is TreeMenuItem menu && menu.Tag is Type type)
        {
            WeakReferenceMessenger.Default.Send(new EntityBrowserOpenMessage(type, menu.Text));
        }
    }

    private void TreeMenuAbout_Click(object sender, EventArgs e) 
    {
        services.GetRequiredService<AboutForm>().ShowDialog();
    }

    private void TreeMenuEmail_Click(object sender, EventArgs e) 
    {
        WeakReferenceMessenger.Default.Send(new PageOpenMessage(typeof(IEmailPage)));
    }
}
