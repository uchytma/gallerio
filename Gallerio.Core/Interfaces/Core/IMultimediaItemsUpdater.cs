using Gallerio.Core.GalleryAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallerio.Core.Interfaces.Core
{
    public interface IMultimediaItemUpdater
    {
        Task ReplaceMultimediaItemsWith(MultimediaSource source, IEnumerable<MultimediaItem> mediaItems);
        Task UpdateItem(MultimediaItem multimediaItem);
    }
}
