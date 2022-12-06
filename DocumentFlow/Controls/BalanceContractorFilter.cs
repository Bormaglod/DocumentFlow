//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.12.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Entities.Companies;

using Microsoft.Extensions.DependencyInjection;

using SqlKata;

namespace DocumentFlow.Controls;

public partial class BalanceContractorFilter : UserControl, IBalanceContractorFilter
{
    private Guid? ownerId;

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

    public Query? CreateQuery<T>()
    {
        if (sfComboBox1.SelectedIndex != -1)
        {
            var query = new Query();
            query.Where("balance_contractor.contract_id", ContractIdentifier);
            return query;
        }

        return null;
    }

    private void ToolButton1_Click(object sender, EventArgs e)
    {
        sfComboBox1.SelectedIndex = -1;
    }
}
