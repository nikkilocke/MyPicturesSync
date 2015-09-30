using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using FlickrNet;

namespace MyPicturesSync {
	/// <summary>
	/// Photo info from Flickr
	/// </summary>
	public class FlickrPhotoInfo {
		public string Id;

		public DateTime? Uploaded;

	}

	/// <summary>
	/// Local photo info
	/// </summary>
	public class PhotoInfo {
		public PhotoInfo() {
			Flickr = new FlickrPhotoInfo();
		}

		public PhotoInfo(string path)
			: this(new FileInfo(path)) {
		}

		public PhotoInfo(FileInfo f)
			: this() {
			FullName = f.FullName;
			FileName = f.Name;
			Folder = f.Directory.Name;
			Name = Path.GetFileNameWithoutExtension(f.Name);
			Description = Folder + "-" + Name;
		}

		public void LoadHash() {
			using (FileStream s = new FileStream(FullName, FileMode.Open, FileAccess.Read)) {
				using (SHA1Managed sha1 = new SHA1Managed()) {
					Hash = BitConverter.ToString(sha1.ComputeHash(s));
				}
			}
			FlickrPhotoInfo p;
			if (StatusForm.Instance.Hashes.TryGetValue(Hash, out p)) {
				Flickr = p;
			} else {
				StatusForm.Instance.Hashes[Hash] = Flickr;
			}
		}

		public string Hash;

		public string Description;

		public string FileName;

		public string FullName;

		public string Folder;

		public string Name;

		public FlickrPhotoInfo Flickr;

		public override string ToString() {
			return FileName;
		}
	}

	/// <summary>
	/// Local folder info
	/// </summary>
	public class Set {
		public Set(string path) {
			Folder = new DirectoryInfo(path);
			Photos = new List<PhotoInfo>();
			if (System.IO.File.Exists(Path.Combine(path, "ignore")))
				return;
			foreach (FileInfo file in Folder.EnumerateFiles("*.jpg")) {
				PhotoInfo p = new PhotoInfo(file);
				Photos.Add(p);
			}
		}

		/// <summary>
		/// Flickr set Id
		/// </summary>
		public string Id;

		public DirectoryInfo Folder;

		public List<PhotoInfo> Photos;

		public override string ToString() {
			return Folder.Name;
		}
	}

	/// <summary>
	/// Does the work of syncing with Flickr
	/// </summary>
	class Uploader {
		/// <summary>
		/// Running thread
		/// </summary>
		Task task;
		/// <summary>
		/// Signal to stop thread
		/// </summary>
		AutoResetEvent signal;
		/// <summary>
		/// Time to stop uploading
		/// </summary>
		DateTime stop;
		/// <summary>
		/// The Flickr instance
		/// </summary>
		Flickr Flickr;
		/// <summary>
		/// The Status form
		/// </summary>
		StatusForm Form;

		public Uploader(StatusForm form) {
			Form = form;
			Flickr = form.Flickr = Form.NewFlickr();
			if (Settings.Default.AccessToken != null) {
				Flickr.OAuthAccessToken = Settings.Default.AccessToken.Token;
				Flickr.OAuthAccessTokenSecret = Settings.Default.AccessToken.TokenSecret;
			}
			signal = new AutoResetEvent(false);
			task = new Task(UploadPhotoList);
		}

		/// <summary>
		/// Sets from the form (so if this thread runs on, it won't update the new Sets)
		/// </summary>
		public Dictionary<string, Set> Sets;

		/// <summary>
		/// AltSets from the form (so if this thread runs on, it won't update the new AltSets)
		/// </summary>
		public Dictionary<string, Set> AltSets;

		public void Start() {
			Form.Log("Start");
			task.Start();
		}

		public void Stop() {
			if (task != null) {
				Form.Log("Stop");
				signal.Set();
			}
		}

