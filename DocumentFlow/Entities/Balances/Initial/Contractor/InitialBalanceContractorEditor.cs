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

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Entities.Companies;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Controls;

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
        EditorControls
            .AddDirectorySelectBox<Contractor>(x => x.ReferenceId, "Контрагент", (select) =>
                select
                    .EnableEditor<IContractorEditor>()
                    .SetDataSource(GetContractors)
                    .DirectoryChanged(ContractorChanged)
                    .SetEditorWidth(400))
            .AddComboBox<Contract>(x => x.ContractId, "Договор", (combo) =>
                combo
                    .SetEditorWidth(400))
            .AddChoice<decimal>(x => x.Amount, "Долг", (choice) =>
                choice
                    .SetChoiceValues(debtChoice)
                    .SetEditorWidth(200))
            .AddCurrencyTextBox(x => x.OperationSumma, "Сумма");
    }

    private IEnumerable<Contractor> GetContractors() => Services.Provider.GetService<IContractorRepository>()!.GetListExisting();

    private void ContractorChanged(Contractor? newValue)
    {
        var repo = Services.Provider.GetService<IContractRepository>();
        var contract = EditorControls.GetControl<IComboBoxControl<Contract>>("ContractId");
        if (newValue != null && repo != null)
        {
            contract.SetDataSource(() => repo.Get(newValue), DataRefreshMethod.Immediately, Document.ContractId);
        }
        else
        {
            contract.RemoveDataSource();
        }
    }
}
