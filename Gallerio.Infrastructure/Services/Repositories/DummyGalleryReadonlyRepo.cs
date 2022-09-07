using Gallerio.Core.Interfaces;
using Gallerio.Core.GalleryAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallerio.Infrastructure.Services.Repositories
{
    public class DummyGalleryReadonlyRepo : IGalleryReadOnlyRepo
    {
        private static Gallery[] galleries = new Gallery[]
        {
            new Gallery(new Guid("7858785c-e0a4-4a08-b112-0347754e478d")),
            new Gallery(new Guid("34139721-7752-4d73-918f-1a4cba73c6cb")),
        };

        public async Task<Gallery> FindGallery(Guid id)
        {
            return galleries.FirstOrDefault(g => g.Id == id) ?? throw new GalleryNotFoundException();
        }

        public async Task<IReadOnlyCollection<Gallery>> GetGalleryList()
        {
            return galleries;
        }
    }
}
