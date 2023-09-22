//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.12.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Models;

using Humanizer;

using SqlKata;
using Microsoft.Extensions.Configuration;
using System.ComponentModel;
using DocumentFlow.Data.Interfaces.Filters;

namespace DocumentFlow.Controls.Filters;

[ToolboxItem(false)]
public partial class BalanceContractorFilter : UserControl, IBalanceContractorFilter
{
    private readonly IContractRepository contracts;
    private Guid? ownerId;

    public BalanceContractorFilter(IContractRepository contracts)
    {
        InitializeComponent();

        this.contracts = contracts;
    }

    public Guid? OwnerId
    {
        get => ownerId;
        set
        {
            ownerId = value;
            if (ownerId != null) 
            {
                var list = contracts.GetByOwner(ownerId, this);
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
                return ((Contract)sfComboBox1.SelectedItem).Id;
            }
        }

        set
        {
            if (value != null) 
            { 
                var c = ((IList<Contract>)sfComboBox1.DataSource).FirstOrDefault(x => x.Id == value);
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

    public object? Settings => null;

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

    public void SettingsLoaded()
    {
    }

    private void ToolButton1_Click(object sender, EventArgs e) => sfComboBox1.SelectedIndex = -1;
}
