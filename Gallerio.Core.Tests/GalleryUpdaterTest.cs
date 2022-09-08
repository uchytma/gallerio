using Gallerio.Core.GalleryAggregate;
using Gallerio.Core.GalleryAggregate.Services;
using Gallerio.Core.Interfaces;
using Gallerio.Infrastructure.Services.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;

namespace Gallerio.Core.Tests
{
    [TestClass]
    public class GalleryUpdaterTest
    {
        private Gallery? _existingGallery;
        private Guid _nonExistingGalleryId = Guid.Empty;
        private IEnumerable<Guid> _allGalleriesId = new Guid[0];
        private GalleryUpdater _galleryUpdater;
        private DummyGalleryRepo _repo;

        public GalleryUpdaterTest()
        {
            _repo = new DummyGalleryRepo();
            _galleryUpdater = new GalleryUpdater(_repo);
            _allGalleriesId = _repo.GetExistingGalleries().Select(d => d.Id);
            _existingGallery = _repo.GetExistingGalleries().First();
            _nonExistingGalleryId = _repo.GetNotExistingGalleryGuid();
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

            Assert.AreEqual(randomName, result.Name);
            Assert.IsTrue(result.Id != Guid.Empty);
            Assert.IsTrue(_repo.GetExistingGalleries().Any(d => d.Id == result.Id));
        }

        /// <summary>
        /// Test that the gallery provider can update a gallery.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task TestUpdateGallery()
        {
            if (_existingGallery is null) throw new NullReferenceException(nameof(_existingGallery));

            string randomName = $"name_{Guid.NewGuid()}";
            var result = await _galleryUpdater.UpdateGallery(new Gallery(_existingGallery.Id, randomName));

            Assert.AreEqual(_existingGallery.Id, result.Id);
            Assert.AreEqual(randomName, result.Name);

            var galleriesFromRepo = _repo.GetExistingGalleries().Where(d => d.Id == result.Id).ToList();
            Assert.AreEqual(1, galleriesFromRepo.Count);

            var galleryFromRepo = galleriesFromRepo.First();

            Assert.AreEqual(_existingGallery.Id, galleryFromRepo.Id);
            Assert.AreEqual(randomName, galleryFromRepo.Name);
        }

        /// <summary>
        /// Test that the gallery provider throws an exception when trying to update a gallery that does not exist
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        [ExpectedException(typeof(GalleryNotFoundException))]
        public async Task TestUpdateGalleryNotFound()
        {
            if (_existingGallery is null) throw new NullReferenceException(nameof(_existingGallery));

            string randomName = $"name_{Guid.NewGuid()}";
            var result = await _galleryUpdater.UpdateGallery(new Gallery(_nonExistingGalleryId, randomName));
        }
    }
}