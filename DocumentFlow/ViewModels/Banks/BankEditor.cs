//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 14.06.2023
//-----------------------------------------------------------------------

using DocumentFlow.Controls;
using DocumentFlow.Data.Models;
using DocumentFlow.Tools;

namespace DocumentFlow.ViewModels;

[Entity(typeof(Bank), RepositoryType = typeof(IBankRepository))]
public partial class BankEditor : EditorPanel, IBankEditor
{
    public BankEditor(IServiceProvider services) : base(services)
    {
        InitializeComponent();
    }

    protected Bank Bank { get; set; } = null!;

    protected override void DoBindingControls()
    {
        textName.DataBindings.Add(nameof(textName.TextValue), DataContext, nameof(Bank.ItemName), true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
        textBik.DataBindings.Add(nameof(textBik.DecimalValue), DataContext, nameof(Bank.Bik), true, DataSourceUpdateMode.OnPropertyChanged);
        textAccount.DataBindings.Add(nameof(textAccount.DecimalValue), DataContext, nameof(Bank.Account), true, DataSourceUpdateMode.OnPropertyChanged);
        textTown.DataBindings.Add(nameof(textTown.TextValue), DataContext, nameof(Bank.Town), true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
    }
}
