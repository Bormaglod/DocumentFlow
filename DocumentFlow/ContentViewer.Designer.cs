namespace DocumentFlow
{
    partial class ContentViewer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.viewerControl1 = new DocumentFlow.ViewerControl();
            this.SuspendLayout();
            // 
            // viewerControl1
            // 
            this.viewerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewerControl1.Location = new System.Drawing.Point(0, 0);
            this.viewerControl1.Name = "viewerControl1";
            this.viewerControl1.Size = new System.Drawing.Size(800, 450);
            this.viewerControl1.TabIndex = 0;
            // 
            // ContentViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.viewerControl1);
            this.Name = "ContentViewer";
            this.Text = "ContentViewer";
            this.ResumeLayout(false);

        }

        #endregion

        private ViewerControl viewerControl1;
    }
}