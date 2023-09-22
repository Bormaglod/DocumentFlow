namespace DocumentFlow.Controls.PageContents
{
    partial class StartPage
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
            toolStrip1 = new ToolStrip();
            buttonRefresh = new ToolStripButton();
            panelCards = new Panel();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { buttonRefresh });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(968, 52);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // buttonRefresh
            // 
            buttonRefresh.Alignment = ToolStripItemAlignment.Right;
            buttonRefresh.Image = DocumentFlow.Properties.Resources.icons8_refresh_30;
            buttonRefresh.ImageScaling = ToolStripItemImageScaling.None;
            buttonRefresh.ImageTransparentColor = Color.Magenta;
            buttonRefresh.Name = "buttonRefresh";
            buttonRefresh.Size = new Size(65, 49);
            buttonRefresh.Text = "Обновить";
            buttonRefresh.TextImageRelation = TextImageRelation.ImageAboveText;
            buttonRefresh.Click += ButtonRefresh_Click;
            // 
            // panelCards
            // 
            panelCards.AutoScroll = true;
            panelCards.Dock = DockStyle.Fill;
            panelCards.Location = new Point(0, 52);
            panelCards.Name = "panelCards";
            panelCards.Size = new Size(968, 325);
            panelCards.TabIndex = 1;
            // 
            // StartPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            Controls.Add(panelCards);
            Controls.Add(toolStrip1);
            Name = "StartPage";
            Size = new Size(968, 377);
            SizeChanged += StartPage_SizeChanged;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripButton buttonRefresh;
        private Panel panelCards;
    }
}
