using Gallerio.Api.Dtos;
using Gallerio.Api.Extensions;
using Gallerio.Core.GalleryAggregate.Exceptions;
using Gallerio.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gallerio.Api.Endpoints
{
    [Route("api/gallery")]
    [ApiController]
    public class GalleryMultimediaSourcesController : ControllerBase
    {
        private readonly IGalleryIndexerFactory _galleryIndexerFactory;
        private readonly IGalleryProvider _galleryProvider;

        public GalleryMultimediaSourcesController(IGalleryIndexerFactory galleryIndexerFactory, IGalleryProvider galleryProvider)
        {
            _galleryIndexerFactory = galleryIndexerFactory;
            _galleryProvider = galleryProvider;
        }

        /// <summary>
        /// Synchronously runs indexing of multimedia items on selected gallery.
        /// It will attempt to recursively detect all multimedia items in all multimedia sources of the gallery.
        /// 
        /// This can take a large amount of time!
        /// 
        /// TODO: Endpoint is not thread safe (for now) adn should be called only once a time.
        /// If indexing is already running, it should return proper response, e.c. 409 Conflict
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{id}/multimediaSources/reindex")]
        [ProducesResponseType(typeof(ReindexMultimediaSourcesResponseDto), 200)]

        public async Task<IActionResult> ReindexMultimediaSources([FromRoute] Guid id)
        {
            try
            {
                var gallery = await _galleryProvider.FindGallery(id);
                var indexerResponse = await _galleryIndexerFactory.Create().ReindexMultimediaResources(gallery);
                return Ok(indexerResponse.ToDto());
            }
            catch (GalleryNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("{id}/multimediaSources")]
        [ProducesResponseType(typeof(IEnumerable<MultimediaSourceDto>), 200)]
        public async Task<IActionResult> GetMultimediaSourcesList([FromRoute] Guid id)
        {
            try
            {
                var gallery = await _galleryProvider.FindGallery(id);
                return Ok(gallery.GetMultimediaSources.Select(d => d.ToDto()));
            }
            catch (GalleryNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
