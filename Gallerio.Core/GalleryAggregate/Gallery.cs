using Gallerio.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallerio.Core.GalleryAggregate
{
    public class Gallery
    {
        private readonly IMultimediaItemProvider _muip;

        internal Gallery(Guid id, string name, string description, string date, int totalPhotosCount, IMultimediaItemProvider muip)
        {
            Id = id;
            Name = name;
            Description = description;
            Date = date;
            TotalPhotosCount = totalPhotosCount;
            _muip = muip;
        }

        public Guid Id { get; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Date { get; set; }

        public int TotalPhotosCount { get; }

        public async Task<IEnumerable<MultimediaItem>> LoadMultimediaItems()
        {
            return await _muip.GetMultimediaItems(this.Id);
        }
    }
}
