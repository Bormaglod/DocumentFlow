//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 15.06.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Editors;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Entities.Companies;
using DocumentFlow.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Balances.Initial;

internal class InitialBalanceContractorEditor : DocumentEditor<InitialBalanceContractor>, IInitialBalanceContractorEditor
{
    public InitialBalanceContractorEditor(IInitialBalanceContractorRepository repository, IPageManager pageManager) 
        : base(repository, pageManager, true) 
    {
        var contractor = new DfDirectorySelectBox<Contractor>("reference_id", "Контрагент", 100, 400)
        {
            OpenAction = (t) => pageManager.ShowEditor<IContractorEditor, Contractor>(t)
        };

        var contract = new DfComboBox<Contract>("contract_id", "Договор", 100, 400);
        var debt = new DfChoice<decimal>("amount", "Долг", 100, 200);
        var op_summa = new DfCurrencyTextBox("operation_summa", "Сумма", 100, 100);

        contractor.SetDataSource(() => Services.Provider.GetService<IContractorRepository>()?.GetAllValid());
        contractor.ValueChanged += (sender, e) =>
        {
            var repo = Services.Provider.GetService<IContractRepository>();
            if (e.NewValue != null && repo != null)
            {
                contract.SetDataSource(() => repo.Get(e.NewValue), true);
                contract.Value = Document.contract_id;
            }
            else
            {
                contract.DeleteDataSource();
            }
        };

        debt.SetChoiceValues(new Dictionary<decimal, string>()
        {
            [-1] = "Наш долг",
            [1] = "Долг контрагента"
        });

        AddControls(new Control[]
        {
            contractor,
            contract,
            op_summa,
            debt
        });
    }
}
