using Gallerio.Core.GalleryAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallerio.Core.Interfaces
{
    public interface IMultimediaItemProvider
    {
        Task<MultimediaItem> FindMultimediaItem(Gallery gallery, Guid multimediaId);
        Task<IEnumerable<MultimediaItem>> GetMultimediaItems(Gallery id);
        Task<IEnumerable<MultimediaItem>> GetMultimediaItems(MultimediaSource id);
    }
}