		/// <summary>
		/// The main thread
		/// </summary>
		void UploadPhotoList() {
			// If a folder is just a date (yyyy-mm-dd), we don't upload it - we only do so if
			// some text has been added to provide a meaningful name. This regex extracts the
			// meaningful part of the name in group 1
			Regex alt = new Regex(@"^[\d- ]*(.*?)$", RegexOptions.Compiled);
			do {
				try {
					Form.Log("Starting");
					Form.Sets = Sets = new Dictionary<string, Set>();
					Form.AltSets = AltSets = new Dictionary<string, Set>();
					Form.ClearFolders();
					Form.LogError("");
					foreach (string folder in Directory.EnumerateDirectories(Settings.Default.Folder)) {
						if (TaskCancelled()) return;
						Form.Status("Loading folder {0}", folder);
						Set s = new Set(folder);
						if (s.Photos.Count > 0) {
							Match m = alt.Match(s.Folder.Name);
							if (m.Success) {
								// The folder name starts with a date
								string key = m.Groups[1].Value;
								if (key == "") {
									Form.Error("Ignored folder {0} with no description");
									continue;
								}
								// Index set by name part only, in case date has been removed from Flickr set name
								AltSets[key] = s;
							}
							Sets[s.Folder.Name] = s;
							Form.AddFolder(s);
						}
					}
					// Have finished building local folder list
					Form.Status("");
					// Now compute hashes
					foreach (Set s in Sets.Values) {
						if (TaskCancelled()) return;
						Form.Status("Checking folder {0}", s.Folder.Name);
						int i = 0;
						foreach (PhotoInfo p in s.Photos) {
							if (TaskCancelled()) return;
							p.LoadHash();
							Form.UpdatePhoto(s, i++);
						}
					}
					Form.Status("");
					if (Settings.Default.AccessToken == null) {
						Form.Log("Not logged in");
						return;
					}
					// Wait until start time (if a start time is specified)
					if (Wait(false)) return;
					// Prevent computer going to sleep during upload
					uint executionState = NativeMethods.SetThreadExecutionState(NativeMethods.ES_CONTINUOUS | NativeMethods.ES_SYSTEM_REQUIRED);
					if (executionState == 0)
						Form.LogError("SetThreadExecutionState failed.");
					try {
						Form.Status("Downloading Flickr sets");
						// Get Flickr sets
						PhotosetCollection sets = Flickr.PhotosetsGetList(1, 1000);
						if (TaskCancelled()) return;
						foreach (Photoset set in sets) {
							Set ours;
							// Match Flickr sets to ours
							if (Sets.TryGetValue(set.Title, out ours) || AltSets.TryGetValue(set.Title, out ours)) {
								ours.Id = set.PhotosetId;
							}
						}
						foreach (Set s in Sets.Values) {
							if (TaskCancelled()) return;
							// Photos matched in the Flickr set
							HashSet<PhotoInfo> matched = new HashSet<PhotoInfo>();
							bool changed = false;
							if (s.Id != null) {
								// There is a Flickr set - get Flickr's list of photos in the set
								Form.Status("Downloading photo details for set {0}", s.Folder.Name);
								foreach (Photo p in Flickr.PhotosetsGetPhotos(s.Id, PhotoSearchExtras.DateUploaded, 1, 1000)) {
									if (TaskCancelled()) return;
									// Match by Id, name, file name or description
									PhotoInfo info = s.Photos.FirstOrDefault(pf => pf.Flickr.Id == p.PhotoId
										|| pf.Name == p.Title
										|| pf.FileName == p.Title
										|| pf.Description == p.Description);
									if (info != null) {
										matched.Add(info);
										if (info.Flickr.Uploaded != p.DateUploaded)
											changed = true;
										info.Flickr.Id = p.PhotoId;
										info.Flickr.Uploaded = p.DateUploaded;
									}
								}
							}
							// Now look for local photos not in the Flickr set
							foreach (PhotoInfo p in s.Photos) {
								if (!matched.Contains(p)) {
									if (p.Flickr.Id != null) {
										// Photo is already on Flickr, but is not in this set
										if (TaskCancelled()) return;
										if (DateTime.Now >= stop) {
											Form.Log("Reached stop time");
											return;
										}
										try {
											Flickr.PhotosetsAddPhoto(s.Id, p.Flickr.Id);
										} catch {
											// Cannot add photo - perhaps it was deleted from Flickr - mark it as missing
											p.Flickr.Id = null;
											p.Flickr.Uploaded = null;
											changed = true;
										}
									}
								}
							}
							if (changed)
								Form.UpdateSet(s);
							// Upload remaining photos
							foreach (PhotoInfo p in s.Photos) {
								if (TaskCancelled()) return;
								if (p.Flickr.Id != null)
									continue;	// Photo is already on Flickr
								if (DateTime.Now >= stop) {
									Form.Log("Reached stop time");
									return;
								}
								Form.Log("Uploading {0}", p.FullName);
								try {
									if (TaskCancelled()) return;
									p.Flickr.Id = Flickr.UploadPicture(p.FullName, p.Name, p.Description, "", false, true, false);
									p.Flickr.Uploaded = DateTime.Now;
									if (changed)
										Form.UpdatePhoto(s, s.Photos.IndexOf(p));
									if (s.Id == null) {
										Form.Log("Creating set {0}", s.Folder.Name);
										s.Id = Flickr.PhotosetsCreate(s.Folder.Name, p.Flickr.Id).PhotosetId;
									} else {
										Flickr.PhotosetsAddPhoto(s.Id, p.Flickr.Id);
									}
								} catch (Exception ex) {
									Form.LogError("Exception: {0}", ex);
								}
							}
						}
						Form.Status("");
						Form.LogError("Completed");
					} finally {
						if (executionState != 0)
							NativeMethods.SetThreadExecutionState(executionState);
					}
				} catch (Exception ex) {
					try {
						Form.LogError("Exception: {0}", ex);
					} catch {
					}
				}
			} while (Wait(true));
		}

