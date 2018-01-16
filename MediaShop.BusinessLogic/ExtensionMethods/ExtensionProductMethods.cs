﻿using System;
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
                float opacity = (float)0.5;
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

            int maxWidth = 300;
            int maxHeight = 300;
            int newWidth;
            int newHeight;

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
            var imageType = new string[]
            {
                "image/gif",
                "image/jpeg",
                "image/pjpeg",
                "image/png",
                "image/x-png",
                "image/svg + xml",
                "image/tiff",
                "image/vnd.microsoft.icon",
                "image/vnd.wap.wbmp",
                "image/webp"
            };

            var musicType = new string[]
            {
                "audio/basic",
                "audio/L24",
                "audio/mp4",
                "audio/aac",
                "audio/mpeg",
                "audio/ogg",
                "audio/vorbis",
                "audio/x - ms - wma",
                "audio/x - ms - wax",
                "audio/vnd.rn - realaudio",
                "audio/vnd.wave",
                "audio/webm"
            };

            var videoType = new string[]
            {
                "video/mpeg",
                "video/mp4",
                "video/ogg",
                "video/quicktime",
                "video/webm",
                "video/x - ms - wmv",
                "video/x - flv",
                "video/3gpp",
                "video/3gpp2"
            };

            if (data == null)
            {
                throw new FileNotFoundException(data + " not found");
            }

            const int MAX_CONTENT = 256;

            var buffer = new byte[MAX_CONTENT];
            MemoryStream ms = new MemoryStream(data, 0, data.Length);
            if (ms.Length >= MAX_CONTENT)
            {
                ms.Read(buffer, 0, MAX_CONTENT);
            }
            else
            {
                ms.Read(buffer, 0, (int)ms.Length);
            }

            var mimeTypePtr = IntPtr.Zero;
            try
            {
                var result = FindMimeFromData(IntPtr.Zero, null, buffer, MAX_CONTENT, null, 0, out mimeTypePtr, 0);
                if (result != 0)
                {
                    Marshal.FreeCoTaskMem(mimeTypePtr);
                    throw Marshal.GetExceptionForHR(result);
                }

                var mime = Marshal.PtrToStringUni(mimeTypePtr);
                Marshal.FreeCoTaskMem(mimeTypePtr);

                if (imageType.Contains(mime))
                {
                    return ProductType.Image;
                }

                if (videoType.Contains(mime))
                {
                    return ProductType.Video;
                }

                if (musicType.Contains(mime))
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
