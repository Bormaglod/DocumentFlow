//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.01.2022
//
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Entities.Equipments;
using DocumentFlow.Entities.Operations;
using DocumentFlow.Entities.Products;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Controls;
using DocumentFlow.Infrastructure.Data;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Calculations;

public class BaseCalculationOperationEditor<T> : Editor<T>
    where T : CalculationOperation, IDocumentInfo, new()
{
    private const int headerWidth = 170;

    public BaseCalculationOperationEditor(IRepository<Guid, T> repository, IPageManager pageManager) : base(repository, pageManager)
    {
        EditorControls
            .AddTextBox(x => x.CalculationName, "Калькуляция", text =>
                text
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(600)
                    .Disable())
            .AddTextBox(x => x.Code, "Код", text =>
                text
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(150)
                    .DefaultAsValue())
            .AddTextBox(x => x.ItemName, "Наименование", text =>
                text
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(400)
                    .If(IsCuttingOperation, x => x.Disable()))
            .AddDirectorySelectBox<Operation>(x => x.ItemId, "Операция", select =>
                select
                    .SetDataSource(GetOperations)
                    .DirectorySelected(OperationSelected)
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(400))
            .AddNumericTextBox(x => x.Price, "Расценка", box =>
                box
                    .SetNumberDecimalDigits(4)
                    .SetHeaderWidth(headerWidth)
                    .DefaultAsValue()
                    .Disable())
            .AddDirectorySelectBox<Equipment>(x => x.EquipmentId, "Оборудование", select =>
                select
                    .SetDataSource(GetEquipments)
                    .EnableEditor<IEquipmentEditor>()
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(300))
            .AddDirectorySelectBox<Equipment>(x => x.ToolsId, "Инструмент", select =>
                select
                    .SetDataSource(GetTools)
                    .EnableEditor<IEquipmentEditor>()
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(300))
            .AddDirectorySelectBox<Material>(x => x.MaterialId, "Материал", select =>
                select
                    .SetRootIdentifier(RootId)
                    .SetDataSource(GetMaterials)
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(300))
            .AddNumericTextBox(x => x.MaterialAmount, "Количество", box =>
                box
                    .SetNumberDecimalDigits(3)
                    .SetHeaderWidth(headerWidth)
                    .DefaultAsValue())
            .AddIntergerTextBox<int>(x => x.Repeats, "Кол-во повторов", box =>
                box
                    .Initial(1)
                    .SetHeaderWidth(headerWidth)
                    .DefaultAsValue()
                    .If(IsCuttingOperation, x => x.Disable()))
            .If(!IsCuttingOperation, controls => 
                controls
                    .AddMultiSelectionComboBox(x => x.PreviousOperation, "Пред. операции", combo =>
                        combo
                            .SetDataSource(GetCalcOperations)
                            .SetHeaderWidth(headerWidth)
                            .SetEditorWidth(500))
                    .AddOperationProperty("Доп. характеристики", prop =>
                        prop
                            .SetHeaderWidth(headerWidth)
                            .SetEditorWidth(500)))
            .AddTextBox(x => x.Note, "Комментарий", text =>
                text
                    .Multiline()
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(500));
    }

    protected virtual Guid? RootId { get; } = null;

    protected virtual bool IsCuttingOperation { get; } = false;

    private IEnumerable<CalculationOperation> GetCalcOperations()
    {
        var repo = Services.Provider.GetService<ICalculationOperationRepository>();
        return repo!.GetListExisting(callback: q =>
        {
            return q
                .Select("id", "code")
                .SelectRaw("code || ', ' || item_name as item_name")
                .Where("owner_id", Document.OwnerId)
                .WhereNotNull("item_name")
                .Where("code", "!=", Document.Code)
                .OrderBy("code");
        });
    }

    private IEnumerable<Operation> GetOperations()
    {
        if (IsCuttingOperation)
        {
            return Services.Provider.GetService<ICuttingRepository>()!.GetListExisting(callback: q => q.OrderBy("segment_length"));
        }
        else
        {
            return Services.Provider.GetService<IOperationRepository>()!.GetListExisting(callback: q => q.OrderBy("item_name"));
        }
    }

    private IEnumerable<Material> GetMaterials()
    {
        if (IsCuttingOperation)
        {
            return Services.Provider.GetService<IMaterialRepository>()!.GetWires();
        }
        else
        {
            return Services.Provider.GetService<IMaterialRepository>()!.GetMaterials();
        }
    }

    private IEnumerable<Equipment> GetEquipments() => Services.Provider.GetService<IEquipmentRepository>()!.GetListExisting(callback: q => q.WhereFalse("is_tools"));

    private IEnumerable<Equipment> GetTools() => Services.Provider.GetService<IEquipmentRepository>()!.GetListExisting(callback: q => q.WhereTrue("is_tools"));

    private void OperationSelected(Operation? newValue)
    {
        if (newValue != null)
        {
            EditorControls.GetControl<INumericTextBoxControl>("Price").NumericValue = newValue.Salary;
            if (newValue is Cutting cutting)
            {
                EditorControls.GetControl<INumericTextBoxControl>("MaterialAmount").NumericValue = cutting.SegmentLength / 1000m;
            }
        }
    }
}