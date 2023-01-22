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
            DocumentNumberControl = new DfIntegerTextBox<int>("document_number", "Номер", 60, 100)
            {
                Dock = DockStyle.Left,
                Width = 165
            };

            var document_date = new DfDateTimePicker("document_date", "от", 25, 170)
            {
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd.MM.yyyy HH:mm:ss",
                Required = true,
                Dock = DockStyle.Left,
                Width = 200
            };

            var organization = new DfComboBox<Organization>("organization_id", "Организация", 100, 200)
            {
                Dock = DockStyle.Left,
                Width = 305
            };

            var panel_header = new Panel()
            {
                Dock = DockStyle.Top,
                Height = 32
            };

            panel_header.Controls.AddRange(new Control[] { organization, document_date, DocumentNumberControl });

            var line = new DfLine();

            organization.SetDataSource(() => Services.Provider.GetService<IOrganizationRepository>()?.GetAll());

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
            DocumentNumberControl.Enabled = Document.id != Guid.Empty;
        }
    }

    protected override void DoCreatedDocument(T document)
    {
        base.DoCreatedDocument(document);
        document.SetOrganization(Services.Provider.GetService<IOrganizationRepository>()!.GetMain().id);
    }
}
