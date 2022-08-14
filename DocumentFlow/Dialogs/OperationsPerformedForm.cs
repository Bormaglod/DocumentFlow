//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 12.06.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Editors;
using DocumentFlow.Entities.Calculations;
using DocumentFlow.Entities.Employees;
using DocumentFlow.Entities.Productions.Lot;
using DocumentFlow.Entities.Products;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Dialogs;

public partial class OperationsPerformedForm : Form
{
    private const int headerWidth = 210;

    private readonly DfDirectorySelectBox<CalculationOperation> operations;
    private readonly DfDirectorySelectBox<OurEmployee> employee;
    private readonly DfDirectorySelectBox<Material> replacing_material;
    private readonly DfIntegerTextBox<long> quantity;

    public OperationsPerformedForm(Guid lot_id)
    {
        InitializeComponent();

        operations = new DfDirectorySelectBox<CalculationOperation>("operation_id", "Операция", headerWidth, 350)
        {
            RemoveEmptyFolders = true,
            TabIndex = 1
        };

        var using_material = new DfTextBox("material_name", "Материал (по спецификации)", headerWidth, 350) { ReadOnly = true, TabIndex = 2 };
        replacing_material = new DfDirectorySelectBox<Material>("replacing_material_id", "Использованный материал", headerWidth, 350) { TabIndex = 3 };
        employee = new DfDirectorySelectBox<OurEmployee>("employee_id", "Исполнитель", headerWidth, 350) { TabIndex = 4 };
        quantity = new DfIntegerTextBox<long>("quantity", "Количество", headerWidth, 150) { TabIndex = 5 };

        operations.SetDataSource(() =>
        {
            var repoLot = Services.Provider.GetService<IProductionLotRepository>();
            if (repoLot == null)
            {
                return null;
            }

            var lot = repoLot.GetById(lot_id);

            var repo = Services.Provider.GetService<ICalculationOperationRepository>();
            if (repo == null)
            {
                return null;
            }

            var op = repo.GetAllValid(callback: query =>
                query
                    .Select("calculation_operation.*")
                    .Select("m.item_name as material_name")
                    .LeftJoin("material as m", "m.id", "material_id")
                    .Where("calculation_operation.owner_id", lot.calculation_id)
                    .OrderBy("code")
                );

            return op;
        }, true);

        operations.ValueChanged += (sender, e) =>
        {
            using_material.Value = e.NewValue?.material_name;
            replacing_material.Enabled = e.NewValue?.material_id != null;
        };

        replacing_material.SetDataSource(() => Services.Provider.GetService<IMaterialRepository>()?.GetAllMaterials(), true);

        employee.SetDataSource(() => Services.Provider.GetService<IOurEmployeeRepository>()?.GetAllValid(), true);

        Controls.AddRange(new Control[]
        {
            quantity,
            employee,
            replacing_material,
            using_material,
            operations
        });

        quantity.Select();
    }

    public CalculationItem? Operation
    {
        get => operations.SelectedItem;
        set => operations.Value = value?.id;
    }

    public OurEmployee? Employee
    {
        get => employee.SelectedItem;
        set => employee.Value = value?.id;
    }

    public Material? ReplacingMaterial
    {
        get => replacing_material.SelectedItem;
        set => replacing_material.Value = value?.id;
    }

    public long Quantity
    {
        get => quantity.NumericValue == null ? 0 : quantity.NumericValue.Value;
        set => quantity.NumericValue = value;
    }

    private void ButtonOk_Click(object sender, EventArgs e)
    {
        if (employee.SelectedItem == null)
        {
            MessageBox.Show("Необходимо выбрать сотрудника", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DialogResult = DialogResult.None;
        }
    }
}
