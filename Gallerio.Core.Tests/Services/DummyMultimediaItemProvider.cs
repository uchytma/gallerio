using Gallerio.Core.GalleryAggregate;
using Gallerio.Core.Interfaces;

namespace Gallerio.Infrastructure.Services.Repositories
{
    public class DummyMultimediaItemProvider : IMultimediaItemProvider
    {
        public async Task<IEnumerable<MultimediaItem>> GetMultimediaItems(Guid id)
        {
            return new List<MultimediaItem>()
            {
                new MultimediaItem(new Guid("d1f91baf-a935-4bf5-93c1-c2034a1690d4"), "test1.jpg"),
                new MultimediaItem(new Guid("a1f91baf-a935-4bf5-93c1-c2034a1690d2"), "test2.jpg"),
            };
        }
    }
}
