using Gallerio.Api.Dtos;
using Gallerio.Api.Extensions;
using Gallerio.Core.GalleryAggregate;
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
        private readonly IMultimediaItemUpdater _muiu;
        private readonly IMultimediaItemProvider _muip;

        public GalleryMultimediaController(
            IGalleryProvider galleryProvider, 
            IMultimediaItemUpdater muiu, 
            IMultimediaItemProvider muip)
        {
            _galleryProvider = galleryProvider;
            _muiu = muiu;
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
                var multimediaItems = await _muip.GetMultimediaItems(gallery);
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
                var multimediaItem = await FindMultimediaItem(id, multimediaItemId);
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
                var multimediaItem = await FindMultimediaItem(id, multimediaItemId);
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
                var multimediaItem = await FindMultimediaItem(id, multimediaItemId);
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
                var multimediaItem = await FindMultimediaItem(id, multimediaItemId);
                multimediaItem.AddTag(model.Name);
                await _muiu.UpdateItem(multimediaItem);
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
                var multimediaItem = await FindMultimediaItem(id, multimediaItemId);
                multimediaItem.RemoveTag(tag);
                await _muiu.UpdateItem(multimediaItem);
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
        
        private async Task<MultimediaItem> FindMultimediaItem(Guid galleryId, Guid multimediaItemId)
        {
            var gallery = await _galleryProvider.FindGallery(galleryId);
            var multimediaItem = await _muip.FindMultimediaItem(gallery, multimediaItemId);
            return multimediaItem;
        }
    }
}
