//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 11.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Controls;

namespace DocumentFlow.Entities.OperationTypes;

public class OperationTypeEditor : Editor<OperationType>, IOperationTypeEditor
{
    private const int headerWidth = 140;

    public OperationTypeEditor(IOperationTypeRepository repository, IPageManager pageManager) : base(repository, pageManager) 
    {
        EditorControls
            .AddTextBox(x => x.ItemName, "Наименование", text =>
                text
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(400))
            .AddCurrencyTextBox(x => x.Salary, "Расценка, руб./час", text =>
                text
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(200)
                    .DefaultAsValue());
    }

    protected override bool ConfirmSaveDocument()
    {
        if (EditorControls.GetControl<ICurrencyTextBoxControl>(x => x.Salary).NumericValue != Document.Salary)
        {
            return MessageBox.Show("Изменение расценки приведет к пересчёту значений расценок производственных операций и необходимости пересчитать калькуляции. Продолжить?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
        }

        return base.ConfirmSaveDocument();
    }
}