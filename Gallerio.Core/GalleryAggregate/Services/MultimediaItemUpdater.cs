using Gallerio.Core.Interfaces.Core;
using Gallerio.Core.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallerio.Core.GalleryAggregate.Services
{
    public class MultimediaItemUpdater : IMultimediaItemUpdater
    {
        private readonly IMultimediaItemsUpdateRepo _updateRepo;

        public MultimediaItemUpdater(IMultimediaItemsUpdateRepo updateRepo)
        {
            _updateRepo = updateRepo;
        }

        public async Task ReplaceMultimediaItemsWith(MultimediaSource source, IEnumerable<MultimediaItem> mediaItems)
        {
            await _updateRepo.ReplaceMultimediaItemsWith(source, mediaItems);
        }

        public async Task UpdateItem(MultimediaItem multimediaItem)
        {
            await _updateRepo.UpdateItem(multimediaItem);
        }
    }
}
