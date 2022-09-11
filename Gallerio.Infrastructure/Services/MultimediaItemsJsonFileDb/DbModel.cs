using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallerio.Infrastructure.Services.MultimediaItemsJsonFileDb
{
    public record DbModel(List<MultimediaItem> MultimediaItems)
    {
        public static DbModel Empty => new DbModel(Enumerable.Empty<MultimediaItem>().ToList());
    }

    public record MultimediaItem(Guid Id, string PartialPath);
}
