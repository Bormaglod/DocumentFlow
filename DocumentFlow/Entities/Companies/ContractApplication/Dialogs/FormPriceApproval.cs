//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
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
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Controls.Editors;
using DocumentFlow.Entities.Products;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Companies;

public partial class FormPriceApproval : Form
{
    private readonly DfDirectorySelectBox<Product> product;
    private readonly DfCurrencyTextBox price;

    protected FormPriceApproval()
    {
        InitializeComponent();

        product = new("reference_id", "Материал / Изделие", 150) { TabIndex = 1, RefreshMethod = DataRefreshMethod.Immediately };
        price = new("price", "Цена", 150) { DefaultAsNull = false, TabIndex = 2 };

        product.SetDataSource(() =>
        {
            var materials = Services.Provider.GetService<IMaterialRepository>();
            var goods = Services.Provider.GetService<IGoodsRepository>();

            return materials!
                .GetAllValid(callback: q => q.OrderBy("item_name"))
                .OfType<Product>()
                .Union(
                    goods!
                    .GetAllValid(callback: q => q.OrderBy("item_name"))
                    .OfType<Product>()
                );
        });

        Controls.AddRange(new Control[]
        {
            price,
            product
        });
    }

    public static DialogResult Create(PriceApproval price)
    {
        FormPriceApproval form = new();
        if (form.ShowDialog() == DialogResult.OK)
        {
            form.SaveControlData(price);
            return DialogResult.OK;
        }

        return DialogResult.Cancel;
    }

    public static DialogResult Edit(PriceApproval price)
    {
        FormPriceApproval form = new();
        form.product.Value = price.product_id;
        form.price.Value = price.price;
        if (form.ShowDialog() == DialogResult.OK)
        {
            form.SaveControlData(price);
            return DialogResult.OK;
        }

        return DialogResult.Cancel;
    }

    private void SaveControlData(PriceApproval priceApproval)
    {
        if (product.SelectedItem != null)
        {
            priceApproval.product_id = product.SelectedItem.id;
        }

        if (price.NumericValue != null)
        {
            priceApproval.price = price.NumericValue.Value;
        }

        priceApproval.SetProductName(product.ValueText);
    }

    private void ButtonOk_Click(object sender, EventArgs e)
    {
        if (product.SelectedItem == null)
        {
            MessageBox.Show("Необхлдимо выбрать материал/изделие.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DialogResult = DialogResult.None;
        }
    }
}
