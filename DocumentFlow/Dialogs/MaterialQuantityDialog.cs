//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.07.2022
//
// Версия 2022.11.26
//  - параметр autoRefresh метода SetDataSource в классе
//    DataSourceControl был удален. Вместо него используется свойство
//    RefreshMethod этого класса в значении DataRefreshMethod.Immediately
// Версия 2023.3.14
//  - GetAllMaterials заменен на GetAllValid
//
//-----------------------------------------------------------------------

using DocumentFlow.Dialogs.Infrastructure;
using DocumentFlow.Entities.Productions.Returns;
using DocumentFlow.Entities.Products;
using DocumentFlow.Infrastructure.Controls;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Dialogs;

public partial class MaterialQuantityDialog : Form, IMaterialQuantityDialog
{
    private readonly IControls<ReturnMaterialsRows> controls;

    public MaterialQuantityDialog(IControls<ReturnMaterialsRows> controls)
    {
        InitializeComponent();

        this.controls = controls;

        controls.Container = Controls;
        controls
            .AddDirectorySelectBox<Material>(x => x.MaterialId, "Материал", select =>
                select
                    .SetDataSource(GetMaterials, DataRefreshMethod.Immediately)
                    .Required()
                    .SetHeaderWidth(120)
                    .EditorFitToSize())
            .AddNumericTextBox(x => x.Quantity, "Количество", text =>
                text
                    .SetNumberDecimalDigits(3)
                    .SetHeaderWidth(120)
                    .DefaultAsValue()
                    .EditorFitToSize());
    }

    private IDirectorySelectBoxControl<Material> Material => controls.GetControl<IDirectorySelectBoxControl<Material>>(x => x.MaterialId);
    
    private INumericTextBoxControl Quantity => controls.GetControl<INumericTextBoxControl>(x => x.Quantity);

    public bool Create(ReturnMaterialsRows row)
    {
        if (ShowDialog() == DialogResult.OK)
        {
            SaveControlData(row);
            return true;
        }

        return false;
    }

    public bool Edit(ReturnMaterialsRows row)
    {
        if (Material is IDataSourceControl<Guid, Material> source) 
        {
            source.Select(row.MaterialId);
        }
        
        Quantity.NumericValue = row.Quantity;

        if (ShowDialog() == DialogResult.OK)
        {
            SaveControlData(row);
            return true;
        }

        return false;
    }

    private void SaveControlData(ReturnMaterialsRows row)
    {
        row.MaterialId = Material.SelectedItem?.Id ?? Guid.Empty;
        row.MaterialName = Material.ValueText;
        row.Quantity = Quantity.NumericValue.GetValueOrDefault();
    }

    private IEnumerable<Material>? GetMaterials() => Services.Provider.GetService<IMaterialRepository>()?.GetListExisting();

    private void ButtonOk_Click(object sender, EventArgs e)
    {
        if (Material.SelectedItem == null)
        {
            MessageBox.Show("Выберите материал", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DialogResult = DialogResult.None;
        }
        else if (Quantity.NumericValue <= 0)
        {
            MessageBox.Show("Количество должно быть больше 0", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DialogResult = DialogResult.None;
        }
    }
}