		/// <summary>
		/// Check for stop signal
		/// </summary>
		bool TaskCancelled() {
			if (signal.WaitOne(1)) {
				Form.Status("Interrupted");
				return true;
			}
			return false;
		}

		/// <summary>
		/// Wait until start time (if specified)
		/// </summary>
		/// <param name="next">Wait until start time tomorrow</param>
		/// <returns>true if stop signal was received</returns>
		bool Wait(bool next) {
			DateTime start = DateTime.Now;
			// Have they specified to start and stop in the same day, and we have already passed stop time?
			if (Settings.Default.Start < Settings.Default.Stop && start.Hour >= Settings.Default.Stop)
				next = true;
			if (next || start.Hour < Settings.Default.Start) {
				string day = "";
				start = new DateTime(start.Year, start.Month, start.Day, Settings.Default.Start, 0, 0);
				if (next && start.Hour >= Settings.Default.Start) {
					start = start.AddDays(1);
					day = " on " + start.ToString("dddd");
				}
				TimeSpan delay = start - DateTime.Now;
				Form.Log("Waiting {2} until {0:HH:mm}{1}", start, day, delay);
				int msec = (int)delay.TotalMilliseconds;
				if (msec > 0 && signal.WaitOne(msec)) {
					Form.Status("Interrupted");
					return true;
				}
			}
			// Have they specified a stop time?
			stop = Settings.Default.Stop >= 24 ? DateTime.MaxValue : new DateTime(start.Year, start.Month, start.Day, Settings.Default.Stop, 0, 0);
			if (stop < start)
				stop = stop.AddDays(1);
			return false;
		}

	}

	internal static class NativeMethods {
		// Import SetThreadExecutionState Win32 API and necessary flags
		[DllImport("kernel32.dll")]
		public static extern uint SetThreadExecutionState(uint esFlags);
		public const uint ES_CONTINUOUS = 0x80000000;
		public const uint ES_SYSTEM_REQUIRED = 0x00000001;
	}
}
