﻿//-----------------------------------------------------------------------
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
        var code = new DfTextBox("code", "Код", headerWidth, 100) { DefaultAsNull = false };
        var name = new DfTextBox("item_name", "Наименование", headerWidth, 400);
        var type = new DfComboBox<OperationType>("type_id", "Тип операции", headerWidth, 250);
        var parent = new DfDirectorySelectBox<Operation>("parent_id", "Группа", headerWidth, 400) { ShowOnlyFolder = true };
        var produced = new DfIntegerTextBox<int>("produced", "Выработка", headerWidth, 100) { DefaultAsNull = false };
        var prod_time = new DfIntegerTextBox<int>("prod_time", "Время выработки, сек.", headerWidth, 100) { DefaultAsNull = false };
        var production_rate = new DfIntegerTextBox<int>("production_rate", "Норма выработка, ед./час", headerWidth, 100) { DefaultAsNull = false, Enabled = false };
        var salary = new DfNumericTextBox("salary", "Зарплата, руб.", headerWidth, 100) { DefaultAsNull = false, Enabled = false, NumberDecimalDigits = 4 };
        var date_norm = new DfDateTimePicker("date_norm", "Дата нормирования", headerWidth, 150) { Required = false };
        var goods = new DfDataGrid<OperationGoods>(Services.Provider.GetService<IOperationGoodsRepository>()!) 
        { 
            Dock = DockStyle.Fill,
            Header = "Операция будет использовоться только при производстве этих изделий"
        };

        type.SetDataSource(() => Services.Provider.GetService<IOperationTypeRepository>()!.GetAllValid(callback: q => q.OrderBy("item_name")));
        parent.SetDataSource(repository.GetOnlyFolders);

        goods.AutoGeneratingColumn += (sender, args) =>
        {
            switch (args.Column.MappingName)
            {
                case "goods_code":
                    args.Column.Width = 150;
                    break;
                case "goods_name":
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
            code,
            name,
            type,
            parent,
            produced,
            prod_time,
            production_rate,
            date_norm,
            salary,
            goods
        });
    }

    protected override void RegisterNestedBrowsers()
    {
        base.RegisterNestedBrowsers();
        RegisterNestedBrowser<IOperationUsageBrowser, OperationUsage>();
    }
}