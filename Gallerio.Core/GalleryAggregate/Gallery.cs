using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallerio.Core.GalleryAggregate
{
    public class Gallery
    {
        public Gallery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
