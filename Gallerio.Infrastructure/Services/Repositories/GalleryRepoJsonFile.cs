using Gallerio.Core.GalleryAggregate;
using Gallerio.Infrastructure.Extensions;
using Gallerio.Infrastructure.Services.MainJsonDb;
using Gallerio.Core.GalleryAggregate.Exceptions;
using Gallerio.Core.Interfaces.Core;
using Gallerio.Core.Interfaces.Infrastructure;

namespace Gallerio.Infrastructure.Services.Repositories
{
    public class GalleryRepoJsonFile : IGalleryReadOnlyRepo, IGalleryUpdateRepo
    {
        private readonly JsonFileDb _db;

        public GalleryRepoJsonFile(JsonFileDb db)
        {
            _db = db;
        }

        public async Task<Gallery> CreateGallery(string name)
        {
            Guid id = Guid.NewGuid();
            (await _db.GetModel()).Galleries.Add(new GalleryModel(id, name, string.Empty, string.Empty, 0, new List<MainJsonDb.MultimediaSource>()));
            await _db.PersistChangesToFile();
            return await FindGallery(id);
        }

        public async Task<Gallery> FindGallery(Guid id)
        {
            var galleryModel = (await _db.GetModel()).Galleries.SingleOrDefault(d => d.Id == id) ?? throw new GalleryNotFoundException();
            return galleryModel.ToDomainModel();
        }

        public async Task<IReadOnlyCollection<Gallery>> GetGalleryList()
        {
            return (await _db.GetModel()).Galleries.Select(d => d.ToDomainModel()).OrderBy(d => d.Name).ToArray();
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
                gallery.MultimediaSources.Select(d => new MainJsonDb.MultimediaSource(d.Id, d.SourceDir)).ToList());
            galleries.Add(model);
            await _db.PersistChangesToFile();
            return await FindGallery(gallery.Id);
        }
    }
}
