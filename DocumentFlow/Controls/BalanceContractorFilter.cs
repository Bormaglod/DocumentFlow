//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.12.2022
//
// Версия 2022.12.17
//  - добавлен метод CreateQuery(string tableName);
// Версия 2023.1.8
//  - добавлен метод Configure и WriteConfigure (реализация метода
//    интерфейса IBalanceContractorFilter
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Entities.Companies;
using DocumentFlow.Settings.Infrastructure;

using Humanizer;

using Microsoft.Extensions.DependencyInjection;

using SqlKata;

namespace DocumentFlow.Controls;

public partial class BalanceContractorFilter : UserControl, IBalanceContractorFilter
{
    private Guid? ownerId;
    private BalanceContractorFilterData? filterData;

    public BalanceContractorFilter()
    {
        InitializeComponent();
    }

    public Guid? OwnerIdentifier 
    {
        get => ownerId;
        set
        {
            ownerId = value;
            if (ownerId != null) 
            {
                var c = Services.Provider.GetService<IContractRepository>();
                var list = c!.GetByOwner(ownerId, this);
                sfComboBox1.DataSource = list;
            }
            else
            {
                sfComboBox1.DataSource = null;
            }
        }
    }

    public Guid? ContractIdentifier 
    { 
        get
        {
            if (sfComboBox1.SelectedIndex == -1)
            {
                return null;
            }
            else
            {
                return ((Contract)sfComboBox1.SelectedItem).id;
            }
        }

        set
        {
            if (value != null) 
            { 
                var c = ((IList<Contract>)sfComboBox1.DataSource).FirstOrDefault(x => x.id == value);
                if (c != null) 
                { 
                    sfComboBox1.SelectedItem = c;
                }
            }
            else
            {
                sfComboBox1.SelectedIndex = -1;
            }
        }
    }

    public Control Control => this;

    public void Configure(IAppSettings appSettings)
    {
        if (OwnerIdentifier == null)
        {
            return;
        }

        filterData = appSettings.Get<BalanceContractorFilterData>("filter");
        ContractIdentifier = filterData.GetContract(OwnerIdentifier.Value);
    }

    public void WriteConfigure(IAppSettings appSettings)
    {
        if (OwnerIdentifier == null)
        {
            return;
        }

        if (filterData == null)
        {
            if (ContractIdentifier != null)
            {
                filterData = new()
                {
                    ContractIdentifiers = new Dictionary<Guid, Guid?>()
                    {
                        [OwnerIdentifier.Value] = ContractIdentifier
                    }
                };
            }
        }
        else
        {
            filterData.ContractIdentifiers ??= new Dictionary<Guid, Guid?>();

            if (filterData.ContractIdentifiers.ContainsKey(OwnerIdentifier.Value))
            {
                if (ContractIdentifier == null)
                {
                    filterData.ContractIdentifiers.Remove(OwnerIdentifier.Value);
                }
                else
                {
                    filterData.ContractIdentifiers[OwnerIdentifier.Value] = ContractIdentifier;
                }
            }
            else
            {
                if (ContractIdentifier != null)
                {
                    filterData.ContractIdentifiers.Add(OwnerIdentifier.Value, ContractIdentifier);
                }
            }

            if (filterData.ContractIdentifiers.Count == 0)
            {
                filterData.ContractIdentifiers = null;
            }
        }

        appSettings.Write("filter", filterData);
    }

    public Query? CreateQuery<T>() => CreateQuery(typeof(T).Name.Underscore());

    public Query? CreateQuery(string tableName)
    {
        if (sfComboBox1.SelectedIndex != -1)
        {
            var query = new Query();
            query.Where("balance_contractor.contract_id", ContractIdentifier);
            return query;
        }

        return null;
    }

    private void ToolButton1_Click(object sender, EventArgs e) => sfComboBox1.SelectedIndex = -1;
}
