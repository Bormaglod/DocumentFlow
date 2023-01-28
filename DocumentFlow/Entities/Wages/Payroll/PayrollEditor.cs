//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.01.2023
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Editors;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Entities.Wages.Dialogs;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Controls;

using Microsoft.Extensions.DependencyInjection;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

namespace DocumentFlow.Entities.Wages;

public class PayrollEditor : DocumentEditor<Payroll>, IPayrollEditor
{
    private readonly DfDataGrid<PayrollEmployee> details;

    public PayrollEditor(IPayrollRepository repository, IPageManager pageManager) : base(repository, pageManager, true) 
    {
        var gross = new DfDocumentSelectBox<GrossPayroll>("owner_id", "Начисление зар. платы", 150, 400)
        {
            OpenAction = (t) => pageManager.ShowEditor<IGrossPayrollEditor, GrossPayroll>(t)
        };

        details = new DfDataGrid<PayrollEmployee>(Services.Provider.GetService<IPayrollEmployeeRepository>()!) { Dock = DockStyle.Fill };

        gross.SetDataSource(() =>
        {
            var repo = Services.Provider.GetService<IGrossPayrollRepository>();
            return repo?.GetAllDefault();
        });

        gross.Columns += (sender, e) => BasePayroll.CreateGridColumns(e.Columns);

        gross.ManualValueChange += (sender, e) =>
        {
            if (e.NewValue == null)
            {
                return;
            }

            if (MessageBox.Show("Заполнить таблицу по данным начисления?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                FillEmployeeList(e.NewValue);
            }
        };

        details.CreateTableSummaryRow(VerticalPosition.Bottom)
            .AsSummary("wage", SummaryColumnFormat.Currency, SelectOptions.All);

        details.AutoGeneratingColumn += (sender, args) =>
         {
             switch (args.Column.MappingName)
             {
                 case "employee_name":
                     args.Column.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
                     break;
                 case "income_item_name":
                     args.Column.Width = 250;
                     break;
                 case "wage":
                     if (args.Column is GridNumericColumn c)
                     {
                         c.FormatMode = Syncfusion.WinForms.Input.Enums.FormatMode.Currency;
                     }

                     args.Column.Width = 150;
                     args.Column.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;

                     break;
             }
         };

        details.DataCreate += (sender, args) =>
        {
            args.Cancel = FormEmployeePayroll.Create(args.CreatingData) == DialogResult.Cancel;
        };

        details.DataEdit += (sender, args) =>
        {
            args.Cancel = FormEmployeePayroll.Edit(args.EditingData) == DialogResult.Cancel;
        };

        details.DataCopy += (sender, args) =>
        {
            args.Cancel = FormEmployeePayroll.Edit(args.CopiedData) == DialogResult.Cancel;
        };

        details.AddCommand("Заполнить", Properties.Resources.icons8_incoming_data_16, (sender, e) =>
        {
            if (gross.SelectedItem != null)
            {
                if (MessageBox.Show("Перед заполнением таблица будет очищена. Продолжить?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    return;
                }

                FillEmployeeList(gross.SelectedItem);
            }
        });

        AddControls(new Control[]
        {
            gross,
            details
        });
    }

    private void FillEmployeeList(GrossPayroll gross)
    {
        var repo = Services.Provider.GetService<IGrossPayrollEmployeeRepository>();
        if (repo != null)
        {
            var employees = repo.GetByOwner(gross.id).Select(x => new PayrollEmployee(Document, x));
            details.Fill(employees);
        }
    }
}