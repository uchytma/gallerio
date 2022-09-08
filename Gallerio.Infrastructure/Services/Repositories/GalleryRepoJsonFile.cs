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

namespace Gallerio.Infrastructure.Services.Repositories
{
    public class GalleryRepoJsonFile : IGalleryReadOnlyRepo
    {
        private readonly JsonFileDb _db;

        public GalleryRepoJsonFile(JsonFileDb db)
        {
            _db = db;
        }
        
        public async Task<Gallery> FindGallery(Guid id)
        {
            var galleryModel = (await _db.GetModel()).Galleries.SingleOrDefault(d => d.Id == id) ?? throw new GalleryNotFoundException();
            return new Gallery(galleryModel.Id, galleryModel.Name);
        }

        public async Task<IReadOnlyCollection<Gallery>> GetGalleryList()
        {
            return (await _db.GetModel()).Galleries.Select(d => new Gallery(d.Id, d.Name)).ToArray();
        }
    }
}
