//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Models;
using DocumentFlow.Dialogs.Interfaces;

using System.Diagnostics.CodeAnalysis;

namespace DocumentFlow.Dialogs;

public partial class MaterialQuantityDialog : Form, IMaterialQuantityDialog
{
    public MaterialQuantityDialog(IMaterialRepository materials)
    {
        InitializeComponent();

        selectMaterial.DataSource = materials.GetListExisting();
    }

    public bool Create<T>([MaybeNullWhen(false)] out T material) where T : new()
    {
        if (ShowDialog() == DialogResult.OK)
        {
            material = new T();
            if (material is ReturnMaterialsRows rows)
            {
                var selected = (Material)selectMaterial.SelectedDocument;

                rows.MaterialId = selected.Id;
                rows.MaterialName = selected.ItemName ?? string.Empty;
                rows.MeasurementName = selected.MeasurementName ?? string.Empty;
                rows.Quantity = textQuantity.DecimalValue;
            }

            return true;
        }

        material = default;
        return false;
    }
    
    public bool Edit<T>(T material)
    {
        if (material is not ReturnMaterialsRows rows)
        {
            return false;
        }
            
        selectMaterial.SelectedItem = rows.MaterialId;
        textQuantity.DecimalValue = rows.Quantity;

        if (ShowDialog() == DialogResult.OK)
        {
            var selected = (Material)selectMaterial.SelectedDocument;

            rows.MaterialId = selected.Id;
            rows.Quantity = textQuantity.DecimalValue;
            rows.MaterialName = selected.ItemName ?? string.Empty;
            rows.MeasurementName = selected.MeasurementName ?? string.Empty;

            return true;
        }

        return false;
    }

    private void ButtonOk_Click(object sender, EventArgs e)
    {
        if (selectMaterial.SelectedItem == Guid.Empty)
        {
            MessageBox.Show("Выберите материал", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DialogResult = DialogResult.None;
        }
        else if (textQuantity.DecimalValue <= 0)
        {
            MessageBox.Show("Количество должно быть больше 0", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DialogResult = DialogResult.None;
        }
    }
}
