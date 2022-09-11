using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallerio.Api.Dtos
{
    public record ReindexMultimediaSourcesResponseDto(int MediaUpdated, int NewMediaAdded, int TotalMediaCount);
    public record MultimediaSourceDto(Guid Id, string SourceConfigurationFilePath);
}
