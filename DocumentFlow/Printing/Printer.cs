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
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.IO.Packaging;
    using System.Linq;
    using System.Text;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Markup;
    using System.Windows.Xps.Packaging;
    using Dapper;
    using DotLiquid;
    using Spire.Pdf;
    using DocumentFlow.Core;
    using DocumentFlow.Data;
    using DocumentFlow.Data.Entities;
    using DocumentFlow.Printing.Core;

    public class Printer
    {
        public class PrintDataset
        {
            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("sql-query")]
            public string Query { get; set; }

            [DefaultValue(false)]
            [JsonPropertyName("unique-result")]
            public bool UniqueResult { get; set; }
        }

        public class PrintDatasets
        {
            [JsonPropertyName("title")]
            public string QueryTitle { get; set; }

            [JsonPropertyName("datasets")]
            public IList<PrintDataset> Datasets { get; set; }
        }

        public static void Preview(PrintForm form, object entity)
        {
            Printer printer = new Printer();
            printer.PrepareAndPreview(form, entity);
        }

        private void PrepareAndPreview(PrintForm form, object entity)
        {
            Preview win = new Preview();

            PrintDatasets pd = JsonSerializer.Deserialize<PrintDatasets>(form.properties);

            var objects = new Dictionary<string, object>();

            using (var conn = Db.OpenConnection())
            {
                win.Title = conn.Query<string>(pd.QueryTitle, entity).Single();

                foreach (var d in pd.Datasets)
                {
                    if (d.UniqueResult)
                    {
                        var t = conn.Query(d.Query, entity).Single();
                        objects.Add(d.Name, t);
                    }
                    else
                    {
                        List<object> list = new List<object>();
                        foreach (var item in conn.Query(d.Query, entity))
                        {
                            if (item is IDictionary<string, object> dict)
                            {
                                var res = new Dictionary<string, object>();
                                foreach (var key in dict.Keys)
                                {
                                    res.Add(key.ToString(), dict[key]);
                                }

                                list.Add(res);
                            }
                        }

                        objects.Add(d.Name, list);
                    }
                }
            }

            string pdfFileName = (Path.GetTempPath() + win.Title.Replace('/', '-').Replace('\\', '-') + ".pdf");

            Template template = Template.Parse(form.form_text);
            string xml = template.Render(Hash.FromDictionary(objects));

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            {
                if (XamlReader.Load(stream) is FlowDocument doc)
                {
                    doc.PagePadding = new Thickness(
                        UnitConverter.Convert(2, GraphicsUnit.Centimeter, GraphicsUnit.Display),
                        UnitConverter.Convert(1, GraphicsUnit.Centimeter, GraphicsUnit.Display),
                        UnitConverter.Convert(1, GraphicsUnit.Centimeter, GraphicsUnit.Display),
                        UnitConverter.Convert(1, GraphicsUnit.Centimeter, GraphicsUnit.Display));
                    doc.PageWidth = UnitConverter.Convert(21f, GraphicsUnit.Centimeter, GraphicsUnit.Display);
                    doc.PageHeight = UnitConverter.Convert(29.7f, GraphicsUnit.Centimeter, GraphicsUnit.Display);
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
                    using (var xpsStream = new MemoryStream())
                    {
                        docPackage = Package.Open(xpsStream, FileMode.Create, FileAccess.ReadWrite);
                        PackageStore.AddPackage(uri, docPackage);

                        var xpsDoc = new XpsDocument(docPackage, CompressionOption.Maximum)
                        {
                            Uri = uri
                        };

                        XpsDocument.CreateXpsDocumentWriter(xpsDoc).Write(paginator);

                        docPackage.Flush();

                        xpsStream.Position = 0;

                        var pdfDoc = new PdfDocument();
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
