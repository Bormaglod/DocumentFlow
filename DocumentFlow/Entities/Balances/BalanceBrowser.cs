//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 31.01.2022
//
// Версия 2022.12.6
//  - в конструктор добавлен параметр filter
// Версия 2023.1.8
//  - в конструктор добавлен параметр settings
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//  - DocumentFlow.Settings.Infrastructure перемещено в DocumentFlow.Infrastructure.Settings
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Core.Exceptions;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Controls;
using DocumentFlow.Infrastructure.Data;
using DocumentFlow.Infrastructure.Settings;
using DocumentFlow.Properties;

using Humanizer;

using System.Reflection;

namespace DocumentFlow.Entities.Balances;

public abstract class BalanceBrowser<T> : Browser<T>
    where T : Balance
{
    private readonly IPageManager pageManager;

    public BalanceBrowser(IRepository<Guid, T> repository, IPageManager pageManager, IFilter? filter = null, IStandaloneSettings? settings = null) 
        : base(repository, pageManager, filter: filter, settings: settings)
    {
        this.pageManager = pageManager;

        Toolbar.IconSize = ButtonIconSize.Small;
        Toolbar.Add("Открыть документ", Resources.icons8_open_document_16, Resources.icons8_open_document_30, OpenDocument);
    }

    private void OpenDocument()
    {
        if (CurrentDocument != null && CurrentDocument.OwnerId != null)
        {
            var strType = $"I{CurrentDocument.DocumentTypeCode.Pascalize()}Editor";

            var type = Assembly
                .GetExecutingAssembly()
                .DefinedTypes
                .FirstOrDefault(x => x.Name == strType && x.Namespace != null && x.Namespace.StartsWith("DocumentFlow.Entities"));

            if (type != null)
            {
                try
                {
                    pageManager.ShowEditor(type, CurrentDocument.OwnerId.Value);
                }
                catch (BrowserException e)
                {
                    MessageBox.Show(e.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
