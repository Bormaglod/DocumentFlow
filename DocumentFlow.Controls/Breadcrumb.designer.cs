﻿namespace DocumentFlow.Controls
{
    partial class Breadcrumb
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
            Syncfusion.Windows.Forms.Tools.MetroSplitButtonRenderer metroSplitButtonRenderer1 = new Syncfusion.Windows.Forms.Tools.MetroSplitButtonRenderer();
            this.panelCrumbs = new Syncfusion.Windows.Forms.Tools.GradientPanel();
            this.buttonHome = new DocumentFlow.Controls.ToolButton();
            this.buttonRefresh = new DocumentFlow.Controls.ToolButton();
            this.buttonUp = new DocumentFlow.Controls.ToolButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelCrumbs)).BeginInit();
            this.panelCrumbs.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelCrumbs
            // 
            this.panelCrumbs.Border3DStyle = System.Windows.Forms.Border3DStyle.Flat;
            this.panelCrumbs.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.panelCrumbs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCrumbs.Controls.Add(this.buttonHome);
            this.panelCrumbs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCrumbs.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.panelCrumbs.Location = new System.Drawing.Point(25, 0);
            this.panelCrumbs.Name = "panelCrumbs";
            this.panelCrumbs.Size = new System.Drawing.Size(722, 25);
            this.panelCrumbs.TabIndex = 15;
            // 
            // buttonHome
            // 
            this.buttonHome.BackClickedColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.buttonHome.BackColor = System.Drawing.Color.White;
            this.buttonHome.BackHoveredColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.buttonHome.BorderClickedColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(209)))), ((int)(((byte)(255)))));
            this.buttonHome.BorderColor = System.Drawing.Color.White;
            this.buttonHome.BorderHoveredColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(232)))), ((int)(((byte)(235)))));
            this.buttonHome.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonHome.Kind = DocumentFlow.Controls.ToolButtonKind.Home;
            this.buttonHome.Location = new System.Drawing.Point(0, 0);
            this.buttonHome.Name = "buttonHome";
            this.buttonHome.Size = new System.Drawing.Size(23, 23);
            this.buttonHome.TabIndex = 0;
            this.buttonHome.Text = "toolButton5";
            this.buttonHome.Click += new System.EventHandler(this.buttonHome_Click);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.BackClickedColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.buttonRefresh.BackColor = System.Drawing.Color.White;
            this.buttonRefresh.BackHoveredColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.buttonRefresh.BorderClickedColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(209)))), ((int)(((byte)(255)))));
            this.buttonRefresh.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.buttonRefresh.BorderHoveredColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(232)))), ((int)(((byte)(235)))));
            this.buttonRefresh.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonRefresh.Kind = DocumentFlow.Controls.ToolButtonKind.Refresh;
            this.buttonRefresh.Location = new System.Drawing.Point(747, 0);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(25, 25);
            this.buttonRefresh.TabIndex = 14;
            this.buttonRefresh.Text = "toolButton4";
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // buttonUp
            // 
            this.buttonUp.BackClickedColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.buttonUp.BackColor = System.Drawing.Color.White;
            this.buttonUp.BackHoveredColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.buttonUp.BorderClickedColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(209)))), ((int)(((byte)(255)))));
            this.buttonUp.BorderColor = System.Drawing.Color.White;
            this.buttonUp.BorderHoveredColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(232)))), ((int)(((byte)(235)))));
            this.buttonUp.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonUp.Enabled = false;
            this.buttonUp.Kind = DocumentFlow.Controls.ToolButtonKind.Up;
            this.buttonUp.Location = new System.Drawing.Point(0, 0);
            this.buttonUp.Name = "buttonUp";
            this.buttonUp.Size = new System.Drawing.Size(25, 25);
            this.buttonUp.TabIndex = 13;
            this.buttonUp.Text = "toolButton3";
            this.buttonUp.Click += new System.EventHandler(this.buttonUp_Click);
            // 
            // Breadcrumb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.panelCrumbs);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.buttonUp);
            this.Name = "Breadcrumb";
            this.Size = new System.Drawing.Size(772, 25);
            ((System.ComponentModel.ISupportInitialize)(this.panelCrumbs)).EndInit();
            this.panelCrumbs.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private ToolButton buttonUp;
        private ToolButton buttonRefresh;
        private Syncfusion.Windows.Forms.Tools.GradientPanel panelCrumbs;
        private ToolButton buttonHome;
    }
}
