using Gallerio.Core.GalleryAggregate;

namespace Gallerio.Infrastructure.Services.MultimediaItemsJsonFileDb
{
    public class JsonFileMultimediaItemsFactory
    {
        Dictionary<Guid, JsonFileMultimediaItemsDb> _sourceCache = new Dictionary<Guid, JsonFileMultimediaItemsDb>();

        public JsonFileMultimediaItemsDb LoadFromCacheOrCreate(MultimediaSource source)
        {
            if (_sourceCache.TryGetValue(source.Id, out JsonFileMultimediaItemsDb? value)) 
                return value;
            
            var db = new JsonFileMultimediaItemsDb(Path.Combine(source.SourceDir, "gallerio.json"));
            _sourceCache.TryAdd(source.Id, db);
            return db;
        }

        public bool InvalidateCacheFor(MultimediaSource source)
        {
            return _sourceCache.Remove(source.Id);
        }
    }
}
