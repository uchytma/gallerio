using Gallerio.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallerio.Core.GalleryAggregate.Services
{
    public class GalleryFactory : IGalleryFactory
    {
        private readonly IMultimediaItemProvider _imip;

        public GalleryFactory(IMultimediaItemProvider imip)
        {
            _imip = imip;
        }

        public Gallery Create(Guid id,
            string name, 
            string description, 
            string date, 
            int totalPhotosCount, 
            IEnumerable<MultimediaSource> multimediaSources)
        {
            return new Gallery(id, name, description, date, totalPhotosCount, multimediaSources, _imip);
        }
    }
}
