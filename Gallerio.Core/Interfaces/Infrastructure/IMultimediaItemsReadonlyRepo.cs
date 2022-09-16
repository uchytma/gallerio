using Gallerio.Core.GalleryAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallerio.Core.Interfaces.Infrastructure
{
    public interface IMultimediaItemsReadonlyRepo
    {
        /// <summary>
        /// returns null when not found.
        /// </summary>
        /// <param name="multimediaSource"></param>
        /// <param name="multimediaId"></param>
        /// <returns></returns>
        Task<MultimediaItem?> FindMultimediaItem(MultimediaSource multimediaSource, Guid multimediaId);
        Task<IReadOnlyCollection<MultimediaItem>> GetMultimediaItems(MultimediaSource source);
    }
}
