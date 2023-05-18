//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 16.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Entities.Employees;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Controls;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Companies;

public class ContractEditor : Editor<Contract>, IContractEditor
{
    private const int headerWidth = 170;

    public ContractEditor(IContractRepository repository, IPageManager pageManager) : base(repository, pageManager) 
    {
        EditorControls
            .AddTextBox(x => x.ContractorName, "Контрагент", text =>
                text
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(400)
                    .Disable())
            .AddChoice<ContractorType>(x => x.ContractorType, "Вид договора", choice =>
                choice
                    .SetChoiceValues(Contract.ContractorTypes)
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(200))
            .AddTextBox(x => x.Code, "Номер договора", text =>
                text
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(200)
                    .DefaultAsValue())
            .AddTextBox(x => x.ItemName, "Наименование", text =>
                text
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(400))
            .AddDateTimePicker(x => x.DocumentDate, "Дата договора", date =>
                date
                    .SetFormat(DateTimePickerFormat.Short)
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(200))
            .AddToggleButton(x => x.TaxPayer, "Плательщик НДС", toggle =>
                toggle
                    .SetHeaderWidth(headerWidth))
            .AddDirectorySelectBox<Employee>(x => x.SignatoryId, "Подпись контрагента", select =>
                select
                    .SetDataSource(GetEmployees)
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(400))
            .AddDirectorySelectBox<OurEmployee>(x => x.OrgSignatoryId, "Подпись организации", select =>
                select
                    .SetDataSource(GetOurEmployees)
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(400))
            .AddDateTimePicker(x => x.DateStart, "Начало действия", date =>
                date
                    .SetFormat(DateTimePickerFormat.Short)
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(200))
            .AddDateTimePicker(x => x.DateEnd, "Окончание действия", date =>
                date
                    .SetFormat(DateTimePickerFormat.Short)
                    .NotRequired()
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(200))
            .AddToggleButton(x => x.IsDefault, "Основной", toggle =>
                toggle
                    .SetHeaderWidth(headerWidth));
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