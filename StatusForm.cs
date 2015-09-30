using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Runtime.Serialization;
using Manina.Windows.Forms;
using FlickrNet;

namespace MyPicturesSync {
	public partial class StatusForm : Form {
		/// <summary>
		/// So other classes can access this form
		/// </summary>
		public static StatusForm Instance;

		/// <summary>
		/// MyPicturesSync Api key and shared secret for Flickr API
		/// </summary>
		public const string ApiKey = "9e9eee5333c3f6b07e9067a43e637b8d";
		public const string SharedSecret = "def55661cdc8d71b";

		/// <summary>
		/// Flickr object talks to Flickr
		/// </summary>
		public Flickr Flickr;

		/// <summary>
		/// Index known Flickr photo info by SHA1 hash of photo
		/// </summary>
		public Dictionary<string, FlickrPhotoInfo> Hashes;

		/// <summary>
		/// Folders on this computer (subfolders of My Pictures) containing pictures
		/// indexed by folder name
		/// </summary>
		public Dictionary<string, Set> Sets;

		/// <summary>
		/// Folders indexed by folder name without leading date
		/// </summary>
		public Dictionary<string, Set> AltSets;

		/// <summary>
		/// This is the class that syncs with Flickr
		/// </summary>
		Uploader uploader;

		/// <summary>
		/// Adapter to show additional details from Flickr in the columns
		/// </summary>
		FlickrInfoAdapter adapter;

		public StatusForm() {
			Instance = this;
			LoadHashes();
			Flickr = NewFlickr();
			InitializeComponent();
			adapter = new FlickrInfoAdapter(Files);
			Files.SetRenderer(new Manina.Windows.Forms.FlickrInfoRenderer());
		}

		/// <summary>
		/// Create a new Flickr object (e.g. when logging in as a different user)
		/// </summary>
		public Flickr NewFlickr() {
			return new Flickr(ApiKey, SharedSecret);
		}

		private void StatusForm_Load(object sender, EventArgs e) {
			foreach (ColumnType t in Enum.GetValues(typeof(ColumnType))) {
				if (t != ColumnType.Custom)
					Files.Columns.Add(t);
			}
			Files.Columns.Add(new ImageListView.ImageListViewColumnHeader(ColumnType.Custom, "Uploaded", 100));
			Files.Columns.Add(new ImageListView.ImageListViewColumnHeader(ColumnType.Custom, "Flickr Id", 100));
			// Set the column visibility and widths from the user's settings file
			int[] arr = Settings.Default.ColumnWidths.Split(',').Select(s => Int32.Parse(s)).ToArray();
			int c = 0;
			foreach(var col in Files.Columns) {
				col.Visible = Settings.Default.Columns.Contains(col.Text);
				col.Width = c >= arr.Length ? 100 : arr[c++];
				ToolStripMenuItem m = new ToolStripMenuItem(col.Text) {
					Checked = col.Visible
				};
				m.Click += ColumnToolStripMenuItem_Click;
				detailsToolStripButton.DropDownItems.Add(m);
			}
			UpdateView();		// Set other settings for the photo viewer
			// If not logged in, open the settings dialog so they can
			if (Settings.Default.AccessToken == null)
				new SettingsForm().ShowDialog();
			StartUpload();
		}

		private void StatusForm_FormClosing(object sender, FormClosingEventArgs e) {
			StopUpload();
			// Save column widths
			Settings.Default.ColumnWidths = string.Join(",", Files.Columns.Select(c => c.Width.ToString()).ToArray());
			Settings.Default.Save();
			// Save hash index
			SaveHashes();
		}

		/// <summary>
		/// Start syncing with Flickr (in another thread)
		/// </summary>
		void StartUpload() {
			StopUpload();
			uploader = new Uploader(this);
			uploader.Start();
		}

		/// <summary>
		/// Stop Flickr sync thread
		/// </summary>
		void StopUpload() {
			if (uploader != null) {
				uploader.Stop();
				uploader = null;
			}
		}

		/// <summary>
		/// Load index of picture SHA1 hash to Flickr photo id
		/// </summary>
		void LoadHashes() {
			if(Settings.Default.AccessToken != null) {
				try {
					// Save to file corresponding to Flickr user name
					string fileName = Path.Combine(Settings.Default.Folder, Settings.Default.AccessToken.Username + ".hash");
					if (File.Exists(fileName)) {
						using (XmlReader reader = XmlReader.Create(fileName)) {
							DataContractSerializer serializer = new DataContractSerializer(typeof(Dictionary<string, FlickrPhotoInfo>));
							Hashes = (Dictionary<string, FlickrPhotoInfo>)serializer.ReadObject(reader);
							return;
						}
					}
				} catch (Exception ex) {
					LogError("HashLoad:{0}", ex);
				}
			}
			Hashes = new Dictionary<string, FlickrPhotoInfo>();
		}

		/// <summary>
		/// Save index of picture SHA1 hash to Flickr photo id
		/// </summary>
		void SaveHashes() {
			if (Settings.Default.AccessToken != null) {
				try {
					// Load from file corresponding to Flickr user name
					string fileName = Path.Combine(Settings.Default.Folder, Settings.Default.AccessToken.Username + ".hash");
					XmlWriterSettings settings = new XmlWriterSettings();
					settings.Indent = true;
					using (XmlWriter writer = XmlWriter.Create(fileName, settings)) {
						DataContractSerializer serializer = new DataContractSerializer(typeof(Dictionary<string, FlickrPhotoInfo>));
						serializer.WriteObject(writer, Hashes);
					}
				} catch (Exception ex) {
					LogError("HashSave:{0}", ex);
				}
			}
		}

