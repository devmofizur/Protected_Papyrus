namespace ProjectPapyrus
{
    partial class DocumentViewer
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
                connection.Close();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DocumentViewer));
            this.ViewImage = new System.Windows.Forms.PictureBox();
            this.ViewWeb = new System.Windows.Forms.WebBrowser();
            this.PdfViewer = new PdfiumViewer.PdfViewer();
            ((System.ComponentModel.ISupportInitialize)(this.ViewImage)).BeginInit();
            this.SuspendLayout();
            // 
            // ViewImage
            // 
            this.ViewImage.BackColor = System.Drawing.Color.Snow;
            this.ViewImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ViewImage.Location = new System.Drawing.Point(0, 0);
            this.ViewImage.Margin = new System.Windows.Forms.Padding(0);
            this.ViewImage.Name = "ViewImage";
            this.ViewImage.Size = new System.Drawing.Size(1346, 719);
            this.ViewImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.ViewImage.TabIndex = 0;
            this.ViewImage.TabStop = false;
            this.ViewImage.Visible = false;
            // 
            // ViewWeb
            // 
            this.ViewWeb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ViewWeb.Location = new System.Drawing.Point(0, 0);
            this.ViewWeb.Margin = new System.Windows.Forms.Padding(0);
            this.ViewWeb.MinimumSize = new System.Drawing.Size(20, 20);
            this.ViewWeb.Name = "ViewWeb";
            this.ViewWeb.Size = new System.Drawing.Size(1346, 719);
            this.ViewWeb.TabIndex = 1;
            this.ViewWeb.Visible = false;
            // 
            // PdfViewer
            // 
            this.PdfViewer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.PdfViewer.BackColor = System.Drawing.Color.Snow;
            this.PdfViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PdfViewer.Location = new System.Drawing.Point(0, 0);
            this.PdfViewer.Name = "PdfViewer";
            this.PdfViewer.Size = new System.Drawing.Size(1346, 719);
            this.PdfViewer.TabIndex = 2;
            this.PdfViewer.Visible = false;
            this.PdfViewer.Load += new System.EventHandler(this.PdfViewer_Load);
            // 
            // DocumentViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1346, 719);
            this.Controls.Add(this.ViewWeb);
            this.Controls.Add(this.PdfViewer);
            this.Controls.Add(this.ViewImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DocumentViewer";
            this.Text = "DocumentViewer";
            this.Load += new System.EventHandler(this.HandleDocumentViewerLoad);
            ((System.ComponentModel.ISupportInitialize)(this.ViewImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox ViewImage;
        private System.Windows.Forms.WebBrowser ViewWeb;
        private PdfiumViewer.PdfViewer PdfViewer;
    }
}