//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 18.01.2022
//
// Версия 2022.8.20
//  - скорректирован порядок обхода элементов управления
// Версия 2022.11.26
//  - параметр autoRefresh метода SetDataSource в классе
//    DataSourceControl был удален. Вместо него используется свойство
//    RefreshMethod этого класса в значении DataRefreshMethod.Immediately
// Версия 2023.2.6
//  - исправлена орфографическая ошибка
//
//-----------------------------------------------------------------------

using DocumentFlow.Dialogs.Infrastructure;
using DocumentFlow.Entities.Companies;
using DocumentFlow.Entities.Products;
using DocumentFlow.Infrastructure.Controls;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Dialogs;

public partial class PriceApprovalDialog : Form, IPriceApprovalDialog
{
    private readonly IControls<PriceApproval> controls;

    public PriceApprovalDialog(IControls<PriceApproval> controls)
    {
        InitializeComponent();

        this.controls = controls;

        controls.Container = Controls;
        controls
            .AddDirectorySelectBox<Product>(x => x.ProductId, "Материал / Изделие", select =>
                select
                    .SetDataSource(GetProducts, DataRefreshMethod.Immediately)
                    .EditorFitToSize()
                    .SetHeaderWidth(150))
            .AddCurrencyTextBox(x => x.Price, "Цена", text =>
                text
                    .EditorFitToSize()
                    .SetHeaderWidth(150)
                    .DefaultAsValue());
    }

    public bool Create(PriceApproval priceApproval)
    {
        if (ShowDialog() == DialogResult.OK)
        {
            SaveControlData(priceApproval);
            return true;
        }

        return false;
    }

    public bool Edit(PriceApproval priceApproval)
    {
        var product = controls.GetControl<IDirectorySelectBoxControl<Product>>();
        var price = controls.GetControl<ICurrencyTextBoxControl>();

        if (product is IDataSourceControl<Guid, Product> source) 
        {
            source.Select(priceApproval.ProductId);
        }
        
        price.NumericValue = priceApproval.Price;
        if (ShowDialog() == DialogResult.OK)
        {
            SaveControlData(priceApproval);
            return true;
        }

        return false;
    }

    private void SaveControlData(PriceApproval priceApproval)
    {
        var product = controls.GetControl<IDirectorySelectBoxControl<Product>>();
        var price = controls.GetControl<ICurrencyTextBoxControl>();

        if (product.SelectedItem != null)
        {
            priceApproval.ProductId = product.SelectedItem.Id;
        }

        if (price.NumericValue != null)
        {
            priceApproval.Price = price.NumericValue.Value;
        }

        priceApproval.SetProductName(product.ValueText);
    }

    private IEnumerable<Product> GetProducts()
    {
        var materials = Services.Provider.GetService<IMaterialRepository>();
        var goods = Services.Provider.GetService<IGoodsRepository>();

        return materials!
            .GetListExisting(callback: q => q.OrderBy("item_name"))
            .OfType<Product>()
            .Union(
                goods!
                .GetListExisting(callback: q => q.OrderBy("item_name"))
                .OfType<Product>()
            );
    }

    private void ButtonOk_Click(object sender, EventArgs e)
    {
        var product = controls.GetControl<IDirectorySelectBoxControl<Product>>();

        if (product.SelectedItem == null)
        {
            MessageBox.Show("Необходимо выбрать материал/изделие.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DialogResult = DialogResult.None;
        }
    }
}
