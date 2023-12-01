//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 23.11.2023
//-----------------------------------------------------------------------

using DocumentFlow.Dialogs.Interfaces;
using DocumentFlow.Scanner;

using Syncfusion.Windows.Forms.Tools;

using System.Diagnostics.CodeAnalysis;

namespace DocumentFlow.Dialogs;

public partial class ScannerDialog : Form, IScannerDialog
{
    private readonly Color SelectedBackColor = Color.FromArgb(22, 165, 220);
    private readonly Color HoverBackColor = Color.FromArgb(199, 224, 244);
    private readonly Color PictureBackColor = SystemColors.Window;

    private readonly IScanner scanner;
    private List<WIADeviceInfo> devices = new();

    private PictureBox? selected;

    public ScannerDialog(IScanner scanner)
    {
        InitializeComponent();

        this.scanner = scanner;
    }

    public PictureBox? SelectedImage
    {
        get => selected;

        set
        {
            if (selected != null)
            {
                selected.BackColor = PictureBackColor;
            }

            selected = value;
            if (selected != null)
            {
                selected.BackColor = SelectedBackColor;
                pictureBox1.Image = selected.Image;
            }
            else
            {
                pictureBox1.Image = null;
            }
        }
    }

    public bool Scan([MaybeNullWhen(false)] out IList<Image> images)
    {
        if (ShowDialog() == DialogResult.OK)
        {
            images = new List<Image>();
            for (int i = 0; i < flowLayoutPanel1.Controls.Count; i++)
            {
                if (flowLayoutPanel1.Controls[i] is PictureBox picture)
                {
                    images.Add(picture.Image);
                }
            }

            return true;
        }

        images = null;
        return false;
    }

    private void ScannerDialog_Load(object sender, EventArgs e)
    {
        devices = scanner.GetDevices().ToList();
        comboDevices.DataSource = devices;

        if (comboDevices.SelectedItem is WIADeviceInfo info)
        {
            scanner.SetDevice(info);
        }
    }

    private void LinkNativeProps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        scanner.ShowDeviceProperties();
    }

    private void LinkItemProps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        scanner.ShowDeviceItemProperties();
    }

    private void ButtonBrowse_Click(object sender, EventArgs e)
    {
        if (scanner.ShowSelectDevice(out var deviceId))
        {
            var info = devices.First(x => x.DeviceID == deviceId);
            comboDevices.SelectedItem = info;
        }
    }

    private void ButtonScan_Click(object sender, EventArgs e)
    {
        var images = scanner.Scan(1);
        var boxes = new List<PictureBox>();

        foreach (var image in images)
        {
            var box = new PictureBox()
            {
                Size = new(100, 100),
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = image
            };

            box.MouseEnter += (sender, e) =>
            {
                if (sender is PictureBox picture)
                {
                    picture.BackColor = picture == selected ? SelectedBackColor : HoverBackColor;
                }
            };

            box.MouseLeave += (sender, e) =>
            {
                if (sender is PictureBox picture)
                {
                    if (picture != selected)
                    {
                        picture.BackColor = PictureBackColor;
                    }
                }
            };

            box.MouseClick += (sender, e) =>
            {
                if (sender is PictureBox picture)
                {
                    SelectedImage = picture;
                }
            };

            boxes.Add(box);
        }

        flowLayoutPanel1.Controls.AddRange(boxes.ToArray());
        SelectedImage = boxes.FirstOrDefault();
    }

    private void ButtonSelectItems_Click(object sender, EventArgs e)
    {
        scanner.ShowSelectItems();
    }

    private void ButtonProperties_Click(object sender, EventArgs e)
    {
        scanner.ShowItemProperties();
    }

    private void LinkViewItemProps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        scanner.ShowItemListProperties();
    }

    private void ComboDevices_SelectedIndexChanging(object sender, SelectedIndexChangingArgs e)
    {
        if (e.NewIndex != -1)
        {
            scanner.SetDevice(devices[e.NewIndex]);
        }
    }

    private void ButtonDeletePage_Click(object sender, EventArgs e)
    {
        if (SelectedImage != null)
        {
            flowLayoutPanel1.Controls.Remove(SelectedImage);
            SelectedImage = null;
        }
    }

    private void ButtonTurnPage_Click(object sender, EventArgs e)
    {
        if (SelectedImage != null)
        {
            SelectedImage.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            SelectedImage.Refresh();

            pictureBox1.Image = SelectedImage.Image;
        }
    }
}
