//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 16.01.2022
//
// Версия 2023.6.3
//  - добавлено поле PaymentPeriod
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Entities.Employees;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Controls;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Companies;

public class ContractEditor : Editor<Contract>, IContractEditor
{
    public ContractEditor(IContractRepository repository, IPageManager pageManager) : base(repository, pageManager) 
    {
        EditorControls
            .SetHeaderWidth(170)
            .AddTextBox(x => x.ContractorName, "Контрагент", text => text
                .SetEditorWidth(400)
                .Disable())
            .AddChoice<ContractorType>(x => x.ContractorType, "Вид договора", choice => choice
                .SetChoiceValues(Contract.ContractorTypes)
                .SetEditorWidth(200))
            .AddTextBox(x => x.Code, "Номер договора", text => text
                .SetEditorWidth(200)
                .DefaultAsValue())
            .AddTextBox(x => x.ItemName, "Наименование", text => text
                .SetEditorWidth(400))
            .AddDateTimePicker(x => x.DocumentDate, "Дата договора", date => date
                .SetFormat(DateTimePickerFormat.Short)
                .SetEditorWidth(200))
            .AddToggleButton(x => x.TaxPayer, "Плательщик НДС")
            .AddDirectorySelectBox<Employee>(x => x.SignatoryId, "Подпись контрагента", select => select
                .SetDataSource(GetEmployees)
                .SetEditorWidth(400))
            .AddDirectorySelectBox<OurEmployee>(x => x.OrgSignatoryId, "Подпись организации", select => select
                .SetDataSource(GetOurEmployees)
                .SetEditorWidth(400))
            .AddDateTimePicker(x => x.DateStart, "Начало действия", date => date
                .SetFormat(DateTimePickerFormat.Short)
                .SetEditorWidth(200))
            .AddDateTimePicker(x => x.DateEnd, "Окончание действия", date => date
                .SetFormat(DateTimePickerFormat.Short)
                .NotRequired()
                .SetEditorWidth(200))
            .AddIntergerTextBox(x => x.PaymentPeriod, "Срок оплаты, дней", text => text
                .SetEditorWidth(100))
            .AddToggleButton(x => x.IsDefault, "Основной");
    }

    protected override void RegisterNestedBrowsers()
    {
        base.RegisterNestedBrowsers();
        RegisterNestedBrowser<IContractApplicationBrowser, ContractApplication>();
    }

    private IEnumerable<Employee> GetEmployees()
    {
        var emps = Services.Provider.GetService<IEmployeeRepository>();
        var signatory = EditorControls.GetControl<IDirectorySelectBoxControl<Employee>>(x => x.SignatoryId);
        if (signatory.SelectedItem != null)
        {
            return emps!.GetByOwner(signatory.SelectedItem.Id, OwnerId, callback: q => q.WhereFalse("employee.deleted"));
        }
        else
        {
            return emps!.GetByOwner(OwnerId, callback: q => q.WhereFalse("employee.deleted"));
        }
    }

    private IEnumerable<OurEmployee> GetOurEmployees() => Services.Provider.GetService<IOurEmployeeRepository>()!.GetListExisting();
}