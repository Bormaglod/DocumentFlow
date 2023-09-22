//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 13.06.2023
//-----------------------------------------------------------------------

using DocumentFlow.Controls;
using DocumentFlow.Data.Models;
using DocumentFlow.Tools;

namespace DocumentFlow.ViewModels;

[Entity(typeof(Okopf), RepositoryType = typeof(IOkopfRepository))]
public partial class OkopfEditor : EditorPanel, IOkopfEditor
{
    public OkopfEditor(IServiceProvider services) : base(services)
    {
        InitializeComponent();
    }

    protected Okopf Okopf { get; set; } = null!;

    protected override void DoBindingControls()
    {
        textCode.DataBindings.Add(nameof(textCode.TextValue), DataContext, nameof(Okopf.Code), false, DataSourceUpdateMode.OnPropertyChanged);
        textName.DataBindings.Add(nameof(textName.TextValue), DataContext, nameof(Okopf.ItemName), true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
    }
}
