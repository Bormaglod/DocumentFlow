//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 18.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Models;
using DocumentFlow.Dialogs.Interfaces;

using System.Diagnostics.CodeAnalysis;

namespace DocumentFlow.Dialogs;

public partial class PriceApprovalDialog : Form, IPriceApprovalDialog
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

    public bool Create<T>([MaybeNullWhen(false)] out T row) where T : new()
    {
        if (ShowDialog() == DialogResult.OK)
        {
            row = new T();
            if (row is PriceApproval price)
            {
                Product product = (Product)selectProduct.SelectedDocument;
                price.ProductId = product.Id;
                price.Price = textPrice.DecimalValue;
                price.ProductName = product.ItemName ?? string.Empty;
                price.MeasurementId = product.MeasurementId;
                price.MeasurementName = product.MeasurementName ?? string.Empty;
            }

            return true;
        }

        row = default;
        return false;
    }

    public bool Edit<T>(T row)
    {
        if (row is not PriceApproval price)
        {
            return false;
        }

        selectProduct.SelectedItem = price.ProductId;
        textPrice.DecimalValue = price.Price;
        
        if (ShowDialog() == DialogResult.OK)
        {
            Product product = (Product)selectProduct.SelectedDocument;

            price.ProductId = product.Id;
            price.Price = textPrice.DecimalValue;
            price.ProductName = product.ItemName ?? string.Empty;
            price.MeasurementId = product.MeasurementId;
            price.MeasurementName = product.MeasurementName ?? string.Empty;

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
