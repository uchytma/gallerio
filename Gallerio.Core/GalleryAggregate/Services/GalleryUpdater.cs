using Gallerio.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallerio.Core.GalleryAggregate.Services
{
    public class GalleryUpdater : IGalleryUpdater
    {
        private readonly IGalleryUpdateRepo _galleryUpdateRepo;

        public GalleryUpdater(IGalleryUpdateRepo galleryUpdateRepo)
        {
            _galleryUpdateRepo = galleryUpdateRepo;
        }

        public async Task<Gallery> CreateGallery(string name)
        {
            return await _galleryUpdateRepo.CreateGallery(name);
        }

        public async Task<Gallery> UpdateGallery(Gallery g)
        {
            return await _galleryUpdateRepo.UpdateGallery(g);
        }
    }
}
