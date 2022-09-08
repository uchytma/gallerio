﻿using Gallerio.Core.Interfaces;
using Gallerio.Core.GalleryAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata.Ecma335;

namespace Gallerio.Infrastructure.Services.Repositories
{
    public class DummyGalleryRepo : IGalleryReadOnlyRepo, IGalleryUpdateRepo
    {
        private static List<Gallery> _galleries = new List<Gallery>
        {
            new Gallery(new Guid("7858785c-e0a4-4a08-b112-0347754e478d"), "Norsko"),
            new Gallery(new Guid("34139721-7752-4d73-918f-1a4cba73c6cb"), "Berlín"),
        };

        /// <summary>
        /// For testing purposes
        /// </summary>
        public List<Gallery> GetExistingGalleries() => _galleries;

        /// <summary>
        /// For testing purposes
        /// </summary>
        public Guid GetNotExistingGalleryGuid() => new Guid("12139721-7752-4d73-918f-1a4cba73c6cb");

        public async Task<Gallery> CreateGallery(string name)
        {
            var g = new Gallery(Guid.NewGuid(), name);
            _galleries.Add(g);
            return g;
        }

        public async Task<Gallery> FindGallery(Guid id)
        {
            return _galleries.FirstOrDefault(g => g.Id == id) ?? throw new GalleryNotFoundException();
        }

        public async Task<IReadOnlyCollection<Gallery>> GetGalleryList()
        {
            return _galleries;
        }

        public async Task<Gallery> UpdateGallery(Gallery gallery)
        {
            var g = await FindGallery(gallery.Id);
            _galleries.Remove(g);
            _galleries.Add(gallery);
            return gallery;
        }
    }
}