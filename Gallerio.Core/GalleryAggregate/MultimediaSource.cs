using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallerio.Core.GalleryAggregate
{
    public class MultimediaSource
    {
        public MultimediaSource(Guid id, string sourceConfigurationFilePath)
        {
            Id = id;
            SourceConfigurationFilePath = sourceConfigurationFilePath;
        }

        public Guid Id { get; }

        public string SourceConfigurationFilePath { get; }
    }
}
