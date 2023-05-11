//-----------------------------------------------------------------------
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

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Dialogs;
using DocumentFlow.Entities.Companies;
using DocumentFlow.Entities.Products;
using DocumentFlow.Entities.Products.Core;
using DocumentFlow.Entities.PurchaseRequestLib;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Controls;
using DocumentFlow.Infrastructure.Data;

using Microsoft.Extensions.DependencyInjection;

using Syncfusion.WinForms.DataGrid.Enums;

using System.Reflection;

namespace DocumentFlow.Entities.Waybills;

public abstract class WaybillEditor<T, P, R> : DocumentEditor<T>
    where T : Waybill, new()
    where P : ProductPrice, new()
    where R : IOwnedRepository<long, P>
{
    private readonly IRepository<Guid, T> repository;

    public WaybillEditor(IRepository<Guid, T> repository, IPageManager pageManager) : base(repository, pageManager, true)
    {
        this.repository = repository;

        EditorControls
            .AddDirectorySelectBox<Contractor>(x => x.ContractorId, "Контрагент", select => select
                .EnableEditor<IContractorEditor>()
                .SetDataSource(GetContractors)
                .DirectoryChanged(ContractorValueChanged)
                .SetHeaderWidth(120)
                .SetEditorWidth(400))
            .AddDirectorySelectBox<Contract>(x => x.ContractId, "Договор", select => select
                .EnableEditor<IContractEditor>()
                .SetDataSource(GetContracts)
                .DirectoryChanged(ContractValueChanged)
                .SetHeaderWidth(120)
                .SetEditorWidth(400))
            .If(typeof(T) == typeof(WaybillReceipt), controls => controls
                .AddDocumentSelectBox<PurchaseRequest>(x => x.OwnerId, "Заявка на покупку", select => select
                    .SetDataSource(GetPurchaseRequests, DataRefreshMethod.OnOpen)
                    .CreateColumns(PurchaseRequest.CreateGridColumns)
                    .DocumentChanged(PurchaseChanged)
                    .DocumentSelected(PurchaseSelected)
                    .SetHeaderWidth(120)
                    .SetEditorWidth(400)))
            .AddPanel(panel => panel
                .SetName("Doc1C")
                .ShowHeader("Докуметы 1С")
                .SetHeight(144)
                .SetDock(DockStyle.Bottom)
                .SetVisible(false)
                .AddControls(controls => controls
                    .AddPanel(panel => panel
                        .SetHeight(32)
                        .AddControls(controls => controls
                            .AddTextBox(x => x.WaybillNumber, "Накладная №", text => text
                                .TextChanged(WaybillNumberChanged)
                                .SetHeaderWidth(110)
                                .SetEditorWidth(120)
                                .SetDock(DockStyle.Left)
                                .SetWidth(235))
                            .AddDateTimePicker(x => x.WaybillDate, "от", date => date
                                .DateChanged(WaybillDateChanged)
                                .SetFormat(DateTimePickerFormat.Short)
                                .SetHeaderWidth(25)
                                .SetEditorWidth(170)
                                .SetDock(DockStyle.Left)
                                .SetWidth(200))))
                    .AddPanel(panel => panel
                        .SetName("Invoice")
                        .SetHeight(32)
                        .AddControls(controls => controls
                            .AddTextBox(x => x.InvoiceNumber, "Счёт-фактура №", text => text
                                .SetHeaderWidth(110)
                                .SetEditorWidth(120)
                                .SetDock(DockStyle.Left)
                                .SetWidth(235))
                            .AddDateTimePicker(x => x.InvoiceDate, "от", date => date
                                .SetFormat(DateTimePickerFormat.Short)
                                .SetHeaderWidth(25)
                                .SetEditorWidth(170)
                                .SetDock(DockStyle.Left)
                                .SetWidth(200))))
                    .AddPanel(panel => panel
                        .SetName("UPD")
                        .SetHeight(32)
                        .AddControls(controls => controls
                            .AddToggleButton(x => x.Upd, "УПД", toggle => toggle
                                .ToggleChanged(UpdValueChanged)
                                .SetHeaderWidth(40)
                                .SetDock(DockStyle.Left)
                                .SetWidth(130))
                            .AddLabel(string.Empty, label => label
                                .SetDock(DockStyle.Fill))))))
            .AddDataGrid<P>(grid => grid
                .SetRepository<R>()
                .GridSummaryRow(VerticalPosition.Bottom, row => row
                    .AsSummary(x => x.ProductCost, SummaryColumnFormat.Currency, SelectOptions.All)
                    .AsSummary(x => x.TaxValue, SummaryColumnFormat.Currency, SelectOptions.All)
                    .AsSummary(x => x.FullCost, SummaryColumnFormat.Currency, SelectOptions.All))
                .DoCreate(DataCreate)
                .DoUpdate(DataEdit)
                .DoCopy(DataCopy)
                .SetDock(DockStyle.Fill));
    }

    private void PurchaseChanged(PurchaseRequest? newValue)
    {
        var contract = EditorControls.GetControl<IDirectorySelectBoxControl<Contract>>(x => x.ContractId);
        contract.SetEnabled(newValue == null);
    }

    private void PurchaseSelected(PurchaseRequest? newValue)
    {
        var contract = EditorControls.GetControl<IDirectorySelectBoxControl<Contract>>(x => x.ContractId);
        if (MessageBox.Show("Заполнить таблицу по данным заявки?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        {
            if (contract is IDataSourceControl<Guid, Contract> contractSource)
            {
                contractSource.Select(newValue?.ContractId);
            }

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
                repo.FillProductListFromPurchaseRequest(doc, newValue);

                var details = EditorControls.GetControl<IDataGridControl<P>>();
                if (details is IDataSourceControl detailSource)
                {
                    detailSource.RefreshDataSource();
                }
                
            }
        }
    }

    private bool DataCreate(P data)
    {
        var contract = EditorControls.GetControl<IDirectorySelectBoxControl<Contract>>(x => x.ContractId);
        ProductPriceDialog<P> form = new(contract.SelectedItem);
        return form.Create(data);
    }

    private DataOperationResult DataEdit(P data)
    {
        var contract = EditorControls.GetControl<IDirectorySelectBoxControl<Contract>>(x => x.ContractId);

        ProductPriceDialog<P> form = new(contract.SelectedItem);
        if (form.Edit(data))
        {
            var attr = typeof(P).GetCustomAttribute<ProductContentAttribute>();
            if (attr != null && attr.Content == ProductContent.All)
            {
                return DataOperationResult.DeleteAndInsert;
            }
            else
            {
                return DataOperationResult.Update;
            }
        }
        else
        {
            return DataOperationResult.Cancel;
        }
    }

    private bool DataCopy(P data)
    {
        var contract = EditorControls.GetControl<IDirectorySelectBoxControl<Contract>>(x => x.ContractId);
        ProductPriceDialog<P> form = new(contract.SelectedItem);
        return form.Edit(data);
    }

    private IEnumerable<Contractor>? GetContractors()
    {
        var repo = Services.Provider.GetService<IContractorRepository>();
        if (repo != null)
        {
            return typeof(T) == typeof(WaybillSale) ? repo.GetCustomers() : repo.GetSuppliers();
        }

        return null;
    }

    private IEnumerable<PurchaseRequest>? GetPurchaseRequests() 
    {
        var contractor = EditorControls.GetControl<IDirectorySelectBoxControl<Contractor>>(x => x.ContractorId);
        if (contractor.SelectedItem != null)
        {
            var repo = Services.Provider.GetService<IPurchaseRequestRepository>();
            return repo!.GetByContractor(contractor.SelectedItem.Id);
        }

        return null;
    }

    private IEnumerable<Contract>? GetContracts()
    {
        var contractor = EditorControls.GetControl<IDirectorySelectBoxControl<Contractor>>(x => x.ContractorId);
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
    }

    private void ContractorValueChanged(Contractor? value)
    {
        var contract = EditorControls.GetControl<IDirectorySelectBoxControl<Contract>>(x => x.ContractId);
        if (contract is IDataSourceControl<Guid, Contract> source) 
        {
            source.RefreshDataSource(Document.ContractId);
        }
    }

    private void ContractValueChanged(Contract? value)
    {
        var doc1c = EditorControls.GetContainer("Doc1C");
        var invoice_panel = EditorControls.GetContainer("Invoice");
        var upd_panel = EditorControls.GetContainer("UPD");

        var upd = EditorControls.GetControl<IToggleButtonControl>(x => x.Upd);

        if (value == null)
        {
            doc1c.SetVisible(false);
        }
        else
        {
            bool taxPayer = value.TaxPayer;
            upd_panel.SetVisible(taxPayer);
            if (taxPayer)
            {
                invoice_panel.SetVisible(!upd.ToggleValue);
            }
            else
            {
                invoice_panel.SetVisible(false);
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

            doc1c.SetHeight(height);
            doc1c.SetVisible(taxPayer || value.ContractorType == ContractorType.Seller);
        }
    }

    private void UpdValueChanged(bool value)
    {
        EditorControls.GetContainer("Invoice").SetVisible(!value);
        EditorControls.GetContainer("Doc1C").SetHeight(value ? 112 : 144);

        EditorControls.GetControl<ILabelControl>().SetVisible(value);

        if (value)
        {
            var waybill_date = EditorControls.GetControl<IDateTimePickerControl>(x => x.WaybillDate);
            var number = EditorControls.GetControl<ITextBoxControl>(x => x.WaybillNumber).Text;
            UpdateInvoiceText(number, waybill_date.Value);
        }
    }

    private void WaybillNumberChanged(string? number)
    {
        var waybill_date = EditorControls.GetControl<IDateTimePickerControl>(x => x.WaybillDate);
        UpdateInvoiceText(number, waybill_date.Value);
    }

    private void WaybillDateChanged(DateTime? date)
    {
        var number = EditorControls.GetControl<ITextBoxControl>(x => x.WaybillNumber).Text;
        UpdateInvoiceText(number, date);
    }

    private void UpdateInvoiceText(string? number, DateTime? date)
    {
        string text = $"Счёт-фактура №{number ?? "?"} от {(date == null ? "?" : date.Value.ToString("d"))}";
        EditorControls.GetControl<ILabelControl>().SetText(text);
    }
}