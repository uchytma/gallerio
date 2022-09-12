using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallerio.Core.GalleryAggregate.Exceptions
{
    public class TagAlreadyExistException : ApplicationException
    {
        public TagAlreadyExistException() : base("Tag already exist.")
        {
        }
    }
}
