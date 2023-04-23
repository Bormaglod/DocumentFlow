//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 05.02.2023
//-----------------------------------------------------------------------

using DocumentFlow.Dialogs.Infrastructure;
using DocumentFlow.Entities.Operations;
using DocumentFlow.Entities.Products;
using DocumentFlow.Infrastructure.Controls;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Dialogs;

public partial class GoodsSelectDialog : Form, IGoodsSelectDialog
{
    private readonly IControls<OperationGoods> controls;
    private readonly IReadOnlyDictionary<string, string> columns = new Dictionary<string, string>
    {
        ["Code"] = "Артикул",
        ["ItemName"] = "Наименование"
    };

    public GoodsSelectDialog(IControls<OperationGoods> controls)
    {
        InitializeComponent();

        this.controls = controls;

        controls.Container = Controls;
        controls
            .AddDirectorySelectBox<Goods>(x => x.GoodsId, "Изделие", text =>
                text
                    .SetDataSource(GetGoods, DataRefreshMethod.Immediately)
                    .Required()
                    .SetColumns(x => x.Code, columns)
                    .EditorFitToSize()
                    .SetHeaderWidth(120));
    }

    public bool Create(OperationGoods operationGoods)
    {
        if (ShowDialog() == DialogResult.OK)
        {
            SaveControlData(operationGoods);
            return true;
        }

        return false;
    }

    public bool Edit(OperationGoods operationGoods)
    {
        var goods = controls.GetControl<IDirectorySelectBoxControl<Goods>>();

        if (goods is IDataSourceControl<Guid, Goods> source) 
        {
            source.Select(operationGoods.GoodsId);
        }
        
        if (ShowDialog() == DialogResult.OK)
        {
            SaveControlData(operationGoods);
            return true;
        }

        return false;
    }

    private IEnumerable<Goods>? GetGoods()
    {
        var repo = Services.Provider.GetService<IGoodsRepository>();
        return repo?.GetAllValid(callback: query => query.OrderByDesc("is_folder").OrderBy("code"));
    }

    private void SaveControlData(OperationGoods operationGoods)
    {
        var goods = controls.GetControl<IDirectorySelectBoxControl<Goods>>();

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
        var goods = controls.GetControl<IDirectorySelectBoxControl<Goods>>();

        if (goods.SelectedItem == null)
        {
            MessageBox.Show("Необходимо выбрать изделие.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DialogResult = DialogResult.None;
        }
    }
}
