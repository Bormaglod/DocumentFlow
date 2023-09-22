//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 25.06.2023
//-----------------------------------------------------------------------

using DocumentFlow.Controls;
using DocumentFlow.Data.Models;
using DocumentFlow.Tools;

namespace DocumentFlow.ViewModels;

[Entity(typeof(OperationType), RepositoryType = typeof(IOperationTypeRepository))]
public partial class OperationTypeEditor : EditorPanel, IOperationTypeEditor
{
    public OperationTypeEditor(IServiceProvider services) : base(services)
    {
        InitializeComponent();
    }

    protected OperationType OperationType { get; set; } = null!;

    protected override void DoBindingControls()
    {
        textName.DataBindings.Add(nameof(textName.TextValue), DataContext, nameof(OperationType.ItemName), false, DataSourceUpdateMode.OnPropertyChanged);
        textSalary.DataBindings.Add(nameof(textSalary.DecimalValue), DataContext, nameof(OperationType.Salary), false, DataSourceUpdateMode.OnPropertyChanged);
    }
}
