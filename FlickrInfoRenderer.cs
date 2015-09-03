using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using MyPicturesSync;

namespace Manina.Windows.Forms {
	class FlickrInfoRenderer : ImageListView.ImageListViewRenderer {
		// Had to copy these because they are marked internal in ImageListView.Utility

		/// <summary>
		/// Gets the scaled size of an image required to fit
		/// in to the given size keeping the image aspect ratio.
		/// </summary>
		/// <param name="image">The source image.</param>
		/// <param name="fit">The size to fit in to.</param>
		/// <returns></returns>
		internal static Size GetSizedImageBounds(Image image, Size fit) {
			float f = System.Math.Max((float)image.Width / (float)fit.Width, (float)image.Height / (float)fit.Height);
			if (f < 1.0f) f = 1.0f; // Do not upsize small images
			int width = (int)System.Math.Round((float)image.Width / f);
			int height = (int)System.Math.Round((float)image.Height / f);
			return new Size(width, height);
		}
		/// <summary>
		/// Gets the bounding rectangle of an image required to fit
		/// in to the given rectangle keeping the image aspect ratio.
		/// </summary>
		/// <param name="image">The source image.</param>
		/// <param name="fit">The rectangle to fit in to.</param>
		/// <param name="hAlign">Horizontal image aligment in percent.</param>
		/// <param name="vAlign">Vertical image aligment in percent.</param>
		/// <returns></returns>
		internal static Rectangle GetSizedImageBounds(Image image, Rectangle fit, float hAlign, float vAlign) {
			Size scaled = GetSizedImageBounds(image, fit.Size);
			int x = fit.Left + (int)(hAlign / 100.0f * (float)(fit.Width - scaled.Width));
			int y = fit.Top + (int)(vAlign / 100.0f * (float)(fit.Height - scaled.Height));

			return new Rectangle(x, y, scaled.Width, scaled.Height);
		}
		/// <summary>
		/// Gets the bounding rectangle of an image required to fit
		/// in to the given rectangle keeping the image aspect ratio.
		/// The image will be centered in the fit box.
		/// </summary>
		/// <param name="image">The source image.</param>
		/// <param name="fit">The rectangle to fit in to.</param>
		/// <returns></returns>
		internal static Rectangle GetSizedImageBounds(Image image, Rectangle fit) {
			return GetSizedImageBounds(image, fit, 50.0f, 50.0f);
		}

		public override void DrawItem(Graphics g, ImageListViewItem item, ItemState state, Rectangle bounds) {
			base.DrawItem(g, item, state, bounds);
			PhotoInfo p = item.Tag as PhotoInfo;
			if (ImageListView.View != View.Details && p != null && p.Flickr.Uploaded != null) {
				// Draw the image
				Image img = item.ThumbnailImage;
				if (img != null) {
					Size itemPadding = new Size(4, 4);
					Rectangle pos = GetSizedImageBounds(img, new Rectangle(bounds.Location + itemPadding, ImageListView.ThumbnailSize));
					Image overlayImage = Resource1.Uploaded;
					int w = Math.Min(overlayImage.Width, pos.Width);
					int h = Math.Min(overlayImage.Height, pos.Height);
					g.DrawImage(overlayImage, pos.Left, pos.Bottom - h, w, h);
				}
			}
		}
	}

	class FlickrInfoAdapter : ImageListViewItemAdaptors.FileSystemAdaptor {
		ImageListView _view;

		public FlickrInfoAdapter(ImageListView view) {
			_view = view;
		}

		/// <summary>
		/// Returns the details for the given item.
		/// </summary>
		/// <param name="key">Item key.</param>
		/// <param name="useWIC">true to use Windows Imaging Component; otherwise false.</param>
		/// <returns>An array of tuples containing item details or null if an error occurs.</returns>
		public override Utility.Tuple<ColumnType, string, object>[] GetDetails(object key, bool useWIC) {
			Utility.Tuple<ColumnType, string, object>[] result = base.GetDetails(key, useWIC);
			if(result == null)
				return null;
			string filename = (string)key;
			List<Utility.Tuple<ColumnType, string, object>> details = new List<Utility.Tuple<ColumnType, string, object>>(result);
			ImageListViewItem item = _view.Items.FirstOrDefault(v => v.FileName == filename);
			if (item != null) {
				PhotoInfo p = item.Tag as PhotoInfo;
				if (p != null) {
					details.Add(new Utility.Tuple<ColumnType, string, object>(ColumnType.Custom, "Flickr Id", p.Flickr.Id));
					details.Add(new Utility.Tuple<ColumnType, string, object>(ColumnType.Custom, "Uploaded", p.Flickr.Uploaded == null ? null : ((DateTime)p.Flickr.Uploaded).ToString("g")));
				}
			}
			return details.ToArray();
		}

	}
}
