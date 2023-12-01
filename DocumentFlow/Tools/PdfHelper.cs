//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 27.06.2022
//-----------------------------------------------------------------------

using FastReport;
using FastReport.Export.Image;

using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout;

using System.IO;

namespace DocumentFlow.Tools;

public enum PdfNamingStrategy { Guid, DateTime }

public class PdfHelper
{
    public static string CreateDocument(Report report, int reportResolution, PdfNamingStrategy namingStrategy)
    {
        var pdfFile = GenerateFileName(namingStrategy);
        var path = Path.GetDirectoryName(pdfFile) ?? string.Empty;

        ImageExport exp = new()
        {
            ImageFormat = ImageExportFormat.Png,
            Resolution = reportResolution
        };

        exp.Export(report, Path.Combine(path, Guid.NewGuid() + ".png"));

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

    public static string CreateDocument(IList<Image> images, PdfNamingStrategy namingStrategy)
    {
        var pdfFile = GenerateFileName(namingStrategy);

        PdfDocument pdf = new(new PdfWriter(pdfFile));
        Document document = new(pdf);
        document.SetMargins(0, 0, 0, 0);

        foreach (var image in images)
        {
            var data = ImageDataFactory.Create(ImageToByteArray(image));
            var pdfImage = new iText.Layout.Element.Image(data);

            var size = new iText.Kernel.Geom.PageSize(
                pdfImage.GetImageWidth().DpiToPoint(image.HorizontalResolution),
                pdfImage.GetImageHeight().DpiToPoint(image.VerticalResolution));
            pdf.AddNewPage(size);

            document.Add(pdfImage);
        }

        document.Close();

        return pdfFile;
    }

    private static byte[] ImageToByteArray(Image imageIn)
    {
        using var ms = new MemoryStream();
        imageIn.Save(ms, imageIn.RawFormat);
        return ms.ToArray();
    }

    private static string GenerateFileName(PdfNamingStrategy namingStrategy)
    {
        FileHelper.DeleteTempFiles("Reports");

        string path = Path.Combine(Path.GetTempPath(), "DocumentFlow", "Reports");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        return namingStrategy switch
        {
            PdfNamingStrategy.Guid => Path.Combine(path, Guid.NewGuid() + ".pdf"),
            PdfNamingStrategy.DateTime => $"SCN_{DateTime.Today:yyyyMMdd}_{(int)(DateTime.Now - DateTime.Today).TotalSeconds}",
            _ => throw new NotImplementedException()
        };
    }
}
