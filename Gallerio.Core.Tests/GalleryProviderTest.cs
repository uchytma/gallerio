using Gallerio.Core.GalleryAggregate;
using Gallerio.Core.GalleryAggregate.Services;
using Gallerio.Infrastructure.Services.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gallerio.Core.Tests
{
    [TestClass]
    public class GalleryProviderTest
    {
        private Guid _existingGalleryId = new Guid("7858785c-e0a4-4a08-b112-0347754e478d");
        private Guid _notFoundGalleryId = new Guid("1858785c-e0a4-4a08-b112-0347754e478d");
        private Guid[] _allGalleriesId = new Guid[]
        {
            new Guid("7858785c-e0a4-4a08-b112-0347754e478d"),
            new Guid("34139721-7752-4d73-918f-1a4cba73c6cb"),
        };

        private GalleryProvider _galleryProvider;

        public GalleryProviderTest()
        {
            DummyGalleryReadonlyRepo repo = new DummyGalleryReadonlyRepo();
            _galleryProvider = new GalleryProvider(repo);
        }

        /// <summary>
        /// Test that the gallery provider can find an existing gallery with valid data
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task TestFindGallery()
        {
            var result = await _galleryProvider.FindGallery(_existingGalleryId);
            Assert.AreEqual(_existingGalleryId, result.Id);
            Assert.AreEqual("Norsko", result.Name);
        }

        /// <summary>
        /// Test that the gallery provider throws an exception when trying to find a gallery that does not exist
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        [ExpectedException(typeof(GalleryNotFoundException))]
        public async Task TestFindGalleryNotFound()
        {
            await _galleryProvider.FindGallery(_notFoundGalleryId);
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