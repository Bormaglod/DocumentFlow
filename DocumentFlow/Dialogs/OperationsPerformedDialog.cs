﻿//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 12.06.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Models;
using DocumentFlow.Tools;

namespace DocumentFlow.Dialogs;

[Dialog]
public partial class OperationsPerformedDialog : Form
{
    private ProductionLot? lot;
    private readonly IMaterialRepository materials;
    private readonly IOurEmployeeRepository emps;
    private readonly ICalculationOperationRepository calcs;

    public OperationsPerformedDialog(IMaterialRepository materials, IOurEmployeeRepository emps, ICalculationOperationRepository calcs)
    {
        InitializeComponent();

        this.materials = materials;
        this.emps = emps;
        this.calcs = calcs;

        ActiveControl = textQuantity;
    }

    public CalculationOperation Operation
    {
        get
        {
            if (selectOperation.SelectedItem == Guid.Empty)
            {
                throw new NullReferenceException();
            }

            return (CalculationOperation)selectOperation.SelectedDocument;
        }
    }

    public OurEmployee Employee
    {
        get
        {
            if (selectEmp.SelectedItem == Guid.Empty)
            {
                throw new NullReferenceException();
            }

            return (OurEmployee)selectEmp.SelectedDocument;
        }
    }

    public bool Show(ProductionLot lot, CalculationOperation? operation, Employee? emp)
    {
        this.lot = lot;

        selectOperation.DataSource = calcs.GetListExisting(callback: query =>
            query
                .Select("calculation_operation.*")
                .Select("m.item_name as material_name")
                .LeftJoin("material as m", "m.id", "material_id")
                .Where("calculation_operation.owner_id", lot.CalculationId)
                .OrderBy("code")
            );
        selectMaterial.DataSource = materials.GetListExisting();
        selectEmp.DataSource = emps.GetListExisting();

        if (operation != null)
        {
            selectOperation.SelectedItem = operation.Id;
        }

        if (emp != null)
        {
            selectEmp.SelectedItem = emp.Id;
        }

        return ShowDialog() == DialogResult.OK;
    }

    public OperationsPerformed Get()
    {
        ArgumentNullException.ThrowIfNull(lot);

        var obj = new OperationsPerformed()
        {
            EmployeeId = selectEmp.SelectedItem,
            OperationId = selectOperation.SelectedItem,
            ReplacingMaterialId = selectMaterial.SelectedItem == Guid.Empty ? null : selectMaterial.SelectedItem,
            Quantity = textQuantity.IntegerValue,
            DoubleRate = checkDoubleRate.CheckValue,
            OwnerId = lot.Id
        };

        return obj;
    }


    private void ButtonOk_Click(object sender, EventArgs e)
    {
        if (selectEmp.SelectedItem == Guid.Empty)
        {
            MessageBox.Show("Необходимо выбрать сотрудника.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DialogResult = DialogResult.None;
            return;
        }

        if (selectOperation.SelectedItem == Guid.Empty)
        {
            MessageBox.Show("Необходимо выбрать выполненную операцию.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DialogResult = DialogResult.None;
            return;
        }

        if (textQuantity.IntegerValue <= 0)
        {
            MessageBox.Show("Необходимо указать количество операций.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DialogResult = DialogResult.None;
            return;
        }

        var operation = (CalculationOperation)selectOperation.SelectedDocument;
        if (operation.MaterialId != null)
        {
            decimal materialCount;
            if (selectMaterial.SelectedItem != Guid.Empty)
            {
                var material = (Material)selectMaterial.SelectedDocument;
                materialCount = materials.GetRemainder(material, dateTime.DateTimeValue);
            }
            else
            {
                materialCount = materials.GetRemainder(operation.MaterialId.Value, dateTime.DateTimeValue);
            }

            decimal expense = textQuantity.IntegerValue * operation.MaterialAmount;
            if (expense > materialCount)
            {
                MessageBox.Show($"Количества материала ({materialCount}) недостаточно для выполнения этой операции.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }
        }
    }

    private void SelectOperation_SelectedItemChanged(object sender, EventArgs e)
    {
        if (selectOperation.SelectedItem == Guid.Empty)
        {
            textSpecMaterial.TextValue = string.Empty;
            selectMaterial.Enabled = false;
        }
        else
        {
            var operation = (CalculationOperation)selectOperation.SelectedDocument;
            textSpecMaterial.TextValue = operation?.MaterialName ?? string.Empty;
            selectMaterial.Enabled = operation?.MaterialId != null;
        }
    }
}
