using Gallerio.Core.GalleryAggregate;
using Gallerio.Core.GalleryAggregate.Exceptions;
using Gallerio.Core.GalleryAggregate.Services;
using Gallerio.Core.Interfaces.Core;
using Gallerio.Core.Interfaces.Infrastructure;
using Gallerio.Core.Tests.Services;
using Gallerio.Infrastructure.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Gallerio.Core.Tests
{
    [TestClass]
    public class MultimediaItemProviderTest
    {

        private MultimediaItemProvider _imip;
        private DummyMultimediaItemsRepo _imirr;
        private DummyGalleryRepo _repo;

        public MultimediaItemProviderTest()
        {
            _repo = new DummyGalleryRepo();
            _imirr = new DummyMultimediaItemsRepo();
            _imip = new MultimediaItemProvider(_imirr);
        }

        private async Task<(Gallery gallery, MultimediaSource source, Guid itemGuid)> Arrange()
        {
            var galleryToTest = _repo.GetExistingGalleries().First();
            var multimediaSourceToTest = galleryToTest.GetMultimediaSources.First();
            var guidTestItem = new Guid("d1f91baf-a935-4bf5-93c1-c2034a1690d4");
            await _imirr.CreateDefaultSourceItems(multimediaSourceToTest);
            return (galleryToTest, multimediaSourceToTest, guidTestItem);
        }

        /// <summary>
        /// Test that we can find valid multimedia items in gallery.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task TestGetMultimediaItemsInGallery()
        {
            //arrange
            var arrangedEnv = await Arrange();

            //act
            var multimediaItems = (await _imip.GetMultimediaItems(arrangedEnv.gallery)).ToList();

            //test
            Assert.AreEqual(2, multimediaItems.Count);

            //Select one media item and test its properties.
            var testItem = multimediaItems.SingleOrDefault(d => d.Id == arrangedEnv.itemGuid);
            Assert.IsNotNull(testItem);
            Assert.AreEqual(arrangedEnv.itemGuid, testItem.Id);
        }

        /// <summary>
        /// Test that we can find valid multimedia items in source.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task TestGetMultimediaItemsInSource()
        {
            //arrange
            var arrangedEnv = await Arrange();

            //act
            var multimediaItems = (await _imip.GetMultimediaItems(arrangedEnv.source)).ToList();

            //test
            Assert.AreEqual(2, multimediaItems.Count);

            //Select one media item and test its properties.
            var testItem = multimediaItems.SingleOrDefault(d => d.Id == arrangedEnv.itemGuid);
            Assert.IsNotNull(testItem);
            Assert.AreEqual(arrangedEnv.itemGuid, testItem.Id);
        }

        /// <summary>
        /// Test that we can find specific multimedia item based on its Guid.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task TestFindMultimediaInGallery()
        {
            //arrange
            var arrangedEnv = await Arrange();

            //act
            var multimediaItem = (await _imip.FindMultimediaItem(arrangedEnv.gallery, arrangedEnv.itemGuid));

            //test
            Assert.IsNotNull(multimediaItem);
            Assert.AreEqual(arrangedEnv.itemGuid, multimediaItem.Id);
            Assert.AreEqual("test1.jpg", multimediaItem.Name);
            Assert.AreEqual(arrangedEnv.source, multimediaItem.Source);
            Assert.AreEqual("dir\\test1.jpg", multimediaItem.PartialPath);
            Assert.AreEqual("image/jpeg", multimediaItem.MimeType);
            Assert.AreEqual("C:\\dev\\gallerio\\099D5200\\dir\\test1.jpg", multimediaItem.FullPath);
            Assert.AreEqual(DateTime.MinValue, multimediaItem.CapturedDateTime);
            Assert.IsTrue(!multimediaItem.Tags.Any());
        }

        /// <summary>
        /// Test that attempt to find non existing multimedia item throws an exception
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        [ExpectedException(typeof(MultimediaItemNotFoundException))]
        public async Task TestFindMultimediaInGalleryNotFound()
        {
            //arrange
            var arrangedEnv = await Arrange();
            var guidTestItemNotExists = new Guid("d1f91baf-a935-4bf5-93c1-c2034a1690d3");

            //act
            var _ = (await _imip.FindMultimediaItem(arrangedEnv.gallery, guidTestItemNotExists));
        }


    }
}
