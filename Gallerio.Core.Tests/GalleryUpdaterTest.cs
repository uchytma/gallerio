using Gallerio.Core.GalleryAggregate;
using Gallerio.Core.GalleryAggregate.Exceptions;
using Gallerio.Core.GalleryAggregate.Services;
using Gallerio.Core.Tests.Comparers;
using Gallerio.Core.Tests.Extensions;
using Gallerio.Infrastructure.Services.Repositories;


namespace Gallerio.Core.Tests
{
    [TestClass]
    public class GalleryUpdaterTest
    {
        private GalleryUpdater _galleryUpdater;
        private DummyGalleryRepo _repo;

        public GalleryUpdaterTest()
        {
            _repo = DummyGalleryRepo.CreateWithData();
            _galleryUpdater = new GalleryUpdater(_repo);
        }

        /// <summary>
        /// Test that the gallery provider can create a new gallery.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task TestCreateGallery()
        {
            string randomName = $"name_{Guid.NewGuid()}";
            var result = await _galleryUpdater.CreateGallery(randomName);

            Assert.IsTrue(result.Id != Guid.Empty);

            Gallery expectedGalleryData = new Gallery(result.Id, randomName, String.Empty, String.Empty, 0, Enumerable.Empty<MultimediaSource>());

            //test returned gallery
            CustomAssert.AreEqual(expectedGalleryData, result, new GalleryEqualityComparer());

            //test that gallery is really added to repo
            var galleryInRepo = await _repo.FindGallery(result.Id);
            Assert.IsNotNull(galleryInRepo);
            CustomAssert.AreEqual(expectedGalleryData, galleryInRepo, new GalleryEqualityComparer());
        }

        /// <summary>
        /// Test that the gallery provider can update a gallery.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task TestUpdateGallery()
        {
            var existingGallery = _repo.GetExistingGalleries().First();

            string randomName = $"name_{Guid.NewGuid()}";
            string randomDescription = $"description_{Guid.NewGuid()}";
            string randomDate = $"date_{Guid.NewGuid()}";
            int photosCount = 0;

            var gallery = new Gallery(existingGallery.Id, randomName, randomDescription, randomDate, photosCount, Enumerable.Empty<MultimediaSource>());
            var result = await _galleryUpdater.UpdateGallery(gallery);

            var expectedGalleryResult = new Gallery(existingGallery.Id, randomName, randomDescription, randomDate, photosCount, Enumerable.Empty<MultimediaSource>());

            //test returned gallery
            Assert.IsNotNull(result);
            CustomAssert.AreEqual(expectedGalleryResult, result, new GalleryEqualityComparer());

            //test that gallery is really updated in repo
            var galleriesFromRepo = _repo.GetExistingGalleries().Where(d => d.Id == result.Id).ToList();
            Assert.AreEqual(1, galleriesFromRepo.Count);

            var galleryFromRepo = galleriesFromRepo.First();

            Assert.IsNotNull(galleryFromRepo);
            CustomAssert.AreEqual(expectedGalleryResult, galleryFromRepo, new GalleryEqualityComparer());
        }

        /// <summary>
        /// Test that the gallery provider throws an exception when trying to update a gallery that does not exist
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        [ExpectedException(typeof(GalleryNotFoundException))]
        public async Task TestUpdateGalleryNotFound()
        {
            var gallery = new Gallery(_repo.GetNotExistingGalleryGuid(), String.Empty, string.Empty, string.Empty, 0, Enumerable.Empty<MultimediaSource>());
            var result = await _galleryUpdater.UpdateGallery(gallery);
        }
    }
}