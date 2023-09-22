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

[Entity(typeof(Measurement), RepositoryType = typeof(IMeasurementRepository))]
public partial class MeasurementEditor : EditorPanel, IMeasurementEditor
{
#if DEBUG
    public MeasurementEditor()
    {
        InitializeComponent();
    }
#endif

    public MeasurementEditor(IServiceProvider services) : base(services)
    {
        InitializeComponent();
    }

    protected Measurement Measurement { get; set; } = null!;

    protected override void DoBindingControls()
    {
        textCode.DataBindings.Add(nameof(textCode.TextValue), DataContext, nameof(Measurement.Code), false, DataSourceUpdateMode.OnPropertyChanged);
        textName.DataBindings.Add(nameof(textName.TextValue), DataContext, nameof(Measurement.ItemName), true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
        textAbbreviation.DataBindings.Add(nameof(textAbbreviation.TextValue), DataContext, nameof(Measurement.Abbreviation), true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
    }
}
