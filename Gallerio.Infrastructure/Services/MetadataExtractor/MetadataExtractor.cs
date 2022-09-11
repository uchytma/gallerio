using Gallerio.Core.GalleryAggregate;
using Gallerio.Core.Interfaces;
using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Gallerio.Infrastructure.Services.MetadataExtractor
{
    public class MetadataExtractorService : IMetadataExtractor
    {
        public MetadataResult LoadMetadata(string path)
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
            
            var metadataResult = new MetadataResult(d);
            return metadataResult;
        }
    }
}
