//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.12.2021
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Repository;
using DocumentFlow.Data.Models;
using DocumentFlow.Settings;
using DocumentFlow.Tools;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Syncfusion.WinForms.DataGrid;

using WinEnums = System.Windows.Forms;

namespace DocumentFlow.ViewModels;

public class MaterialBrowser : ProductBrowser<Material>, IMaterialBrowser
{
    public MaterialBrowser(
        IServiceProvider services, 
        IMaterialRepository repository, 
        IDocumentRefsRepository documentRefs,
        IConfiguration configuration,
        IProductRowHeader productRowHeader,
        IBreadcrumb navigator,
        IOptions<LocalSettings> options) 
        : base(services, repository, documentRefs, configuration, productRowHeader, navigator, options)
    {
        var min_order = CreateNumeric(x => x.MinOrder, "Мин. заказ", 100, decimalDigits: 3);
        var ext_article = CreateText(x => x.ExtArticle, "Доп. артикул", 120);
        var cross = CreateText(x => x.CrossName, "Кросс-артикул", 120);
        var kind = CreateText(x => x.MaterialKindName, "Тип материала", 100, visible: false);

        min_order.CellStyle.HorizontalAlignment = WinEnums.HorizontalAlignment.Right;

        AddColumns(new GridColumn[] { min_order, ext_article, cross, kind });

        var db = services.GetRequiredService<IDatabase>();
        if (db.HasPrivilege("materials", Privilege.Select))
        {
            var balance = CreateNumeric(x => x.ProductBalance, "Тек. остаток", 120, decimalDigits: 3);
            var material_using = CreateBoolean(x => x.MaterialUsing, "Используется", 120);
            var status = CreateImage(x => x.PriceStatus, "Статус цены", 100);

            balance.CellStyle.HorizontalAlignment = WinEnums.HorizontalAlignment.Right;

            AddColumns(new GridColumn[] { balance, material_using, status });
        }
    }

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
