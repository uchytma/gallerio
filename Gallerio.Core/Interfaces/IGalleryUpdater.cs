using Gallerio.Core.GalleryAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallerio.Core.Interfaces
{
    public interface IGalleryUpdater
    {
        public Task<Gallery> CreateGallery(string name);
        public Task<Gallery> UpdateGallery(Gallery g);
    }
}
