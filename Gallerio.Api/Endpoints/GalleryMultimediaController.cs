using Gallerio.Api.Dtos;
using Gallerio.Api.Extensions;
using Gallerio.Core.GalleryAggregate;
using Gallerio.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gallerio.Api.Endpoints
{
    [Route("api/gallery")]
    [ApiController]
    public class GalleryMultimediaController : ControllerBase
    {
        private readonly IGalleryProvider _galleryProvider;

        public GalleryMultimediaController(IGalleryProvider galleryProvider)
        {
            _galleryProvider = galleryProvider;
        }

        [HttpGet]
        [Route("{id}/multimedia")]
        [ProducesResponseType(typeof(IEnumerable<MultimediaItemDto>), 200)]
        public async Task<IActionResult> FindGalleryMultimediaList([FromRoute] Guid id)
        {
            try
            {
                var gallery = await _galleryProvider.FindGallery(id);
                var multimediaItems = await gallery.LoadMultimediaItems();
                return Ok(multimediaItems.Select(d => d.ToDto()));
            }
            catch (GalleryNotFoundException)
            {
                return NotFound("Galerie nenalezena.");
            }
        }


        [HttpGet]
        [Route("{id}/multimedia/{multimediaItemId}")]
        [ProducesResponseType(typeof(IEnumerable<MultimediaItemDto>), 200)]
        public async Task<IActionResult> FindGalleryMultimediaItem([FromRoute] Guid id, [FromRoute] Guid multimediaItemId)
        {
            try
            {
                var gallery = await _galleryProvider.FindGallery(id);
                var multimediaItem = await gallery.FindMultimediaItem(multimediaItemId);
                return Ok(multimediaItem);
            }
            catch (GalleryNotFoundException)
            {
                return NotFound("Galerie nenalezena.");
            }
            catch (MultimediaItemNotFoundException)
            {
                return NotFound("MultimediaItem nenalezen.");
            }
        }

        [HttpGet]
        [Route("{id}/multimedia/{multimediaItemId}/raw")]
        [ProducesResponseType(typeof(IEnumerable<MultimediaItemDto>), 200)]
        public async Task<IActionResult> FindGalleryMultimediaItemRaw([FromRoute] Guid id, [FromRoute] Guid multimediaItemId)
        {
            try
            {
                var gallery = await _galleryProvider.FindGallery(id);
                var multimediaItem = await gallery.FindMultimediaItem(multimediaItemId);
                return PhysicalFile(multimediaItem.FullPath, multimediaItem.MimeType, multimediaItem.Name, true);
            }
            catch (GalleryNotFoundException)
            {
                return NotFound("Galerie nenalezena.");
            }
            catch (MultimediaItemNotFoundException)
            {
                return NotFound("MultimediaItem nenalezen.");
            }
        }
    }
}
