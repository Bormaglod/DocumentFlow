//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 21.05.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Infrastructure;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Core;
using DocumentFlow.Data.Core.Repository;
using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

using Syncfusion.WinForms.DataGrid.Styles;

namespace DocumentFlow.Entities.Products;

public abstract class ProductBrowser<T> : Browser<T>
    where T : Product
{
    public ProductBrowser(IRepository<Guid, T> repository, IPageManager pageManager, IProductRowHeader productRowHeader, IBreadcrumb navigator) 
        : base(repository, pageManager, rowHeaderImage: productRowHeader, navigator: navigator)
    {
        AllowGrouping();
    }

    protected override bool OnDrawPreviewRow(Graphics graphics, T row, Rectangle bounds, PreviewRowStyleInfo style)
    {
        if (CanExpandPreview(row))
        {
            var b = graphics.ClipBounds;
            graphics.SetClip(bounds);

            int x = bounds.X + 50;

            if (row.Documents == null)
            {
                var docs = Services.Provider.GetService<IDocumentRefsRepository>();
                if (docs != null)
                {
                    row.SetDocuments(docs.GetByOwner(row.id, callback: q => q.WhereNotNull("thumbnail")));
                }
            }

            if (row.Documents != null)
            {
                foreach (var t in row.Documents)
                {
                    var image = ImageHelper.Base64ToImage(t.thumbnail!);
                    var rect = new Rectangle(x, bounds.Y + 2, 120, 120);

                    graphics.DrawImage(image, rect);

                    x += 130;
                }
            }

            // To draw the bottom and right border for the preview row.
            graphics.DrawLine(new Pen(style.Borders.Bottom.Color), new Point(bounds.Left, bounds.Bottom - 1), new Point(bounds.Right, bounds.Bottom - 1));
            graphics.DrawLine(new Pen(style.Borders.Right.Color), new Point(bounds.Right - 1, bounds.Top), new Point(bounds.Right - 1, bounds.Bottom - 1));

            graphics.SetClip(b);
            return true;
        }

        return false;
    }

    protected override bool CanExpandPreview(T row) => row.thumbnails;
}
