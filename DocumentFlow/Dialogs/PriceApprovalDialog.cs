//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 18.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Tools;
using DocumentFlow.Data.Models;

using System.Diagnostics.CodeAnalysis;

namespace DocumentFlow.Dialogs;

[Dialog]
public partial class PriceApprovalDialog : Form
{
    public PriceApprovalDialog(IMaterialRepository materials, IGoodsRepository goods)
    {
        InitializeComponent();

        selectProduct.DataSource = materials
            .GetListExisting(callback: q => q.OrderBy("item_name"))
            .OfType<Product>()
            .Union(
                goods!
                .GetListExisting(callback: q => q.OrderBy("item_name"))
                .OfType<Product>()
            );
    }

    public bool Create([MaybeNullWhen(false)] out PriceApproval priceApproval)
    {
        if (ShowDialog() == DialogResult.OK)
        {
            Product product = (Product)selectProduct.SelectedDocument;
            priceApproval = new PriceApproval()
            {
                ProductId = product.Id,
                Price = textPrice.DecimalValue,
                ProductName = product.ItemName ?? string.Empty,
                MeasurementId = product.MeasurementId,
                MeasurementName = product.MeasurementName ?? string.Empty
            };

            return true;
        }

        priceApproval = default;
        return false;
    }

    public bool Edit(PriceApproval priceApproval)
    {
        selectProduct.SelectedItem = priceApproval.ProductId;
        textPrice.DecimalValue = priceApproval.Price;
        
        if (ShowDialog() == DialogResult.OK)
        {
            Product product = (Product)selectProduct.SelectedDocument;

            priceApproval.ProductId = product.Id;
            priceApproval.Price = textPrice.DecimalValue;
            priceApproval.ProductName = product.ItemName ?? string.Empty;
            priceApproval.MeasurementId = product.MeasurementId;
            priceApproval.MeasurementName = product.MeasurementName ?? string.Empty;

            return true;
        }

        return false;
    }

    private void ButtonOk_Click(object sender, EventArgs e)
    {
        if (selectProduct.SelectedItem == Guid.Empty)
        {
            MessageBox.Show("Необходимо выбрать материал/изделие.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DialogResult = DialogResult.None;
        }
    }
}
