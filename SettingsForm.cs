using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FlickrNet;

namespace MyPicturesSync {
	public partial class SettingsForm : Form {
		OAuthRequestToken requestToken;

		public SettingsForm() {
			InitializeComponent();
		}

		private void SettingsForm_Load(object sender, EventArgs e) {
			FolderName.Text = Settings.Default.Folder;
			Start.SelectedIndex = Settings.Default.Start;
			Stop.SelectedIndex = Settings.Default.Stop;
			if (Settings.Default.AccessToken == null) {
				// Not logged in - force login
				Login();
			} else {
				// Logged in - hide verifier label
				VerifierLabel.Visible = false;
				VerifierCode.Visible = false;
				UserName.Text = Settings.Default.AccessToken.FullName;
				btnLogin.Text = "Change user";
			}
		}

		private void Browse_Click(object sender, EventArgs e) {
			FolderBrowserDialog dlg = new FolderBrowserDialog();
			dlg.SelectedPath = FolderName.Text;
			if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				FolderName.Text = dlg.SelectedPath;
		}

		private void btnOK_Click(object sender, EventArgs e) {
			Settings.Default.Folder = FolderName.Text;
			Settings.Default.Start = Start.SelectedIndex;
			Settings.Default.Stop = Stop.SelectedIndex;
			if (VerifierCode.Visible && VerifierCode.Text.Trim() != "") {
				try {
					Flickr f = StatusForm.Instance.NewFlickr();
					var accessToken = StatusForm.Instance.Flickr.OAuthGetAccessToken(requestToken, VerifierCode.Text.Trim());
					Settings.Default.AccessToken = accessToken;
					StatusForm.Instance.Flickr = f;
					StatusForm.Instance.LogError("Successfully authenticated as {0}", accessToken.FullName);
				} catch (FlickrApiException ex) {
					StatusForm.Instance.LogError("Failed to get access token. {0}", ex);
				}
			}
			Settings.Default.Save();
		}

		private void btnLogin_Click(object sender, EventArgs e) {
			Login();
		}

		/// <summary>
		/// Log in to Flickr
		/// </summary>
		void Login() {
			requestToken = StatusForm.Instance.Flickr.OAuthGetRequestToken("oob");
			string url = StatusForm.Instance.Flickr.OAuthCalculateAuthorizationUrl(requestToken.Token, AuthLevel.Write);
			System.Diagnostics.Process.Start(url);
			VerifierLabel.Visible = true;
			VerifierCode.Visible = true;
			VerifierCode.Focus();
		}

	}
}
