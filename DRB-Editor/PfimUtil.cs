using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;

namespace DRB_Editor
{
    internal static class PfimUtil
    {
        public static Bitmap LoadTexture(byte[] bytes)
        {
            Pfim.IImage pfimImage;
            using (var ms = new MemoryStream(bytes))
            {
                pfimImage = Pfim.Pfim.FromStream(ms);
            }

            System.Drawing.Imaging.PixelFormat format;
            switch (pfimImage.Format)
            {
                case Pfim.ImageFormat.Rgb24:
                    format = System.Drawing.Imaging.PixelFormat.Format24bppRgb;
                    break;

                case Pfim.ImageFormat.Rgba32:
                    format = System.Drawing.Imaging.PixelFormat.Format32bppArgb;
                    break;

                case Pfim.ImageFormat.R5g5b5:
                    format = System.Drawing.Imaging.PixelFormat.Format16bppRgb555;
                    break;

                case Pfim.ImageFormat.R5g6b5:
                    format = System.Drawing.Imaging.PixelFormat.Format16bppRgb565;
                    break;

                case Pfim.ImageFormat.R5g5b5a1:
                    format = System.Drawing.Imaging.PixelFormat.Format16bppArgb1555;
                    break;

                case Pfim.ImageFormat.Rgb8:
                    format = System.Drawing.Imaging.PixelFormat.Format8bppIndexed;
                    break;

                default:
                    throw new FormatException($"{pfimImage.Format} is not recognized for Bitmap on Windows Forms.");
            }

            GCHandle handle = GCHandle.Alloc(pfimImage.Data, GCHandleType.Pinned);
            IntPtr ptr = Marshal.UnsafeAddrOfPinnedArrayElement(pfimImage.Data, 0);
            var bitmap = new Bitmap(pfimImage.Width, pfimImage.Height, pfimImage.Stride, format, ptr);

            if (format == System.Drawing.Imaging.PixelFormat.Format8bppIndexed)
            {
                var palette = bitmap.Palette;
                for (int i = 0; i < 256; i++)
                {
                    palette.Entries[i] = Color.FromArgb((byte)i, (byte)i, (byte)i);
                }
                bitmap.Palette = palette;
            }

            // Just copy the whole thing so I don't have to maintain handles
            var copy = new Bitmap(bitmap);
            handle.Free();
            return copy;
        }
    }
}
