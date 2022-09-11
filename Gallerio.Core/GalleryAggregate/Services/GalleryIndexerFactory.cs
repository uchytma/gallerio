using Gallerio.Core.Interfaces;

namespace Gallerio.Core.GalleryAggregate.Services
{
    public class GalleryIndexerFactory : IGalleryIndexerFactory
    {
        private readonly IMultimediaItemProvider _muip;
        private readonly IMultimediaItemUpdater _muiu;

        public GalleryIndexerFactory(IMultimediaItemProvider muip, IMultimediaItemUpdater muiu)
        {
            _muip = muip;
            _muiu = muiu;
        }

        public IGalleryIndexer Create()
        {
            return new GalleryIndexer(_muip, _muiu);
        }
    }
}
