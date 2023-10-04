//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Models;
using DocumentFlow.Tools;

using System.Diagnostics.CodeAnalysis;

namespace DocumentFlow.Dialogs;

[Dialog]
public partial class MaterialQuantityDialog : Form
{
    public MaterialQuantityDialog(IMaterialRepository materials)
    {
        InitializeComponent();

        selectMaterial.DataSource = materials.GetListExisting();
    }

    public bool Create([MaybeNullWhen(false)] out ReturnMaterialsRows materialRows)
    {
        if (ShowDialog() == DialogResult.OK)
        {
            Material material = (Material)selectMaterial.SelectedDocument;
            materialRows = new ReturnMaterialsRows()
            {
                MaterialId = material.Id,
                MaterialName = material.ItemName ?? string.Empty,
                MeasurementName = material.MeasurementName ?? string.Empty,
                Quantity = textQuantity.DecimalValue
            };

            return true;
        }

        materialRows = default;
        return false;
    }

    public bool Edit(ReturnMaterialsRows materialRows)
    {
        selectMaterial.SelectedItem = materialRows.MaterialId;
        textQuantity.DecimalValue = materialRows.Quantity;

        if (ShowDialog() == DialogResult.OK)
        {
            Material material = (Material)selectMaterial.SelectedDocument;

            materialRows.MaterialId = material.Id;
            materialRows.Quantity = textQuantity.DecimalValue;
            materialRows.MaterialName = material.ItemName ?? string.Empty;
            materialRows.MeasurementName = material.MeasurementName ?? string.Empty;

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
