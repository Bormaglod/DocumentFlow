﻿//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.01.2022
//
// Версия 2022.8.17
//  - исправлена процедура ContractValueChanged для корректного скрытия
//    или отображения полей содержащих данные документов 1с
// Версия 2022.8.31
//  - если класс представленый как P имеет атрибут ProductContentAttribute,
//    то для таких объектов правило изменения содержимого в grid-таблице
//    меняется с Update на DeleteAndInsert
// Версия 2022.11.26
//  - добавлено поле для выбора заявки на покупку
//  - параметр autoRefresh метода SetDataSource в классе
//    DataSourceControl был удален. Вместо него используется свойство
//    RefreshMethod этого класса в значении DataRefreshMethod.Immediately
// Версия 2022.12.1
//  - поле purchase добавляется для выбора только при редактировании
//    поступления товара
// Версия 2022.12.3
//  - в функции AsSummary заменён параметр includeDeleted имеющий значение
//    true на options равный SelectOptions.All
// Версия 2022.12.7
//  - метод UpdateCurrencyColumn стал статическим
// Версия 2022.12.11
//  - добавлен запрос на заполнение данными из заявки
// Версия 2022.12.17
//  - вызов GetAllDefault (поле "Заявка на покупку") заменен на GetByContractor
//  - добавлен запрос на запись документа перед заполнением табличной
//    части
//  - добавлена возможность открыть окно с заявкой
// Версия 2023.1.15
//  - удалены ссылки на WaybillProcessing, т.к. WaybillProcessingEditor
//    больше не наследуется от WaybillEditor
//  - таблица теперь корректно размещается в окне
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Controls.Editors;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Entities.Companies;
using DocumentFlow.Entities.Products;
using DocumentFlow.Entities.Products.Core;
using DocumentFlow.Entities.Products.Dialogs;
using DocumentFlow.Entities.PurchaseRequestLib;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Controls;
using DocumentFlow.Infrastructure.Data;

using Microsoft.Extensions.DependencyInjection;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.Reflection;

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
        contractor = CreateDirectorySelectBox<Contractor, IContractorEditor>(x => x.ContractorId, "Контрагент", 120, 400);
        contract = CreateDirectorySelectBox<Contract, IContractEditor>(x => x.ContractId, "Договор", 120, 400);

        waybill_number = CreateTextBox(x => x.WaybillNumber, "Накладная №", 110, 120);
        waybill_number.Dock = DockStyle.Left;
        waybill_number.Width = 235;

        waybill_date = CreateDateTimePicker(x => x.WaybillDate, "от", 25, 170, format: DateTimePickerFormat.Short);
        waybill_date.Dock = DockStyle.Left;
        waybill_date.Width = 200;

        var waybill_panel = CreatePanel(new Control[] { waybill_date, waybill_number });

        var invoice_number = CreateTextBox(x => x.InvoiceNumber, "Счёт-фактура №", 110, 120);
        invoice_number.Dock = DockStyle.Left;
        invoice_number.Width = 235;

        var invoice_date = CreateDateTimePicker(x => x.InvoiceDate, "от", 25, 170, format: DateTimePickerFormat.Short);
        invoice_date.Dock = DockStyle.Left;
        invoice_date.Width = 200;

        invoice_panel = CreatePanel(new Control[] { invoice_date, invoice_number });

        invoice_text = new Label() { Padding = new(10, 0, 0, 0), AutoSize = true, Dock = DockStyle.Left };
        upd = CreateToggleButton(x => x.Upd, "УПД", 40);
        upd.Dock = DockStyle.Left;
        upd.Width = 130;

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
                        repo.GetCustomers(contractor.SelectedItem.Id) :
                        repo.GetSuppliers(contractor.SelectedItem.Id);
                }
            }

            return null;
        });

        waybill_number.ValueChanged += (sender, e) => UpdateInvoiceText();
        waybill_date.ValueChanged += (sender, e) => UpdateInvoiceText();
        upd.ValueChanged += UpdValueChanged;

        details.CreateTableSummaryRow(VerticalPosition.Bottom)
            .AsSummary("ProductCost", SummaryColumnFormat.Currency, SelectOptions.All)
            .AsSummary("TaxValue", SummaryColumnFormat.Currency, SelectOptions.All)
            .AsSummary("FullCost", SummaryColumnFormat.Currency, SelectOptions.All);

        details.AutoGeneratingColumn += (sender, args) =>
        {
            switch (args.Column.MappingName)
            {
                case "Id":
                    args.Cancel = true;
                    break;
                case "ProductName":
                    args.Column.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
                    break;
                case "Code":
                    args.Column.AutoSizeColumnsMode = AutoSizeColumnsMode.AllCells;
                    break;
                case "Amount":
                    args.Column.Width = 100;
                    break;
                case "Price":
                    UpdateCurrencyColumn(args.Column, 100);
                    break;
                case "ProductCost":
                    UpdateCurrencyColumn(args.Column, 140);
                    break;
                case "Tax":
                    args.Column.Width = 80;
                    args.Column.CellStyle.HorizontalAlignment = HorizontalAlignment.Center;
                    break;
                case "TaxValue":
                    UpdateCurrencyColumn(args.Column, 140);
                    break;
                case "FullCost":
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

            var attr = typeof(P).GetCustomAttribute<ProductContentAttribute>();
            if (attr != null && attr.Content == ProductContent.All)
            {
                args.Rule = RuleChange.DeleteAndInsert;
            }
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

        if (typeof(T) == typeof(WaybillReceipt))
        {
            var purchase = CreateDocumentSelectBox<PurchaseRequest, IPurchaseRequestEditor>(x => x.OwnerId, "Заявка на покупку", 120, 400, refreshMethod: DataRefreshMethod.OnOpen);
            purchase.SetDataSource(() =>
            {
                if (contractor.SelectedItem != null)
                {
                    var repo = Services.Provider.GetService<IPurchaseRequestRepository>();
                    return repo!.GetByContractor(contractor.SelectedItem.Id);
                }

                return null;
            });

            purchase.Columns += (sender, e) => PurchaseRequest.CreateGridColumns(e.Columns);

            purchase.ValueChanged += (sender, e) => contract.Enabled = e.NewValue == null;

            purchase.ManualValueChange += (sender, e) =>
            {
                if (MessageBox.Show("Заполнить таблицу по данным заявки?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    contract.Value = e.NewValue?.ContractId;

                    if (IsCreating)
                    {
                        if (MessageBox.Show("Перед заполнением документ должен быть записан. Записать?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            Save();
                        }
                        else
                        {
                            return;
                        }
                    }
                    if (repository is IWaybillReceiptRepository repo && Document is WaybillReceipt doc)
                    {
                        repo.FillProductListFromPurchaseRequest(doc, e.NewValue);
                        details.RefreshDataSource();
                    }
                }
            };

            controls.Add(purchase);
        }

        controls.AddRange(new Control[]
        {
            details,
            doc1c
        });

        AddControls(controls);

        details.BringToFront();
    }

    protected abstract IOwnedRepository<long, P> GetDetailsRepository();

    private static void UpdateCurrencyColumn(GridColumn column, int width)
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
        contract.Value = Document.ContractId;
    }

    private void ContractValueChanged(object? sender, EventArgs e)
    {
        if (contract.SelectedItem == null)
        {
            doc1c.Visible = false;
        }
        else
        {
            bool taxPayer = contract.SelectedItem.TaxPayer;
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