﻿//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.12.2021
//
// Версия 2022.9.3
//  - удалены методы IsColumnVisible, IsAllowVisibilityColumn,
//    AvailableWireColumn и OnChangeParent
//  - удалена колонка "Тип провода"
// Версия 2023.1.8
//  - в конструктор добавлен параметр settings
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//  - DocumentFlow.Settings.Infrastructure перемещено в DocumentFlow.Infrastructure.Settings
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Controls;
using DocumentFlow.Infrastructure.Data;
using DocumentFlow.Infrastructure.Settings;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.Input.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.Products;

public class MaterialBrowser : ProductBrowser<Material>, IMaterialBrowser
{
    public MaterialBrowser(IMaterialRepository repository, IPageManager pageManager, IProductRowHeader productRowHeader, IBreadcrumb navigator, IStandaloneSettings settings) 
        : base(repository, pageManager, productRowHeader, navigator: navigator, settings: settings)
    {
        var id = CreateText(x => x.Id, "Id", 180, visible: false);
        var code = CreateText(x => x.Code, "Код", 150);
        var name = CreateText(x => x.ItemName, "Наименование", hidden: false);
        var measurement = CreateText(x => x.MeasurementName, "Ед. изм.", width: 100);
        var price = CreateCurrency(x => x.Price, "Цена", 100);
        var vat = CreateNumeric(x => x.Vat, "НДС", 80, mode: FormatMode.Percent);
        var weight = CreateNumeric(x => x.Weight, "Вес, г", 100, decimalDigits: 3);
        var min_order = CreateNumeric(x => x.MinOrder, "Мин. заказ", 100, decimalDigits: 3);
        var ext_article = CreateText(x => x.ExtArticle, "Доп. артикул", 120);
        var cross = CreateText(x => x.CrossName, "Кросс-артикул", 120);

        name.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
        measurement.CellStyle.HorizontalAlignment = HorizontalAlignment.Center;
        vat.CellStyle.HorizontalAlignment = HorizontalAlignment.Center;
        min_order.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;

        AddColumns(new GridColumn[] { id, code, name, measurement, price, vat, weight, min_order, ext_article, cross });

        if (repository.HasPrivilege("materials", Privilege.Select))
        {
            var balance = CreateNumeric(x => x.ProductBalance, "Тек. остаток", 120, decimalDigits: 3);
            var material_using = CreateBoolean(x => x.MaterialUsing, "Используется", 120);
            var status = CreateImage(x => x.PriceStatus, "Статус цены", 100);

            balance.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;

            AddColumns(new GridColumn[] { balance, material_using, status });
        }

        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [name] = ListSortDirection.Ascending
        });

        AllowDrawPreviewRow(height: 125);
    }

    protected override string HeaderText => "Материалы";

    protected override void BrowserImageStyle(Material document, string column, ImageCellStyle style)
    {
        base.BrowserImageStyle(document, column, style);
        if (column == nameof(document.PriceStatus))
        {
            if (document.IsFolder)
            {
                style.DisplayText = string.Empty;
            }
            else
            {
                switch (document.PriceStatus)
                {
                    case 0:
                        style.Image = Properties.Resources.perfect;
                        style.DisplayText = "Действующая";
                        break;
                    case 1:
                        style.Image = Properties.Resources.sufficient;
                        style.DisplayText = "Ручной ввод";
                        break;
                    case 2:
                        style.Image = Properties.Resources.insufficient;
                        style.DisplayText = "Устаревшая";
                        break;
                    default:
                        break;
                }

                style.TextImageRelation = TextImageRelation.ImageBeforeText;
            }
        }
    }
}
