using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Gallerio.Core.GalleryAggregate
{
    public class MultimediaItem
    {
        public MultimediaItem(Guid id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public Guid Id { get; }

        /// <summary>
        /// Name is unique in scope of MultimediaSource.
        /// The Name is partial file path from base path (which is multimedia SourceDir)
        /// </summary>
        public string Name { get; }
    }
}
