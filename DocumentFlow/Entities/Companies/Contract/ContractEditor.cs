//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 16.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Editors;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Entities.Employees;
using DocumentFlow.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Companies;

public class ContractEditor : Editor<Contract>, IContractEditor
{
    private const int headerWidth = 170;

    public ContractEditor(IContractRepository repository, IPageManager pageManager) : base(repository, pageManager) 
    {
        var contractor_name = new DfTextBox("contractor_name", "Контрагент", headerWidth, 400) { Enabled = false };
        var type = new DfChoice<ContractorType>("ContractorType", "Вид договора", headerWidth, 200);
        var code = new DfTextBox("code", "Номер договора", headerWidth, 200) { DefaultAsNull = false };
        var name = new DfTextBox("item_name", "Наименование", headerWidth, 400);
        var date = new DfDateTimePicker("document_date", "Дата договора", headerWidth, 200) { Format = DateTimePickerFormat.Short };
        var tax_payer = new DfToggleButton("tax_payer", "Плательщик НДС", headerWidth);
        var signatory = new DfDirectorySelectBox<Employee>("signatory_id", "Подпись контрагента", headerWidth, 400);
        var org_signatory = new DfDirectorySelectBox<Employee>("org_signatory_id", "Подпись организации", headerWidth, 400);
        var date_start = new DfDateTimePicker("date_start", "Начало действия", headerWidth, 200) { Format = DateTimePickerFormat.Short };
        var date_end = new DfDateTimePicker("date_end", "Окончание действия", headerWidth, 200) { Required = false, Format = DateTimePickerFormat.Short };
        var is_default = new DfToggleButton("is_default", "Основной", headerWidth);

        type.SetChoiceValues(Contract.ContractorTypes);

        signatory.SetDataSource(() =>
        {
            var emps = Services.Provider.GetService<IEmployeeRepository>();
            if (signatory.SelectedItem != null)
            {
                return emps!.GetByOwner(signatory.SelectedItem.Id, OwnerId, callback: q => q.WhereFalse("employee.deleted"));
            }
            else
            {
                return emps!.GetByOwner(OwnerId, callback: q => q.WhereFalse("employee.deleted"));
            }
        });

        org_signatory.SetDataSource(() => Services.Provider.GetService<IOurEmployeeRepository>()!.GetAllValid());

        AddControls(new Control[]
        {
            contractor_name,
            type,
            code,
            name,
            date,
            tax_payer,
            signatory,
            org_signatory,
            date_start,
            date_end,
            is_default
        });
    }

    protected override void RegisterNestedBrowsers()
    {
        base.RegisterNestedBrowsers();
        RegisterNestedBrowser<IContractApplicationBrowser, ContractApplication>();
    }
}