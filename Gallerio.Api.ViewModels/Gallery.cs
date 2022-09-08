using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallerio.Api.Dtos
{
    public record GalleryDto(Guid Id, string Name);
    public record CreateGalleryDto(string Name);
    public record UpdateGalleryDto(string? Name);
}
