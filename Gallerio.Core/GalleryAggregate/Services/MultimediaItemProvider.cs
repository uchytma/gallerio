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
        public MultimediaItemProvider()
        {
        }

        public async Task<IEnumerable<MultimediaItem>> GetMultimediaItems(Guid id)
        {
            var l = new List<MultimediaItem>()
            {
                new MultimediaItem(new Guid("d1f91baf-a935-4bf5-93c1-c2034a1690d4"), "test1.jpg"),
                new MultimediaItem(new Guid("a1f91baf-a935-4bf5-93c1-c2034a1690d2"), "test2.jpg"),
            };

            for (int i = 1; i <= 2000; i++)
            {
                l.Add(new MultimediaItem(Guid.NewGuid(), $"test{i}.jpg"));
            }

            return l;
        }
    }
}
