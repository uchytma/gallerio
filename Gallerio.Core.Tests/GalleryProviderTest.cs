using Gallerio.Core.GalleryAggregate;
using Gallerio.Core.GalleryAggregate.Exceptions;
using Gallerio.Core.GalleryAggregate.Services;
using Gallerio.Core.Tests.Comparers;
using Gallerio.Core.Tests.Extensions;
using Gallerio.Infrastructure.Services.Repositories;


namespace Gallerio.Core.Tests
{
    [TestClass]
    public class GalleryProviderTest
    {
        private GalleryProvider _galleryProvider;
        private DummyGalleryRepo _repo;

        public GalleryProviderTest()
        {
            _repo = DummyGalleryRepo.CreateWithData();
            _galleryProvider = new GalleryProvider(_repo);
        }

        /// <summary>
        /// Test that the gallery provider can find an existing gallery with valid data
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task TestFindGallery()
        {
            Gallery galleryToSearch = _repo.GetExistingGalleries().First();

            var result = await _galleryProvider.FindGallery(galleryToSearch.Id);
           
            Assert.IsNotNull(result);
            CustomAssert.AreEqual(galleryToSearch, result, new GalleryEqualityComparer());
        }

        /// <summary>
        /// Test that the gallery provider throws an exception when trying to find a gallery that does not exist
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        [ExpectedException(typeof(GalleryNotFoundException))]
        public async Task TestFindGalleryNotFound()
        {
            await _galleryProvider.FindGallery(_repo.GetNotExistingGalleryGuid());
        }

        /// <summary>
        /// Test that the gallery provider can return a list of all galleries
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task TestGetGalleryList()
        {
            var allGarreriesFromRepo = await _repo.GetGalleryList();

            var galleries = await _galleryProvider.GetGalleryList();
           
            Assert.AreEqual(allGarreriesFromRepo.Count, galleries.Count);

            foreach (var g in allGarreriesFromRepo)
            {
                var foundGallery = galleries.Single(d => d.Id == g.Id);
                Assert.IsNotNull(foundGallery);
                CustomAssert.AreEqual(g, foundGallery, new GalleryEqualityComparer());
            }
        }
    }
}