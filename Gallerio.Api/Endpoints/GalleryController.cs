using Gallerio.Api.Dtos;
using Gallerio.Api.Extensions;
using Gallerio.Core.GalleryAggregate.Exceptions;
using Gallerio.Core.GalleryAggregate.Services;
using Gallerio.Core.Interfaces.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gallerio.Api.Endpoints
{
    [Route("api/gallery")]
    [ApiController]
    public class GalleryController : ControllerBase
    {
        private readonly IGalleryProvider _galleryProvider;
        private readonly IGalleryUpdater _galleryUpdater;
        

        public GalleryController(IGalleryProvider galleryProvider, IGalleryUpdater galleryUpdater)
        {
            _galleryProvider = galleryProvider;
            _galleryUpdater = galleryUpdater;
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


        /// <summary>
        /// Only for test purposes.
        /// Endpoint will be deleted/reimplemented in the future.
        /// 
        /// Exports (copy) all 'TOP' tagged media from specified gallery to directory C:\\dev\\gallerio\\export.
        /// All exported media will lost original names and folder structure.
        /// (It will be exported to a single directory with generated name export_<guid>.)
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{id}/exportGalleryMultimediaWithTopTag")]
        public async Task<IActionResult> ExportGalleryMultimediaWithTopTag([FromRoute] Guid id)
        {
            try
            {
                var gallery = await _galleryProvider.FindGallery(id);
                var items = await gallery.LoadMultimediaItems();
                var itemsToExport = items.Where(d => d.Tags.Contains("TOP"));
                var destDirName = Path.Combine("C:\\dev\\gallerio\\export", $"export_{Guid.NewGuid()}");
                Directory.CreateDirectory(destDirName);

                foreach (var a in itemsToExport)
                {
                    var sourceFileName = a.FullPath;
                    var destFileName = Path.Combine(destDirName, $"{a.Id}{Path.GetExtension(a.FullPath)}");
                    System.IO.File.Copy(sourceFileName, destFileName, false);
                }
              
                return Ok();
            }
            catch (GalleryNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
