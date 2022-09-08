using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallerio.Api.ViewModels
{
    public record GalleryViewModel(Guid Id, string Name);
    public record CreateGalleryViewModel(string Name);
    public record UpdateGalleryViewModel(string? Name);
}
