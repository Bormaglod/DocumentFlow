//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 16.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Dialogs.Infrastructure;
using DocumentFlow.Infrastructure;

namespace DocumentFlow.Entities.Companies;

public class ContractApplicationEditor : Editor<ContractApplication>, IContractApplicationEditor
{
    private const int headerWidth = 170;

    public ContractApplicationEditor(IContractApplicationRepository repository, IPageManager pageManager) : base(repository, pageManager) 
    {
        EditorControls
            .AddTextBox(x => x.ContractName, "Договор", text =>
                text
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(400)
                    .Disable())
            .AddTextBox(x => x.Code, "Номер приложения", text =>
                text
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(200))
            .AddTextBox(x => x.ItemName, "Наименование", text =>
                text
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(400))
            .AddDateTimePicker(x => x.DocumentDate, "Дата подписания", date =>
                date
                    .SetFormat(DateTimePickerFormat.Short)
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(200))
            .AddDateTimePicker(x => x.DateStart, "Начало действия", date =>
                date
                    .SetFormat(DateTimePickerFormat.Short)
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(200))
            .AddDateTimePicker(x => x.DateEnd, "Окончание действия", date =>
                date
                    .NotRequired()
                    .SetFormat(DateTimePickerFormat.Short)
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(200))
            .AddDataGrid<PriceApproval>(grid =>
                grid
                    .SetRepository<IPriceApprovalRepository>()
                    .Dialog<IPriceApprovalDialog>()
                    .SetDock(DockStyle.Fill));

        RegisterReport(new ContractApplicationReport());
    }

}