		/// <summary>
		/// Add details of a found local folder to the UI
		/// </summary>
		public void AddFolder(Set s) {
			if (InvokeRequired) {
				Action action = delegate() { AddFolder(s); };
				Invoke(action);
			} else {
				Folders.Items.Add(s);
			}
		}

		/// <summary>
		/// Remove all folders from the UI
		/// </summary>
		public void ClearFolders() {
			if (InvokeRequired) {
				Action action = delegate() { ClearFolders(); };
				Invoke(action);
			} else {
				Folders.Items.Clear();
			}
		}

		/// <summary>
		/// Display and log an error
		/// </summary>
		public void LogError(string message) {
			if (string.IsNullOrWhiteSpace(message)) {
				Error("");
			} else {
				using (StreamWriter w = new StreamWriter("MyPicturesSync.log", true))
					w.WriteLine(DateTime.Now.ToString("s") + ":" + message);
				Error(string.Format("{0:HH:mm} {1}", DateTime.Now, message.Split('\n')[0]));
			}
		}

		/// <summary>
		/// Display and log an error
		/// </summary>
		public void LogError(string format, params object[] args) {
			LogError(string.Format(format, args));
		}

		/// <summary>
		/// Display something in the error box
		/// </summary>
		public void Error(string message) {
			if (InvokeRequired) {
				Action action = delegate() { Error(message); };
				Invoke(action);
			} else {
				lblError.Text = message;
			}
		}

		/// <summary>
		/// Display something in the error box
		/// </summary>
		public void Error(string format, params object[] args) {
			Error(string.Format(format, args));
		}

		/// <summary>
		/// Display something in the status box
		/// </summary>
		public void Status(string message) {
			if (InvokeRequired) {
				Action action = delegate() { Status(message); };
				Invoke(action);
			} else {
				lblStatus.Text = message;
			}
		}

		/// <summary>
		/// Display something in the status box
		/// </summary>
		public void Status(string format, params object[] args) {
			Status(string.Format(format, args));
		}

		/// <summary>
		/// Log and display something in the status box
		/// </summary>
		public void Log(string message) {
			using (StreamWriter w = new StreamWriter("MyPicturesSync.log", true))
				w.WriteLine(DateTime.Now.ToString("s") + ":" + message);
			Status(message.Split('\n')[0]);
		}

		/// <summary>
		/// Log and display something in the status box
		/// </summary>
		public void Log(string format, params object[] args) {
			Log(string.Format(format, args));
		}

		/// <summary>
		/// A Set's (folder) details have changed - update the UI
		/// </summary>
		/// <param name="set"></param>
		public void UpdateSet(Set set) {
			if (InvokeRequired) {
				Action action = delegate() { UpdateSet(set); };
				Invoke(action);
			} else {
				Set s = (Set)Folders.SelectedItem;
				if (s == set)
					Files.Refresh();
			}
		}

		/// <summary>
		/// An individual photo's details have changed - update the UI
		/// </summary>
		public void UpdatePhoto(Set set, int photo) {
			if (InvokeRequired) {
				Action action = delegate() { UpdatePhoto(set, photo); };
				Invoke(action);
			} else {
				Set s = (Set)Folders.SelectedItem;
				if (s == set)
					Files.Refresh();
			}
		}

		/// <summary>
		/// Set the photo viewer view mode and columns from Settings
		/// </summary>
		public void UpdateView() {
			try {
				Files.View = (Manina.Windows.Forms.View)Enum.Parse(typeof(Manina.Windows.Forms.View), Settings.Default.View);
			} catch {
			}
			for(int i = 0; i < Files.Columns.Count; i++) {
				Files.Columns[i].Visible = Settings.Default.Columns.Contains(Files.Columns[i].Text);
			}
		}

		private void Folders_SelectedIndexChanged(object sender, EventArgs e) {
			Files.Items.Clear();
			Set s = (Set)Folders.SelectedItem;
			if (s != null) {
				foreach (PhotoInfo p in s.Photos) {
					Files.Items.Add(new ImageListViewItem(p.FullName) {
						Tag = p
					}, adapter);
				}
			}
		}

		private void btnSettings_Click(object sender, EventArgs e) {
			StopUpload();
			SaveHashes();	// Because Settings may change user name
			new SettingsForm().ShowDialog();
			LoadHashes();
			StartUpload();
		}

		private void ColumnToolStripMenuItem_Click(object sender, EventArgs e) {
			System.Windows.Forms.ToolStripMenuItem item = (System.Windows.Forms.ToolStripMenuItem)sender;
			item.Checked = !item.Checked;
			Settings.Default.Columns.Clear();
			foreach (ToolStripMenuItem i in detailsToolStripButton.DropDownItems) {
				if (i.Checked)
					Settings.Default.Columns.Add(i.Text);
			}
			Settings.Default.Save();
			UpdateView();
		}

		private void paneToolStripButton_Click(object sender, EventArgs e) {
			Settings.Default.View = "Pane";
			Settings.Default.Save();
			UpdateView();
		}

		private void galleryToolStripButton_Click(object sender, EventArgs e) {
			Settings.Default.View = "Gallery";
			Settings.Default.Save();
			UpdateView();
		}

		private void thumbnailsToolStripButton_Click(object sender, EventArgs e) {
			Settings.Default.View = "Thumbnails";
			Settings.Default.Save();
			UpdateView();
		}

		private void detailsToolStripButton_Click(object sender, EventArgs e) {
			Settings.Default.View = "Details";
			Settings.Default.Save();
			UpdateView();
		}

	}

}
