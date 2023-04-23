//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.08.2022
//
// Версия 2022.12.3
//  - в функции AsSummary заменён параметр includeDeleted имеющий значение
//    true на options равный SelectOptions.All
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
//
//-----------------------------------------------------------------------

using DocumentFlow.Dialogs.Infrastructure;
using DocumentFlow.Entities.Wages.Core;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Controls;
using DocumentFlow.Infrastructure.Data;

using Microsoft.Extensions.DependencyInjection;

using Syncfusion.WinForms.DataGrid.Enums;

namespace DocumentFlow.Entities.Wages;

public abstract class BasePayrollEditor<T, P, E> : BillingDocumentEditor<T>
    where T : BasePayroll, new()
    where P : WageEmployee, new()
    where E : IPayrollEmployeeRepository<P>
{
    public BasePayrollEditor(IDocumentRepository<T> repository, IPageManager pageManager) : base(repository, pageManager) 
    {
        var repo = Services.Provider.GetService<E>();

        EditorControls
            .AddDataGrid<P>(grid =>
                grid
                    .SetRepository<E>()
                    .GridSummaryRow(VerticalPosition.Bottom, row =>
                        row
                            .AsSummary(x => x.Wage, SummaryColumnFormat.Currency, SelectOptions.All))
                    .Dialog<IWageEmployeeDialog<P>>()
                    .AddCommand("Заполнить", Properties.Resources.icons8_incoming_data_16, PopulateDataGrid)
                    .SetDock(DockStyle.Fill));
    }

    protected abstract void PopulateDataGrid(IDataGridControl<P> grid);
}