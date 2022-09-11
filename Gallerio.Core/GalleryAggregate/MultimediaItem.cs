using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Gallerio.Core.GalleryAggregate
{
    public class MultimediaItem
    {
        public MultimediaItem(Guid id, string partialPath, MultimediaSource source, DateTime capturedDateTime)
        {
            this.Id = id;
            this.PartialPath = partialPath;
            this.Source = source;
            CapturedDateTime = capturedDateTime;
        }

        public Guid Id { get; }

        /// <summary>
        /// PartialPath is unique in scope of MultimediaSource.
        /// The Name is partial file path from base path (which is multimedia SourceDir)
        /// </summary>
        public string PartialPath { get; }

        public string Name => PartialPath.Split('\\').Last();

        public MultimediaSource Source { get; }

        public string FullPath => Path.Combine(Source.SourceDir, this.PartialPath);

        /// <summary>
        /// TODO: detect mime type from Name extension. 
        /// For now we have jpeg images only.
        /// </summary>
        public string MimeType => "image/jpeg";

        public DateTime CapturedDateTime { get; }
    }
}
