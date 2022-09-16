using Gallerio.Core.GalleryAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallerio.Core.Interfaces.Infrastructure
{
    public interface IGalleryReadOnlyRepo
    {
        Task<Gallery> FindGallery(Guid id);
        Task<IReadOnlyCollection<Gallery>> GetGalleryList();
    }
}
