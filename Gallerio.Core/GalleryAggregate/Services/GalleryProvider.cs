using Gallerio.Core.Interfaces.Core;
using Gallerio.Core.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallerio.Core.GalleryAggregate.Services
{
    public class GalleryProvider : IGalleryProvider
    {
        private readonly IGalleryReadOnlyRepo _galleryReadOnlyRepo;

        public GalleryProvider(IGalleryReadOnlyRepo galleryReadOnlyRepo)
        {
            this._galleryReadOnlyRepo = galleryReadOnlyRepo;
        }

        public Task<Gallery> FindGallery(Guid id)
        {
            return _galleryReadOnlyRepo.FindGallery(id);
        }

        public Task<IReadOnlyCollection<Gallery>> GetGalleryList()
        {
            return _galleryReadOnlyRepo.GetGalleryList();
        }
    }
}
