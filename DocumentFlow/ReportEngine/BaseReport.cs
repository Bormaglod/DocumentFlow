//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 27.06.2022
//-----------------------------------------------------------------------

using DocumentFlow.Core;

using FastReport;
using FastReport.Export.Image;

using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout;

using System.IO;

namespace DocumentFlow.ReportEngine;

public class BaseReport
{
    public static string CreatePdfDocument(Report report)
    {
        ImageExport exp = new();

        FileHelper.DeleteTempFiles("Reports");

        string path = Path.Combine(Path.GetTempPath(), "DocumentFlow", "Reports");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        exp.ImageFormat = ImageExportFormat.Png;
        exp.Resolution = Properties.Settings.Default.ReportResolution;
        exp.Export(report, Path.Combine(path, Guid.NewGuid() + ".png"));

        var pdfFile = Path.Combine(path, Guid.NewGuid() + ".pdf");

        PdfDocument pdf = new(new PdfWriter(pdfFile));
        Document document = new(pdf);
        document.SetMargins(0, 0, 0, 0);

        foreach (string fileName in exp.GeneratedFiles)
        {
            var data = ImageDataFactory.Create(fileName);
            var image = new iText.Layout.Element.Image(data);

            var size = new iText.Kernel.Geom.PageSize(
                image.GetImageWidth().DpiToPoint(exp.ResolutionX),
                image.GetImageHeight().DpiToPoint(exp.ResolutionY));
            pdf.AddNewPage(size);

            document.Add(image);
        }

        document.Close();

        return pdfFile;
    }
}
