//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.11.2021
//
// Версия 2022.12.6
//  - добавлено свойство OwnerIdentifier
// Версия 2022.12.17
//  - добавлен метод CreateQuery(string tableName);
// Версия 2023.1.8
//  - добавлен метод Configure и WriteConfigure (реализация метода
//    интерфейса IBalanceContractorFilter
// Версия 2023.1.15
//  - DocumentFilterData переименован в DocumentFilterSettings
// Версия 2023.1.17
//  - namespace заменен с DocumentFlow.Controls на DocumentFlow.Controls.Filters
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Entities.Companies;
using DocumentFlow.Settings.Infrastructure;

using Humanizer;

using Microsoft.Extensions.DependencyInjection;

using SqlKata;

namespace DocumentFlow.Controls.Filters
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

        public void Configure(IAppSettings appSettings)
        {
            var data = appSettings.Get<DocumentFilterSettings>("filter");
            
            DateFromEnabled = data.DateFromEnabled;
            DateToEnabled = data.DateToEnabled;
            DateFrom = data.DateFrom;
            DateTo = data.DateTo;
        }

        public void WriteConfigure(IAppSettings appSettings)
        {
            DocumentFilterSettings data = new()
            {
                DateFromEnabled = DateFromEnabled,
                DateToEnabled = DateToEnabled,
                DateFrom = DateFrom,
                DateTo = DateTo
            };

            appSettings.Write("filter", data);
        }

        public void SetDateRange(DateRange range) => dateRangeControl1.SetRange(range);

        public Query? CreateQuery<T>() => CreateQuery(typeof(T).Name.Underscore());

        public virtual Query? CreateQuery(string tableName)
        {
            var query = new Query();
            if (comboOrg.SelectedItem is Organization org)
            {
                query.Where($"{tableName}.organization_id", org.id);
            }

            if (DateFromEnabled || DateToEnabled)
            {
                if (DateFrom == null)
                {
                    query.Where($"{tableName}.document_date", "<=", DateTo);
                }
                else if (DateTo == null)
                {
                    query.Where($"{tableName}.document_date", ">=", DateFrom);
                }
                else
                {
                    query.WhereBetween($"{tableName}.document_date", DateFrom, DateTo);
                }
            }

            if (query.Clauses.Count > 0)
            {
                return query;
            }

            return null;
        }

        private void ButtonClearOrg_Click(object sender, EventArgs e) => comboOrg.SelectedIndex = -1;
    }
}
