using Gallerio.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallerio.Core.GalleryAggregate.Services
{
    public class MultimediaItemProvider : IMultimediaItemProvider
    {
        private readonly IMultimediaItemsReadonlyRepo _readonlyRepo;


        public MultimediaItemProvider(IMultimediaItemsReadonlyRepo repo)
        {
            _readonlyRepo = repo;
        }

        public async Task<MultimediaItem> FindMultimediaItem(Gallery gallery, Guid multimediaId)
        {
            foreach (var source in gallery.GetMultimediaSources)
            {
                var res = await this.FindMultimediaItem(source, multimediaId);
                if (res is not null) return res;
            }

            throw new MultimediaItemNotFoundException();
        }

        public async Task<IEnumerable<MultimediaItem>> GetMultimediaItems(Gallery gallery)
        {
            List<MultimediaItem> items = new List<MultimediaItem>();
            foreach (var source in gallery.GetMultimediaSources)
            {
                items.AddRange(await GetMultimediaItems(source));
            }
            return items.OrderBy(d => d.CapturedDateTime);
        }

        public async Task<IEnumerable<MultimediaItem>> GetMultimediaItems(MultimediaSource multimediaSource)
        {
            return (await _readonlyRepo.GetMultimediaItems(multimediaSource)).AsEnumerable();
        }

        public async Task<MultimediaItem?> FindMultimediaItem(MultimediaSource multimediaSource, Guid multimediaId)
        {
            return await _readonlyRepo.FindMultimediaItem(multimediaSource, multimediaId);
        }
    }
}
