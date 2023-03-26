//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 05.02.2023
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Controls.Editors;
using DocumentFlow.Entities.Products;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Operations.Dialogs;

public partial class GoodsSelectForm : Form
{
    private readonly DfDirectorySelectBox<Goods> goods;

    protected GoodsSelectForm()
    {
        InitializeComponent();

        goods = new(string.Empty, "Изделме", 120)
        {
            Required = true,
            NameColumn = "Code",
            Columns = new Dictionary<string, string>
            {
                ["Code"] = "Артикул",
                ["ItemName"] = "Наименование"
            },
            RefreshMethod = DataRefreshMethod.Immediately
        };

        goods.SetDataSource(() =>
        {
            var repo = Services.Provider.GetService<IGoodsRepository>();
            return repo?.GetAllValid(callback: query => query.OrderByDesc("is_folder").OrderBy("code"));
        });

        Controls.Add(goods);
    }

    public static DialogResult Create(OperationGoods operationGoods)
    {
        GoodsSelectForm form = new();
        if (form.ShowDialog() == DialogResult.OK)
        {
            form.SaveControlData(operationGoods);
            return DialogResult.OK;
        }

        return DialogResult.Cancel;
    }

    public static DialogResult Edit(OperationGoods operationGoods)
    {
        GoodsSelectForm form = new();
        form.goods.Value = operationGoods.GoodsId;
        if (form.ShowDialog() == DialogResult.OK)
        {
            form.SaveControlData(operationGoods);
            return DialogResult.OK;
        }

        return DialogResult.Cancel;
    }

    private void SaveControlData(OperationGoods operationGoods)
    {
        if (goods.SelectedItem != null)
        {
            operationGoods.GoodsId = goods.SelectedItem.Id;
            operationGoods.SetGoodsData(goods.SelectedItem);
        }
        else
        {
            operationGoods.ClearGoodsData();
        }
    }

    private void ButtonOk_Click(object sender, EventArgs e)
    {
        if (goods.SelectedItem == null)
        {
            MessageBox.Show("Необходимо выбрать изделие.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DialogResult = DialogResult.None;
        }
    }
}
