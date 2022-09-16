using Gallerio.Core.GalleryAggregate;
using Gallerio.Core.GalleryAggregate.Exceptions;
using Gallerio.Core.GalleryAggregate.Services;
using Gallerio.Core.Interfaces.Core;
using Gallerio.Core.Tests.Services;
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
        private GalleryUpdater _galleryUpdater;
        private DummyGalleryRepo _repo;
        private IGalleryFactory _galleryFactory;
        private IMultimediaItemProvider _multimediaItemProvider;
        private DummyMultimediaItemsRepo _itemsRepo;

        public GalleryUpdaterTest()
        {
            _itemsRepo = new DummyMultimediaItemsRepo();
            _multimediaItemProvider = new MultimediaItemProvider(_itemsRepo);
            _galleryFactory = new GalleryFactory(_multimediaItemProvider);
            _repo = new DummyGalleryRepo(_galleryFactory);
            _galleryUpdater = new GalleryUpdater(_repo);
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
            Assert.AreEqual(string.Empty, result.Description);
            Assert.AreEqual(string.Empty, result.Date);
            Assert.AreEqual(0, result.TotalPhotosCount);
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
            string randomDescription = $"description_{Guid.NewGuid()}";
            string randomDate = $"date_{Guid.NewGuid()}";
            int photosCount = 0;

            var gallery = _galleryFactory.Create(_existingGallery.Id, randomName, randomDescription, randomDate, photosCount, Enumerable.Empty<MultimediaSource>());
            var result = await _galleryUpdater.UpdateGallery(gallery);

            Assert.AreEqual(_existingGallery.Id, result.Id);
            Assert.AreEqual(randomName, result.Name);
            Assert.AreEqual(randomDescription, result.Description);
            Assert.AreEqual(randomDate, result.Date);
            Assert.AreEqual(photosCount, result.TotalPhotosCount);

            var galleriesFromRepo = _repo.GetExistingGalleries().Where(d => d.Id == result.Id).ToList();
            Assert.AreEqual(1, galleriesFromRepo.Count);

            var galleryFromRepo = galleriesFromRepo.First();

            Assert.AreEqual(_existingGallery.Id, galleryFromRepo.Id);
            Assert.AreEqual(randomName, galleryFromRepo.Name);
            Assert.AreEqual(randomDescription, galleryFromRepo.Description);
            Assert.AreEqual(randomDate, galleryFromRepo.Date);
            Assert.AreEqual(photosCount, galleryFromRepo.TotalPhotosCount);
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
            var gallery = _galleryFactory.Create(_nonExistingGalleryId, randomName, string.Empty, string.Empty, 0, Enumerable.Empty<MultimediaSource>());
            var result = await _galleryUpdater.UpdateGallery(gallery);
        }
    }
}