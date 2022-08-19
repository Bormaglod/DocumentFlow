﻿//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.01.2022
//
// Версия 2022.8.17
//  - исправлена процедура ContractValueChanged для корректного скрытия
//    или отображения полей содержащих данные документов 1с
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Editors;
using DocumentFlow.Controls.Infrastructure;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Entities.Companies;
using DocumentFlow.Entities.Productions.Order;
using DocumentFlow.Entities.Productions.Processing;
using DocumentFlow.Entities.Products;
using DocumentFlow.Entities.Products.Dialogs;
using DocumentFlow.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

namespace DocumentFlow.Entities.Waybills;

public abstract class WaybillEditor<T, P> : DocumentEditor<T>
    where T : Waybill, new()
    where P : ProductPrice, new()
{
    private readonly DfDirectorySelectBox<Contractor> contractor;
    private readonly DfDirectorySelectBox<Contract> contract;
    private readonly Panel upd_panel;
    private readonly Panel invoice_panel;
    private readonly DfToggleButton upd;
    private readonly DfPanel doc1c;
    private readonly Label invoice_text;
    private readonly DfTextBox waybill_number;
    private readonly DfDateTimePicker waybill_date;

    public WaybillEditor(IRepository<Guid, T> repository, IPageManager pageManager) : base(repository, pageManager, true)
    {
        contractor = new DfDirectorySelectBox<Contractor>("contractor_id", "Контрагент", 80, 400)
        {
            OpenAction = (t) => pageManager.ShowEditor<IContractorEditor, Contractor>(t)
        };

        contract = new DfDirectorySelectBox<Contract>("contract_id", "Договор", 80, 400)
        {
            OpenAction = (t) => pageManager.ShowEditor<IContractEditor, Contract>(t)
        };

        DfDocumentSelectBox<ProductionOrder>? order = null;
        if (typeof(T) == typeof(WaybillProcessing))
        {
            order = new DfDocumentSelectBox<ProductionOrder>("owner_id", "Заказ", 80, 400)
            {
                OpenAction = (t) => pageManager.ShowEditor<IProductionOrderEditor, ProductionOrder>(t)
            };

            order.SetDataSource(() =>
            {
                var repo = Services.Provider.GetService<IProductionOrderRepository>();
                return repo!.GetAllDefault(callback: q => q
                    .WhereFalse("production_order.deleted")
                    .WhereTrue("carried_out")
                    .WhereFalse("closed"));
            });

            order.Columns += (sender, e) => ProductionOrder.CreateGridColumns(e.Columns);
        }

        waybill_number = new DfTextBox("waybill_number", "Накладная №", 110, 120) { Dock = DockStyle.Left, Width = 235 };
        waybill_date = new DfDateTimePicker("waybill_date", "от", 25, 170) { Dock = DockStyle.Left, Width = 200, Format = DateTimePickerFormat.Short };
        var waybill_panel = CreatePanel(new Control[] { waybill_date, waybill_number });

        var invoice_number = new DfTextBox("invoice_number", "Счёт-фактура №", 110, 120) { Dock = DockStyle.Left, Width = 235 };
        var invoice_date = new DfDateTimePicker("invoice_date", "от", 25, 170) { Dock = DockStyle.Left, Width = 200, Format = DateTimePickerFormat.Short };
        invoice_panel = CreatePanel(new Control[] { invoice_date, invoice_number });

        invoice_text = new Label() { Padding = new(10, 0, 0, 0), AutoSize = true, Dock = DockStyle.Left };
        upd = new DfToggleButton("upd", "УПД", 40) { Dock = DockStyle.Left, Width = 130 };
        upd_panel = CreatePanel(new Control[] { invoice_text, upd });

        doc1c = new DfPanel()
        {
            Header = "Докуметы 1С",
            Height = 144,
            Dock = DockStyle.Bottom,
            Padding = new Padding(0, 10, 0, 0),
            Visible = false
        };

        doc1c.AddControls(new Control[] { waybill_panel, invoice_panel, upd_panel });

        var details = new DfDataGrid<P>(GetDetailsRepository()) { Dock = DockStyle.Fill };

        contractor.ValueChanged += ContractorValueChanged;
        contractor.SetDataSource(() =>
        {
            var repo = Services.Provider.GetService<IContractorRepository>();
            if (repo != null)
            {
                return typeof(T) == typeof(WaybillSale) ? repo.GetCustomers() : repo.GetSuppliers();
            }

            return null;
        });

        contract.ValueChanged += ContractValueChanged;
        contract.SetDataSource(() =>
        {
            if (contractor.SelectedItem != null)
            {
                var repo = Services.Provider.GetService<IContractRepository>();
                if (repo != null)
                {
                    return typeof(T) == typeof(WaybillSale) ?
                        repo.GetCustomers(contractor.SelectedItem.id) :
                        repo.GetSuppliers(contractor.SelectedItem.id);
                }
            }

            return null;
        });

        waybill_number.ValueChanged += (sender, e) => UpdateInvoiceText();
        waybill_date.ValueChanged += (sender, e) => UpdateInvoiceText();
        upd.ValueChanged += UpdValueChanged;

        details.CreateTableSummaryRow(VerticalPosition.Bottom)
            .AsSummary("product_cost", SummaryColumnFormat.Currency, true)
            .AsSummary("tax_value", SummaryColumnFormat.Currency, true)
            .AsSummary("full_cost", SummaryColumnFormat.Currency, true);

        details.AutoGeneratingColumn += (sender, args) =>
        {
            switch (args.Column.MappingName)
            {
                case "id":
                    args.Cancel = true;
                    break;
                case "product_name":
                    args.Column.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
                    break;
                case "code":
                    args.Column.AutoSizeColumnsMode = AutoSizeColumnsMode.AllCells;
                    break;
                case "amount":
                    args.Column.Width = 100;
                    break;
                case "price":
                    UpdateCurrencyColumn(args.Column, 100);
                    break;
                case "product_cost":
                    UpdateCurrencyColumn(args.Column, 140);
                    break;
                case "tax":
                    args.Column.Width = 80;
                    args.Column.CellStyle.HorizontalAlignment = HorizontalAlignment.Center;
                    break;
                case "tax_value":
                    UpdateCurrencyColumn(args.Column, 140);
                    break;
                case "full_cost":
                    UpdateCurrencyColumn(args.Column, 140);
                    break;
            }
        };

        details.DataCreate += (sender, args) =>
        {
            args.Cancel = FormProductPrice<P>.Create(args.CreatingData, contract.SelectedItem) == DialogResult.Cancel;
        };

        details.DataEdit += (sender, args) =>
        {
            args.Cancel = FormProductPrice<P>.Edit(args.EditingData, contract.SelectedItem) == DialogResult.Cancel;
        };

        details.DataCopy += (sender, args) =>
        {
            args.Cancel = FormProductPrice<P>.Edit(args.CopiedData, contract.SelectedItem) == DialogResult.Cancel;
        };

        List<Control> controls = new()
        {
            contractor,
            contract
        };

        if (order != null)
        {
            controls.Add(order);
        }

        controls.AddRange(new Control[]
        {
            details,
            doc1c
        });

        AddControls(controls);
    }

    protected abstract IOwnedRepository<long, P> GetDetailsRepository();

    private void UpdateCurrencyColumn(GridColumn column, int width)
    {
        if (column is GridNumericColumn c)
        {
            c.FormatMode = Syncfusion.WinForms.Input.Enums.FormatMode.Currency;
        }

        column.Width = width;
        column.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
    }

    private void ContractorValueChanged(object? sender, EventArgs e)
    {
        contract.RefreshDataSource();
        contract.Value = Document.contract_id;
    }

    private void ContractValueChanged(object? sender, EventArgs e)
    {
        bool taxPayer = false;
        if (contract.SelectedItem == null)
        {
            doc1c.Visible = false;
        }
        else
        {
            taxPayer = contract.SelectedItem.tax_payer;
            upd_panel.Visible = taxPayer;
            if (taxPayer)
            {
                invoice_panel.Visible = !upd.ToggleValue;
            }
            else
            {
                invoice_panel.Visible = false;
            }

            int height = 80;
            if (taxPayer)
            {
                height += 32;
                if (!upd.ToggleValue)
                {
                    height += 32;
                }
            }

            doc1c.Height = height;

            doc1c.Visible = taxPayer || contract.SelectedItem.ContractorType == ContractorType.Seller;
        }
    }

    private void UpdValueChanged(object? sender, EventArgs e)
    {
        invoice_panel.Visible = !upd.ToggleValue;
        invoice_text.Visible = upd.ToggleValue;
        doc1c.Height = upd.ToggleValue ? 112 : 144;

        UpdateInvoiceText();
    }

    private void UpdateInvoiceText()
    {
        if (waybill_number.Value == null || waybill_date.Value == null)
        {
            invoice_text.Text = $"Счёт-фактура №? от ?";
        }
        else
        {
            invoice_text.Text = $"Счёт-фактура №{waybill_number.Value} от {waybill_date.Value:d}";
        }
    }
}