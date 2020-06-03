//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 10.08.2015
// Time: 10:14
//-----------------------------------------------------------------------

namespace DocumentFlow.Printing
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.IO;
    using System.IO.Packaging;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Markup;
    using System.Windows.Xps.Packaging;
    using DotLiquid;
    using Newtonsoft.Json;
    using NHibernate;
    using Spire.Pdf;
    using DocumentFlow.Core;
    using DocumentFlow.Data.Core;
    using DocumentFlow.Data.Entities;
    using DocumentFlow.DataSchema;
    using DocumentFlow.Printing.Xaml;

    public class Printer
    {
        private IDictionary ownerRow;

        public static void Preview(PrintForm form, ISession session, IDictionary row)
        {
            Printer printer = new Printer();
            printer.PrepareAndPreview(form, session, row);
        }

        private void PrepareAndPreview(PrintForm form, ISession session, IDictionary row)
        {
            ownerRow = row;
            Preview win = new Preview();

            PrintDatasets pd = JsonConvert.DeserializeObject<PrintDatasets>(form.Properties);

            dynamic titleRow = new ExpandoObject().With(Db.ExecuteSelect(session, pd.QueryTitle, null, (x) => row[x]).Single());
            win.Title = titleRow.title;

            Dictionary<string, object> objects = new Dictionary<string, object>();
            foreach (var d in pd.Datasets)
            {
                if (d.UniqueResult)
                {
                    objects.Add(d.Name, Db.ExecuteSelect(session, d.Query, null, (x) => row[x]).Single());
                }
                else
                {
                    List<object> res = new List<object>();
                    foreach (IDictionary item in Db.ExecuteSelect(session, d.Query, null, (x) => ownerRow[x]))
                    {
                        res.Add(item);
                    }

                    if (res.Count > 0)
                        objects.Add(d.Name, res);
                }
            }

            string pdfFileName = (Path.GetTempPath() + win.Title.Replace('/', '-').Replace('\\', '-') + ".pdf");

            Template template = Template.Parse(form.FormText);
            string xml = template.Render(Hash.FromDictionary(objects));

            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            {
                FlowDocument doc = XamlReader.Load(stream) as FlowDocument;
                if (doc != null)
                {
                    doc.PagePadding = new Thickness(
                        UnitConverter.ConvertCentimeter(2, GraphicsUnit.Display),
                        UnitConverter.ConvertCentimeter(1, GraphicsUnit.Display),
                        UnitConverter.ConvertCentimeter(1, GraphicsUnit.Display),
                        UnitConverter.ConvertCentimeter(1, GraphicsUnit.Display));
                    doc.PageWidth = UnitConverter.ConvertCentimeter(21f, GraphicsUnit.Display);
                    doc.PageHeight = UnitConverter.ConvertCentimeter(29.7f, GraphicsUnit.Display);
                    doc.ColumnWidth = doc.PageWidth;

                    // http://stackoverflow.com/questions/14903362/keeping-bindings-when-putting-a-flowdocument-through-pagination
                    doc.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.SystemIdle, new Action(() => { }));

                    Uri uri = new Uri(@"memorystream://PreviewTemporary.xps");
                    Package docPackage = PackageStore.GetPackage(uri);
                    if (docPackage != null)
                    {
                        PackageStore.RemovePackage(uri);
                    }

                    DocumentPaginator paginator = ((IDocumentPaginatorSource)doc).DocumentPaginator;
                    using (MemoryStream xpsStream = new MemoryStream())
                    {
                        docPackage = Package.Open(xpsStream, FileMode.Create, FileAccess.ReadWrite);
                        PackageStore.AddPackage(uri, docPackage);

                        XpsDocument xpsDoc = new XpsDocument(docPackage, CompressionOption.Maximum);

                        xpsDoc.Uri = uri;
                        XpsDocument.CreateXpsDocumentWriter(xpsDoc).Write(paginator);

                        docPackage.Flush();

                        xpsStream.Position = 0;

                        PdfDocument pdfDoc = new PdfDocument();
                        pdfDoc.LoadFromXPS(xpsStream);
                        using (var pdfStream = new FileStream(pdfFileName, FileMode.Create))
                        {
                            pdfDoc.SaveToStream(pdfStream);
                        }

                        ((DocumentViewer)win.FindName("docViewer")).Document = xpsDoc.GetFixedDocumentSequence();

                        xpsDoc.Close();
                    }

                    win.ShowDialog();
                }
            }

            if (File.Exists(pdfFileName))
                File.Delete(pdfFileName);
        }
    }
}
