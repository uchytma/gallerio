using Gallerio.Core.Interfaces;

namespace Gallerio.Core.GalleryAggregate.Services
{
    public class GalleryIndexerFactory : IGalleryIndexerFactory
    {
        private readonly IMultimediaItemProvider _muip;
        private readonly IMultimediaItemUpdater _muiu;
        private readonly IMetadataExtractor _imex;
        private readonly IGalleryUpdater _galleryUpdater;

        public GalleryIndexerFactory(IMultimediaItemProvider muip, IMultimediaItemUpdater muiu, IMetadataExtractor imex, IGalleryUpdater galleryUpdater)
        {
            _muip = muip;
            _muiu = muiu;
            _imex = imex;
            _galleryUpdater = galleryUpdater;
        }

        public IGalleryIndexer Create()
        {
            return new GalleryIndexer(_muip, _muiu, _imex, _galleryUpdater);
        }
    }
}
