//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 16.01.2022
//
// Версия 2023.5.21
//  - добавлено поле Note
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Dialogs.Infrastructure;
using DocumentFlow.Infrastructure;

namespace DocumentFlow.Entities.Companies;

public class ContractApplicationEditor : Editor<ContractApplication>, IContractApplicationEditor
{
    public ContractApplicationEditor(IContractApplicationRepository repository, IPageManager pageManager) : base(repository, pageManager) 
    {
        EditorControls
            .SetHeaderWidth(170)
            .AddTextBox(x => x.ContractName, "Договор", text => text
                .SetEditorWidth(400)
                .Disable())
            .AddTextBox(x => x.Code, "Номер приложения", text => text
                .SetEditorWidth(200))
            .AddTextBox(x => x.ItemName, "Наименование", text => text
                .SetEditorWidth(400))
            .AddDateTimePicker(x => x.DocumentDate, "Дата подписания", date => date
                .SetFormat(DateTimePickerFormat.Short)
                .SetEditorWidth(200))
            .AddDateTimePicker(x => x.DateStart, "Начало действия", date => date
                .SetFormat(DateTimePickerFormat.Short)
                .SetEditorWidth(200))
            .AddDateTimePicker(x => x.DateEnd, "Окончание действия", date => date
                .NotRequired()
                .SetFormat(DateTimePickerFormat.Short)
                .SetEditorWidth(200))
            .AddTextBox(x => x.Note, "Примечание", text => text
                .Multiline()
                .EditorFitToSize()
                .SetDock(DockStyle.Bottom))
            .AddDataGrid<PriceApproval>(grid => grid
                .SetRepository<IPriceApprovalRepository>()
                .Dialog<IPriceApprovalDialog>()
                .SetDock(DockStyle.Fill));

        RegisterReport(new ContractApplicationReport());
    }

}