//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 27.07.2023
//-----------------------------------------------------------------------

using CommunityToolkit.Mvvm.Messaging;

using DocumentFlow.Controls;
using DocumentFlow.Controls.Editors;
using DocumentFlow.Controls.Events;
using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Models;
using DocumentFlow.Messages;
using DocumentFlow.Tools;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.ViewModels;

[Entity(typeof(Deduction), RepositoryType = typeof(IDeductionRepository))]
public partial class DeductionEditor : EditorPanel, IDeductionEditor
{
    private readonly IServiceProvider services;

    public DeductionEditor(IServiceProvider services) : base(services)
    {
        InitializeComponent();

        this.services = services;

        choiceBase.FromEnum<BaseDeduction>(KeyGenerationMethod.PostgresEnumValue);
    }

    protected Deduction Deduction { get; set; } = null!;

    protected override void DoBindingControls()
    {
        textName.DataBindings.Add(nameof(textName.TextValue), DataContext, nameof(Deduction.ItemName), true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
        choiceBase.DataBindings.Add(nameof(choiceBase.ChoiceValue), DataContext, nameof(Deduction.BaseCalc), false, DataSourceUpdateMode.OnPropertyChanged);
        comboPerson.DataBindings.Add(nameof(comboPerson.SelectedItem), DataContext, nameof(Deduction.PersonId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty);
    }

    protected override void CreateDataSources()
    {
        comboPerson.DataSource = services
            .GetRequiredService<IPersonRepository>()
            .GetListExisting(callback: q => q.OrderBy("item_name"));
    }

    private void ChoiceBase_ChoiceValueChanged(object sender, EventArgs e)
    {
        comboPerson.Visible = choiceBase.ChoiceValue == "person";
        textPercent.Visible = choiceBase.ChoiceValue == "salary" || choiceBase.ChoiceValue == "material";
        textSumma.Visible = choiceBase.ChoiceValue == "person";

        if (textPercent.Visible)
        {
            textSumma.DataBindings.Clear();
            if (textPercent.DataBindings.Count == 0)
            {
                textPercent.DataBindings.Add(nameof(textPercent.PercentValue), DataContext, nameof(Deduction.Value), false, DataSourceUpdateMode.OnPropertyChanged);
            }
        }
        else if (textSumma.Visible)
        {
            textPercent.DataBindings.Clear();
            if (textSumma.DataBindings.Count == 0)
            {
                textSumma.DataBindings.Add(nameof(textSumma.DecimalValue), DataContext, nameof(Deduction.Value), false, DataSourceUpdateMode.OnPropertyChanged);
            }
        }
    }

    private void ComboPerson_OpenButtonClick(object sender, DocumentSelectedEventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new EntityEditorOpenMessage(typeof(IPersonEditor), e.Document));
    }
}
