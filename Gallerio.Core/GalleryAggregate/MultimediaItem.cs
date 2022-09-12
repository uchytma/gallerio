using Gallerio.Core.GalleryAggregate.Exceptions;
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
        public MultimediaItem(Guid id,
            string partialPath,
            MultimediaSource source,
            DateTime capturedDateTime,
            IEnumerable<string> tags)
        {
            this.Id = id;
            this.PartialPath = partialPath;
            this.Source = source;
            this.CapturedDateTime = capturedDateTime;
            this._tags = tags.ToList();
        }

        private readonly List<string> _tags;

        public IEnumerable<string> Tags => _tags;

        public void AddTag(string tag)
        {
            if (_tags.Contains(tag))
                throw new TagAlreadyExistException();
            _tags.Add(tag);
        }

        public void RemoveTag(string tag)
        {
            if (!_tags.Contains(tag))
                throw new TagNotFoundException();
            _tags.Remove(tag);
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
