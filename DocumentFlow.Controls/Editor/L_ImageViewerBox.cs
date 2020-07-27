//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 17.08.2019
// Time: 18:19
//-----------------------------------------------------------------------

namespace DocumentFlow.Controls
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using DocumentFlow.Core;
    using DocumentFlow.DataSchema;

    public partial class L_ImageViewerBox : UserControl, ILabeled
    {
        private string base64Image;

        public L_ImageViewerBox()
        {
            InitializeComponent();
        }

        public string Base64Image
        {
            get => base64Image;

            set
            {
                base64Image = value;
                pictureBox1.Image = ImageHelper.Base64ToImage(base64Image);
            }
        }

        string ILabeled.Text { get => label1.Text; set => label1.Text = value; }

        int ILabeled.Width { get => label1.Width; set => label1.Width = value; }

        int ILabeled.EditWidth { get => panel3.Width; set => panel3.Width = value; }

        bool ILabeled.AutoSize { get => label1.AutoSize; set => label1.AutoSize = value; }

        ContentAlignment ILabeled.TextAlign { get => label1.TextAlign; set => label1.TextAlign = value; }

        bool ILabeled.Visible
        {
            get => label1.Visible;
            set
            {
                label1.Visible = value;
                panel3.Dock = value ? DockStyle.Left : DockStyle.Top;
            }
        }

        private void ButtonSelectImage_Click_1(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Base64Image = ImageHelper.ImageToBase64(openFileDialog1.FileName);
            }
        }
    }
}
