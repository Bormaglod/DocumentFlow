﻿//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 17.08.2019
// Time: 18:19
//-----------------------------------------------------------------------

using System;
using System.Drawing;
using System.Windows.Forms;
using DocumentFlow.Code;
using DocumentFlow.Core;

namespace DocumentFlow.Controls.Editor
{
    public partial class L_ImageViewerBox : UserControl, ILabelControl, IEditControl
    {
        private string base64Image;

        public L_ImageViewerBox()
        {
            InitializeComponent();
            Nullable = true;
    }

        public event EventHandler ValueChanged;

        public string Base64Image
        {
            get => base64Image;

            set
            {
                base64Image = value;
                pictureBox1.Image = ImageHelper.Base64ToImage(base64Image);
            }
        }

        string ILabelControl.Text { get => label1.Text; set => label1.Text = value; }

        int ILabelControl.Width { get => label1.Width; set => label1.Width = value; }

        bool ILabelControl.AutoSize { get => label1.AutoSize; set => label1.AutoSize = value; }

        ContentAlignment ILabelControl.TextAlign { get => label1.TextAlign; set => label1.TextAlign = value; }

        bool ILabelControl.Visible
        {
            get => label1.Visible;
            set
            {
                label1.Visible = value;
                panel3.Dock = value ? DockStyle.Left : DockStyle.Top;
            }
        }

        int IEditControl.Width { get => panel3.Width; set => panel3.Width = value; }

        object IValuable.Value
        {
            get => Nullable ? Base64Image.NullIfEmpty() : base64Image;
            set => Base64Image = value == null ? string.Empty : value.ToString();
        }

        bool IEditControl.FitToSize { get; set; }

        public bool Nullable { get; set; }

        private void ButtonSelectImage_Click_1(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Base64Image = ImageHelper.ImageToBase64(openFileDialog1.FileName);
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
