//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 05.07.2023
//-----------------------------------------------------------------------

using DocumentFlow.Data.Models;
using DocumentFlow.Interfaces;
using DocumentFlow.Tools;

namespace DocumentFlow.ViewModels;

[Entity(typeof(OurEmployee), RepositoryType = typeof(IOurEmployeeRepository))]
public partial class OurEmployeeEditor : BaseEmployeeEditor, IOurEmployeeEditor
{
    public OurEmployeeEditor(IServiceProvider services, IPageManager pageManager) : base(services, pageManager)
    {
        InitializeComponent();
    }

    protected OurEmployee Employee { get; set; } = null!;

    public override void RegisterNestedBrowsers()
    {
        EditorPage.RegisterNestedBrowser<IBalanceEmployeeBrowser>();
    }
}
