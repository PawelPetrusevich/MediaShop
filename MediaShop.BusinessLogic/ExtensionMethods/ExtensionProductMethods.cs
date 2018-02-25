using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using MediaShop.BusinessLogic.Properties;
using MediaShop.Common.Enums;
using MediaShop.Common.Models.Content;
using NReco.VideoConverter;

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
            return converter.ConvertTo<byte[]>(originalImageBitmap);
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
            return converter.ConvertTo<byte[]>(resizedBitmap);
        }

        /// <summary>
        /// converter to type 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="converter"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T ConvertTo<T>(this ImageConverter converter, object source)
        {
            return (T)converter.ConvertTo(source, typeof(T));
        }

        /// <summary>
        /// creat a protected product
        /// </summary>
        /// <param name="originalMusicByte">original music byte array</param>
        /// <returns>protected music byte array</returns>
        public static byte[] GetProtectedMusic(this byte[] originalMusicByte)
        {
            var targetLength = originalMusicByte.Length / 10;
            var protectedMusicByte = new byte[targetLength];
            Array.Copy(originalMusicByte, protectedMusicByte, targetLength);
            return protectedMusicByte;
        }

        /// <summary>
        /// Determine the type of file and this MIME(не все форматы)
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

        /// <summary>
        /// MIMY Type whith magic number
        /// </summary>
        /// <param name="data">file in byte array</param>
        /// <returns>Product Type</returns>
        public static ProductType GetMimeFromByteArray(this byte[] data)
        {
            string[] imageType =
            {
                "FF-D8-FF-DB",
                "FF-D8-FF-E0",
                "FF-D8-FF-E2",
                "FF-D8-FF-E3",
                "89-50-4E-47",
                "47-49-46-38",
            };
            string[] videoType =
            {
                "52-49-46-46",
                "00-00-00-14",
                "00-00-00-20",
                "00-00-00-18",
                "52-49-46-46",
                "00-00-01-B3",
                "00-00-01-BA"
            };
            string[] audioType =
            {
                "52-49-46",
                "30-26-B2",
                "49-44-33"
            };
            byte[] temp = new byte[16];
            Array.Copy(data, temp, 16);
            string dataHex = BitConverter.ToString(data);

            if (imageType.Contains(dataHex.Substring(0, 11)))
            {
                return ProductType.Image;
            }

            if (videoType.Contains(dataHex.Substring(0, 11)))
            {
                return ProductType.Video;
            }

            if (audioType.Contains(dataHex.Substring(0, 8)))
            {
                return ProductType.Music;
            }

            return ProductType.unknow;
        }

        /// <summary>
        /// Make Protected video
        /// </summary>
        /// <param name="originalVideoInBytes">byte array whith original video</param>
        /// <returns>return byte array</returns>
        public static byte[] GetProtectedVideo(this byte[] originalVideoInBytes)
        {
            string originalVideoPath = $"{System.Web.HttpContext.Current.Server.MapPath("~/App_Data/")}{Guid.NewGuid()}.mp4";
            string protectedVideoPath = $"{System.Web.HttpContext.Current.Server.MapPath("~/App_Data/")}{Guid.NewGuid()}.mp4";
            File.WriteAllBytes(originalVideoPath, originalVideoInBytes);
            var ffmpegconvert = new FFMpegConverter();
            ffmpegconvert.ConvertMedia(originalVideoPath, null, protectedVideoPath, null, new ConvertSettings() { Seek = 0, MaxDuration = 5, VideoCodec = "libx264", AudioCodec = "mp3", VideoFrameRate = 25, VideoFrameSize = "640x360" });
            var result = File.ReadAllBytes(protectedVideoPath);
            File.Delete(originalVideoPath);
            File.Delete(protectedVideoPath);
            return result;
        }

        /// <summary>
        /// Make Protected video
        /// </summary>
        /// <param name="originalVideoInBytes">byte array whith original video</param>
        /// <returns>return byte array</returns>
        public static byte[] GetProtectedVideoAsync(this byte[] originalVideoInBytes, HttpContext context)
        {
            string originalVideoPath = $"{context.Server.MapPath("~/App_Data/")}{Guid.NewGuid()}.mp4";
            string protectedVideoPath = $"{context.Server.MapPath("~/App_Data/")}{Guid.NewGuid()}.mp4";
            File.WriteAllBytes(originalVideoPath, originalVideoInBytes);
            var setting = new ConvertSettings()
            {
                Seek = 0,
                MaxDuration = 5,
                VideoCodec = "libx264",
                AudioCodec = "mp3",
                VideoFrameRate = 25,
                VideoFrameSize = "640x360"
            };
            var ffmpegconvert = new FFMpegConverter();
            ffmpegconvert.ConvertMedia(originalVideoPath, null, protectedVideoPath, null, setting);
            var result = File.ReadAllBytes(protectedVideoPath);
            File.Delete(originalVideoPath);
            File.Delete(protectedVideoPath);
            return result;
        }

        /// <summary>
        /// creat video frame
        /// </summary>
        /// <param name="originalVideoBytes">byte aaray whith original file</param>
        /// <returns>frame</returns>
        public static byte[] GetCompresedVideoFrame(this byte[] originalVideoBytes)
        {
            string originalVideoPath = $"{System.Web.HttpContext.Current.Server.MapPath("~/App_Data/")}{Guid.NewGuid()}.mp4";
            string compresedVideoFramePath = $"{System.Web.HttpContext.Current.Server.MapPath("~/App_Data/")}{Guid.NewGuid()}.jpg";
            File.WriteAllBytes(originalVideoPath, originalVideoBytes);
            var ffmpegconverter = new FFMpegConverter();
            ffmpegconverter.GetVideoThumbnail(originalVideoPath, compresedVideoFramePath, 3);
            var result = File.ReadAllBytes(compresedVideoFramePath);
            File.Delete(originalVideoPath);
            File.Delete(compresedVideoFramePath);
            return result;
        }

        /// <summary>
        /// creat video frame for async methods
        /// </summary>
        /// <param name="originalVideoBytes">byte aaray whith original file</param>
        /// <returns>frame</returns>
        public static byte[] GetCompresedVideoFrameAsync(this byte[] originalVideoBytes, HttpContext context)
        {
            string originalVideoPath = $"{context.Server.MapPath("~/App_Data/")}{Guid.NewGuid()}.mp4";
            string compresedVideoFramePath = $"{context.Server.MapPath("~/App_Data/")}{Guid.NewGuid()}.jpg";
            File.WriteAllBytes(originalVideoPath, originalVideoBytes);
            var ffmpegconverter = new FFMpegConverter();
            ffmpegconverter.GetVideoThumbnail(originalVideoPath, compresedVideoFramePath, 3);
            var result = File.ReadAllBytes(compresedVideoFramePath);
            File.Delete(originalVideoPath);
            File.Delete(compresedVideoFramePath);
            return result;
        }

        public static Dictionary<string, string> GetProductSearchPropeprties(this Product product)
        {
            var propList = new Dictionary<string, string>();

            foreach (var prop in typeof(Product).GetProperties())
            {
                if (prop.PropertyType == typeof(decimal) || prop.PropertyType == typeof(string))
                {
                    string represent = Regex.Replace(prop.Name, "([a-z])_?([A-Z])", "$1 $2");
                    propList.Add(prop.Name, represent);
                }
            }

            return propList;
        }

        public static List<string> GetProductSearchOperands(this Product product)
        {
            return new List<string> { ">", "<", "=", "Contains" };
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
