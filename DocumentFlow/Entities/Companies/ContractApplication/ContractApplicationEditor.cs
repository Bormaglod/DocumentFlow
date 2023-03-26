//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 16.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Editors;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

namespace DocumentFlow.Entities.Companies;

public class ContractApplicationEditor : Editor<ContractApplication>, IContractApplicationEditor
{
    private const int headerWidth = 170;

    public ContractApplicationEditor(IContractApplicationRepository repository, IPageManager pageManager) : base(repository, pageManager) 
    {
        var grid = new DfDataGrid<PriceApproval>(Services.Provider.GetService<IPriceApprovalRepository>()!) { Dock = DockStyle.Fill };

        grid.AutoGeneratingColumn += (sender, args) =>
        {
            switch (args.Column.MappingName)
            {
                case "Id":
                case "OwnerId":
                    args.Cancel = true;
                    break;
                case "ProductName":
                    args.Column.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
                    args.Column.HeaderText = "Материал / Изделие";
                    break;
                case "MeasurementName":
                    args.Column.Width = 100;
                    break;
                case "Price":
                    if (args.Column is GridNumericColumn c)
                    {
                        c.FormatMode = Syncfusion.WinForms.Input.Enums.FormatMode.Currency;
                        c.Width = 100;
                        c.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
                    }
                    
                    break;
            }
        };

        grid.DataCreate += (sender, args) =>
        {
            args.Cancel = FormPriceApproval.Create(args.CreatingData) == DialogResult.Cancel;
        };

        grid.DataEdit += (sender, args) =>
        {
            args.Cancel = FormPriceApproval.Edit(args.EditingData) == DialogResult.Cancel;
        };

        grid.DataCopy += (sender, args) =>
        {
            args.Cancel = FormPriceApproval.Edit(args.CopiedData) == DialogResult.Cancel;
        };

        AddControls(new Control[]
        {
            CreateTextBox(x => x.ContractName, "Договор", headerWidth, 400, enabled: false),
            CreateTextBox(x => x.Code, "Номер приложения", headerWidth, 200, defaultAsNull: false),
            CreateTextBox(x => x.ItemName, "Наименование", headerWidth, 400),
            CreateDateTimePicker(x => x.DocumentDate, "Дата подписания", headerWidth, 200, format: DateTimePickerFormat.Short),
            CreateDateTimePicker(x => x.DateStart, "Начало действия", headerWidth, 200, format: DateTimePickerFormat.Short),
            CreateDateTimePicker(x => x.DateEnd, "Окончание действия", headerWidth, 200, required: false, format: DateTimePickerFormat.Short),
            grid
        });

        RegisterReport(new ContractApplicationReport());
    }

}