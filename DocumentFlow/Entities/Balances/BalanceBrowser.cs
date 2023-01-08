﻿//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 31.01.2022
//
// Версия 2022.12.6
//  - в конструктор добавлен параметр filter
// Версия 2023.1.8
//  - в конструктор добавлен параметр settings
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Infrastructure;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Core.Exceptions;
using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Infrastructure;
using DocumentFlow.Properties;
using DocumentFlow.Settings.Infrastructure;

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
        if (CurrentDocument != null && CurrentDocument.owner_id != null)
        {
            var strType = $"I{CurrentDocument.document_type_code.Pascalize()}Editor";

            var type = Assembly
                .GetExecutingAssembly()
                .DefinedTypes
                .FirstOrDefault(x => x.Name == strType && x.Namespace != null && x.Namespace.StartsWith("DocumentFlow.Entities"));

            if (type != null)
            {
                try
                {
                    pageManager.ShowEditor(type, CurrentDocument.owner_id.Value);
                }
                catch (BrowserException e)
                {
                    MessageBox.Show(e.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
