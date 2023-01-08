//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 20.01.2022
//
// Версия 2022.9.2
//  - добавлен параметр customizeColumn определяющий возможность
//    настройки колонок
// Версия 2023.1.8
//  - удалён параметр customizeColumn
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Settings;

using Syncfusion.Windows.Forms.Tools;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.DataGrid.Events;
using Syncfusion.WinForms.ListView.Events;

using System.Drawing.Printing;

namespace DocumentFlow.Dialogs;

public partial class BrowserCustomizationForm : Form
{
    private readonly Dictionary<PaperSize, int> paperSizes = new();
    private readonly Dictionary<TreeNodeAdv, TabPage> pages;
    private readonly BrowserSettings settings;

    private class FontInfo
    {
        public FontInfo(string name, ReportFont font)
        {
            Name = name;
            SourceFont = font;
            FamilyName = font.FamilyName;
            Size = font.Size;
            Style = font.Style;
        }

        public string Name { get; set; }
        public string FamilyName { get; set; } = "Times New Roman";
        public float Size { get; set; } = 10;
        public FontStyle Style { get; set; } = FontStyle.Regular;
        public ReportFont SourceFont { get; set; }
        public Font Font
        {
            get => new (FamilyName, Size, Style);
            set
            {
                FamilyName = value.Name;
                Size = value.Size;
                Style = value.Style;
            }
        }

        public void UpdateSourceFont()
        {
            SourceFont.FamilyName = FamilyName;
            SourceFont.Size = Size;
            SourceFont.Style = Style;
        }

        public override string ToString() => Name;
    }

    public BrowserCustomizationForm(BrowserSettings settings)
    {
        InitializeComponent();

        this.settings = settings;

        var columns = new TreeNodeAdv("Колонки");
        var appearance = new TreeNodeAdv("Внешний вид", new TreeNodeAdv[] { columns });
        var iface = new TreeNodeAdv("Интерфейс", new TreeNodeAdv[] { appearance });

        var paper = new TreeNodeAdv("Paper");
        var margins = new TreeNodeAdv("Margins");
        var fonts = new TreeNodeAdv("Шрифты");
        var print = new TreeNodeAdv("Печать", new TreeNodeAdv[] { paper, margins, fonts });

        treeViewAdv1.Nodes.AddRange(new TreeNodeAdv[] { iface, print });

        treeViewAdv1.ExpandAll();

        pages = new()
        {
            [columns] = tabPageColumns,
            [paper] = tabPagePaper,
            [margins] = tabPageMargins,
            [fonts] = tabPageFonts
        };

        gridColumns.DataSource = settings.Columns;

        PrinterSettings ps = new();
        foreach (var size in ps.PaperSizes.OfType<PaperSize>())
        {
            int idx = comboBoxPaperSizes.Items.Add(size.PaperName);
            paperSizes.Add(size, idx);
        }

        textWidth.Text = settings.Page.Settings.PaperWidth.ToString();
        textHeight.Text = settings.Page.Settings.PaperHeight.ToString();

        var paperSize = paperSizes.Keys.FirstOrDefault(x => settings.Page.Settings.PaperSizesEqual(x));
        if (paperSize != null)
        {
            comboBoxPaperSizes.SelectedIndex = paperSizes[paperSize]; 
        }
        else
        {
            var custom = paperSizes.Keys.FirstOrDefault(x => x.Kind == PaperKind.Custom);
            if (custom != null)
            {
                comboBoxPaperSizes.SelectedIndex = paperSizes[custom];
            }
            else
            {
                comboBoxPaperSizes.SelectedIndex = -1;
            }
        }

        radioLandscape.Checked = settings.Page.Settings.Landscape;

        checkBoxMirrorMargins.Checked = settings.Page.Settings.MirrorMargins;

        textBoxLeft.IntegerValue = settings.Page.Settings.LeftMargin;
        textBoxTop.IntegerValue = settings.Page.Settings.TopMargin;
        textBoxBottom.IntegerValue = settings.Page.Settings.BottomMargin;
        textBoxRight.IntegerValue = settings.Page.Settings.RightMargin;

        List<FontInfo> fontList = new()
        {
            new FontInfo("Заголовок страницы", settings.Page.Fonts.Title),
            new FontInfo("Заголовок таблицы", settings.Page.Fonts.Header),
            new FontInfo("Основной шрифт", settings.Page.Fonts.Base)
        };

        listViewFonts.DataSource = fontList;
        listViewFonts.SelectedIndex = 0;

        treeViewAdv1.SelectedItem = iface;
    }

    public IReadOnlyList<BrowserColumn>? Columns => gridColumns.DataSource as IReadOnlyList<BrowserColumn>;

    private void GridColumns_AutoGeneratingColumn(object sender, AutoGeneratingColumnArgs e)
    {
        switch (e.Column.MappingName)
        {
            case "Header":
                e.Column.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
                break;
        }
    }

