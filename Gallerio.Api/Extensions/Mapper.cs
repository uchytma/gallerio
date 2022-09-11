using Gallerio.Api.Dtos;
using Gallerio.Core.GalleryAggregate;
using Gallerio.Core.GalleryAggregate.Services;

namespace Gallerio.Api.Extensions
{
    public static class Mapper
    {
        public static GalleryDto ToDto(this Gallery model)
        {
            return new GalleryDto(model.Id, model.Name, model.Description, model.Date, model.TotalPhotosCount);
        }

        public static MultimediaItemDto ToDto(this MultimediaItem model)
        {
            return new MultimediaItemDto(model.Id, model.PartialPath, model.CapturedDateTime);
        }

        public static ReindexMultimediaSourcesResponseDto ToDto(this GalleryIndexer.ReindexMultimediaResourcesResponse model)
        {
            return new ReindexMultimediaSourcesResponseDto(model.NewMediaAdded, model.TotalMediaCount);
        }

        public static MultimediaSourceDto ToDto(this MultimediaSource model)
        {
            return new MultimediaSourceDto(model.Id, model.SourceDir);
        }
    }
}
