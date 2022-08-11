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
        var contract_name = new DfTextBox("contract_name", "Договор", headerWidth, 400) { Enabled = false };
        var code = new DfTextBox("code", "Номер приложения", headerWidth, 200) { DefaultAsNull = false };
        var name = new DfTextBox("item_name", "Наименование", headerWidth, 400);
        var date = new DfDateTimePicker("document_date", "Дата подписания", headerWidth, 200) { Format = DateTimePickerFormat.Short };
        var date_start = new DfDateTimePicker("date_start", "Начало действия", headerWidth, 200) { Format = DateTimePickerFormat.Short };
        var date_end = new DfDateTimePicker("date_end", "Окончание действия", headerWidth, 200) { Required = false, Format = DateTimePickerFormat.Short };
        var grid = new DfDataGrid<PriceApproval>(Services.Provider.GetService<IPriceApprovalRepository>()!) { Dock = DockStyle.Fill };

        grid.AutoGeneratingColumn += (sender, args) =>
        {
            switch (args.Column.MappingName)
            {
                case "id":
                case "owner_id":
                    args.Cancel = true;
                    break;
                case "product_name":
                    args.Column.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
                    args.Column.HeaderText = "Материал / Изделие";
                    break;
                case "measurement_name":
                    args.Column.Width = 100;
                    break;
                case "price":
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
            contract_name,
            code,
            name,
            date,
            date_start,
            date_end,
            grid
        });

        RegisterReport(new ContractApplicationReport());
    }

}