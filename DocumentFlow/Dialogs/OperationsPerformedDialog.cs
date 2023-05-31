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
// Версия 2023.5.31
//  - добавлена проверка значения Quantity. Оно должно быть больше 0
//  - параметр lotId из конструктора перемещен в метод Show
//  - в конструктор добавлены интерфейсы (с целью создания этого окна
//    с помощью соответствующего сервиса)
//  - добавлена реализация интерфейса IOperationsPerformedDialog и
//    соответственно удалены свойства Employee и Operation
//  - добавлена проверка на соответствие списываемого материала и
//    остатка
//  - установка DataSource поля OperationId перенесена в метод Show,
//    т.к. для неё требуется значение lotId, а оно устанавливается
//    именно в этом методе
//
//-----------------------------------------------------------------------

using DocumentFlow.Dialogs.Infrastructure;
using DocumentFlow.Entities.Calculations;
using DocumentFlow.Entities.Employees;
using DocumentFlow.Entities.Productions.Lot;
using DocumentFlow.Entities.Productions.Performed;
using DocumentFlow.Entities.Products;
using DocumentFlow.Infrastructure.Controls;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Dialogs;

public partial class OperationsPerformedDialog : Form, IOperationsPerformedDialog
{
    private Guid lotId;
    private readonly IControls<OperationsPerformed> controls;
    private readonly IMaterialRepository materials;
    private readonly IOurEmployeeRepository emps;
    private readonly IProductionLotRepository lots;
    private readonly ICalculationOperationRepository calcs;

    public OperationsPerformedDialog(IMaterialRepository materials, IOurEmployeeRepository emps, IProductionLotRepository lots, ICalculationOperationRepository calcs)
    {
        InitializeComponent();

        this.materials = materials;
        this.emps = emps;
        this.lots = lots;
        this.calcs = calcs;

        controls = Services.Provider.GetService<IControls<OperationsPerformed>>()!;
        controls.Container = Controls;

        controls
            .SetHeaderWidth(210)
            .SetEditorWidth(350)
            .AddDateTimePicker(x => x.DocumentDate, "Дата/время", date => date
                .SetCustomFormat("dd.MM.yyyy HH:mm:ss")
                .SetEditorWidth(200))
            .AddDirectorySelectBox<CalculationOperation>(x => x.OperationId, "Операция", select => select
                .RemoveEmptyFolders()
                .DirectoryChanged(OperationChanged))
            .AddTextBox(x => x.MaterialName, "Материал (по спецификации)", text => text.ReadOnly())
            .AddDirectorySelectBox<Material>(x => x.ReplacingMaterialId, "Использованный материал", select => select
                .SetDataSource(GetMaterials, DataRefreshMethod.Immediately))
            .AddDirectorySelectBox<OurEmployee>(x => x.EmployeeId, "Исполнитель", select => select
                .SetDataSource(GetOurEmployees, DataRefreshMethod.Immediately))
            .AddIntergerTextBox<long>(x => x.Quantity, "Количество", text => text.DefaultAsValue())
            .AddCheckBox(x => x.DoubleRate, "Двойная оплата", check => check.AllowThreeState())
            .Select(x => x.Quantity);
    }

    public bool Show(Guid lotId, Guid? operationId, Guid? empId)
    {
        this.lotId = lotId;

        var operation = controls.GetControl<IDirectorySelectBoxControl<CalculationOperation>>(x => x.OperationId);
        operation.SetDataSource(GetCalculationOperations, DataRefreshMethod.Immediately);

        controls.Initialize(new { OperationId = operationId, EmployeeId = empId });
        return ShowDialog() == DialogResult.OK;
    }

    public OperationsPerformed Get()
    {
        var obj = controls.Get();
        obj.OwnerId = lotId; 
        return obj;
    }

    public CalculationOperation? GetOperation() => controls.GetControl<IDirectorySelectBoxControl<CalculationOperation>>(x => x.OperationId).SelectedItem;

    public OurEmployee? GetEmployee() => controls.GetControl<IDirectorySelectBoxControl<OurEmployee>>(x => x.EmployeeId).SelectedItem;

    private void OperationChanged(CalculationOperation? newValue)
    {
        var using_material = controls.GetControl<ITextBoxControl>(x => x.MaterialName);
        var replacing_material = controls.GetControl<IDirectorySelectBoxControl<Material>>(x => x.ReplacingMaterialId);

        using_material.SetText(newValue?.MaterialName);
        replacing_material.SetEnabled(newValue?.MaterialId != null);
    }

    private IEnumerable<CalculationOperation>? GetCalculationOperations()
    {
        var lot = lots.Get(lotId);
        var op = calcs.GetListExisting(callback: query =>
            query
                .Select("calculation_operation.*")
                .Select("m.item_name as material_name")
                .LeftJoin("material as m", "m.id", "material_id")
                .Where("calculation_operation.owner_id", lot.CalculationId)
                .OrderBy("code")
            );

        return op;
    }

    private IEnumerable<Material> GetMaterials() => materials.GetListExisting();

    private IEnumerable<OurEmployee> GetOurEmployees() => emps.GetListExisting();

    private void ButtonOk_Click(object sender, EventArgs e)
    {
        if (GetEmployee() == null)
        {
            MessageBox.Show("Необходимо выбрать сотрудника.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DialogResult = DialogResult.None;
            return;
        }

        var operation = GetOperation();
        if (operation == null)
        {
            MessageBox.Show("Необходимо выбрать выполненную операцию.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DialogResult = DialogResult.None;
            return;
        }

        var q = controls.GetControl<IIntegerTextBoxControl<long>>(x => x.Quantity);
        if (q.NumericValue <= 0)
        {
            MessageBox.Show("Необходимо указать количество операций.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DialogResult = DialogResult.None;
            return;
        }

        var date = controls.GetControl<IDateTimePickerControl>(x => x.DocumentDate);
        var material = controls.GetControl<IDirectorySelectBoxControl<Material>>(x => x.ReplacingMaterialId);

        decimal materialCount = 0;
        if (operation.MaterialId != null)
        {
            if (material.SelectedItem != null)
            {
                materialCount = materials.GetRemainder(material.SelectedItem, date.Value);
            }
            else
            {
                materialCount = materials.GetRemainder(operation.MaterialId.Value, date.Value);
            }

            decimal expense = (q.NumericValue ?? 0) * operation.MaterialAmount;
            if (expense > materialCount)
            {
                MessageBox.Show($"Количества материала ({materialCount}) недостаточно для выполнения этой операции.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }
        }
    }
}
