using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallerio.Core.GalleryAggregate
{
    public class MultimediaSource
    {
        public MultimediaSource(Guid id, string sourceDir)
        {
            Id = id;
            SourceDir = sourceDir;
        }

        public Guid Id { get; }

        public string SourceDir { get; }
    }
}
