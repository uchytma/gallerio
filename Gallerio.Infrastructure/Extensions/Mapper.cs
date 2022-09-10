using Gallerio.Core.GalleryAggregate;
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
        internal static Gallery ToDomainModel(this GalleryModel model)
        {
            return new Gallery(model.Id, model.Name, model.Description, model.Date, model.PhotosTotalCount);
        }
    }
}
