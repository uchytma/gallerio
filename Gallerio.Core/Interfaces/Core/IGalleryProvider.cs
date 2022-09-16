using Gallerio.Core.GalleryAggregate;

namespace Gallerio.Core.Interfaces.Core
{
    public interface IGalleryProvider
    {
        Task<Gallery> FindGallery(Guid id);
        Task<IReadOnlyCollection<Gallery>> GetGalleryList();
    }
}