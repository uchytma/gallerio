using Gallerio.Core.GalleryAggregate;
using Gallerio.Core.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallerio.Core.Tests.Services
{
    internal class DummyMultimediaItemsRepo : IMultimediaItemsReadonlyRepo, IMultimediaItemsUpdateRepo
    {
        private Dictionary<Guid, List<MultimediaItem>> _multimediaItems = new Dictionary<Guid, List<MultimediaItem>>();

        /// <summary>
        /// For testing purposes
        /// </summary>
        public async Task<IEnumerable<MultimediaItem>> CreateDefaultSourceItems(MultimediaSource source)
        {
            var items = new List<MultimediaItem>()
            {
                new MultimediaItem(new Guid("d1f91baf-a935-4bf5-93c1-c2034a1690d4"), "dir\\test1.jpg", source, DateTime.MinValue, Enumerable.Empty<string>()),
                new MultimediaItem(new Guid("a1f91baf-a935-4bf5-93c1-c2034a1690d2"), "dir\\test2.jpg", source, DateTime.MinValue, Enumerable.Empty<string>()),
            };
            await ReplaceMultimediaItemsWith(source, items);
            return items;
        }

        public async Task<IReadOnlyCollection<MultimediaItem>> GetMultimediaItems(MultimediaSource source)
        {
            IEnumerable<MultimediaItem> multimediaItems = _multimediaItems[source.Id] ?? Enumerable.Empty<MultimediaItem>();
            return multimediaItems.ToArray();
        }

        public async Task ReplaceMultimediaItemsWith(MultimediaSource source, IEnumerable<MultimediaItem> mediaItems)
        {
            _multimediaItems.Remove(source.Id);
            _multimediaItems.Add(source.Id, mediaItems.ToList());
        }

        public async Task<MultimediaItem?> FindMultimediaItem(MultimediaSource multimediaSource, Guid multimediaId)
        {
            return FindItem(multimediaSource, multimediaId)?.item;
        }

        public async Task DeleteItem(MultimediaItem multimediaItem)
        {
            var item = FindItem(multimediaItem.Source, multimediaItem.Id);
            if (item == null || item.Value.item == null) return;
            item.Value.sourceList.Remove(item.Value.item);
        }

        public async Task UpdateItem(MultimediaItem multimediaItem)
        {
            var item = FindItem(multimediaItem.Source, multimediaItem.Id);
            if (item == null || item.Value.item == null) goto addItem;
            item.Value.sourceList.Remove(item.Value.item);

        addItem:
            item!.Value.sourceList.Add(multimediaItem);
        }

        private (MultimediaItem? item, List<MultimediaItem> sourceList)? FindItem(MultimediaSource multimediaSource, Guid multimediaId)
        {
            var sourceListExists = _multimediaItems.TryGetValue(multimediaSource.Id, out var itemsInSource);
            if (!sourceListExists || itemsInSource is null) return null;
            return (itemsInSource.FirstOrDefault(d => d.Id == multimediaId), itemsInSource);
        }
    }
}
