//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 16.08.2023
//-----------------------------------------------------------------------

using DocumentFlow.Controls;
using DocumentFlow.Data.Models;
using DocumentFlow.Tools;

namespace DocumentFlow.ViewModels;

[Entity(typeof(Equipment), RepositoryType = typeof(IEquipmentRepository))]
public partial class EquipmentEditor : EditorPanel, IEquipmentEditor
{
    public EquipmentEditor(IServiceProvider services) : base(services)
    {
        InitializeComponent();
    }

    protected Equipment Equipment { get; set; } = null!;

    protected override void DoBindingControls()
    {
        textCode.DataBindings.Add(nameof(textCode.TextValue), DataContext, nameof(Equipment.Code), false, DataSourceUpdateMode.OnPropertyChanged);
        textName.DataBindings.Add(nameof(textName.TextValue), DataContext, nameof(Equipment.ItemName), true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
        textSerial.DataBindings.Add(nameof(textSerial.TextValue), DataContext, nameof(Equipment.SerialNumber), true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
        dateCommissioning.DataBindings.Add(nameof(dateCommissioning.DateTimeValue), DataContext, nameof(Equipment.Commissioning), true, DataSourceUpdateMode.OnPropertyChanged, DateTime.MinValue);
        textStartingHits.DataBindings.Add(nameof(textStartingHits.IntegerValue), DataContext, nameof(Equipment.StartingHits), true, DataSourceUpdateMode.OnPropertyChanged, 0);
        toggleTool.DataBindings.Add(nameof(toggleTool.ToggleValue), DataContext, nameof(Equipment.IsTools), false, DataSourceUpdateMode.OnPropertyChanged);
    }
}
