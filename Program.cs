using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyPicturesSync {
	static class Program {
		public const string ApiKey = "9e9eee5333c3f6b07e9067a43e637b8d";
		public const string SharedSecret = "def55661cdc8d71b";
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() {
			try {
				if (string.IsNullOrWhiteSpace(Settings.Default.Folder)) {
					Settings.Default.Folder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
					Settings.Default.Save();
				}
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new StatusForm());
			} catch (Exception ex) {
				System.Diagnostics.Debug.WriteLine(ex.ToString());
			}
		}
	}
}
