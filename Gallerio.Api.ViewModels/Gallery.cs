using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallerio.Api.ViewModels
{
    public record GalleryViewModel;
    public record GalleryViewModelWithId(Guid id) : GalleryViewModel;
}
