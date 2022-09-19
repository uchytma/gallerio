using Gallerio.Core.GalleryAggregate;
using Gallerio.Core.GalleryAggregate.Services;
using Gallerio.Core.Tests.Services;
using Gallerio.Infrastructure.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallerio.Core.Tests
{
    [TestClass]
    public class MultimediaItemUpdaterTest
    {
        private MultimediaItemUpdater _imiu;
        private DummyMultimediaItemsRepo _imirr;
        private DummyGalleryRepo _repo;

        public MultimediaItemUpdaterTest()
        {
            _repo = new DummyGalleryRepo();
            _imirr = new DummyMultimediaItemsRepo();
            _imiu = new MultimediaItemUpdater(_imirr);
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
        /// Test that update item method update partial path of specified item by Guid
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task TestUpdateItem()
        {
            //arrange
            var arrange = await Arrange();

            //test current state
            var currentItem = await _imirr.FindMultimediaItem(arrange.source, arrange.itemGuid);
            Assert.IsNotNull(currentItem);
            Assert.AreEqual("dir\\test1.jpg", currentItem.PartialPath);
            Assert.AreEqual(arrange.itemGuid, currentItem.Id);

            //update
            MultimediaItem mi = new MultimediaItem(currentItem.Id, "dir\\changed.jpg", arrange.source, currentItem.CapturedDateTime, currentItem.Tags);
            await _imiu.UpdateItem(mi);

            //test updated item
            var updatedItem = await _imirr.FindMultimediaItem(arrange.source, arrange.itemGuid);
            Assert.IsNotNull(updatedItem);
            Assert.AreEqual("dir\\changed.jpg", updatedItem.PartialPath);
            Assert.AreEqual(arrange.itemGuid, updatedItem.Id);
        }

        /// <summary>
        /// Test that method really replace all multimedia items in specified source
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task TestReplaceMultimediaItemWith()
        {
            //arrange
            var arrange = await Arrange();
            IEnumerable<MultimediaItem> toReplace = new MultimediaItem[]
            {
                new MultimediaItem(Guid.NewGuid(), "test99.jpg", arrange.source, DateTime.MaxValue, Enumerable.Empty<string>()),
                new MultimediaItem(Guid.NewGuid(), "test100.jpg", arrange.source, DateTime.Now, new string[] { "a", "b", "c" }),
            };

            //replace
            await _imiu.ReplaceMultimediaItemsWith(arrange.source, toReplace);

            //test
            var updatedItems = await _imirr.GetMultimediaItems(arrange.source);
            Assert.IsTrue(toReplace.Count() == updatedItems.Count);

            foreach (var updatedItem in updatedItems)
            {
                var sourceItem = toReplace.FirstOrDefault(d => d.Id == updatedItem.Id);
                Assert.IsNotNull(sourceItem);
                Assert.AreEqual(sourceItem.Id, updatedItem.Id);
                Assert.AreEqual(sourceItem.Name, updatedItem.Name);
            }
        }
    }
}
