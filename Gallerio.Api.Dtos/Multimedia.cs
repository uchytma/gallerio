using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallerio.Api.Dtos
{
    public record MultimediaItemDto(Guid Id, string PartialPath, DateTime CaptureDateTime);
    public record MultimediaItemTagsDto(List<string> Tags);
    public record AddTag(string Name);
}
