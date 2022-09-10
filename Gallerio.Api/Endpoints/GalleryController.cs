using Gallerio.Api.Dtos;
using Gallerio.Api.Extensions;
using Gallerio.Core.GalleryAggregate;
using Gallerio.Core.GalleryAggregate.Services;
using Gallerio.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gallerio.Api.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class GalleryController : ControllerBase
    {
        private readonly IGalleryProvider _galleryProvider;
        private readonly IGalleryUpdater _galleryUpdater;

        public GalleryController(IGalleryProvider galleryProvider, IGalleryUpdater galleryUpdater)
        {
            this._galleryProvider = galleryProvider;
            this._galleryUpdater = galleryUpdater;
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(GalleryDto), 200)]
        public async Task<IActionResult> FindGallery([FromRoute] Guid id)
        {
            try
            {
                var gallery = await _galleryProvider.FindGallery(id);
                return Ok(gallery.ToDto());
            }
            catch (GalleryNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GalleryDto>), 200)]
        public async Task<IActionResult> GetGalleryList()
        {
            var galleryList = await _galleryProvider.GetGalleryList();
            return Ok(galleryList.Select(d => d.ToDto()));
        }

        [HttpPost]
        [ProducesResponseType(typeof(GalleryDto), 200)]
        public async Task<IActionResult> CreateGallery([FromBody] CreateGalleryDto model)
        {
            var createdGallery = await _galleryUpdater.CreateGallery(model.Name);
            return Ok(createdGallery.ToDto());
        }

        [HttpPatch]
        [Route("{id}")]
        [ProducesResponseType(typeof(GalleryDto), 200)]
        public async Task<IActionResult> UpdateGallery([FromRoute] Guid id, [FromBody] UpdateGalleryDto model)
        {
            try
            {
                var gallery = await _galleryProvider.FindGallery(id);

                gallery.Name = model.Name ?? gallery.Name;
                gallery.Description = model.Description ?? gallery.Description;
                gallery.Date = model.Date ?? gallery.Date;

                var updatedGallery = await _galleryUpdater.UpdateGallery(gallery);
                return Ok(updatedGallery.ToDto());
            }
            catch (GalleryNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
