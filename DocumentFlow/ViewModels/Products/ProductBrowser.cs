//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 21.05.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Tools;
using DocumentFlow.Data.Models;
using DocumentFlow.Data.Interfaces.Repository;
using DocumentFlow.Interfaces;
using DocumentFlow.Settings;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.DataGrid.Styles;
using Syncfusion.WinForms.Input.Enums;

using System.ComponentModel;

namespace DocumentFlow.ViewModels;

public abstract class ProductBrowser<T> : BrowserPage<T>
    where T : Product
{
    private readonly IDocumentRefsRepository documentRefs;
    private readonly ThumbnailRowSettings settings;

    public ProductBrowser(
        IServiceProvider services,
        IPageManager pageManager,
        IRepository<Guid, T> repository,
        IDocumentRefsRepository documentRefs,
        IConfiguration configuration,
        IRowHeaderImage rowHeaderImage,
        IBreadcrumb navigator,
        IOptions<LocalSettings> options)
        : base(services, pageManager, repository, configuration, rowHeaderImage, navigator)
    {
        this.documentRefs = documentRefs;

        settings = options.Value.PreviewRows.ThumbnailRow;

        var id = CreateText(x => x.Id, "Id", 180, visible: false);
        var code = CreateText(x => x.Code, "Код", 150);
        var name = CreateText(x => x.ItemName, "Наименование", hidden: false);
        var measurement = CreateText(x => x.MeasurementName, "Ед. изм.", width: 100);
        var price = CreateCurrency(x => x.Price, "Цена", 100);
        var vat = CreateNumeric(x => x.Vat, "НДС", 80, mode: FormatMode.Percent);
        var weight = CreateNumeric(x => x.Weight, "Вес, г", 100, decimalDigits: 3);

        name.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
        measurement.CellStyle.HorizontalAlignment = HorizontalAlignment.Center;
        vat.CellStyle.HorizontalAlignment = HorizontalAlignment.Center;

        AddColumns(new GridColumn[] { id, code, name, measurement, price, vat, weight });

        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [name] = ListSortDirection.Ascending
        });

        CreateGrouping();
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        AllowDrawPreviewRow(height: settings.RowHeight);
    }

    protected override bool OnDrawPreviewRow(Graphics graphics, T row, Rectangle bounds, PreviewRowStyleInfo style)
    {
        var b = graphics.ClipBounds;
        graphics.SetClip(bounds);

        int imageHeight = settings.RowHeight - settings.VerticalBounds * 2;
        int x = bounds.X + settings.LeftIndent;

        if (row.Documents == null)
        {
            row.SetDocuments(documentRefs.GetDocumentsWithThumbnails(row.Id));
        }

        if (row.Documents != null)
        {
            foreach (var t in row.Documents)
            {
                if (t.Thumbnail == null)
                {
                    continue;
                }

                int imageWidth;

                var image = ImageHelper.Base64ToImage(t.Thumbnail);
                if (image.Width != image.Height)
                {
                    imageWidth = imageHeight * image.Width / image.Height;
                }
                else
                {
                    imageWidth = imageHeight;
                }

                var rect = new Rectangle(x, bounds.Y + settings.VerticalBounds, imageWidth, imageHeight);

                graphics.DrawImage(image, rect);

                x += imageWidth + settings.HorizontalBounds;
            }
        }

        // To draw the bottom and right border for the preview row.
        graphics.DrawLine(new Pen(style.Borders.Bottom.Color), new Point(bounds.Left, bounds.Bottom - 1), new Point(bounds.Right, bounds.Bottom - 1));
        graphics.DrawLine(new Pen(style.Borders.Right.Color), new Point(bounds.Right - 1, bounds.Top), new Point(bounds.Right - 1, bounds.Bottom - 1));

        graphics.SetClip(b);
        return true;
    }

    protected override bool CanExpandPreview(T row) => row.Thumbnails;
}
