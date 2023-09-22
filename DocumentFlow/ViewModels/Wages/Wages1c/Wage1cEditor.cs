//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 19.09.2023
//-----------------------------------------------------------------------

using DocumentFlow.Controls;
using DocumentFlow.Controls.Enums;
using DocumentFlow.Controls.Events;
using DocumentFlow.Data.Models;
using DocumentFlow.Dialogs;
using DocumentFlow.Tools;

using Humanizer;

using Microsoft.Extensions.DependencyInjection;

using System.Globalization;

namespace DocumentFlow.ViewModels;

[Entity(typeof(Wage1c), RepositoryType = typeof(IWage1cRepository))]
public partial class Wage1cEditor : EditorPanel, IWage1cEditor
{
    private readonly IServiceProvider services;

    public Wage1cEditor(IServiceProvider services) : base(services)
    {
        InitializeComponent();

        this.services = services;

        var months = new Dictionary<short, string>();
        for (short i = 1; i < 13; i++)
        {
            months.Add(i, CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i).Humanize());
        }

        choiceMonth.From(months);
        Payroll.OrganizationId = services.GetRequiredService<IOrganizationRepository>().GetMain().Id;
    }

    protected Wage1c Payroll { get; set; } = null!;

    protected override void AfterConstructData(ConstructDataMethod method)
    {
        textDocNumber.Enabled = Payroll.Id != Guid.Empty;
    }

    protected override void DoBindingControls()
    {
        textDocNumber.DataBindings.Add(nameof(textDocNumber.IntegerValue), DataContext, nameof(GrossPayroll.DocumentNumber), true, DataSourceUpdateMode.OnPropertyChanged, 0);
        dateDocument.DataBindings.Add(nameof(dateDocument.DateTimeValue), DataContext, nameof(GrossPayroll.DocumentDate), true, DataSourceUpdateMode.OnPropertyChanged);
        comboOrg.DataBindings.Add(nameof(comboOrg.SelectedItem), DataContext, nameof(GrossPayroll.OrganizationId), false, DataSourceUpdateMode.OnPropertyChanged);
        choiceMonth.DataBindings.Add(nameof(choiceMonth.ChoiceValue), DataContext, nameof(GrossPayroll.BillingMonth), true, DataSourceUpdateMode.OnPropertyChanged);
        textYear.DataBindings.Add(nameof(textYear.IntegerValue), DataContext, nameof(GrossPayroll.BillingYear), true, DataSourceUpdateMode.OnPropertyChanged);
    }

    protected override void CreateDataSources()
    {
        comboOrg.DataSource = services.GetRequiredService<IOrganizationRepository>().GetList();
        gridEmps.DataSource = Payroll.Employees;
    }

    private void GridEmps_CreateRow(object sender, DependentEntitySelectEventArgs e)
    {
        var dialog = services.GetRequiredService<WageEmployeeDialog>();
        if (dialog.Create(out Wage1cEmployee? wage))
        {
            e.DependentEntity = wage;
        }
        else
        {
            e.Accept = false;
        }
    }

    private void GridEmps_EditRow(object sender, DependentEntitySelectEventArgs e)
    {
        if (e.DependentEntity is Wage1cEmployee wage)
        {
            var dialog = services.GetRequiredService<WageEmployeeDialog>();

            if (dialog.Edit(wage))
            {
                return;
            }
        }

        e.Accept = false;
    }
}
