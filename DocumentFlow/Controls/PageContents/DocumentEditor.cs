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

using DocumentFlow.Controls.Editors;
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
        if (generateStandardHeader)
        {
            DocumentNumberControl = CreateIntegerTextBox<int>(x => x.DocumentNumber, "Номер", 60, 100);
            DocumentNumberControl.Dock = DockStyle.Left;
            DocumentNumberControl.Width = 165;

            var document_date = CreateDateTimePicker(x => x.DocumentDate, "от", 25, 170, format: DateTimePickerFormat.Custom, required: true);
            document_date.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            document_date.Dock = DockStyle.Left;
            document_date.Width = 200;

            var organization = CreateComboBox<Organization>(x => x.OrganizationId, "Организация", 100, 200, data: GetOrganizations);
            organization.Dock = DockStyle.Left;
            organization.Width = 305;

            var panel_header = new Panel()
            {
                Dock = DockStyle.Top,
                Height = 32
            };

            panel_header.Controls.AddRange(new Control[] { organization, document_date, DocumentNumberControl });

            var line = new DfLine();

            AddControls(new Control[]
            {
                panel_header,
                line
            });
        }
    }

    virtual protected DfIntegerTextBox<int>? DocumentNumberControl { get; } = null;

    protected override void DoAfterRefreshData()
    {
        base.DoAfterRefreshData();
        if (DocumentNumberControl != null)
        {
            DocumentNumberControl.Enabled = Document.Id != Guid.Empty;
        }
    }

    protected override void DoCreatedDocument(T document)
    {
        base.DoCreatedDocument(document);
        document.SetOrganization(Services.Provider.GetService<IOrganizationRepository>()!.GetMain().Id);
    }

    private IEnumerable<Organization> GetOrganizations() => Services.Provider.GetService<IOrganizationRepository>()!.GetAll();
}
