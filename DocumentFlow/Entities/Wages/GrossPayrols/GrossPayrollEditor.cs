//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Controls;

namespace DocumentFlow.Entities.Wages;

public class GrossPayrollEditor : BasePayrollEditor<GrossPayroll, GrossPayrollEmployee, IGrossPayrollEmployeeRepository>, IGrossPayrollEditor
{
    private readonly IGrossPayrollRepository repository;

    public GrossPayrollEditor(IGrossPayrollRepository repository, IPageManager pageManager) : base(repository, pageManager) 
    {
        this.repository = repository;
    }

    protected override void PopulateDataGrid(IDataGridControl<GrossPayrollEmployee> grid)
    {
        if (MessageBox.Show("Перед заполнением таблица будет очищена. Продолжить?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
        {
            return;
        }

        string question1 = "Документ не записан, для заполнения таблицы документ должен быть записан. Записать?";
        string question2 = "Для заполнения таблицы документ должен быть записан. Записать?";

        string question = Document.Id == default ?
            question1 :
            (Document.BillingYear != BillingYear || Document.BillingMonth != BilingMonth ?
                question2 :
                string.Empty);

        if (!string.IsNullOrEmpty(question))
        {
            if (MessageBox.Show(question, "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            if (!Save(true))
            {
                return;
            }
        }

        repository.CalculateEmployeeWages(Document.Id);

        if (grid is IDataSourceControl source)
        {
            source.RefreshDataSource();
        }
    }
}