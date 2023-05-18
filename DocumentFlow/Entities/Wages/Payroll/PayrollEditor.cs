//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.01.2023
//
// Версия 2023.2.11
//  - в методе FillEmployeeList вызов GetByOwner заменен на GetSummaryWage
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Dialogs.Infrastructure;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Controls;

using Microsoft.Extensions.DependencyInjection;

using Syncfusion.WinForms.DataGrid.Enums;

namespace DocumentFlow.Entities.Wages;

public class PayrollEditor : DocumentEditor<Payroll>, IPayrollEditor
{
    public PayrollEditor(IPayrollRepository repository, IPageManager pageManager) : base(repository, pageManager, true) 
    {
        EditorControls
            .AddDocumentSelectBox<GrossPayroll>(x => x.OwnerId, "Начисление зар. платы", select =>
                select
                    .SetDataSource(GetGrosses)
                    .EnableEditor<IGrossPayrollEditor>()
                    .CreateColumns(BasePayroll.CreateGridColumns)
                    .DocumentSelected(GrossPayrollSelected)
                    .SetHeaderWidth(150)
                    .SetEditorWidth(400))
            .AddDataGrid<PayrollEmployee>(grid => 
                grid
                    .SetRepository<IPayrollEmployeeRepository>()
                    .Dialog<IWageEmployeeDialog<PayrollEmployee>>()
                    .GridSummaryRow(VerticalPosition.Bottom, row =>
                        row
                            .AsSummary(x => x.Wage, SummaryColumnFormat.Currency, SelectOptions.All))
                    .AddCommand("Заполнить", Properties.Resources.icons8_incoming_data_16, PopulateCommand)
                    .SetDock(DockStyle.Fill));
    }

    private void PopulateCommand(IDataGridControl<PayrollEmployee> grid)
    {
        var gross = EditorControls.GetControl<IDocumentSelectBoxControl<GrossPayroll>>();
        if (gross.SelectedItem != null)
        {
            if (MessageBox.Show("Перед заполнением таблица будет очищена. Продолжить?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }

            FillEmployeeList(gross.SelectedItem, grid);
        }
    }

    private void FillEmployeeList(GrossPayroll gross, IDataGridControl<PayrollEmployee>? grid = null)
    {
        var repo = Services.Provider.GetService<IGrossPayrollEmployeeRepository>();
        if (repo != null)
        {
            var employees = repo.GetSummaryWage(gross)
                .Select(x => new PayrollEmployee(Document, x));
            grid ??= EditorControls.GetControl<IDataGridControl<PayrollEmployee>>();
            grid.Fill(employees);
        }
    }

    private void GrossPayrollSelected(GrossPayroll? newValue)
    {
        if (newValue == null)
        {
            return;
        }

        if (MessageBox.Show("Заполнить таблицу по данным начисления?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        {
            FillEmployeeList(newValue);
        }
    }

    private IEnumerable<GrossPayroll> GetGrosses() => Services.Provider.GetService<IGrossPayrollRepository>()!.GetListUserDefined();
}