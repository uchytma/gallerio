using Gallerio.Api.Dtos;
using Gallerio.Api.Extensions;
using Gallerio.Core.GalleryAggregate.Exceptions;
using Gallerio.Core.GalleryAggregate.Services;
using Gallerio.Core.Interfaces.Core;
using Microsoft.AspNetCore.Mvc;

namespace Gallerio.Api.Endpoints
{
    [Route("api/gallery")]
    [ApiController]
    public class GalleryMultimediaController : ControllerBase
    {
        private readonly IGalleryProvider _galleryProvider;
        private readonly IMultimediaItemUpdater _muip;

        public GalleryMultimediaController(IGalleryProvider galleryProvider, IMultimediaItemUpdater muip)
        {
            _galleryProvider = galleryProvider;
            _muip = muip;
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
                return Ok(multimediaItem.ToDto());
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

        [HttpGet]
        [Route("{id}/multimedia/{multimediaItemId}/tags")]
        [ProducesResponseType(typeof(IEnumerable<MultimediaItemTagsDto>), 200)]
        public async Task<IActionResult> FindGalleryMultimediaItemTags([FromRoute] Guid id, [FromRoute] Guid multimediaItemId)
        {
            try
            {
                var gallery = await _galleryProvider.FindGallery(id);
                var multimediaItem = await gallery.FindMultimediaItem(multimediaItemId);
                return Ok(new MultimediaItemTagsDto(multimediaItem.Tags.ToList()));
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

        [HttpPost]
        [Route("{id}/multimedia/{multimediaItemId}/tags")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> FindGalleryMultimediaItemTagsAdd([FromRoute] Guid id, [FromRoute] Guid multimediaItemId, [FromBody] AddTag model)
        {
            try
            {
                var gallery = await _galleryProvider.FindGallery(id);
                var multimediaItem = await gallery.FindMultimediaItem(multimediaItemId);
                multimediaItem.AddTag(model.Name);
                await _muip.UpdateItem(multimediaItem);
                return Ok(new MultimediaItemTagsDto(multimediaItem.Tags.ToList()));
            }
            catch (GalleryNotFoundException)
            {
                return NotFound("Gallery not found.");
            }
            catch (MultimediaItemNotFoundException)
            {
                return NotFound("MultimediaItem not found.");
            }
            catch (TagAlreadyExistException)
            {
                return Conflict("Tag already exists.");
            }
        }

        [HttpDelete]
        [Route("{id}/multimedia/{multimediaItemId}/tags/{tag}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> FindGalleryMultimediaItemTagsRemove([FromRoute] Guid id, [FromRoute] Guid multimediaItemId, [FromRoute] string tag)
        {
            try
            {
                var gallery = await _galleryProvider.FindGallery(id);
                var multimediaItem = await gallery.FindMultimediaItem(multimediaItemId);
                multimediaItem.RemoveTag(tag);
                await _muip.UpdateItem(multimediaItem);
                return Ok(new MultimediaItemTagsDto(multimediaItem.Tags.ToList()));
            }
            catch (GalleryNotFoundException)
            {
                return NotFound("Gallery not found.");
            }
            catch (MultimediaItemNotFoundException)
            {
                return NotFound("MultimediaItem not found.");
            }
            catch (TagNotFoundException)
            {
                return Conflict("Tag not found.");
            }
        }
    }
}
