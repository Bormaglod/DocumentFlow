//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Wages;

public class GrossPayrollEditor : BasePayrollEditor<GrossPayroll, GrossPayrollEmployee, IGrossPayrollEmployeeRepository>, IGrossPayrollEditor
{
    public GrossPayrollEditor(IGrossPayrollRepository repository, IPageManager pageManager) : base(repository, pageManager) 
    {
        EmployeeRows.AddCommand("Заполнить", Properties.Resources.icons8_incoming_data_16, (sender, e) =>
        {
            if (MessageBox.Show("Перед заполнением таблица будет очищена. Продолжить?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }

            if (Document.id == default)
            {
                if (MessageBox.Show("Документ не записан, для заполнения таблицы документ должен быть записан. Записать?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

                if (!Save(true))
                {
                    return;
                }
            }

            var repo = Services.Provider.GetService<IGrossPayrollRepository>();
            repo!.CalculateEmployeeWages(Document.id, BillingYear, BilingMonth);

            EmployeeRows.RefreshDataSource();
        });
    }
}