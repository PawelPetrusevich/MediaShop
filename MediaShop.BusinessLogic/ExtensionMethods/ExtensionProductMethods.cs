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
        /// MIMY Type whith magic number
        /// </summary>
        /// <param name="data">file in byte array</param>
        /// <returns>Product Type</returns>
        public static ProductType GetMimeFromByteArray(this byte[] data)
        {
            string[] imageType =
            {
                "FF-D8-FF",
                "89-50-4E",
                "47-49-46",
            };
            string[] videoType =
            {
                "52-49-46",
                "00-00-01",
                "66-74-79",
                "00-00-00",
                "46-4C-56"
            };
            string[] audioType =
            {
                "52-49-46",
                "30-26-B2",
                "49-44-33",
                "FF-F1-80",
                "4F-67-67"
            };
            byte[] temp = new byte[16];
            Array.Copy(data, temp, 16);
            string dataHex = BitConverter.ToString(temp);

            if (imageType.Contains(dataHex.Substring(0, 8)))
            {
                return ProductType.Image;
            }

            if (videoType.Contains(dataHex.Substring(0, 8)))
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
    }
}
