//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.01.2021
// Time: 23:36
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using DocumentFlow.Core;

namespace DocumentFlow.Reports
{
    public class ReportPage
    {
        private int current = 1;
        private float currentBandTop = 0;
        private PageSize pageSize;
        private Margin marginSize;
        private PdfPage currentPage;

        public ReportPage()
        {
            pageSize = new PageSize();
            marginSize = new Margin(pageSize);
        }

        public Report Report { get; set; }

        public string Title { get; set; }

        public PageSize PageSize 
        {
            get => pageSize;
            set
            {
                pageSize = value;
                marginSize.SetSize(pageSize);
            }
        }

        public Margin MarginSize 
        {
            get => marginSize;
            set
            {
                marginSize = value;
                marginSize.SetSize(pageSize);
            }
        }

        public Band ReportTitleBand { get; set; }
        public Band PageHeaderBand { get; set; }
        public List<DataBandComplex> DataBands { get; set; }
        public Band PageFooterBand { get; set; }
        public Band ReportSummary { get; set; }
        public int Current => current;
        public float CurrentBandTop => currentBandTop;

        public void GeneratePdf(PdfDocument doc)
        {
            float maxHeight = GetMaxDataBandHeight(2);
            for (int i = 0; i < DataBands.Count; i++)
            {
                if (DataBands[i].Header != null && DataBands[i].Header.Height > maxHeight)
                {
                    throw new Exception($"Раздел {DataBands[i].Header.Name} не умещается на страницу.");
                }

                if (DataBands[i].DataBand != null)
                {
                    if (DataBands[i].DataBand.Height > maxHeight)
                    {
                        throw new Exception($"Раздел {DataBands[i].DataBand.Name} не умещается на страницу.");
                    }
                    else
                    {
                        DataBands[i].DataBand.Reset();
                    }
                }

                if (DataBands[i].Footer != null && DataBands[i].Footer.Height > maxHeight)
                {
                    throw new Exception($"Раздел {DataBands[i].Footer.Name} не умещается на страницу.");
                }
            }

            CreatePdfPage(doc);

            for (int i = 0; i < DataBands.Count; i++)
            {
                if (DataBands[i].DataBand == null)
                {
                    continue;
                }

                DrawDataBand(doc, DataBands[i].Header);
                do
                {
                    DrawDataBand(doc, DataBands[i].DataBand);
                    DataBands[i].DataBand.NextRow();
                } while (!DataBands[i].DataBand.EOL());

                DrawDataBand(doc, DataBands[i].Footer);
            }

            DrawDataBand(doc, ReportSummary);
        }

        private void CreatePdfPage(PdfDocument doc)
        {
            currentPage = doc.Pages.Add();
            PdfGraphics graphics = currentPage.Graphics;

            current = doc.Pages.Count;

            if (current == 1 && ReportTitleBand != null)
            {
                currentBandTop = 0;
                ReportTitleBand.GeneratePdf(graphics);
            }

            if (PageHeaderBand != null)
            {
                currentBandTop = ReportTitleBand?.Height ?? 0;
                PageHeaderBand.GeneratePdf(graphics);
            }

            if (PageFooterBand != null)
            {
                currentBandTop = PageSize.Height - MarginSize.Bottom;
                PageFooterBand.GeneratePdf(graphics);
            }

            currentBandTop = (ReportTitleBand?.Height ?? 0) + (PageHeaderBand?.Height ?? 0);
        }

        private float GetMaxDataBandHeight(int page)
        {
            float max_height = MarginSize.Height - (PageHeaderBand?.Height ?? 0) - (PageFooterBand?.Height ?? 0);
            if (page == 1 && ReportTitleBand != null)
            {
                max_height -= ReportTitleBand.Height;
            }

            return max_height;
        }

        private void DrawDataBand(PdfDocument doc, Band band)
        {
            if (band != null)
            {
                if (currentBandTop + band.Height > GetMaxDataBandHeight(Current))
                {
                    CreatePdfPage(doc);
                }

                band.GeneratePdf(currentPage.Graphics);
                currentBandTop += band.Height;
            }
        }
    }
}
