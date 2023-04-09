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

using DocumentFlow.Controls.Editors;
using DocumentFlow.Entities.Products;
using DocumentFlow.Infrastructure.Controls;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Companies;

public partial class FormPriceApproval : Form
{
    private readonly DfDirectorySelectBox<Product> product;
    private readonly DfCurrencyTextBox price;

    protected FormPriceApproval()
    {
        InitializeComponent();

        product = new("ReferenceId", "Материал / Изделие", 150) { TabIndex = 1, RefreshMethod = DataRefreshMethod.Immediately, EditorFitToSize = true };
        price = new("Price", "Цена", 150) { DefaultAsNull = false, TabIndex = 2, EditorFitToSize = true };

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
        form.product.Value = price.ProductId;
        form.price.Value = price.Price;
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
            priceApproval.ProductId = product.SelectedItem.Id;
        }

        if (price.NumericValue != null)
        {
            priceApproval.Price = price.NumericValue.Value;
        }

        priceApproval.SetProductName(product.ValueText);
    }

    private void ButtonOk_Click(object sender, EventArgs e)
    {
        if (product.SelectedItem == null)
        {
            MessageBox.Show("Необходимо выбрать материал/изделие.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DialogResult = DialogResult.None;
        }
    }
}
