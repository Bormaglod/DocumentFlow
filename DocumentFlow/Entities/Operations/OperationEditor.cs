//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 11.01.2022
//
// Версия 2023.2.4
//  - добавлен поле "Дата нормирования"
// Версия 2023.2.6
//  - добавлено поле goods
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Editors;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Entities.Operations.Dialogs;
using DocumentFlow.Entities.OperationTypes;
using DocumentFlow.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

using Syncfusion.WinForms.DataGrid.Enums;

namespace DocumentFlow.Entities.Operations;

public class OperationEditor : Editor<Operation>, IOperationEditor
{
    private const int headerWidth = 180;

    public OperationEditor(IOperationRepository repository, IPageManager pageManager) : base(repository, pageManager)
    {
        var goods = new DfDataGrid<OperationGoods>(Services.Provider.GetService<IOperationGoodsRepository>()!) 
        { 
            Dock = DockStyle.Fill,
            Header = "Операция будет использоваться только при производстве этих изделий"
        };

        goods.AutoGeneratingColumn += (sender, args) =>
        {
            switch (args.Column.MappingName)
            {
                case "GoodsCode":
                    args.Column.Width = 150;
                    break;
                case "GoodsName":
                    args.Column.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
                    break;
                default:
                    args.Cancel = true;
                    break;
            }
        };

        goods.DataCreate += (sender, args) =>
        {
            args.Cancel = GoodsSelectForm.Create(args.CreatingData) == DialogResult.Cancel;
        };

        goods.DataEdit += (sender, args) =>
        {
            args.Cancel = GoodsSelectForm.Edit(args.EditingData) == DialogResult.Cancel;
        };

        goods.DataCopy += (sender, args) =>
        {
            args.Cancel = GoodsSelectForm.Edit(args.CopiedData) == DialogResult.Cancel;
        };

        AddControls(new Control[]
        {
            CreateTextBox(x => x.Code, "Код", headerWidth, 100, defaultAsNull: false),
            CreateTextBox(x => x.ItemName, "Наименование", headerWidth, 400),
            CreateComboBox(x => x.TypeId, "Тип операции", headerWidth, 250, data: GetTypes),
            CreateDirectorySelectBox(x => x.ParentId, "Группа", headerWidth, 400, showOnlyFolder: true, data: repository.GetOnlyFolders),
            CreateIntegerTextBox<int>(x => x.Produced, "Выработка", headerWidth, 100, defaultAsNull: false),
            CreateIntegerTextBox<int>(x => x.ProdTime, "Время выработки, сек.", headerWidth, 100, defaultAsNull: false),
            CreateIntegerTextBox<int>(x => x.ProductionRate, "Норма выработка, ед./час", headerWidth, 100, defaultAsNull: false, enabled: false),
            CreateDateTimePicker(x => x.DateNorm, "Дата нормирования", headerWidth, 150, required: false),
            CreateNumericTextBox(x => x.Salary, "Зарплата, руб.", headerWidth, 100, defaultAsNull: false, enabled: false, digits: 4),
            goods
        });
    }

    protected override void RegisterNestedBrowsers()
    {
        base.RegisterNestedBrowsers();
        RegisterNestedBrowser<IOperationUsageBrowser, OperationUsage>();
    }

    private IEnumerable<OperationType> GetTypes() => Services.Provider.GetService<IOperationTypeRepository>()!.GetAllValid(callback: q => q.OrderBy("item_name"));
}