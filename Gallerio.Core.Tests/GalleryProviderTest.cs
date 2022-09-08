using Gallerio.Core.GalleryAggregate;
using Gallerio.Core.GalleryAggregate.Services;
using Gallerio.Infrastructure.Services.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;

namespace Gallerio.Core.Tests
{
    [TestClass]
    public class GalleryProviderTest
    {
        private Gallery? _existingGallery;
        private Guid _nonExistingGalleryId = Guid.Empty;
        private IEnumerable<Guid> _allGalleriesId = new Guid[0];
        private GalleryProvider _galleryProvider;

        public GalleryProviderTest()
        {
            DummyGalleryRepo repo = new DummyGalleryRepo();
            _galleryProvider = new GalleryProvider(repo);
            _allGalleriesId = repo.GetExistingGalleries().Select(d => d.Id);
            _existingGallery = repo.GetExistingGalleries().First();
            _nonExistingGalleryId = repo.GetNotExistingGalleryGuid();
        }

        /// <summary>
        /// Test that the gallery provider can find an existing gallery with valid data
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task TestFindGallery()
        {
            if (_existingGallery is null) throw new NullReferenceException(nameof(_existingGallery));

            var result = await _galleryProvider.FindGallery(_existingGallery.Id);
            Assert.AreEqual(_existingGallery.Id, result.Id);
            Assert.AreEqual(_existingGallery.Name, result.Name);
        }

        /// <summary>
        /// Test that the gallery provider throws an exception when trying to find a gallery that does not exist
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        [ExpectedException(typeof(GalleryNotFoundException))]
        public async Task TestFindGalleryNotFound()
        {
            await _galleryProvider.FindGallery(_nonExistingGalleryId);
        }

        /// <summary>
        /// Test that the gallery provider can return a list of all galleries
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task GetGalleryList()
        {
            var galleries = await _galleryProvider.GetGalleryList();
            Assert.AreEqual(_allGalleriesId.Count(), galleries.Count);

            foreach (var g in _allGalleriesId)
            {
                Assert.IsTrue(galleries.Any(d => d.Id == g));
            }
        }
    }
}