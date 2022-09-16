using Gallerio.Core.GalleryAggregate;
using Gallerio.Core.GalleryAggregate.Services;

namespace Gallerio.Core.Interfaces.Core
{
    public interface IGalleryIndexer
    {
        Task<GalleryIndexer.ReindexMultimediaResourcesResponse> ReindexMultimediaResources(Gallery gallery);
    }
}
