using Gallerio.Core.GalleryAggregate;
using Gallerio.Core.Interfaces;
using Gallerio.Infrastructure.Services.MultimediaItemsJsonFileDb;

namespace Gallerio.Infrastructure.Services.Repositories
{
    public class MultimediaItemsRepoJsonFile : IMultimediaItemsReadonlyRepo, IMultimediaItemsUpdateRepo
    {
        private readonly JsonFileMultimediaItemsFactory _multimediaItemsFactory;

        public MultimediaItemsRepoJsonFile(JsonFileMultimediaItemsFactory multimediaItems)
        {
            _multimediaItemsFactory = multimediaItems;
        }

        public async Task<Core.GalleryAggregate.MultimediaItem?> FindMultimediaItem(MultimediaSource source, Guid multimediaId)
        {
            var dbModel = await _multimediaItemsFactory.LoadFromCacheOrCreate(source).GetModel();
            var dbMultimediaItem = dbModel.MultimediaItems.FirstOrDefault(d => d.Id == multimediaId);
            if (dbMultimediaItem == null) return null;
            return new Core.GalleryAggregate.MultimediaItem(dbMultimediaItem.Id, dbMultimediaItem.PartialPath, source);
        }

        public async Task<IReadOnlyCollection<Core.GalleryAggregate.MultimediaItem>> GetMultimediaItems(MultimediaSource source)
        {
            var dbModel = await _multimediaItemsFactory.LoadFromCacheOrCreate(source).GetModel();
            return dbModel.MultimediaItems.Select(d => new Core.GalleryAggregate.MultimediaItem(d.Id, d.PartialPath, source)).ToList();
        }

        public async Task ReplaceMultimediaItemsWith(MultimediaSource source, IEnumerable<Core.GalleryAggregate.MultimediaItem> mediaItems)
        {
            var db = _multimediaItemsFactory.LoadFromCacheOrCreate(source);
            var dbModel = await db.GetModel();
            dbModel.MultimediaItems.Clear();
            dbModel.MultimediaItems.AddRange(mediaItems.Select(d => new MultimediaItemsJsonFileDb.MultimediaItem(d.Id, d.PartialPath)));
            await db.PersistChangesToFile();
            _multimediaItemsFactory.InvalidateCacheFor(source);
        }
    }
}
