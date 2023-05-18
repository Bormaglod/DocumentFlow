//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 17.06.2022
//
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Entities.Companies;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Data;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Controls.PageContents;

public class DocumentEditor<T> : Editor<T>
    where T: class, IBaseDocument, new()
{
    public DocumentEditor(IRepository<Guid, T> repository, IPageManager pageManager, bool generateStandardHeader) : base(repository, pageManager) 
    {
        if (!generateStandardHeader)
        {
            return;
        }

        EditorControls
            .AddPanel((panel) =>
                panel
                    .SetDock(DockStyle.Top)
                    .SetHeight(32)
                    .AddControls((controls) =>
                        controls
                            .AddIntergerTextBox(x => x.DocumentNumber, "Номер", (text) =>
                                text
                                    .SetHeaderWidth(60)
                                    .SetDock(DockStyle.Left)
                                    .SetWidth(165))
                            .AddDateTimePicker(x => x.DocumentDate, "от", (date) =>
                                date
                                    .SetCustomFormat("dd.MM.yyyy HH:mm:ss")
                                    .SetHeaderWidth(25)
                                    .SetEditorWidth(170)
                                    .SetDock(DockStyle.Left)
                                    .SetWidth(200))
                            .AddComboBox<Organization>(x => x.OrganizationId, "Организация", (combo) =>
                                combo
                                    .SetDataSource(GetOrganizations)
                                    .SetEditorWidth(200)
                                    .SetDock(DockStyle.Left)
                                    .SetWidth(305))))
            .AddLine();
    }

    protected override void DoAfterRefreshData()
    {
        base.DoAfterRefreshData();
        EditorControls.GetControl(x => x.DocumentNumber)
                      .SetEnabled(Document.Id != Guid.Empty);
    }

    protected override void DoCreatedDocument(T document)
    {
        base.DoCreatedDocument(document);
        document.SetOrganization(Services.Provider.GetService<IOrganizationRepository>()!.GetMain().Id);
    }

    private IEnumerable<Organization> GetOrganizations() => Services.Provider.GetService<IOrganizationRepository>()!.GetList();
}
