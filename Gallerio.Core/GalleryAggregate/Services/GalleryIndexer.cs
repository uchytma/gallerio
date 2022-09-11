using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gallerio.Core.Interfaces;

namespace Gallerio.Core.GalleryAggregate.Services
{
    public class GalleryIndexer : IGalleryIndexer
    {
        public GalleryIndexer()
        {
        }

        public Task<ReindexMultimediaResourcesResponse> ReindexMultimediaResources(Gallery gallery)
        {
            throw new NotImplementedException();
        }

        public record ReindexMultimediaResourcesResponse(int MediaUpdated, int NewMediaAdded, int TotalMediaCount);
    }
}
