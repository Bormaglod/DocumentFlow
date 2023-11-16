//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 12.06.2023
//-----------------------------------------------------------------------

using CommunityToolkit.Mvvm.Messaging;

using DocumentFlow.Controls.Enums;
using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Data;
using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Tools;
using DocumentFlow.Messages;
using DocumentFlow.Tools;
using DocumentFlow.Tools.Exceptions;

using Microsoft.Extensions.DependencyInjection;

using System.ComponentModel;
using System.Reflection;

namespace DocumentFlow.Controls;

[ToolboxItem(false)]
public partial class EditorPanel : UserControl, IEditor
{
    private bool enabled = true;
    private IEditorPage? editorPage;

    private readonly bool acceptSupported;
    private readonly Type repositoryType;
    private readonly object repository;
    
    private IDocumentInfo currentDocument;

    private readonly PropertyInfo entityProp;
    private readonly MethodInfo addMethod;
    private readonly MethodInfo updateMethod;
    private readonly MethodInfo getMethod;
    private readonly MethodInfo? acceptMethod;

    //public event EventHandler? HeaderChanged;

#if DEBUG
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
    public EditorPanel()
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
    {
        InitializeComponent();
    }
#endif

    public EditorPanel(IServiceProvider services)
    {
        InitializeComponent();

        var type = GetType();
        var entityAttr = type.GetCustomAttribute<EntityAttribute>();
        if (entityAttr == null)
        {
            ArgumentNullException.ThrowIfNull(entityAttr);
        }

        var types = from p in type.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance)
                    where p.PropertyType == entityAttr.EntityType && p.CanWrite
                    select p;

        if (types.Count() > 1)
        {
            entityProp = types.First(x => x.GetCustomAttribute<EntityInfo>() != null);
        }
        else
        {
            entityProp = types.First();
        }

        currentDocument = (IDocumentInfo)(Activator.CreateInstance(entityAttr.EntityType) ?? throw new Exception("The object must be inherited from IDocumentInfo"));
        entityProp.SetValue(this, currentDocument);

        if (entityAttr.RepositoryType == null)
        {
            var ns = entityAttr.EntityType.Namespace ?? string.Empty;
            var name = entityAttr.EntityType.Name;
            var asm = entityAttr.EntityType.Assembly.FullName;

            var repoName = $"{ns}.I{name}Repository, {asm}";

            repositoryType = Type.GetType(repoName) ?? throw new Exception($"Type {repoName} not found.");
        }
        else
        {
            repositoryType = entityAttr.RepositoryType;
        }

        repository = services.GetRequiredService(repositoryType);

        var repoType = repository.GetType();
        getMethod = repoType.GetMethod("Get", new Type[] { typeof(Guid), typeof(bool), typeof(bool) }) ?? throw new Exception(nameof(getMethod));
        addMethod = repoType.GetMethod("Add", new Type[] { entityAttr.EntityType }) ?? throw new Exception(nameof(addMethod));
        updateMethod = repoType.GetMethod("Update", new Type[] { entityAttr.EntityType }) ?? throw new Exception(nameof(updateMethod));
        acceptMethod = repoType.GetMethod("Accept", new Type[] { entityAttr.EntityType });

