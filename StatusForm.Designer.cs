namespace MyPicturesSync {
	partial class StatusForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StatusForm));
			this.lblStatus = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.Files = new Manina.Windows.Forms.ImageListView();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.Folders = new System.Windows.Forms.ListBox();
			this.SetName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lblError = new System.Windows.Forms.Label();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.paneToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.galleryToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.thumbnailsToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.detailsToolStripButton = new System.Windows.Forms.ToolStripDropDownButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.btnSettings = new System.Windows.Forms.ToolStripButton();
			this.panel1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblStatus
			// 
			this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblStatus.Location = new System.Drawing.Point(0, 393);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(667, 18);
			this.lblStatus.TabIndex = 0;
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.Controls.Add(this.Files);
			this.panel1.Controls.Add(this.splitter1);
			this.panel1.Controls.Add(this.Folders);
			this.panel1.Location = new System.Drawing.Point(4, 28);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(663, 362);
			this.panel1.TabIndex = 3;
			// 
			// Files
			// 
			this.Files.CacheLimit = "0";
			this.Files.ColumnHeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.Files.DefaultImage = ((System.Drawing.Image)(resources.GetObject("Files.DefaultImage")));
			this.Files.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Files.ErrorImage = ((System.Drawing.Image)(resources.GetObject("Files.ErrorImage")));
			this.Files.GroupHeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.Files.Location = new System.Drawing.Point(183, 0);
			this.Files.Name = "Files";
			this.Files.PersistentCacheDirectory = "";
			this.Files.PersistentCacheSize = ((long)(100));
			this.Files.Size = new System.Drawing.Size(480, 362);
			this.Files.TabIndex = 3;
			this.Files.UseEmbeddedThumbnails = Manina.Windows.Forms.UseEmbeddedThumbnails.Never;
			this.Files.View = Manina.Windows.Forms.View.Pane;
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(180, 0);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(3, 362);
			this.splitter1.TabIndex = 2;
			this.splitter1.TabStop = false;
			// 
			// Folders
			// 
			this.Folders.Dock = System.Windows.Forms.DockStyle.Left;
			this.Folders.Location = new System.Drawing.Point(0, 0);
			this.Folders.Name = "Folders";
			this.Folders.Size = new System.Drawing.Size(180, 362);
			this.Folders.TabIndex = 1;
			this.Folders.SelectedIndexChanged += new System.EventHandler(this.Folders_SelectedIndexChanged);
			// 
			// SetName
			// 
			this.SetName.Text = "Photo Set";
			// 
			// lblError
			// 
			this.lblError.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblError.ForeColor = System.Drawing.Color.Red;
			this.lblError.Location = new System.Drawing.Point(0, 411);
			this.lblError.Name = "lblError";
			this.lblError.Size = new System.Drawing.Size(667, 18);
			this.lblError.TabIndex = 4;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.paneToolStripButton,
            this.galleryToolStripButton,
            this.thumbnailsToolStripButton,
            this.detailsToolStripButton,
            this.toolStripSeparator1,
            this.btnSettings});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(669, 25);
			this.toolStrip1.TabIndex = 5;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// paneToolStripButton
			// 
			this.paneToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.paneToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("paneToolStripButton.Image")));
			this.paneToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.paneToolStripButton.Name = "paneToolStripButton";
			this.paneToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.paneToolStripButton.Text = "Pane view";
			this.paneToolStripButton.ToolTipText = "Pane view";
			this.paneToolStripButton.Click += new System.EventHandler(this.paneToolStripButton_Click);
			// 
			// galleryToolStripButton
			// 
			this.galleryToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.galleryToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("galleryToolStripButton.Image")));
			this.galleryToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.galleryToolStripButton.Name = "galleryToolStripButton";
			this.galleryToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.galleryToolStripButton.Text = "Gallery view";
			this.galleryToolStripButton.ToolTipText = "Gallery view";
			this.galleryToolStripButton.Click += new System.EventHandler(this.galleryToolStripButton_Click);
			// 
			// thumbnailsToolStripButton
			// 
			this.thumbnailsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.thumbnailsToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("thumbnailsToolStripButton.Image")));
			this.thumbnailsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.thumbnailsToolStripButton.Name = "thumbnailsToolStripButton";
			this.thumbnailsToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.thumbnailsToolStripButton.Text = "Thumbnails view";
			this.thumbnailsToolStripButton.ToolTipText = "Thumbnails view";
			this.thumbnailsToolStripButton.Click += new System.EventHandler(this.thumbnailsToolStripButton_Click);
			// 
			// detailsToolStripButton
			// 
			this.detailsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.detailsToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("detailsToolStripButton.Image")));
			this.detailsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.detailsToolStripButton.Name = "detailsToolStripButton";
			this.detailsToolStripButton.Size = new System.Drawing.Size(29, 22);
			this.detailsToolStripButton.Text = "Details view";
			this.detailsToolStripButton.ToolTipText = "Details view";
			this.detailsToolStripButton.Click += new System.EventHandler(this.detailsToolStripButton_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// btnSettings
			// 
			this.btnSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnSettings.Image = ((System.Drawing.Image)(resources.GetObject("btnSettings.Image")));
			this.btnSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnSettings.Name = "btnSettings";
			this.btnSettings.Size = new System.Drawing.Size(23, 22);
			this.btnSettings.Text = "Settings";
			this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
			// 
			// StatusForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(669, 427);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.lblError);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.lblStatus);
			this.Name = "StatusForm";
			this.Text = "My Pictures Sync";
			this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StatusForm_FormClosing);
			this.Load += new System.EventHandler(this.StatusForm_Load);
			this.panel1.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.Panel panel1;
		private Manina.Windows.Forms.ImageListView Files;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.ListBox Folders;
		private System.Windows.Forms.ColumnHeader SetName;
		private System.Windows.Forms.Label lblError;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton paneToolStripButton;
		private System.Windows.Forms.ToolStripButton galleryToolStripButton;
		private System.Windows.Forms.ToolStripButton thumbnailsToolStripButton;
		private System.Windows.Forms.ToolStripDropDownButton detailsToolStripButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton btnSettings;
	}
}

