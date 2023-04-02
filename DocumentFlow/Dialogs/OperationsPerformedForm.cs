//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 12.06.2022
//
// Версия 2022.9.9
//  - добавлено поле double_rate
// Версия 2022.11.26
//  - параметр autoRefresh метода SetDataSource в классе
//    DataSourceControl был удален. Вместо него используется свойство
//    RefreshMethod этого класса в значении DataRefreshMethod.Immediately
// Версия 2022.12.2
//  - поле double_rate теперь редактируется с помощью DfCheckBox, что бы
//    была возможность установки неопределенного значения
//  - удалена инициализпция значения свойства double_rate
// Версия 2022.12.3
//  - в double_rate инициализация параметра allowThreeState
//    перенесена в параметры конструктора
//  - double_rate теперь является полем класса
//  - добавлено свойство DoubleRate
// Версия 2023.3.14
//  - GetAllMaterials заменен на GetAllValid
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Editor;
using DocumentFlow.Controls.Editors;
using DocumentFlow.Entities.Calculations;
using DocumentFlow.Entities.Employees;
using DocumentFlow.Entities.Productions.Lot;
using DocumentFlow.Entities.Products;
using DocumentFlow.Infrastructure.Controls;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Dialogs;

public partial class OperationsPerformedForm : Form
{
    private const int headerWidth = 210;

    private readonly DfDirectorySelectBox<CalculationOperation> operations;
    private readonly DfDirectorySelectBox<OurEmployee> employee;
    private readonly DfDirectorySelectBox<Material> replacing_material;
    private readonly DfIntegerTextBox<long> quantity;
    private readonly DfCheckBox double_rate;

    public OperationsPerformedForm(Guid lot_id)
    {
        InitializeComponent();

        operations = new DfDirectorySelectBox<CalculationOperation>("operation_id", "Операция", headerWidth, 350)
        {
            RemoveEmptyFolders = true,
            TabIndex = 1,
            RefreshMethod = DataRefreshMethod.Immediately
        };

        var using_material = new DfTextBox("material_name", "Материал (по спецификации)", headerWidth, 350) { ReadOnly = true, TabIndex = 2 };
        replacing_material = new DfDirectorySelectBox<Material>("replacing_material_id", "Использованный материал", headerWidth, 350) { TabIndex = 3, RefreshMethod = DataRefreshMethod.Immediately };
        employee = new DfDirectorySelectBox<OurEmployee>("employee_id", "Исполнитель", headerWidth, 350) { TabIndex = 4, RefreshMethod = DataRefreshMethod.Immediately };
        quantity = new DfIntegerTextBox<long>("quantity", "Количество", headerWidth, 150) { TabIndex = 5 };
        double_rate = new DfCheckBox("double_rate", "Двойная оплата", headerWidth, allowThreeState: true) { TabIndex = 6 };

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
                    .Where("calculation_operation.owner_id", lot.CalculationId)
                    .OrderBy("code")
                );

            return op;
        });

        operations.ValueChanged += (sender, e) =>
        {
            using_material.Value = e.NewValue?.MaterialName;
            replacing_material.Enabled = e.NewValue?.MaterialId != null;
        };

        replacing_material.SetDataSource(() => Services.Provider.GetService<IMaterialRepository>()?.GetAllValid());

        employee.SetDataSource(() => Services.Provider.GetService<IOurEmployeeRepository>()?.GetAllValid());

        Controls.AddRange(new Control[]
        {
            double_rate,
            quantity,
            employee,
            replacing_material,
            using_material,
            operations
        });

        quantity.Select();
    }

    public CalculationOperation? Operation
    {
        get => operations.SelectedItem;
        set => operations.Value = value?.Id;
    }

    public OurEmployee? Employee
    {
        get => employee.SelectedItem;
        set => employee.Value = value?.Id;
    }

    public Material? ReplacingMaterial
    {
        get => replacing_material.SelectedItem;
        set => replacing_material.Value = value?.Id;
    }

    public long Quantity
    {
        get => quantity.NumericValue == null ? 0 : quantity.NumericValue.Value;
        set => quantity.NumericValue = value;
    }

    public bool? DoubleRate
    {
        get => (bool?)double_rate.Value;
        set => double_rate.Value = value;
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
