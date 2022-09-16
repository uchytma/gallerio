using Gallerio.Core.GalleryAggregate;

namespace Gallerio.Core.Interfaces.Core
{
    public interface IGalleryFactory
    {
        Gallery Create(Guid id, string name, string description, string date, int totalPhotosCount, IEnumerable<MultimediaSource> multimediaSources);
    }
}