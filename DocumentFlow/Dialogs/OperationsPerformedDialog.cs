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

using DocumentFlow.Entities.Calculations;
using DocumentFlow.Entities.Employees;
using DocumentFlow.Entities.Productions.Lot;
using DocumentFlow.Entities.Productions.Performed;
using DocumentFlow.Entities.Products;
using DocumentFlow.Infrastructure.Controls;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Dialogs;

public partial class OperationsPerformedDialog : Form
{
    private const int headerWidth = 210;

    private readonly Guid lotId;
    private readonly IControls<OperationsPerformed> controls;

    public OperationsPerformedDialog(Guid lotId)
    {
        InitializeComponent();

        this.lotId = lotId;

        controls = Services.Provider.GetService<IControls<OperationsPerformed>>()!;
        controls.Container = Controls;

        controls
            .AddDateTimePicker(x => x.DocumentDate, "Дата/время", date =>
                date
                    .SetCustomFormat("dd.MM.yyyy HH:mm:ss")
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(200))
            .AddDirectorySelectBox<CalculationOperation>(x => x.OperationId, "Операция", select =>
                select
                    .RemoveEmptyFolders()
                    .DirectoryChanged(OperationChanged)
                    .SetDataSource(GetCalculationOperations, DataRefreshMethod.Immediately)
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(350))
            .AddTextBox(x => x.MaterialName, "Материал (по спецификации)", text =>
                text
                    .ReadOnly()
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(350))
            .AddDirectorySelectBox<Material>(x => x.ReplacingMaterialId, "Использованный материал", select =>
                select
                    .SetDataSource(GetMaterials, DataRefreshMethod.Immediately)
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(350))
            .AddDirectorySelectBox<OurEmployee>(x => x.EmployeeId, "Исполнитель", select =>
                select
                    .SetDataSource(GetOurEmployees, DataRefreshMethod.Immediately)
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(350))
            .AddIntergerTextBox<long>(x => x.Quantity, "Количество", text =>
                text
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(150))
            .AddCheckBox(x => x.DoubleRate, "Двойная оплата", check =>
                check
                    .AllowThreeState()
                    .SetHeaderWidth(headerWidth))
            .Select(x => x.Quantity);
    }

    public void Initialize(object value) => controls.Initialize(value);

    public OperationsPerformed Get()
    {
        var obj = controls.Get();
        obj.OwnerId = lotId; 
        return obj;
    }

    public CalculationOperation? Operation => controls.GetControl<IDirectorySelectBoxControl<CalculationOperation>>(x => x.OperationId).SelectedItem;

    public OurEmployee? Employee => controls.GetControl<IDirectorySelectBoxControl<OurEmployee>>(x => x.EmployeeId).SelectedItem;

    private void OperationChanged(CalculationOperation? newValue)
    {
        var using_material = controls.GetControl<ITextBoxControl>(x => x.MaterialName);
        var replacing_material = controls.GetControl<IDirectorySelectBoxControl<Material>>(x => x.ReplacingMaterialId);

        using_material.SetText(newValue?.MaterialName);
        replacing_material.SetEnabled(newValue?.MaterialId != null);
    }

    private IEnumerable<CalculationOperation>? GetCalculationOperations()
    {
        var repoLot = Services.Provider.GetService<IProductionLotRepository>();
        if (repoLot == null)
        {
            return null;
        }

        var lot = repoLot.Get(lotId);

        var repo = Services.Provider.GetService<ICalculationOperationRepository>();
        if (repo == null)
        {
            return null;
        }

        var op = repo.GetListExisting(callback: query =>
            query
                .Select("calculation_operation.*")
                .Select("m.item_name as material_name")
                .LeftJoin("material as m", "m.id", "material_id")
                .Where("calculation_operation.owner_id", lot.CalculationId)
                .OrderBy("code")
            );

        return op;
    }

    private IEnumerable<Material> GetMaterials() => Services.Provider.GetService<IMaterialRepository>()!.GetListExisting();

    private IEnumerable<OurEmployee> GetOurEmployees() => Services.Provider.GetService<IOurEmployeeRepository>()!.GetListExisting();

    private void ButtonOk_Click(object sender, EventArgs e)
    {
        if (Employee == null)
        {
            MessageBox.Show("Необходимо выбрать сотрудника", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DialogResult = DialogResult.None;
        }

        if (Operation == null)
        {
            MessageBox.Show("Необходимо выбрать выполненную операцию", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DialogResult = DialogResult.None;
        }
    }
}
