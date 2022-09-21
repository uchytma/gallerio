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
        private readonly IExifDataUpdater _exifUpdater;

        public MultimediaItemUpdater(IMultimediaItemsUpdateRepo updateRepo, IExifDataUpdater exifUpdater)
        {
            _updateRepo = updateRepo;
            _exifUpdater = exifUpdater;
        }

        public async Task ReplaceMultimediaItemsWith(MultimediaSource source, IEnumerable<MultimediaItem> mediaItems)
        {
            await _updateRepo.ReplaceMultimediaItemsWith(source, mediaItems);
        }

        public async Task UpdateItem(MultimediaItem multimediaItem)
        {
            await _updateRepo.UpdateItem(multimediaItem);
            await _exifUpdater.SetExifCustomData(new ExifCustomDataModel(multimediaItem.Tags.ToList()), multimediaItem.FullPath);
        }
    }
}
