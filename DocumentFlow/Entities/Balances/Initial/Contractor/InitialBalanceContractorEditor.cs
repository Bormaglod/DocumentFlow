//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 15.06.2022
//
// Версия 2022.11.26
//  - параметр autoRefresh метода SetDataSource в классе
//    DataSourceControl был удален. Вместо него используется свойство
//    RefreshMethod этого класса в значении DataRefreshMethod.Immediately
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Entities.Companies;
using DocumentFlow.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Balances.Initial;

internal class InitialBalanceContractorEditor : DocumentEditor<InitialBalanceContractor>, IInitialBalanceContractorEditor
{
    private readonly Dictionary<decimal, string> debtChoice = new()
    {
        [-1] = "Наш долг",
        [1] = "Долг контрагента"
    };

public InitialBalanceContractorEditor(IInitialBalanceContractorRepository repository, IPageManager pageManager) 
        : base(repository, pageManager, true) 
    {
        var contractor = CreateDirectorySelectBox<Contractor, IContractorEditor>(x => x.ReferenceId, "Контрагент", 100, 400, data: GetContractors);
        var contract = CreateComboBox<Contract>(x => x.ContractId, "Договор", 100, 400, refreshMethod: DataRefreshMethod.Immediately);
        var debt = CreateChoice(x => x.Amount, "Долг", 100, 200, choices: debtChoice);
        var op_summa = CreateCurrencyTextBox(x => x.OperationSumma, "Сумма", 100, 100);

        contractor.ValueChanged += (sender, e) =>
        {
            var repo = Services.Provider.GetService<IContractRepository>();
            if (e.NewValue != null && repo != null)
            {
                contract.SetDataSource(() => repo.Get(e.NewValue));
                contract.Value = Document.ContractId;
            }
            else
            {
                contract.DeleteDataSource();
            }
        };

        AddControls(new Control[]
        {
            contractor,
            contract,
            op_summa,
            debt
        });
    }

    private IEnumerable<Contractor> GetContractors() => Services.Provider.GetService<IContractorRepository>()!.GetAllValid();
}
