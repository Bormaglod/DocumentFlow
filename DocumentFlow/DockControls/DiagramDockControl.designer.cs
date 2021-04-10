namespace DocumentFlow
{
    partial class DiagramDockControl
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Syncfusion.Windows.Forms.Diagram.Binding binding2 = new Syncfusion.Windows.Forms.Diagram.Binding();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DiagramDockControl));
            Syncfusion.Windows.Forms.Edit.Implementation.Config.Config config2 = new Syncfusion.Windows.Forms.Edit.Implementation.Config.Config();
            this.diagram1 = new Syncfusion.Windows.Forms.Diagram.Controls.Diagram(this.components);
            this.model1 = new Syncfusion.Windows.Forms.Diagram.Model(this.components);
            this.tabSplitterContainer1 = new Syncfusion.Windows.Forms.Tools.TabSplitterContainer();
            this.pageDiagram = new Syncfusion.Windows.Forms.Tools.TabSplitterPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.paletteGroupBar1 = new Syncfusion.Windows.Forms.Diagram.Controls.PaletteGroupBar(this.components);
            this.propertyEditor1 = new Syncfusion.Windows.Forms.Diagram.Controls.PropertyEditor(this.components);
            this.pageCode = new Syncfusion.Windows.Forms.Tools.TabSplitterPage();
            this.editControl1 = new Syncfusion.Windows.Forms.Edit.EditControl();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.buttonFill = new System.Windows.Forms.ToolStripButton();
            this.buttonRefresh = new System.Windows.Forms.ToolStripButton();
            this.buttonPreview = new System.Windows.Forms.ToolStripButton();
            this.buttonPrint = new System.Windows.Forms.ToolStripButton();
            this.buttonSave = new System.Windows.Forms.ToolStripButton();
            this.buttonSaveAndClose = new System.Windows.Forms.ToolStripButton();
            this.buttonUndo = new System.Windows.Forms.ToolStripButton();
            this.buttonRedo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripPanelItem1 = new Syncfusion.Windows.Forms.Tools.ToolStripPanelItem();
            this.buttonCut = new System.Windows.Forms.ToolStripButton();
            this.buttonCopy = new System.Windows.Forms.ToolStripButton();
            this.buttonPaste = new System.Windows.Forms.ToolStripButton();
            this.buttonDelete = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.diagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.model1)).BeginInit();
            this.tabSplitterContainer1.SuspendLayout();
            this.pageDiagram.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paletteGroupBar1)).BeginInit();
            this.pageCode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editControl1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // diagram1
            // 
            this.diagram1.BackColor = System.Drawing.Color.DarkGray;
            binding2.DefaultConnector = null;
            binding2.DefaultNode = null;
            binding2.Diagram = this.diagram1;
            binding2.Id = null;
            binding2.Label = ((System.Collections.Generic.List<string>)(resources.GetObject("binding2.Label")));
            binding2.ParentId = null;
            this.diagram1.Binding = binding2;
            this.diagram1.Controller.Constraint = Syncfusion.Windows.Forms.Diagram.Constraints.PageEditable;
            this.diagram1.Controller.DefaultConnectorTool = Syncfusion.Windows.Forms.Diagram.ConnectorTool.OrgLineConnectorTool;
            this.diagram1.Controller.PasteOffset = new System.Drawing.SizeF(10F, 10F);
            this.diagram1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.diagram1.EnableTouchMode = false;
            this.diagram1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.diagram1.HScroll = true;
            this.diagram1.LayoutManager = null;
            this.diagram1.Location = new System.Drawing.Point(220, 0);
            this.diagram1.MetroScrollBars = true;
            this.diagram1.Model = this.model1;
            this.diagram1.Name = "diagram1";
            this.diagram1.Size = new System.Drawing.Size(499, 438);
            this.diagram1.SmartSizeBox = false;
            this.diagram1.TabIndex = 1;
            this.diagram1.Text = "diagram1";
            // 
            // 
            // 
            this.diagram1.View.ClientRectangle = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.diagram1.View.Controller = this.diagram1.Controller;
            this.diagram1.View.Grid.MinPixelSpacing = 4F;
            this.diagram1.View.ZoomType = Syncfusion.Windows.Forms.Diagram.ZoomType.Center;
            this.diagram1.VScroll = true;
            // 
            // model1
            // 
            this.model1.AlignmentType = Syncfusion.Windows.Forms.Diagram.AlignmentType.SelectedNode;
            this.model1.BackgroundStyle.PathBrushStyle = Syncfusion.Windows.Forms.Diagram.PathGradientBrushStyle.RectangleCenter;
            this.model1.DocumentScale.DisplayName = "NoScale";
            this.model1.DocumentScale.Height = 1F;
            this.model1.DocumentScale.Width = 1F;
            this.model1.DocumentSize.DisplayName = "A4: 210 mm x 297 mm";
            this.model1.DocumentSize.Height = 1122F;
            this.model1.DocumentSize.Width = 793F;
            this.model1.LineBridgingEnabled = true;
            this.model1.LineRoutingEnabled = true;
            this.model1.LineStyle.DashPattern = null;
            this.model1.LineStyle.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.model1.LogicalSize = new System.Drawing.SizeF(793F, 1122F);
            this.model1.OptimizeLineBridging = true;
            this.model1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.model1.ShadowStyle.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))));
            this.model1.ShadowStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))));
            // 
            // tabSplitterContainer1
            // 
            this.tabSplitterContainer1.Collapsed = true;
            this.tabSplitterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabSplitterContainer1.Location = new System.Drawing.Point(0, 52);
            this.tabSplitterContainer1.Name = "tabSplitterContainer1";
            this.tabSplitterContainer1.PrimaryPages.AddRange(new Syncfusion.Windows.Forms.Tools.TabSplitterPage[] {
            this.pageDiagram});
            this.tabSplitterContainer1.SecondaryPages.AddRange(new Syncfusion.Windows.Forms.Tools.TabSplitterPage[] {
            this.pageCode});
            this.tabSplitterContainer1.Size = new System.Drawing.Size(1010, 458);
            this.tabSplitterContainer1.SplitterBackColor = System.Drawing.SystemColors.Control;
            this.tabSplitterContainer1.SplitterPosition = 438;
            this.tabSplitterContainer1.TabIndex = 1;
            // 
            // pageDiagram
            // 
            this.pageDiagram.AutoScroll = true;
            this.pageDiagram.Controls.Add(this.splitContainer1);
            this.pageDiagram.Hide = false;
            this.pageDiagram.Image = global::DocumentFlow.Properties.Resources.icons8_mind_map_12x11;
            this.pageDiagram.Location = new System.Drawing.Point(0, 0);
            this.pageDiagram.Name = "pageDiagram";
            this.pageDiagram.Size = new System.Drawing.Size(1010, 438);
            this.pageDiagram.TabIndex = 1;
            this.pageDiagram.Text = "Диаграмма";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.diagram1);
            this.splitContainer1.Panel1.Controls.Add(this.paletteGroupBar1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.propertyEditor1);
            this.splitContainer1.Size = new System.Drawing.Size(1010, 438);
            this.splitContainer1.SplitterDistance = 719;
            this.splitContainer1.TabIndex = 3;
            // 
            // paletteGroupBar1
            // 
            this.paletteGroupBar1.AllowDrop = true;
            this.paletteGroupBar1.AnimatedSelection = false;
            this.paletteGroupBar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.paletteGroupBar1.BeforeTouchSize = new System.Drawing.Size(220, 438);
            this.paletteGroupBar1.BorderColor = System.Drawing.Color.White;
            this.paletteGroupBar1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.paletteGroupBar1.CollapseImage = ((System.Drawing.Image)(resources.GetObject("paletteGroupBar1.CollapseImage")));
            this.paletteGroupBar1.Diagram = null;
            this.paletteGroupBar1.Dock = System.Windows.Forms.DockStyle.Left;
            this.paletteGroupBar1.EditMode = false;
            this.paletteGroupBar1.ExpandButtonToolTip = null;
            this.paletteGroupBar1.ExpandImage = ((System.Drawing.Image)(resources.GetObject("paletteGroupBar1.ExpandImage")));
            this.paletteGroupBar1.FlatLook = true;
            this.paletteGroupBar1.ForeColor = System.Drawing.Color.White;
            this.paletteGroupBar1.GroupBarDropDownToolTip = null;
            this.paletteGroupBar1.GroupBarItemHeight = 32;
            this.paletteGroupBar1.IndexOnVisibleItems = true;
            this.paletteGroupBar1.Location = new System.Drawing.Point(0, 0);
            this.paletteGroupBar1.MinimizeButtonToolTip = null;
            this.paletteGroupBar1.Name = "paletteGroupBar1";
            this.paletteGroupBar1.NavigationPaneTooltip = null;
            this.paletteGroupBar1.PopupClientSize = new System.Drawing.Size(0, 0);
            this.paletteGroupBar1.Size = new System.Drawing.Size(220, 438);
            this.paletteGroupBar1.SmartSizeBox = false;
            this.paletteGroupBar1.Splittercolor = System.Drawing.Color.Red;
            this.paletteGroupBar1.TabIndex = 2;
            this.paletteGroupBar1.Text = "paletteGroupBar1";
            this.paletteGroupBar1.ThemeName = "Metro";
            this.paletteGroupBar1.ThemeStyle.CollapsedViewStyle.ItemStyle.SelectedForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.paletteGroupBar1.ThemeStyle.ItemStyle.SelectedForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.paletteGroupBar1.ThemeStyle.StackedViewStyle.CollapsedItemStyle.SelectedForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.paletteGroupBar1.VisualStyle = Syncfusion.Windows.Forms.VisualStyle.Metro;
            // 
            // propertyEditor1
            // 
            this.propertyEditor1.Diagram = this.diagram1;
            this.propertyEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyEditor1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.propertyEditor1.Location = new System.Drawing.Point(0, 0);
            this.propertyEditor1.Name = "propertyEditor1";
            this.propertyEditor1.Size = new System.Drawing.Size(287, 438);
            this.propertyEditor1.TabIndex = 2;
            // 
            // pageCode
            // 
            this.pageCode.AutoScroll = true;
            this.pageCode.Controls.Add(this.editControl1);
            this.pageCode.Hide = false;
            this.pageCode.Image = global::DocumentFlow.Properties.Resources.icons8_source_14x10;
            this.pageCode.Location = new System.Drawing.Point(0, 0);
            this.pageCode.Name = "pageCode";
            this.pageCode.Size = new System.Drawing.Size(1010, 438);
            this.pageCode.TabIndex = 2;
            this.pageCode.Text = "Код";
            // 
            // editControl1
            // 
            this.editControl1.AllowZoom = false;
            this.editControl1.ChangedLinesMarkingLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(238)))), ((int)(((byte)(98)))));
            this.editControl1.CodeSnipptSize = new System.Drawing.Size(100, 100);
            this.editControl1.Configurator = config2;
            this.editControl1.ContextChoiceBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.editControl1.ContextChoiceBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(166)))), ((int)(((byte)(50)))));
            this.editControl1.ContextChoiceForeColor = System.Drawing.SystemColors.InfoText;
            this.editControl1.ContextPromptBackgroundBrush = new Syncfusion.Drawing.BrushInfo(System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255))))));
            this.editControl1.ContextTooltipBackgroundBrush = new Syncfusion.Drawing.BrushInfo(System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(232)))), ((int)(((byte)(236))))));
            this.editControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editControl1.IndicatorMarginBackColor = System.Drawing.Color.Empty;
            this.editControl1.LineNumbersColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.editControl1.Location = new System.Drawing.Point(0, 0);
            this.editControl1.Name = "editControl1";
            this.editControl1.RenderRightToLeft = false;
            this.editControl1.SaveOnClose = false;
            this.editControl1.ScrollPosition = new System.Drawing.Point(0, 0);
            this.editControl1.SelectionTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(214)))), ((int)(((byte)(255)))));
            this.editControl1.ShowHorizontalSplitters = false;
            this.editControl1.ShowVerticalSplitters = false;
            this.editControl1.Size = new System.Drawing.Size(1010, 438);
            this.editControl1.StatusBarSettings.CoordsPanel.Width = 150;
            this.editControl1.StatusBarSettings.EncodingPanel.Width = 100;
            this.editControl1.StatusBarSettings.FileNamePanel.Width = 100;
            this.editControl1.StatusBarSettings.InsertPanel.Width = 33;
            this.editControl1.StatusBarSettings.Offcie2007ColorScheme = Syncfusion.Windows.Forms.Office2007Theme.Blue;
            this.editControl1.StatusBarSettings.Offcie2010ColorScheme = Syncfusion.Windows.Forms.Office2010Theme.Blue;
            this.editControl1.StatusBarSettings.StatusPanel.Width = 70;
            this.editControl1.StatusBarSettings.TextPanel.Width = 214;
            this.editControl1.StatusBarSettings.VisualStyle = Syncfusion.Windows.Forms.Tools.Controls.StatusBar.VisualStyle.Default;
            this.editControl1.TabIndex = 0;
            this.editControl1.Text = "";
            this.editControl1.UseXPStyleBorder = true;
            this.editControl1.VisualColumn = 1;
            this.editControl1.VScrollMode = Syncfusion.Windows.Forms.Edit.ScrollMode.Immediate;
            this.editControl1.ZoomFactor = 1F;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 52);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 52);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonFill,
            this.buttonRefresh,
            this.toolStripSeparator1,
            this.buttonPrint,
            this.buttonPreview,
            this.buttonSave,
            this.buttonSaveAndClose,
            this.toolStripSeparator2,
            this.buttonUndo,
            this.buttonRedo,
            this.toolStripSeparator3,
            this.toolStripPanelItem1,
            this.buttonPaste,
            this.buttonDelete});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1010, 52);
            this.toolStrip1.TabIndex = 0;
            // 
            // buttonFill
            // 
            this.buttonFill.Image = global::DocumentFlow.Properties.Resources.icons8_create_30;
            this.buttonFill.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonFill.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonFill.Name = "buttonFill";
            this.buttonFill.Size = new System.Drawing.Size(70, 49);
            this.buttonFill.Text = "Заполнить";
            this.buttonFill.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonFill.Click += new System.EventHandler(this.buttonFill_Click);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Image = global::DocumentFlow.Properties.Resources.icons8_renew_30;
            this.buttonRefresh.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(65, 49);
            this.buttonRefresh.Text = "Обновить";
            this.buttonRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // buttonPreview
            // 
            this.buttonPreview.Image = global::DocumentFlow.Properties.Resources.icons8_preview_30;
            this.buttonPreview.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonPreview.Name = "buttonPreview";
            this.buttonPreview.Size = new System.Drawing.Size(109, 49);
            this.buttonPreview.Text = "Просмотр печати";
            this.buttonPreview.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonPreview.Click += new System.EventHandler(this.buttonPreview_Click);
            // 
            // buttonPrint
            // 
            this.buttonPrint.Image = global::DocumentFlow.Properties.Resources.icons8_print_30;
            this.buttonPrint.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonPrint.Name = "buttonPrint";
            this.buttonPrint.Size = new System.Drawing.Size(50, 49);
            this.buttonPrint.Text = "Печать";
            this.buttonPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonPrint.Click += new System.EventHandler(this.buttonPrint_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Image = global::DocumentFlow.Properties.Resources.icons8_save_30;
            this.buttonSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(70, 49);
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.Image = global::DocumentFlow.Properties.Resources.icons8_save_close_30;
            this.buttonSaveAndClose.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonSaveAndClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonSaveAndClose.Name = "buttonSaveAndClose";
            this.buttonSaveAndClose.Size = new System.Drawing.Size(127, 49);
            this.buttonSaveAndClose.Text = "Сохранить и закрыть";
            this.buttonSaveAndClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonSaveAndClose.Click += new System.EventHandler(this.buttonSaveAndClose_Click);
            // 
            // buttonUndo
            // 
            this.buttonUndo.Image = global::DocumentFlow.Properties.Resources.icons8_undo_30;
            this.buttonUndo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonUndo.Name = "buttonUndo";
            this.buttonUndo.Size = new System.Drawing.Size(65, 49);
            this.buttonUndo.Text = "Отменить";
            this.buttonUndo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonUndo.Click += new System.EventHandler(this.buttonUndo_Click);
            // 
            // buttonRedo
            // 
            this.buttonRedo.Image = global::DocumentFlow.Properties.Resources.icons8_redo_30;
            this.buttonRedo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonRedo.Name = "buttonRedo";
            this.buttonRedo.Size = new System.Drawing.Size(70, 49);
            this.buttonRedo.Text = "Повторить";
            this.buttonRedo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonRedo.Click += new System.EventHandler(this.buttonRedo_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 52);
            // 
            // toolStripPanelItem1
            // 
            this.toolStripPanelItem1.CausesValidation = false;
            this.toolStripPanelItem1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.toolStripPanelItem1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonCut,
            this.buttonCopy});
            this.toolStripPanelItem1.Name = "toolStripPanelItem1";
            this.toolStripPanelItem1.Size = new System.Drawing.Size(96, 52);
            this.toolStripPanelItem1.Text = "toolStripPanelItem1";
            this.toolStripPanelItem1.Transparent = true;
            // 
            // buttonCut
            // 
            this.buttonCut.Image = global::DocumentFlow.Properties.Resources.icons8_cut_16;
            this.buttonCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonCut.Name = "buttonCut";
            this.buttonCut.Size = new System.Drawing.Size(78, 20);
            this.buttonCut.Text = "Вырезать";
            this.buttonCut.Click += new System.EventHandler(this.buttonCut_Click);
            // 
            // buttonCopy
            // 
            this.buttonCopy.Image = global::DocumentFlow.Properties.Resources.icons8_copy_16;
            this.buttonCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonCopy.Name = "buttonCopy";
            this.buttonCopy.Size = new System.Drawing.Size(92, 20);
            this.buttonCopy.Text = "Копировать";
            this.buttonCopy.Click += new System.EventHandler(this.buttonCopy_Click);
            // 
            // buttonPaste
            // 
            this.buttonPaste.Image = global::DocumentFlow.Properties.Resources.icons8_paste_30;
            this.buttonPaste.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonPaste.Name = "buttonPaste";
            this.buttonPaste.Size = new System.Drawing.Size(59, 49);
            this.buttonPaste.Text = "Вставить";
            this.buttonPaste.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonPaste.Click += new System.EventHandler(this.buttonPaste_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Image = global::DocumentFlow.Properties.Resources.icons8_cancel_30;
            this.buttonDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(55, 49);
            this.buttonDelete.Text = "Удалить";
            this.buttonDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // DiagramViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabSplitterContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "DiagramViewer";
            this.Size = new System.Drawing.Size(1010, 510);
            ((System.ComponentModel.ISupportInitialize)(this.diagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.model1)).EndInit();
            this.tabSplitterContainer1.ResumeLayout(false);
            this.pageDiagram.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.paletteGroupBar1)).EndInit();
            this.pageCode.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.editControl1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Syncfusion.Windows.Forms.Tools.TabSplitterContainer tabSplitterContainer1;
        private Syncfusion.Windows.Forms.Tools.TabSplitterPage pageDiagram;
        private Syncfusion.Windows.Forms.Tools.TabSplitterPage pageCode;
        private Syncfusion.Windows.Forms.Edit.EditControl editControl1;
        private Syncfusion.Windows.Forms.Diagram.Controls.Diagram diagram1;
        private Syncfusion.Windows.Forms.Diagram.Model model1;
        private Syncfusion.Windows.Forms.Diagram.Controls.PropertyEditor propertyEditor1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripButton buttonFill;
        private System.Windows.Forms.ToolStripButton buttonRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton buttonSaveAndClose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton buttonSave;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton buttonUndo;
        private System.Windows.Forms.ToolStripButton buttonRedo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private Syncfusion.Windows.Forms.Tools.ToolStripPanelItem toolStripPanelItem1;
        private System.Windows.Forms.ToolStripButton buttonCut;
        private System.Windows.Forms.ToolStripButton buttonCopy;
        private System.Windows.Forms.ToolStripButton buttonPaste;
        private System.Windows.Forms.ToolStripButton buttonDelete;
        private Syncfusion.Windows.Forms.Diagram.Controls.PaletteGroupBar paletteGroupBar1;
        private System.Windows.Forms.ToolStripButton buttonPreview;
        private System.Windows.Forms.ToolStripButton buttonPrint;
    }
}
