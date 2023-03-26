//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 16.01.2022
//-----------------------------------------------------------------------

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
        var contractor_name = CreateTextBox(x => x.ContractorName, "Контрагент", headerWidth, 400, enabled: false);
        var type = CreateChoice(x => x.ContractorType, "Вид договора", headerWidth, 200, choices: Contract.ContractorTypes);
        var code = CreateTextBox(x => x.Code, "Номер договора", headerWidth, 200, defaultAsNull: false);
        var name = CreateTextBox(x => x.ItemName, "Наименование", headerWidth, 400);
        var date = CreateDateTimePicker(x => x.DocumentDate, "Дата договора", headerWidth, 200, format: DateTimePickerFormat.Short);
        var tax_payer = CreateToggleButton(x => x.TaxPayer, "Плательщик НДС", headerWidth);
        var signatory = CreateDirectorySelectBox<Employee>(x => x.SignatoryId, "Подпись контрагента", headerWidth, 400);
        var org_signatory = CreateDirectorySelectBox<Employee>(x => x.OrgSignatoryId, "Подпись организации", headerWidth, 400);
        var date_start = CreateDateTimePicker(x => x.DateStart, "Начало действия", headerWidth, 200, format: DateTimePickerFormat.Short);
        var date_end = CreateDateTimePicker(x => x.DateEnd, "Окончание действия", headerWidth, 200, required: false, format: DateTimePickerFormat.Short);
        var is_default = CreateToggleButton(x => x.IsDefault, "Основной", headerWidth);

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