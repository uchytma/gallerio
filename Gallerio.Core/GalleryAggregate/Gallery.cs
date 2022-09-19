using Gallerio.Core.Interfaces.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallerio.Core.GalleryAggregate
{
    public class Gallery
    {
        private readonly List<MultimediaSource> _multimediaSources;

        public Gallery(Guid id, 
            string name, 
            string description, 
            string date, 
            int totalPhotosCount, 
            IEnumerable<MultimediaSource> multimediaSources)
        {
            Id = id;
            Name = name;
            Description = description;
            Date = date;
            TotalPhotosCount = totalPhotosCount;
            _multimediaSources = multimediaSources.ToList();
        }

        public Guid Id { get; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Date { get; set; }

        public int TotalPhotosCount { get; set; }

        public IReadOnlyCollection<MultimediaSource> GetMultimediaSources => _multimediaSources;
    }
}
