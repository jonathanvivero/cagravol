using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace SGE.Cagravol.Application.Core.Extensions.Utils
{
	public static class ImageUtils
	{

		private const int bytesPerPixel = 4;
		public static Bitmap ChangeOpacity(Image img, float opacityvalue)
		{
			Bitmap bmp = new Bitmap(img.Width, img.Height); // Determining Width and Height of Source Image
			Graphics graphics = Graphics.FromImage(bmp);
			ColorMatrix colormatrix = new ColorMatrix();
			colormatrix.Matrix33 = opacityvalue;
			ImageAttributes imgAttribute = new ImageAttributes();
			imgAttribute.SetColorMatrix(colormatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
			graphics.DrawImage(img, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imgAttribute);
			graphics.Dispose();   // Releasing all resource used by graphics 
			return bmp;
		}

		public static Image ChangeImageOpacity(Image originalImage, double opacity)
		{
			if ((originalImage.PixelFormat & PixelFormat.Indexed) == PixelFormat.Indexed)
			{
				// Cannot modify an image with indexed colors
				return originalImage;
			}

			Bitmap bmp = (Bitmap)originalImage.Clone();

			// Specify a pixel format.
			PixelFormat pxf = PixelFormat.Format32bppArgb;

			// Lock the bitmap's bits.
			Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
			BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, pxf);

			// Get the address of the first line.
			IntPtr ptr = bmpData.Scan0;

			// Declare an array to hold the bytes of the bitmap.
			// This code is specific to a bitmap with 32 bits per pixels 
			// (32 bits = 4 bytes, 3 for RGB and 1 byte for alpha).
			int numBytes = bmp.Width * bmp.Height * bytesPerPixel;
			byte[] argbValues = new byte[numBytes];

			// Copy the ARGB values into the array.
			System.Runtime.InteropServices.Marshal.Copy(ptr, argbValues, 0, numBytes);

			// Manipulate the bitmap, such as changing the
			// RGB values for all pixels in the the bitmap.
			for (int counter = 0; counter < argbValues.Length; counter += bytesPerPixel)
			{
				// argbValues is in format BGRA (Blue, Green, Red, Alpha)

				// If 100% transparent, skip pixel
				if (argbValues[counter + bytesPerPixel - 1] == 0)
					continue;

				int pos = 0;
				pos++; // B value
				pos++; // G value
				pos++; // R value

				argbValues[counter + pos] = (byte)(argbValues[counter + pos] * opacity);
			}

			// Copy the ARGB values back to the bitmap
			System.Runtime.InteropServices.Marshal.Copy(argbValues, 0, ptr, numBytes);

			// Unlock the bits.
			bmp.UnlockBits(bmpData);

			return bmp;
		}


		public static Bitmap AlterTransparency (Image image, byte alpha){

			Bitmap original = new Bitmap(image);
			Bitmap transparent = new Bitmap(image.Width, image.Height);
			

			Color c = Color.Black;
			Color v = Color.Black;
			
			for (int w = 0; w < image.Width; w++) {
				for (int h = 0; h < image.Height; h++) {
					c = original.GetPixel(w, h);
					v = Color.FromArgb(alpha, c.R, c.G, c.B);
					transparent.SetPixel(w, h, v);
				}
			}

			return transparent;

		}

	}
}
