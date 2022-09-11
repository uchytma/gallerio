using Gallerio.Core.GalleryAggregate;
using Gallerio.Core.Interfaces;
using Gallerio.Infrastructure.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallerio.Infrastructure.Extensions
{
    internal static class Mapper
    {
        internal static Gallery ToDomainModel(this GalleryModel model, IGalleryFactory galleryFactory)
        {
            return galleryFactory.Create(model.Id,
                model.Name,
                model.Description, 
                model.Date, 
                model.PhotosTotalCount, 
                model.MultimediaSources?.Select(d => new Core.GalleryAggregate.MultimediaSource(d.Id, d.SourceConfigurationFilePath)) 
                    ?? Enumerable.Empty<Core.GalleryAggregate.MultimediaSource>());
        }
    }
}
