//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Infrastructure;
using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

using Npgsql;

using Syncfusion.Windows.Forms.Tools;

namespace DocumentFlow;

public class PageManager : IPageManager
{
    private readonly Dictionary<Type, Type> editors = new();
    private readonly Dictionary<TabPageAdv, IPage> pages = new();
    private readonly Stack<TabPageAdv> historyPages = new();
    private readonly ITabPages tabPages;

    public PageManager(ITabPages pages)
    {
        tabPages = pages;

        Type browserType = typeof(IBrowser<>);
        Type editorType = typeof(IEditor<,>);
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(p => p.IsInterface)
            .Where(p => p.GetInterfaces().Any(i => i.IsGenericType && editorType == i.GetGenericTypeDefinition()));

        foreach (var type in types)
        {
            var interfaces = type.GetInterfaces();
            foreach (var item in interfaces.Where(i => i.IsGenericType))
            {
                var t = item.GenericTypeArguments.FirstOrDefault(t => t.GetInterfaces().Any(i => i.IsGenericType && browserType == i.GetGenericTypeDefinition()));
                if (t != null)
                {
                    editors.Add(t, type);
                    break;
                }
            }
        }
    }

    public void AddToHistory(TabPageAdv page) => historyPages.Push(page);

    public void ClosePage(IPage page) => OnPageClose(page, removePageControl: true);

    public void OnPageClosing()
    {
        foreach (var item in pages)
        {
            item.Value.OnPageClosing();
        }
    }

    public bool PageCanEdited(Type browserType) => editors.ContainsKey(browserType);

    public void ShowBrowser(Type browserType)
    {
        var page = pages.FirstOrDefault(p => p.Value.GetType().GetInterface(browserType.Name) != null).Key;
        if (page == null)
        {
            try
            {
                var browser = Services.Provider.GetService(browserType);
                if (browser is Control control && browser is IBrowserPage browserPage)
                {
                    browserPage.Refresh();
                    page = AddPage(browserPage);
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

        if (page != null)
        {
            tabPages.Selected = page;
        }
    }

    public void ShowEditor<E>(Guid id)
        where E : IEditorPage
    {
        var editor = Services.Provider.GetService<E>();
        if (editor != null)
        {
            ShowEditor(editor, id);
        }
    }

    public void ShowEditor<E, T>(T document)
        where E : IEditorPage
        where T : IDocumentInfo
    {
        ShowEditor<E>(document.id);
    }

    public void ShowEditor(Type editorType, Guid id)
    {
        var editor = Services.Provider.GetService(editorType);
        if (editor is IEditorPage page)
        {
            ShowEditor(page, id);
        }
    }

    public void ShowEditor(IEditorPage editorPage, Guid objectId)
    {
        TabPageAdv? page = pages.FirstOrDefault(p => (p.Value as IEditorPage)?.Id == objectId).Key;

        if (page == null && editorPage is Control control)
        {
            editorPage.SetEntityParameters(objectId, null, null, false);
            page = AddPage(editorPage);
        }

        if (page != null)
        {
            tabPages.Selected = page;
        }
    }

    public void ShowEditor(Type browserType, Guid? objectId, Guid? owner_id, Guid? parentId, bool readOnly)
    {
        if (editors.ContainsKey(browserType))
        {
            TabPageAdv? page = null;
            if (objectId != null)
            {
                page = pages.FirstOrDefault(p => p.Value.GetType().GetInterface(editors[browserType].Name) != null && (p.Value as IEditorPage)?.Id == objectId).Key;
            }

            if (page == null)
            {
                try
                {
                    var editor = Services.Provider.GetService(editors[browserType]);
                    if (editor is Control control && editor is IEditorPage editorPage)
                    {
                        editorPage.SetEntityParameters(objectId, owner_id, parentId, readOnly);
                        page = AddPage(editorPage);
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

            if (page != null)
            {
                tabPages.Selected = page;
            }
        }
    }

    private TabPageAdv AddPage(IPage newPage)
    {
        if (newPage is Control control)
        {
            control.Dock = DockStyle.Fill;
            control.TextChanged += PageTextChanged;

            TabPageAdv? page = new(newPage.Text);
            page.Closed += PageClosed;
            page.Closing += PageClosing;

            page.Controls.Add(control);

            tabPages.Add(page);

            pages.Add(page, newPage);

            return page;
        }

        throw new ArrayTypeMismatchException("Параметр newPage должен наследоваться от Control");
    }

    private void PageTextChanged(object? sender, EventArgs e)
    {
        if (sender is IPage page)
        {
            var tab = pages.FirstOrDefault(p => p.Value == page).Key;
            if (tab != null)
            {
                tab.Text = page.Text;
            }
        }
    }

    private void PageClosed(object? sender, EventArgs e)
    {
        if (sender is TabPageAdv page && pages.ContainsKey(page))
        {
            OnPageClose(pages[page], page);
        }
    }

    private void PageClosing(object sender, TabPageAdvClosingEventArgs args)
    {
        if (historyPages.Count > 0)
        {
            if (historyPages.Peek() == args.TabPageAdv)
            {
                historyPages.Pop();
            }
        }
    }

    private void OnPageClose(IPage page, TabPageAdv? pageControl = null, bool removePageControl = false)
    {
        if (pageControl == null)
        {
            pageControl = pages.FirstOrDefault(p => p.Value == page).Key;
            if (pageControl == null)
            {
                return;
            }
        }

        page.OnPageClosing();

        pages.Remove(pageControl);
        if (removePageControl)
        {
            tabPages.Remove(pageControl);
        }

        if (historyPages.Count > 0)
        {
            tabPages.Selected = historyPages.Pop();
        }
    }
}
