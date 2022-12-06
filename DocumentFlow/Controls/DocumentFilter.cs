//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.11.2021
//
// Версия 2022.12.6
//  - добавлено свойство OwnerIdentifier
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Entities.Companies;

using Humanizer;

using Microsoft.Extensions.DependencyInjection;

using SqlKata;

namespace DocumentFlow.Controls
{
    public partial class DocumentFilter : UserControl, IDocumentFilter
    {
        public DocumentFilter()
        {
            InitializeComponent();

            var orgs = Services.Provider.GetService<IOrganizationRepository>();

            var list = orgs!.GetAll();
            comboOrg.DataSource = list;
            comboOrg.SelectedItem = list.FirstOrDefault(x => x.default_org);
        }

        public Guid? OwnerIdentifier { get; set; }

        public bool DateFromEnabled 
        {
            get => dateRangeControl1.FromEnabled;
            set => dateRangeControl1.FromEnabled = value;
        }
        public bool DateToEnabled 
        {
            get => dateRangeControl1.ToEnabled;
            set => dateRangeControl1.ToEnabled = value;
        }

        public DateTime? DateFrom 
        {
            get => dateRangeControl1.From;
            set => dateRangeControl1.From = value;
        }

        public DateTime? DateTo 
        {
            get => dateRangeControl1.To;
            set => dateRangeControl1.To = value;
        }

        public Control Control => this;

        public void SetDateRange(DateRange range) => dateRangeControl1.SetRange(range);

        public Query? CreateQuery<T>()
        {
            var table = typeof(T).Name.Underscore();

            var query = new Query();
            if (comboOrg.SelectedItem is Organization org)
            {
                query.Where($"{table}.organization_id", org.id);
            }

            if (DateFromEnabled || DateToEnabled)
            {
                if (DateFrom == null)
                {
                    query.Where($"{table}.document_date", "<=", DateTo);
                }
                else if (DateTo == null)
                {
                    query.Where($"{table}.document_date", ">=", DateFrom);
                }
                else
                {
                    query.WhereBetween($"{table}.document_date", DateFrom, DateTo);
                }
            }

            if (query.Clauses.Count > 0)
            {
                return query;
            }

            return null;
        }

        private void ButtonClearOrg_Click(object sender, EventArgs e)
        {
            comboOrg.SelectedIndex = -1;
        }
    }
}
