//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Tools;
using DocumentFlow.Data.Interfaces;
using DocumentFlow.Interfaces;

using Microsoft.Extensions.DependencyInjection;

using Npgsql;

using System.Reflection;

namespace DocumentFlow;

public class PageManager : IPageManager
{
    private readonly Dictionary<Type, Type> editors = new();
    private readonly List<IPage> pages = new();

    private readonly IServiceProvider services;

    public PageManager(IServiceProvider services)
    {
        this.services = services;

        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(p => p.IsInterface && p.GetInterface(nameof(IBrowserPage)) != null);

        foreach (var browserType in types)
        {
            var attr = browserType.GetCustomAttribute<EntityEditorAttribute>();
            if (attr != null)
            {
                editors.Add(browserType, attr.EditorType);
            }
        }
    }

    public void NotifyMainFormClosing()
    {
        foreach (var item in pages)
        {
            item.NotifyPageClosing();
        }
    }

    public bool PageCanEdited(Type browserType) => editors.ContainsKey(browserType);

    public void ShowBrowser(Type browserType)
    {
        var docking = services.GetRequiredService<IDockingManager>();

        var page = pages.FirstOrDefault(p => p.GetType().GetInterface(browserType.Name) != null);
        if (page == null)
        {
            try
            {
                var browser = services.GetRequiredService(browserType);
                if (browser is IBrowserPage browserPage)
                {
                    browserPage.UpdatePage();
                    pages.Add(browserPage);
                    docking.Activate(browserPage);
                }
            }
            catch (PostgresException e)
            {
                if (e.SqlState == "42501")
                {
                    MessageBox.Show("Доступ запрещен", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    throw;
                }
            }
        }
        else
        {
            ActivatePage(docking, page);
        }
    }

    public void ShowEditor<E>(IDocumentInfo document) where E : IEditor => ShowEditor(typeof(E), document);

    public void ShowEditor(Type editorType, IDocumentInfo document)
    {
        Guid? parent = null;
        if (document is IDirectory directory)
        {
            parent = directory.ParentId;
        }

        ShowEditor(editorType, document.Id, null, parent, document.Deleted);
    }

    public void ShowEditor(Type editorType, Guid id) => ShowEditor(editorType, id, null, null, false);

    public void ShowAssociateEditor<B>(IDocumentInfo document)
        where B : IBrowserPage
    {
        Guid? parent = null;
        if (document is IDirectory directory)
        {
            parent = directory.ParentId;
        }

        ShowAssociateEditor(typeof(B), document.Id, null, parent, document.Deleted);
    }

    public void ShowAssociateEditor<B>(Guid id)
        where B : IBrowserPage
    {
        ShowAssociateEditor(typeof(B), id, null, null, false);
    }

    public void ShowAssociateEditor(Type browserType, Guid? objectId, IDocumentInfo? owner, Guid? parentId, bool readOnly)
    {
        if (editors.ContainsKey(browserType))
        {
            Type editorType = editors[browserType];
            ShowEditor(editorType, objectId, owner, parentId, readOnly);
        }
    }

    private void ShowEditor(Type editorType, Guid? objectId, IDocumentInfo? owner, Guid? parentId, bool readOnly)
    {
        IEditorPage? page = null;
        if (objectId != null)
        {
            page = pages
                .OfType<IEditorPage>()
                .Where(p => p.Editor.GetType().GetInterface(editorType.Name) != null)
                .FirstOrDefault(p => p.Editor.DocumentInfo.Id == objectId);
        }

        var docking = services.GetRequiredService<IDockingManager>();
        if (page == null)
        {
            try
            {
                page = services.GetRequiredService<IEditorPage>();

                var editor = services.GetRequiredService(editorType);

                if (editor is IDocumentEditor documentEditor && owner != null)
                {
                    documentEditor.SetOwner(owner);
                }

                if (editor is IDirectoryEditor directoryEditor && parentId != null)
                {
                    directoryEditor.ParentId = parentId;
                }

                if (editor is IEditor e)
                {
                    e.EnabledEditor = !readOnly;
                    page.Editor = e;
                    if (objectId != null)
                    {
                        page.RefreshPage(objectId.Value);
                    }
                    else
                    {
                        page.CreatePage();
                    }
                }

                pages.Add(page);
                docking.Activate(page);
            }
            catch (PostgresException e)
            {
                if (e.SqlState == "42501")
                {
                    MessageBox.Show("Доступ запрещен", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    throw;
                }
            }
        }
        else
        {
            ActivatePage(docking, page);
        }
    }

    public void ShowStartPage() => ShowPage<IStartPage>();

    public void ShowEmailPage() => ShowPage<IEmailPage>();

    public void ShowAbout() => services.GetRequiredService<AboutForm>().ShowDialog();

    private void ShowPage<P>() where P : IPage
    {
        var docking = services.GetRequiredService<IDockingManager>();

        var page = pages.OfType<P>().FirstOrDefault();
        if (page == null)
        {
            page = services.GetRequiredService<P>();
            pages.Add(page);

            ActivatePage(docking, page);
        }
        else
        {
            ActivatePage(docking, page);
        }
    }

    private static void ActivatePage(IDockingManager docking, IPage page)
    {
        if (!docking.IsVisibility(page))
        {
            page.RefreshPage();
        }

        docking.Activate(page);
    }
}