        acceptSupported = entityAttr.EntityType.GetInterfaces().Contains(typeof(IAccountingDocument));
    }

    public IEditorPage EditorPage
    {
        get
        {
            if (editorPage == null)
            {
                throw new NullReferenceException(nameof(EditorPage));
            }

            return editorPage;
        }

        set
        {
            if (editorPage != value)
            {
                editorPage = value;
                if (editorPage != null)
                {
                    RegisterReports();
                }
            }
        }
    }

    public bool EnabledEditor
    {
        get => enabled;
        set
        {
            if (enabled != value)
            {
                enabled = value;

                foreach (var control in this.Childs().OfType<IAccess>())
                {
                    control.EnabledEditor = enabled;
                }

                OnReadOnlyChanged();
            }
        }
    }

    public IDocumentInfo DocumentInfo => currentDocument;

    public virtual void RegisterNestedBrowsers() { }

    protected virtual void RegisterReports() { }

    protected virtual void OnReadOnlyChanged() { }

    protected virtual bool ConfirmSaveDocument() => true;

    protected virtual void AfterConstructData(ConstructDataMethod method) { }

    protected virtual void OnEntityPropertyChanged(string? propertyName)
    {
        switch (currentDocument)
        {
            case Directory:
                if (propertyName == nameof(Directory.ItemName))
                {
                    OnHeaderChanged();
                }

                break;
            case BaseDocument:
                if (propertyName == nameof(BaseDocument.DocumentNumber) || propertyName == nameof(BaseDocument.DocumentDate))
                {
                    OnHeaderChanged();
                }

                break;

        }
    }

    protected virtual void DoBindingControls() { }

    protected virtual void CreateDataSources() { }

    protected void OnHeaderChanged()
    {
        string readOnlyText = enabled ? string.Empty : " (только для чтения)";
        
        var attr = currentDocument.GetType().GetCustomAttribute<EntityNameAttribute>();

        string header;
        if (attr != null)
        {
            if (currentDocument.Id == Guid.Empty)
            {
                header = $"{attr.Name} (новый)";
            }
            else
            {
                header = $"{attr.Name} - {currentDocument}{readOnlyText}";
            }
        }
        else
        {
            header = $"{currentDocument}{readOnlyText}";
        }

        WeakReferenceMessenger.Default.Send(new EditorPageHeaderChangedMessage(header));
    }

    protected IDirectory GetDirectory()
    {
        if (currentDocument is IDirectory document)
        {
            return document;
        }

        throw new Exception("The object must be inherited from IDirectory");
    }

    private void SetDefaultParams(INotifyPropertyChanged entity)
    {
        entity.PropertyChanged += EntityPropertyChanged;
        OnHeaderChanged();
    }

    private void InternalBindingControls(object document)
    {
        CreateDataSources();

        foreach (Control control in this.Childs())
        {
            control.DataBindings.Clear();
        }

        DataContext = document;
        DoBindingControls();

        if (document is INotifyPropertyChanged notify)
        {
            SetDefaultParams(notify);
        }
    }

    #region IEditor interface implemented

    bool IEditor.AcceptSupported => acceptSupported;

    void IEditor.Create()
    {
        InternalBindingControls(currentDocument);
        currentDocument.Loaded();
        AfterConstructData(ConstructDataMethod.Create);
    }

    IDocumentInfo IEditor.Load(Guid identifier)
    {
        currentDocument = (IDocumentInfo)(getMethod.Invoke(repository, new object[] { identifier, true, false }) ?? throw new Exception($"Failed to upload document with ID {{{identifier}}}."));
        entityProp.SetValue(this, currentDocument);

        currentDocument.Loaded();
        InternalBindingControls(currentDocument);
        AfterConstructData(ConstructDataMethod.Load);

        return currentDocument;
    }

    IDocumentInfo IEditor.Save()
    {
        if (!ConfirmSaveDocument())
        {
            throw new UserCanceledException();
        }

        if (currentDocument.Id == Guid.Empty)
        {
            currentDocument = (IDocumentInfo)(addMethod.Invoke(repository, new[] { currentDocument }) ?? throw new Exception($"Failed to saved document with ID {{{currentDocument.Id}}}. Failed attempt to added data."));
        }
        else
        {
            updateMethod.Invoke(repository, new[] { currentDocument });
            currentDocument = (IDocumentInfo)(getMethod.Invoke(repository, new object[] { currentDocument.Id, true, false }) ?? throw new Exception($"Failed to saved document with ID {{{currentDocument.Id}}}. Failed attempt to get data."));
        }

        entityProp.SetValue(this, currentDocument);
        InternalBindingControls(currentDocument);
        AfterConstructData(ConstructDataMethod.Save);

        return currentDocument;
    }

    void IEditor.Accept()
    {
        if (currentDocument.Id != Guid.Empty)
        {
            acceptMethod?.Invoke(repository, new[] { currentDocument });
        }
    }

    void IEditor.Reload()
    {
        if (currentDocument.Id != Guid.Empty)
        {
            currentDocument = (IDocumentInfo)(getMethod.Invoke(repository, new object[] { currentDocument.Id, true, false }) ?? throw new Exception($"Failed to upload document with ID {{{currentDocument.Id}}}."));
            entityProp.SetValue(this, currentDocument);

            InternalBindingControls(currentDocument);
        }

        AfterConstructData(ConstructDataMethod.Reload);
    }

    #endregion

    private void EntityPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        OnEntityPropertyChanged(e.PropertyName);
    }
}
