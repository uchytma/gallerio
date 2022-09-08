using Gallerio.Api.ViewModels;
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
        [ProducesResponseType(typeof(GalleryViewModel), 200)]
        public async Task<IActionResult> FindGallery([FromRoute] Guid id)
        {
            try
            {
                var gallery = await _galleryProvider.FindGallery(id);
                return Ok(new GalleryViewModel(gallery.Id, gallery.Name));
            }
            catch (GalleryNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GalleryViewModel>), 200)]
        public async Task<IActionResult> GetGalleryList()
        {
            var galleryList = await _galleryProvider.GetGalleryList();
            return Ok(galleryList.Select(d => new GalleryViewModel(d.Id, d.Name)));
        }

        [HttpPost]
        [ProducesResponseType(typeof(GalleryViewModel), 200)]
        public async Task<IActionResult> CreateGallery([FromBody] CreateGalleryViewModel model)
        {
            var createdGallery = await _galleryUpdater.CreateGallery(model.Name);
            return Ok(new GalleryViewModel(createdGallery.Id, createdGallery.Name));
        }

        [HttpPatch]
        [Route("{id}")]
        [ProducesResponseType(typeof(GalleryViewModel), 200)]
        public async Task<IActionResult> UpdateGallery([FromRoute] Guid id, [FromBody] UpdateGalleryViewModel model)
        {
            try
            {
                var gallery = await _galleryProvider.FindGallery(id);
                var updatedGallery = await _galleryUpdater.UpdateGallery(new Gallery(gallery.Id, model.Name ?? gallery.Name));
                return Ok(new GalleryViewModel(updatedGallery.Id, updatedGallery.Name));
            }
            catch (GalleryNotFoundException)
            {
                return NotFound();
            }
           
        }
    }
}
