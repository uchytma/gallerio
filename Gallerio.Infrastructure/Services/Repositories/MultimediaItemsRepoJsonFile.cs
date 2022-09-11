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

        public async Task<IReadOnlyCollection<Core.GalleryAggregate.MultimediaItem>> GetMultimediaItems(MultimediaSource source)
        {
            var dbmodel = await _multimediaItemsFactory.LoadFromCacheOrCreate(source).GetModel();
            return dbmodel.MultimediaItems.Select(d => new Core.GalleryAggregate.MultimediaItem(d.Id, d.Name)).ToList();
        }

        public async Task ReplaceMultimediaItemsWith(MultimediaSource source, IEnumerable<Core.GalleryAggregate.MultimediaItem> mediaItems)
        {
            var db = _multimediaItemsFactory.LoadFromCacheOrCreate(source);
            var dbmodel = await db.GetModel();
            dbmodel.MultimediaItems.Clear();
            dbmodel.MultimediaItems.AddRange(mediaItems.Select(d => new MultimediaItemsJsonFileDb.MultimediaItem(d.Id, d.Name)));
            await db.PersistChangesToFile();
            _multimediaItemsFactory.InvalidateCacheFor(source);
        }
    }
}
