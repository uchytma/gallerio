using Gallerio.Core.GalleryAggregate;
using Gallerio.Core.GalleryAggregate.Exceptions;
using Gallerio.Core.GalleryAggregate.Services;
using Gallerio.Core.Tests.Comparers;
using Gallerio.Core.Tests.Extensions;
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
    public class MultimediaItemUpdaterTest : MultimediaItemTestBase
    {
        private MultimediaItemUpdater _imiu;

        public MultimediaItemUpdaterTest()
        {
            _imiu = new MultimediaItemUpdater(DummyMultimediaItemsRepo);
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
            var currentItem = await DummyMultimediaItemsRepo.FindMultimediaItem(arrange.Source, arrange.Item.Id);
            Assert.IsNotNull(currentItem);
            Assert.AreEqual("dir\\test1.jpg", currentItem.PartialPath);
            Assert.AreEqual(arrange.Item.Id, currentItem.Id);

            //update
            MultimediaItem mi = new MultimediaItem(currentItem.Id, "dir\\changed.jpg", arrange.Source, currentItem.CapturedDateTime, currentItem.Tags);
            await _imiu.UpdateItem(mi);

            //test updated item
            var updatedItem = await DummyMultimediaItemsRepo.FindMultimediaItem(arrange.Source, arrange.Item.Id);
            Assert.IsNotNull(updatedItem);
            Assert.AreEqual("dir\\changed.jpg", updatedItem.PartialPath);
            Assert.AreEqual(arrange.Item.Id, updatedItem.Id);
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
                new MultimediaItem(Guid.NewGuid(), "test99.jpg", arrange.Source, DateTime.MaxValue, Enumerable.Empty<string>()),
                new MultimediaItem(Guid.NewGuid(), "test100.jpg", arrange.Source, DateTime.Now, new string[] { "a", "b", "c" }),
            };

            //replace
            await _imiu.ReplaceMultimediaItemsWith(arrange.Source, toReplace);

            //test
            var updatedItems = await DummyMultimediaItemsRepo.GetMultimediaItems(arrange.Source);
            Assert.IsTrue(toReplace.Count() == updatedItems.Count);

            foreach (var updatedItem in updatedItems)
            {
                var sourceItem = toReplace.FirstOrDefault(d => d.Id == updatedItem.Id);
                Assert.IsNotNull(sourceItem);
                CustomAssert.AreEqual(sourceItem, updatedItem, new MultimediaItemEqualityComparer());
            }
        }
    }
}
