namespace MyPicturesSync {
	partial class SettingsForm {
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
			this.label1 = new System.Windows.Forms.Label();
			this.FolderName = new System.Windows.Forms.TextBox();
			this.Browse = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.Start = new System.Windows.Forms.ComboBox();
			this.Stop = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.UserName = new System.Windows.Forms.Label();
			this.VerifierLabel = new System.Windows.Forms.Label();
			this.VerifierCode = new System.Windows.Forms.TextBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnLogin = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(94, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "My Pictures Folder";
			// 
			// FolderName
			// 
			this.FolderName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.FolderName.Location = new System.Drawing.Point(164, 11);
			this.FolderName.Name = "FolderName";
			this.FolderName.ReadOnly = true;
			this.FolderName.Size = new System.Drawing.Size(471, 20);
			this.FolderName.TabIndex = 1;
			// 
			// Browse
			// 
			this.Browse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.Browse.Location = new System.Drawing.Point(644, 10);
			this.Browse.Name = "Browse";
			this.Browse.Size = new System.Drawing.Size(27, 20);
			this.Browse.TabIndex = 2;
			this.Browse.Text = "...";
			this.Browse.UseVisualStyleBackColor = true;
			this.Browse.Click += new System.EventHandler(this.Browse_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(13, 38);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(53, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Start hour";
			// 
			// Start
			// 
			this.Start.FormattingEnabled = true;
			this.Start.Items.AddRange(new object[] {
            "any time",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "midnight"});
			this.Start.Location = new System.Drawing.Point(164, 38);
			this.Start.Name = "Start";
			this.Start.Size = new System.Drawing.Size(134, 21);
			this.Start.TabIndex = 4;
			// 
			// Stop
			// 
			this.Stop.FormattingEnabled = true;
			this.Stop.Items.AddRange(new object[] {
            "midnight",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "when complete"});
			this.Stop.Location = new System.Drawing.Point(504, 38);
			this.Stop.Name = "Stop";
			this.Stop.Size = new System.Drawing.Size(131, 21);
			this.Stop.TabIndex = 4;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(357, 38);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(53, 13);
			this.label3.TabIndex = 3;
			this.label3.Text = "Stop hour";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(13, 68);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(29, 13);
			this.label4.TabIndex = 5;
			this.label4.Text = "User";
			// 
			// UserName
			// 
			this.UserName.AutoSize = true;
			this.UserName.Location = new System.Drawing.Point(166, 68);
			this.UserName.Name = "UserName";
			this.UserName.Size = new System.Drawing.Size(55, 13);
			this.UserName.TabIndex = 6;
			this.UserName.TabStop = true;
			this.UserName.Text = "                ";
			// 
			// VerifierLabel
			// 
			this.VerifierLabel.AutoSize = true;
			this.VerifierLabel.Location = new System.Drawing.Point(12, 94);
			this.VerifierLabel.Name = "VerifierLabel";
			this.VerifierLabel.Size = new System.Drawing.Size(66, 13);
			this.VerifierLabel.TabIndex = 7;
			this.VerifierLabel.Text = "Verifier code";
			// 
			// VerifierCode
			// 
			this.VerifierCode.Location = new System.Drawing.Point(164, 87);
			this.VerifierCode.Name = "VerifierCode";
			this.VerifierCode.Size = new System.Drawing.Size(134, 20);
			this.VerifierCode.TabIndex = 8;
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(584, 132);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 9;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(19, 132);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 10;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnLogin
			// 
			this.btnLogin.Location = new System.Drawing.Point(356, 63);
			this.btnLogin.Name = "btnLogin";
			this.btnLogin.Size = new System.Drawing.Size(75, 23);
			this.btnLogin.TabIndex = 11;
			this.btnLogin.Text = "Login";
			this.btnLogin.UseVisualStyleBackColor = true;
			this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
			// 
			// SettingsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(681, 165);
			this.Controls.Add(this.btnLogin);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.VerifierCode);
			this.Controls.Add(this.VerifierLabel);
			this.Controls.Add(this.UserName);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.Stop);
			this.Controls.Add(this.Start);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.Browse);
			this.Controls.Add(this.FolderName);
			this.Controls.Add(this.label1);
			this.Name = "SettingsForm";
			this.Text = "Settings";
			this.Load += new System.EventHandler(this.SettingsForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox FolderName;
		private System.Windows.Forms.Button Browse;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox Start;
		private System.Windows.Forms.ComboBox Stop;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label UserName;
		private System.Windows.Forms.Label VerifierLabel;
		private System.Windows.Forms.TextBox VerifierCode;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnLogin;

	}
}