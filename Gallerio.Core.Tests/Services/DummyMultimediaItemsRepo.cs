using Gallerio.Core.GalleryAggregate;
using Gallerio.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallerio.Core.Tests.Services
{
    internal class DummyMultimediaItemsRepo : IMultimediaItemsReadonlyRepo, IMultimediaItemsUpdateRepo
    {
        public async Task<IReadOnlyCollection<MultimediaItem>> GetMultimediaItems(MultimediaSource source)
        {
             return new List<MultimediaItem>()
            {
                new MultimediaItem(new Guid("d1f91baf-a935-4bf5-93c1-c2034a1690d4"), "test1.jpg", source, DateTime.MinValue, Enumerable.Empty<string>()),
                new MultimediaItem(new Guid("a1f91baf-a935-4bf5-93c1-c2034a1690d2"), "test2.jpg", source, DateTime.MinValue, Enumerable.Empty<string>()),
            };
        }

        public Task ReplaceMultimediaItemsWith(MultimediaSource source, IEnumerable<MultimediaItem> mediaItems)
        {
            throw new NotImplementedException();
        }

        public Task<MultimediaItem?> FindMultimediaItem(MultimediaSource multimediaSource, Guid multimediaId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteItem(MultimediaItem multimediaItem)
        {
            throw new NotImplementedException();
        }

        public Task UpdateItem(MultimediaItem multimediaItem)
        {
            throw new NotImplementedException();
        }
    }
}