    private void ButtonChangeColumn_Click(object sender, EventArgs e)
    {
        if (gridColumns.SelectedItem is BrowserColumn column)
        {
            var form = new ColumnCutomizeForm(column);
            if (form.ShowDialog() == DialogResult.OK)
            {
                gridColumns.Refresh();
            }
        }
    }

    private void GridColumns_QueryCellStyle(object sender, QueryCellStyleEventArgs e)
    {
        if (e.DataRow.RowData is BrowserColumn column)
        {
            e.Style.TextColor = column.Hidden ? SystemColors.ControlText : Color.Silver;
        }
    }

    private void RadioPortrait_CheckChanged(object sender, EventArgs e)
    {
        pictureBox1.Image = Properties.Resources.portrait;
        UpdateSizeValues(comboBoxPaperSizes.SelectedIndex);
    }

    private void RadioLandscape_CheckChanged(object sender, EventArgs e)
    {
        pictureBox1.Image = Properties.Resources.landscape;
        UpdateSizeValues(comboBoxPaperSizes.SelectedIndex);
    }

    private void TreeViewAdv1_AfterSelect(object sender, EventArgs e)
    {
        if (treeViewAdv1.SelectedNode != null)
        {
            if (pages.ContainsKey(treeViewAdv1.SelectedNode))
            {
                tabControl1.SelectedTab = pages[treeViewAdv1.SelectedNode];
            }
            else
            {
                labelEmpty.Text = treeViewAdv1.SelectedNode.Text;
                tabControl1.SelectedTab = tabPageEmpty;
            }

            ActiveControl = treeViewAdv1;
        }
    }

    private void UpdateSizeValues(int selectedIndex)
    {
        var ps = paperSizes.FirstOrDefault(x => x.Value == selectedIndex);
        if (ps.Key != null)
        {
            var (w, h) = (Math.Round(ps.Key.Width * 25.4 / 100), Math.Round(ps.Key.Height * 25.4 / 100));
            if (radioLandscape.Checked)
            {
                (w, h) = (h, w);
            }

            textWidth.Text = w.ToString();
            textHeight.Text = h.ToString();
        }
    }

    private void ComboBoxPaperSizes_SelectedIndexChanging(object sender, SelectedIndexChangingArgs e)
    {
        if (e.NewIndex != -1)
        {
            UpdateSizeValues(e.NewIndex);
        }
    }

    private void ButtonOk_Click(object sender, EventArgs e)
    {
        settings.Page.Settings.PaperWidth = Convert.ToInt32(textWidth.IntegerValue);
        settings.Page.Settings.PaperHeight = Convert.ToInt32(textHeight.IntegerValue);
        
        settings.Page.Settings.Landscape = radioLandscape.Checked;
        settings.Page.Settings.MirrorMargins = checkBoxMirrorMargins.Checked;

        settings.Page.Settings.LeftMargin = Convert.ToInt32(textBoxLeft.IntegerValue);
        settings.Page.Settings.TopMargin = Convert.ToInt32(textBoxTop.IntegerValue);
        settings.Page.Settings.BottomMargin = Convert.ToInt32(textBoxBottom.IntegerValue);
        settings.Page.Settings.RightMargin = Convert.ToInt32(textBoxRight.IntegerValue);

        if (listViewFonts.DataSource is IEnumerable<FontInfo> list)
        {
            foreach (var fontInfo in list)
            {
                fontInfo.UpdateSourceFont();
            }
        }
    }

    private void UpdateFontSample(FontInfo fi)
    {
        string style = string.Empty;

        if (fi.Style.HasFlag(FontStyle.Bold | FontStyle.Italic))
        {
            style = "Полужирный курсив";
        }
        else if (fi.Style.HasFlag(FontStyle.Italic))
        {
            style = "Курсив";
        }
        else if (fi.Style.HasFlag(FontStyle.Bold))
        {
            style = "Полужирный";
        }
        else if (fi.Style.HasFlag(FontStyle.Regular))
        {
            style = "Обычный";
        }

        textBoxFontSample.Text = $"{fi.FamilyName} {fi.Size} {style}\r\nСъешь ещё этих мягких французских булок, да выйпей чаю";
        textBoxFontSample.Font = fi.Font;
    }

    private void ListViewFonts_SelectionChanged(object sender, ItemSelectionChangedEventArgs e)
    {
        if (e.AddedItems.Count > 0 && e.AddedItems[0] is FontInfo fi)
        {
            UpdateFontSample(fi);
        }
    }

    private void ButtonChangeFont_Click(object sender, EventArgs e)
    {
        if (listViewFonts.SelectedItem is FontInfo fi)
        {
            fontDialog1.Font = fi.Font;
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                fi.Font = fontDialog1.Font;
                UpdateFontSample(fi);
            }
        }
    }
}
