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
        var calculation_name = new DfTextBox("calculation_name", "Калькуляция", headerWidth, 600) { Enabled = false };
        var code = new DfTextBox("code", "Код", headerWidth, 150) { DefaultAsNull = false };
        var name = new DfTextBox("item_name", "Наименование", headerWidth, 400);
        var operation = new DfDirectorySelectBox<Operation>("item_id", "Операция", headerWidth, 400);
        var price = new DfNumericTextBox("price", "Расценка", headerWidth, 100) { DefaultAsNull = false, Enabled = false, NumberDecimalDigits = 4 };
        var equipment = new DfDirectorySelectBox<Equipment>("equipment_id", "Оборудование", headerWidth, 300);
        var tools = new DfDirectorySelectBox<Equipment>("tools_id", "Инструмент", headerWidth, 300);
        var material = new DfDirectorySelectBox<Material>("material_id", "Материал", headerWidth, 300) { RootIdentifier = RootId };
        var amount = new DfNumericTextBox("material_amount", "Количество", headerWidth, 100) { DefaultAsNull = false, NumberDecimalDigits = 3 };
        var repeats = new DfIntegerTextBox<int>("repeats", "Кол-во повторов", headerWidth, 100) { DefaultAsNull = false };

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
                price.Value = e.NewValue.salary;
                if (e.NewValue is Cutting cutting)
                {
                    amount.Value = cutting.segment_length / 1000m;
                }
            }
        };

        equipment.SetDataSource(() => 
            Services.Provider
                .GetService<IEquipmentRepository>()!
                .GetAllValid(callback: q => q.WhereFalse("is_tools")));
        
        tools.SetDataSource(() => 
            Services.Provider
                .GetService<IEquipmentRepository>()!
                .GetAllValid(callback: q => q.WhereTrue("is_tools")));

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
            calculation_name,
            code,
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
            var prev = new DfMultiSelectionComboBox("previous_operation", "Пред. операции", headerWidth, 500);
            prev.SetDataSource(() =>
            {
                var repo = Services.Provider.GetService<ICalculationOperationRepository>();
                return repo!.GetAllValid(callback: q =>
                {
                    return q
                        .Select("id", "code")
                        .SelectRaw("code || ', ' || item_name as item_name")
                        .Where("owner_id", Document.owner_id)
                        .WhereNotNull("item_name")
                        .Where("code", "!=", Document.code)
                        .OrderBy("code");
                });
            });

            controls.Add(prev);

            var p = new DfOperationProperty("Доп. характеристики", headerWidth, 500);
            controls.Add(p);
        }

        var note = new DfTextBox("note", "Комментарий", headerWidth, 500) { Multiline = true, Height = 75 };
        controls.Add(note);

        AddControls(controls);
    }

    protected virtual Guid? RootId { get; } = null;

    protected virtual bool IsCuttingOperation { get; } = false;
}