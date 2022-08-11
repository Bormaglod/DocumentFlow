//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 11.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Editors;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Infrastructure;

namespace DocumentFlow.Entities.OperationTypes;

public class OperationTypeEditor : Editor<OperationType>, IOperationTypeEditor
{
    private const int headerWidth = 140;
    private readonly DfCurrencyTextBox salary;

    public OperationTypeEditor(IOperationTypeRepository repository, IPageManager pageManager) : base(repository, pageManager) 
    {
        var name = new DfTextBox("item_name", "Наименование", headerWidth, 400);
        salary = new DfCurrencyTextBox("salary", "Расценка, руб./час", headerWidth, 200) { DefaultAsNull = false };

        AddControls(new Control[] { name, salary });
    }

    protected override bool ConfirmSaveDocument()
    {
        if (salary.NumericValue != Document.salary)
        {
            return MessageBox.Show("Изменение расценки приведет к пересчёту значений расценок производственных операций и необходимости пересчитать калькуляции. Продолжить?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
        }

        return base.ConfirmSaveDocument();
    }
}