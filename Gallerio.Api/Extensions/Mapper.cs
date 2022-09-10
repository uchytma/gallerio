using Gallerio.Api.Dtos;
using Gallerio.Core.GalleryAggregate;

namespace Gallerio.Api.Extensions
{
    public static class Mapper
    {
        public static GalleryDto ToDto(this Gallery model)
        {
            return new GalleryDto(model.Id, model.Name, model.Description, model.Date, model.TotalPhotosCount);
        }
    }
}
