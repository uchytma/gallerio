using Gallerio.Core.GalleryAggregate;
using Gallerio.Core.Interfaces.Infrastructure;
using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Gallerio.Infrastructure.Services.MetadataExtractor
{
    public class MetadataExtractorService : IMetadataExtractor
    {
        /// <summary>
        /// SixLabors.ImageSharp is slow, we are using MetadataExtractor for reading EXIF metadata.
        /// When reading exif custom metadata fails, it does not throw an exception and continue with processing.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public MetadataResult<T?> LoadMetadata<T>(string path)
        {
            var directories = ImageMetadataReader.ReadMetadata(path);
            var subIfdDirectory = directories.OfType<ExifSubIfdDirectory>().FirstOrDefault();
            var dateTime = subIfdDirectory?.GetDescription(ExifDirectoryBase.TagDateTimeOriginal);
            if (dateTime == null) dateTime = subIfdDirectory?.GetDescription(ExifDirectoryBase.TagDateTime);
            if (dateTime == null) dateTime = subIfdDirectory?.GetDescription(ExifDirectoryBase.TagDateTimeDigitized);

            DateTime? d = null;
            if (dateTime != null)
            {
                d = DateTime.ParseExact(dateTime, "yyyy:MM:dd HH:mm:ss", CultureInfo.InvariantCulture);
            }

            T? customData = default;
            try
            {
                var customDataBase64 = subIfdDirectory?.GetDescription(ExifDirectoryBase.TagUserComment);
                if (string.IsNullOrEmpty(customDataBase64)) goto afterCustomDataProcessing;
                if (customDataBase64 != null)
                {
                    var base64EncodedBytes = System.Convert.FromBase64String(customDataBase64);
                    string customDataString = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
                    customData = JsonSerializer.Deserialize<T>(customDataString);
                }
            }
            catch (Exception)
            {
                // EXIF custom data not recognized.
                // TODO: logging / handle this exception
            }

        afterCustomDataProcessing:
            var metadataResult = new MetadataResult<T?>(d, customData);
            return metadataResult;
        }
    }
}
