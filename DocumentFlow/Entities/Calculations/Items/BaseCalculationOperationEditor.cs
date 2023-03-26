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

using DocumentFlow.Controls.Editors;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Entities.Equipments;
using DocumentFlow.Entities.Operations;
using DocumentFlow.Entities.Products;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Data;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Calculations;

public class BaseCalculationOperationEditor<T> : Editor<T>
    where T : CalculationOperation, IDocumentInfo, new()
{
    private const int headerWidth = 170;

    public BaseCalculationOperationEditor(IRepository<Guid, T> repository, IPageManager pageManager) : base(repository, pageManager)
    {
        var name = CreateTextBox(x => x.ItemName, "Наименование", headerWidth, 400);
        var operation = CreateDirectorySelectBox<Operation>(x => x.ItemId, "Операция", headerWidth, 400);
        var price = CreateNumericTextBox(x => x.Price, "Расценка", headerWidth, 100, defaultAsNull: false, enabled: false, digits: 4);
        var equipment = CreateDirectorySelectBox<Equipment>(x => x.EquipmentId, "Оборудование", headerWidth, 300, data: GetEquipments);
        var tools = CreateDirectorySelectBox<Equipment>(x => x.ToolsId, "Инструмент", headerWidth, 300, data: GetTools);
        var material = CreateDirectorySelectBox<Material>(x => x.MaterialId, "Материал", headerWidth, 300, rootIdentifier: RootId);
        var amount = CreateNumericTextBox(x => x.MaterialAmount, "Количество", headerWidth, 100, defaultAsNull: false, digits: 3);
        var repeats = CreateIntegerTextBox<int>(x => x.Repeats, "Кол-во повторов", headerWidth, 100, defaultAsNull: false);

        if (IsCuttingOperation)
        {
            name.Enabled = false;
            amount.Enabled = false;
        }

        repeats.NumericValue = 1;

        operation.SetDataSource(() =>
        {
            if (IsCuttingOperation)
            {
                return Services.Provider.GetService<ICuttingRepository>()!.GetAllValid(callback: q => q.OrderBy("segment_length"));
            }
            else
            {
                return Services.Provider.GetService<IOperationRepository>()!.GetAllValid(callback: q => q.OrderBy("item_name"));
            }
        });

        operation.ManualValueChange += (s, e) =>
        {
            if (e.NewValue != null)
            {
                price.Value = e.NewValue.Salary;
                if (e.NewValue is Cutting cutting)
                {
                    amount.Value = cutting.SegmentLength / 1000m;
                }
            }
        };

        material.SetDataSource(() =>
        {
            if (IsCuttingOperation)
            {
                return Services.Provider.GetService<IMaterialRepository>()!.GetWires();
            }
            else
            {
                return Services.Provider.GetService<IMaterialRepository>()!.GetMaterials();
            }
        });

        var controls = new List<Control>()
        {
            CreateTextBox(x => x.CalculationName, "Калькуляция", headerWidth, 600, enabled: false),
            CreateTextBox(x => x.Code, "Код", headerWidth, 150, defaultAsNull: false),
            name,
            operation,
            price,
            equipment,
            tools,
            material,
            amount,
            repeats
        };

        if (!IsCuttingOperation)
        {
            var prev = CreateMultiSelectionComboBox(x => x.PreviousOperation, "Пред. операции", headerWidth, 500);
            prev.SetDataSource(() =>
            {
                var repo = Services.Provider.GetService<ICalculationOperationRepository>();
                return repo!.GetAllValid(callback: q =>
                {
                    return q
                        .Select("id", "code")
                        .SelectRaw("code || ', ' || item_name as item_name")
                        .Where("owner_id", Document.OwnerId)
                        .WhereNotNull("item_name")
                        .Where("code", "!=", Document.Code)
                        .OrderBy("code");
                });
            });

            controls.Add(prev);

            var p = new DfOperationProperty("Доп. характеристики", headerWidth, 500);
            controls.Add(p);
        }

        var note = CreateMultilineTextBox(x => x.Note, "Комментарий", headerWidth, 500);
        controls.Add(note);

        AddControls(controls);
    }

    protected virtual Guid? RootId { get; } = null;

    protected virtual bool IsCuttingOperation { get; } = false;

    private IEnumerable<Equipment> GetEquipments() => Services.Provider.GetService<IEquipmentRepository>()!.GetAllValid(callback: q => q.WhereFalse("is_tools"));

    private IEnumerable<Equipment> GetTools() => Services.Provider.GetService<IEquipmentRepository>()!.GetAllValid(callback: q => q.WhereTrue("is_tools"));
}