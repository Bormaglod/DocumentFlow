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

using DocumentFlow.Controls.Core;
using DocumentFlow.Controls.Editors;
using DocumentFlow.Entities.Products;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Productions.Returns.Dialogs;

public partial class FormMaterialQuantity : Form
{
    private readonly DfDirectorySelectBox<Material> material;
    private readonly DfNumericTextBox quantity;

    protected FormMaterialQuantity()
    {
        InitializeComponent();

        material = new("MaterialId", "Материал", 120) { Required = true, RefreshMethod = DataRefreshMethod.Immediately };
        quantity = new("Quantity", "Количество", 120) { DefaultAsNull = false, NumberDecimalDigits = 3 };

        material.SetDataSource(() => Services.Provider.GetService<IMaterialRepository>()?.GetAllValid());

        Controls.AddRange(new Control[]
        {
            quantity,
            material
        });
    }

    public static DialogResult Create(ReturnMaterialsRows row)
    {
        FormMaterialQuantity form = new();
        if (form.ShowDialog() == DialogResult.OK)
        {
            form.SaveControlData(row);
            return DialogResult.OK;
        }

        return DialogResult.Cancel;
    }

    public static DialogResult Edit(ReturnMaterialsRows row)
    {
        FormMaterialQuantity form = new();
        form.material.Value = row.MaterialId;
        form.quantity.Value = row.Quantity;

        if (form.ShowDialog() == DialogResult.OK)
        {
            form.SaveControlData(row);
            return DialogResult.OK;
        }

        return DialogResult.Cancel;
    }

    private void SaveControlData(ReturnMaterialsRows row)
    {
        row.MaterialId = material.SelectedItem?.Id ?? Guid.Empty;
        row.MaterialName = material.ValueText;
        row.Quantity = quantity.NumericValue.GetValueOrDefault();
    }

    private void ButtonOk_Click(object sender, EventArgs e)
    {
        if (material.Value == null)
        {
            MessageBox.Show("Выберите материал", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DialogResult = DialogResult.None;
        }
        else if (quantity.NumericValue <= 0)
        {
            MessageBox.Show("Количество должно быть больше 0", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DialogResult = DialogResult.None;
        }
    }
}
