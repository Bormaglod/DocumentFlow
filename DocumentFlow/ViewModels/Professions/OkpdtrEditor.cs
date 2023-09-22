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

[Entity(typeof(Okpdtr), RepositoryType = typeof(IOkpdtrRepository))]
public partial class OkpdtrEditor : EditorPanel, IOkpdtrEditor
{
    public OkpdtrEditor(IServiceProvider services) : base(services)
    {
        InitializeComponent();
    }

    protected Okpdtr Okpdtr { get; set; } = null!;

    protected override void DoBindingControls()
    {
        textCode.DataBindings.Add(nameof(textCode.TextValue), DataContext, nameof(Okpdtr.Code), false, DataSourceUpdateMode.OnPropertyChanged);
        textName.DataBindings.Add(nameof(textName.TextValue), DataContext, nameof(Okpdtr.ItemName), true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
        textSignatory.DataBindings.Add(nameof(textSignatory.TextValue), DataContext, nameof(Okpdtr.SignatoryName), true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
    }
}
