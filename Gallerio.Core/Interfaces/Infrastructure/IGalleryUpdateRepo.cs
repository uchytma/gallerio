using Gallerio.Core.GalleryAggregate;

namespace Gallerio.Core.Interfaces.Infrastructure
{
    public interface IGalleryUpdateRepo
    {
        Task<Gallery> CreateGallery(string name);
        Task<Gallery> UpdateGallery(Gallery gallery);
    }
}
