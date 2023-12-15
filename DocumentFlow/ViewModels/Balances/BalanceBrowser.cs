//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 31.01.2022
//-----------------------------------------------------------------------

using CommunityToolkit.Mvvm.Messaging;

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Interfaces.Repository;
using DocumentFlow.Data.Models;
using DocumentFlow.Messages;
using DocumentFlow.Properties;

using Humanizer;

using Microsoft.Extensions.Configuration;

using System.Reflection;

namespace DocumentFlow.ViewModels;

public abstract class BalanceBrowser<T> : BrowserPage<T>
    where T : Balance
{
    public BalanceBrowser(IServiceProvider services, IRepository<Guid, T> repository, IConfiguration configuration, IFilter? filter = null) 
        : base(services, repository, configuration, filter: filter)
    {
        ToolBar.SmallIcons();
        ToolBar.Add("Открыть документ", Resources.icons8_open_document_16, Resources.icons8_open_document_30, OpenDocument);
    }

    private void OpenDocument()
    {
        if (CurrentDocument != null && CurrentDocument.OwnerId != null)
        {
            var strType = $"I{CurrentDocument.DocumentTypeCode.Pascalize()}Editor";

            var type = Assembly
                .GetExecutingAssembly()
                .DefinedTypes
                .FirstOrDefault(x => x.Name == strType && x.Namespace != null && x.Namespace.StartsWith("DocumentFlow.ViewModels"));

            if (type != null)
            {
                try
                {
                    WeakReferenceMessenger.Default.Send(new EntityEditorOpenMessage(type, CurrentDocument.OwnerId.Value));
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
