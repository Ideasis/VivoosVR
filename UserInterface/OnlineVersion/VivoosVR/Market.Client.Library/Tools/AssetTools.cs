using Core.Common.DataModel;
using Core.Common.DataModel.Market;
using Core.Common.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Market.Client.Tools
{
    public static class AssetTools
    {
        public static ImageSource ConvertToImageSource(this byte[] imageData)
        {
            if (imageData == null) return null;

            var source = new BitmapImage();
            source.BeginInit();
            source.StreamSource = new MemoryStream(imageData);
            source.EndInit();
            source.Freeze();

            return source;
        }

        public static async Task<ImageSource> GetCachedImage(this IPublicMarketService service, Guid assetId)
        {
            string tempFolder = Path.GetTempPath();
            string filename = "";
            byte[] data = null;

            filename = Path.Combine(tempFolder, string.Format("{0}.ideasisimage", assetId));
            if (File.Exists(filename))
            {
                using (FileStream SourceStream = File.Open(filename, FileMode.Open))
                {
                    data = new byte[SourceStream.Length];
                    await SourceStream.ReadAsync(data, 0, (int)SourceStream.Length);
                }

                return data.ConvertToImageSource();
            }

            return await Task.Run<ImageSource>(() =>
             {
                 AssetThumbnail thumbnail = service.GetImage(assetId);

                 if (thumbnail != null)
                 {
                     data = thumbnail.Thumbnail;
                     using (var fs = File.Open(filename, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read))
                     {
                         fs.Write(data, 0, data.Count());
                         fs.Flush(true);
                         fs.Close();
                     }

                     return data.ConvertToImageSource();
                 }
                 else
                 {
                     return null;
                 }
             });
        }
    }
}
