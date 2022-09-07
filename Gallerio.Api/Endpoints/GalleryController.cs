using Gallerio.Api.ViewModels;
using Gallerio.Core.GalleryAggregate;
using Gallerio.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gallerio.Api.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class GalleryController : ControllerBase
    {
        private readonly IGalleryProvider _galleryReadOnlyRepo;

        public GalleryController(IGalleryProvider galleryProvider)
        {
            this._galleryReadOnlyRepo = galleryProvider;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> FindGallery([FromRoute] Guid id)
        {
            try
            {
                var gallery = await _galleryReadOnlyRepo.FindGallery(id);
                return Ok(new GalleryViewModelWithId(gallery.Id));
            }
            catch (GalleryNotFoundException)
            {
                return NotFound();
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> GetGalleryList()
        {
            var galleryList = await _galleryReadOnlyRepo.GetGalleryList();
            return Ok(galleryList.Select(d => new GalleryViewModelWithId(d.Id)));
        }
    }
}
