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
        Task<IEnumerable<MultimediaItem>> GetMultimediaItems(Guid id);
    }
}
