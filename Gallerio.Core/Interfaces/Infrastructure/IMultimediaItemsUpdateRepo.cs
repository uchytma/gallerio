using Gallerio.Core.GalleryAggregate;

namespace Gallerio.Core.Interfaces.Infrastructure
{
    public interface IMultimediaItemsUpdateRepo
    {
        Task ReplaceMultimediaItemsWith(MultimediaSource source, IEnumerable<MultimediaItem> mediaItems);
        Task UpdateItem(MultimediaItem multimediaItem);
    }
}
