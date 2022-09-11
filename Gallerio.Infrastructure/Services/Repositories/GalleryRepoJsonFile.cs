using Gallerio.Core.Interfaces;
using Gallerio.Core.GalleryAggregate;
using Gallerio.Infrastructure.Db;
using Gallerio.Infrastructure.Extensions;

namespace Gallerio.Infrastructure.Services.Repositories
{
    public class GalleryRepoJsonFile : IGalleryReadOnlyRepo, IGalleryUpdateRepo
    {
        private readonly JsonFileDb _db;
        private readonly IGalleryFactory _galleryFactory;

        public GalleryRepoJsonFile(JsonFileDb db, IGalleryFactory galleryFactory)
        {
            _db = db;
            _galleryFactory = galleryFactory;
        }

        public async Task<Gallery> CreateGallery(string name)
        {
            Guid id = Guid.NewGuid();
            (await _db.GetModel()).Galleries.Add(new GalleryModel(id, name, string.Empty, string.Empty, 0, new List<Db.MultimediaSource>()));
            await _db.PersistChangesToFile();
            return await FindGallery(id);
        }

        public async Task<Gallery> FindGallery(Guid id)
        {
            var galleryModel = (await _db.GetModel()).Galleries.SingleOrDefault(d => d.Id == id) ?? throw new GalleryNotFoundException();
            return galleryModel.ToDomainModel(_galleryFactory);
        }

        public async Task<IReadOnlyCollection<Gallery>> GetGalleryList()
        {
            return (await _db.GetModel()).Galleries.Select(d => d.ToDomainModel(_galleryFactory)).ToArray();
        }

        public async Task<Gallery> UpdateGallery(Gallery gallery)
        {
            var galleries = (await _db.GetModel()).Galleries;
            var galleryModel = galleries.SingleOrDefault(d => d.Id == gallery.Id) ?? throw new GalleryNotFoundException();
            galleries.Remove(galleryModel);
            var model = new GalleryModel(gallery.Id,
                gallery.Name, 
                gallery.Description,
                gallery.Date, 
                gallery.TotalPhotosCount,
                gallery.GetMultimediaSources.Select(d => new Db.MultimediaSource(d.Id, d.SourceConfigurationFilePath)).ToList());
            galleries.Add(model);
            await _db.PersistChangesToFile();
            return await FindGallery(gallery.Id);
        }
    }
}
