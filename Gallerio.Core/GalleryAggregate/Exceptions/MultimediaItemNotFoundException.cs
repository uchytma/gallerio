using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallerio.Core.GalleryAggregate.Exceptions
{
    public class MultimediaItemNotFoundException : ApplicationException
    {
        public MultimediaItemNotFoundException() : base("Multimedia item not found.")
        {
        }
    }
}
