using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gallerio.Core.Interfaces;

namespace Gallerio.Core.GalleryAggregate.Services
{

    public class GalleryIndexer : IGalleryIndexer
    {
        private readonly IMultimediaItemProvider _muip;
        private readonly IMultimediaItemUpdater _muiu;
       
        private int _mediaAdded = 0;

        private string[] _allowedExtensions = new string[] { ".jpg", ".jpeg" };

        internal GalleryIndexer(IMultimediaItemProvider muip, IMultimediaItemUpdater muiu)
        {
            _muip = muip;
            _muiu = muiu;
        }

        public async Task<ReindexMultimediaResourcesResponse> ReindexMultimediaResources(Gallery gallery)
        {
            var sources = gallery.GetMultimediaSources;
            List<MultimediaItem> mediaItems = new List<MultimediaItem>();

            foreach (var source in sources)
            {
                mediaItems.AddRange(await ProcessSource(source));
            }

            return new ReindexMultimediaResourcesResponse(_mediaAdded, mediaItems.Count());
        }

        private async Task<IEnumerable<MultimediaItem>> ProcessSource(MultimediaSource source)
        {
            List<MultimediaItem> newResultItems = new List<MultimediaItem>();

            IEnumerable<MultimediaItem> items = await _muip.GetMultimediaItems(source);
            Dictionary<string, MultimediaItem> itemsDict = items.ToDictionary(d => d.Name, d => d); //search by name performance optimalization

            foreach (string filePath in Directory.EnumerateFiles(source.SourceDir, "*.*", SearchOption.AllDirectories))
            {
                string fileName = filePath.Substring(source.SourceDir.Length + 1);
                if (itemsDict.TryGetValue(fileName, out MultimediaItem? existingItem))
                {
                    newResultItems.Add(existingItem);
                    continue;
                }

                if (!SupportedMultimediaType(filePath)) continue;

                var newFoundMultimediaItem = new MultimediaItem(Guid.NewGuid(), fileName);
                newResultItems.Add(newFoundMultimediaItem);
                _mediaAdded++;
            }

            await _muiu.ReplaceMultimediaItemsWith(source, newResultItems);

            return newResultItems;
        }

        private bool SupportedMultimediaType(string filePath)
        {
            string extension = Path.GetExtension(filePath).ToLowerInvariant();
            return _allowedExtensions.Contains(extension);
        }

        public record ReindexMultimediaResourcesResponse(int NewMediaAdded, int TotalMediaCount);
    }
}
