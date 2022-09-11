using Gallerio.Api.Dtos;

namespace Gallerio.Web.Client.Model
{
    public class MultimediaItem
    {
        public MultimediaItem(MultimediaItemDto multimediaItemDto)
        {
            MultimediaItemDto = multimediaItemDto;
        }

        public MultimediaItemDto MultimediaItemDto { get;}

        public string GetLinkRaw(Guid galleryId) => $"https://localhost:7173/api/gallery/{galleryId}/multimedia/{MultimediaItemDto.Id}/raw";

    }
}
