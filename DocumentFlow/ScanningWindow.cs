//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.12.2020
// Time: 22:53
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DocumentFlow.Data.Core;

namespace DocumentFlow
{
    public partial class ScanningWindow : Form
    {
        public class ScanImage
        {
            public Image Image { get; set; }
        }

        private readonly BindingList<ScanImage> images;

        public ScanningWindow()
        {
            InitializeComponent();
            images = new BindingList<ScanImage>();
            gridImages.DataSource = images;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                twain32.CloseDataSource();
                twain32.Acquire();
            }
            catch (Exception ex)
            {
                ExceptionHelper.MesssageBox(ex);
            }
        }

        private void twain32_AcquireCompleted(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < twain32.ImageCount; i++)
                {
                    ScanImage scanImage = new ScanImage
                    {
                        Image = twain32.GetImage(i)
                    };

                    images.Add(scanImage);
                    /*var pdf = new PdfDocument();
                    for (int i = 0; i < twain32.ImageCount; i++)
                    {
                        var image = twain32.GetImage(i);

                        var sizeImage = new SizeF(image.Width / image.HorizontalResolution, image.Height / image.VerticalResolution);
                        var pageSize = UnitConverter.Convert(sizeImage, Core.GraphicsUnit.Inch, Core.GraphicsUnit.Point);

                        var page = pdf.Pages.Add(pageSize, new PdfMargins(0f));

                        page.Canvas.DrawImage(PdfImage.FromImage(image), new PointF(0, 0));
                    }

                    SaveDocument(pdf);*/
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.MesssageBox(ex);
            }
        }
    }
}
