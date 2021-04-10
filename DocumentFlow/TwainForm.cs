//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.12.2020
// Time: 22:53
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DocumentFlow.Core;
using DocumentFlow.Data;
using DocumentFlow.Interfaces;

namespace DocumentFlow
{
    public partial class TwainForm : Form
    {
        private readonly Color SelectedBackColor = Color.FromArgb(22, 165, 220);
        private readonly Color HoverBackColor = Color.FromArgb(199, 224, 244);
        private readonly Color PictureBackColor = SystemColors.Window;

        private readonly ITwain twain;
        private Guid id;
        private PictureBox selected = null;

        public TwainForm(ITwain twain)
        {
            InitializeComponent();

            panelImages.AutoScroll = false;
            panelImages.HorizontalScroll.Enabled = false;
            panelImages.HorizontalScroll.Visible = false;
            panelImages.HorizontalScroll.Maximum = 0;
            panelImages.AutoScroll = true;

            id = Guid.NewGuid();

            this.twain = twain;
            this.twain.CapturingImage += Twain_CapturingImage;
        }

        public List<Image> Images => panelImages.Controls.OfType<PictureBox>()
                                                         .OrderBy(p => p.TabIndex)
                                                         .Select(p => p.Image)
                                                         .ToList();

        private void AddImage(Image image)
        {
            PictureBox pictureBox = new()
            {
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(panelImages.Width, 200),
                Image = image,
                Dock = DockStyle.Top,
                TabIndex = panelImages.Controls.Count
            };
            
            pictureBox.MouseEnter += (s, e) =>
            {
                if (s is PictureBox picture)
                {
                    picture.BackColor = picture == selected ? SelectedBackColor : HoverBackColor;
                }
            };

            pictureBox.MouseLeave += (s, e) =>
            {
                if (s is PictureBox picture)
                {
                    if (picture != selected)
                    {
                        picture.BackColor = PictureBackColor;
                    }
                }
            };

            pictureBox.MouseClick += (s, e) =>
            {
                if (s is PictureBox picture)
                {
                    if (selected != null)
                    {
                        selected.BackColor = PictureBackColor;
                    }

                    selected = picture;
                    picture.BackColor = SelectedBackColor;
                }
            };

            panelImages.Controls.Add(pictureBox);

            pictureBox.BringToFront();
        }

        private void Twain_CapturingImage(object sender, CupturingImageEventArgs e)
        {
            if (e.DestinationId != id)
                return;

            AddImage(e.Image);
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                twain.Capture(id);
            }
            catch (Exception ex)
            {
                ExceptionHelper.MesssageBox(ex);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (selected != null)
            {
                panelImages.Controls.Remove(selected);
            }
        }
    }
}
