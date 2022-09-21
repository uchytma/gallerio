using Gallerio.Core.Interfaces.Infrastructure;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Gallerio.Infrastructure.Services.ExifDataUpdater
{
    public class SharpImageExifUpdater : IExifDataUpdater
    {
        public async Task SetExifCustomData<T>(T data, string imageFullPath)
        {
            SixLabors.ImageSharp.Image image = Image.Load(imageFullPath);
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data));
            image.Metadata.ExifProfile.SetValue(ExifTag.UserComment, new EncodedString(EncodedString.CharacterCode.ASCII, Convert.ToBase64String(plainTextBytes)));
            await image.SaveAsJpegAsync(imageFullPath, new JpegEncoder { Quality = 100 });
        }
    }
}
