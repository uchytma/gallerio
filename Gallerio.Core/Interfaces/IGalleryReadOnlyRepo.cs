
using Gallerio.Core.GalleryAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallerio.Core.Interfaces
{
    public interface IGalleryReadOnlyRepo
    {
        Task<Gallery> FindGallery(Guid id);
        Task<IReadOnlyCollection<Gallery>> GetGalleryList();
    }

    public interface IGalleryUpdateRepo
    {
        Task<Gallery> CreateGallery(string name);
        Task<Gallery> UpdateGallery(Gallery gallery);
    }
}
