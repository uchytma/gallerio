using Gallerio.Core.Interfaces;
using Gallerio.Core.GalleryAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Gallerio.Api.Options;
using Gallerio.Infrastructure.Options;
using System.Text.Json;
using System.Diagnostics.CodeAnalysis;
using Gallerio.Infrastructure.Db;
using System.Runtime.CompilerServices;
using Gallerio.Infrastructure.Extensions;

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
            (await _db.GetModel()).Galleries.Add(new GalleryModel(id, name, string.Empty, string.Empty, 0));
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
            return (await _db.GetModel()).Galleries.Select(d => d.ToDomainModel()).ToArray();
        }

        public async Task<Gallery> UpdateGallery(Gallery gallery)
        {
            var galleries = (await _db.GetModel()).Galleries;
            var galleryModel = galleries.SingleOrDefault(d => d.Id == gallery.Id) ?? throw new GalleryNotFoundException();
            galleries.Remove(galleryModel);
            galleries.Add(new GalleryModel(gallery.Id, gallery.Name, gallery.Description, gallery.Date, gallery.TotalPhotosCount));
            await _db.PersistChangesToFile();
            return await FindGallery(gallery.Id);
        }
    }
}
