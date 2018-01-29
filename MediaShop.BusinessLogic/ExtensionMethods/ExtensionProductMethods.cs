using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MediaShop.BusinessLogic.Properties;
using MediaShop.Common.Enums;

namespace MediaShop.BusinessLogic.ExtensionMethods
{
    /// <summary>
    /// Class with Methods for file processing
    /// </summary>
    public static class ExtensionProductMethods
    {
        /// <summary>
        /// Get protected copy of image in original size
        /// </summary>
        /// <param name="originalImageByte">original file in byte[]</param>
        /// <returns>Protected file</returns>
        public static byte[] GetProtectedImage(this byte[] originalImageByte)
        {
            byte[] fileByte = new byte[1];
            if (!ReferenceEquals(originalImageByte, null))
            {
                fileByte = new byte[originalImageByte.Length];
                Array.Copy(originalImageByte, fileByte, originalImageByte.Length);
            }

            Bitmap originalImageBitmap;

            using (MemoryStream ms = new MemoryStream(fileByte))
            {
                originalImageBitmap = (Bitmap)Image.FromStream(ms);
            }

            var watermarkBitmap = Resources.WaterMark;
            using (Graphics gr = Graphics.FromImage(originalImageBitmap))
            {
                float opacity = 0;
                float.TryParse(Resources.WatermarkOpacity, out opacity);

                ImageAttributes attr = new ImageAttributes();
                ColorMatrix matrix = new ColorMatrix(new float[][]
                {
                    new float[] { opacity, 0f, 0f, 0f, 0f },
                    new float[] { 0f, opacity, 0f, 0f, 0f },
                    new float[] { 0f, 0f, opacity, 0f, 0f },
                    new float[] { 0f, 0f, 0f, opacity, 0f },
                    new float[] { 0f, 0f, 0f, 0f, opacity }
                });
                attr.SetColorMatrix(matrix);
                watermarkBitmap.MakeTransparent(Color.White);
                gr.DrawImage(watermarkBitmap, new Rectangle(0, 0, originalImageBitmap.Width, originalImageBitmap.Height), 0, 0, watermarkBitmap.Width, watermarkBitmap.Height, GraphicsUnit.Pixel, attr);
            }

            ImageConverter converter = new ImageConverter();
            var protectedImageByte = (byte[])converter.ConvertTo(originalImageBitmap, typeof(byte[]));

            return protectedImageByte;
        }

        /// <summary>
        /// Get compressed copy of image
        /// </summary>
        /// <param name="originalImageByte">original file in byte[]</param>
        /// <returns>compressed file</returns>
        public static byte[] GetCompressedImage(this byte[] originalImageByte)
        {
            byte[] fileByte = new byte[1];
            if (!ReferenceEquals(originalImageByte, null))
            {
                fileByte = new byte[originalImageByte.Length];
                Array.Copy(originalImageByte, fileByte, originalImageByte.Length);
            }

            Bitmap originalImageBitmap;

            using (MemoryStream ms = new MemoryStream(fileByte))
            {
                originalImageBitmap = (Bitmap)Image.FromStream(ms);
            }

            int maxWidth = 0, maxHeight = 0;
            int.TryParse(Resources.MaxWidthCompressedImage, out maxWidth);
            int.TryParse(Resources.MaxHeightCompressedImage, out maxHeight);

            int newWidth, newHeight;

            if (originalImageBitmap.Width < maxWidth && originalImageBitmap.Height < maxHeight)
            {
                newWidth = originalImageBitmap.Width;
                newHeight = originalImageBitmap.Height;
            }
            else if (originalImageBitmap.Width < maxWidth)
            {
                newHeight = maxHeight;
                newWidth = originalImageBitmap.Width * newHeight / originalImageBitmap.Height;
            }
            else
            {
                newWidth = maxWidth;
                newHeight = originalImageBitmap.Height * newWidth / originalImageBitmap.Width;
            }

            var resizedBitmap = new Bitmap(newWidth, newHeight);

            using (Graphics gr = Graphics.FromImage(resizedBitmap))
            {
                gr.DrawImage(originalImageBitmap, 0, 0, newWidth, newHeight);
            }

            ImageConverter converter = new ImageConverter();
            var comressedImageByte = (byte[])converter.ConvertTo(resizedBitmap, typeof(byte[]));

            return comressedImageByte;
        }

        /// <summary>
        /// Determine the type of file and this MIME
        /// </summary>
        /// <param name="data">upload file in byte[]</param>
        /// <returns>Product Type</returns>
        public static ProductType GetMimeFromFile(this byte[] data)
        {
            if (data == null)
            {
                throw new FileNotFoundException(Resources.ErrorGettingMime);
            }

            var buffer = new byte[int.Parse(Resources.MAX_CONTENT)];
            MemoryStream ms = new MemoryStream(data, 0, data.Length);
            if (ms.Length >= int.Parse(Resources.MAX_CONTENT))
            {
                ms.Read(buffer, 0, int.Parse(Resources.MAX_CONTENT));
            }
            else
            {
                ms.Read(buffer, 0, (int)ms.Length);
            }

            var mimeTypePtr = IntPtr.Zero;
            try
            {
                var result = FindMimeFromData(IntPtr.Zero, null, buffer, int.Parse(Resources.MAX_CONTENT), null, 0, out mimeTypePtr, 0);
                if (result != 0)
                {
                    Marshal.FreeCoTaskMem(mimeTypePtr);
                    throw Marshal.GetExceptionForHR(result);
                }

                var mime = Marshal.PtrToStringUni(mimeTypePtr);
                Marshal.FreeCoTaskMem(mimeTypePtr);

                if (Resources.imageType.Contains(mime))
                {
                    return ProductType.Image;
                }

                if (Resources.videoType.Contains(mime))
                {
                    return ProductType.Video;
                }

                if (Resources.musicType.Contains(mime))
                {
                    return ProductType.Music;
                }

                return ProductType.unknow;
            }
            catch (Exception)
            {
                if (mimeTypePtr != IntPtr.Zero)
                {
                    Marshal.FreeCoTaskMem(mimeTypePtr);
                }

                return ProductType.unknow;
            }
        }

        [DllImport("urlmon.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = false)]
        private static extern int FindMimeFromData(
            IntPtr pbcIntPtr,
            [MarshalAs(UnmanagedType.LPWStr)] string pwzUrl,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeParamIndex = 3)] byte[] pbcBuffer,
            int pbcSize,
            [MarshalAs(UnmanagedType.LPWStr)] string pwzMimeProposed,
            int dwcMimeFlags,
            out IntPtr ppwzMimeOut,
            int dwcReserved);
    }
}
