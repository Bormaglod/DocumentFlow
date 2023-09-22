//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 09.07.2023
//-----------------------------------------------------------------------

using DocumentFlow.Controls;
using DocumentFlow.Data.Models;
using DocumentFlow.Tools;

namespace DocumentFlow.ViewModels;

[Entity(typeof(Wire), RepositoryType = typeof(IWireRepository))]
public partial class WireEditor : EditorPanel, IWireEditor
{
    public WireEditor(IServiceProvider services) : base(services)
    {
        InitializeComponent();
    }

    protected Wire Wire { get; set; } = null!;

    protected override void DoBindingControls()
    {
        textCode.DataBindings.Add(nameof(textCode.TextValue), DataContext, nameof(Wire.Code), false, DataSourceUpdateMode.OnPropertyChanged);
        textName.DataBindings.Add(nameof(textName.TextValue), DataContext, nameof(Wire.ItemName), true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
        textWSize.DataBindings.Add(nameof(textWSize.DecimalValue), DataContext, nameof(Wire.Wsize), false, DataSourceUpdateMode.OnPropertyChanged);
    }
}
