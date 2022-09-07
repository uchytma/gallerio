using Gallerio.Core.GalleryAggregate;

namespace Gallerio.Core.Interfaces
{
    public interface IGalleryProvider
    {
        Task<Gallery> FindGallery(Guid id);
        Task<IReadOnlyCollection<Gallery>> GetGalleryList();
    }
}