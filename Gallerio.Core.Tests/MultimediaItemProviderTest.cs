using Gallerio.Core.GalleryAggregate;
using Gallerio.Core.GalleryAggregate.Exceptions;
using Gallerio.Core.GalleryAggregate.Services;
using Gallerio.Core.Interfaces.Core;
using Gallerio.Core.Interfaces.Infrastructure;
using Gallerio.Core.Tests.Comparers;
using Gallerio.Core.Tests.Extensions;
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
    public class MultimediaItemProviderTest : MultimediaItemTestBase
    {
        private MultimediaItemProvider _mip;


        public MultimediaItemProviderTest()
        {
            _mip = new MultimediaItemProvider(DummyMultimediaItemsRepo);
        }

     
        /// <summary>
        /// Test that we can find valid multimedia items in gallery.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task TestGetMultimediaItemsInGallery()
        {
            var arrangedEnv = await Arrange();

            var multimediaItems = (await _mip.GetMultimediaItems(arrangedEnv.Gallery)).ToList();

            Assert.AreEqual(2, multimediaItems.Count);

            //Select one media item and test its properties.
            var testItem = multimediaItems.SingleOrDefault(d => d.Id == arrangedEnv.Item.Id);
            Assert.IsNotNull(testItem);
            CustomAssert.AreEqual(arrangedEnv.Item, testItem, new MultimediaItemEqualityComparer());
        }

        /// <summary>
        /// Test that we can find valid multimedia items in source.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task TestGetMultimediaItemsInSource()
        {
            var arrangedEnv = await Arrange();

            var multimediaItems = (await _mip.GetMultimediaItems(arrangedEnv.Source)).ToList();

            Assert.AreEqual(2, multimediaItems.Count);

            //Select one media item and test its properties.
            var testItem = multimediaItems.SingleOrDefault(d => d.Id == arrangedEnv.Item.Id);
            Assert.IsNotNull(testItem);
            CustomAssert.AreEqual(arrangedEnv.Item, testItem, new MultimediaItemEqualityComparer());
        }

        /// <summary>
        /// Test that we can find specific multimedia item based on its Guid.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task TestFindMultimediaInGallery()
        {
            var arrangedEnv = await Arrange();

            var multimediaItem = (await _mip.FindMultimediaItem(arrangedEnv.Gallery, arrangedEnv.Item.Id));

            Assert.IsNotNull(multimediaItem);
            CustomAssert.AreEqual(arrangedEnv.Item, multimediaItem, new MultimediaItemEqualityComparer());
        }

        /// <summary>
        /// Test that attempt to find non existing multimedia item throws an exception
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        [ExpectedException(typeof(MultimediaItemNotFoundException))]
        public async Task TestFindMultimediaInGalleryNotFound()
        {
            var arrangedEnv = await Arrange();
            var guidTestItemNotExists = new Guid("d1f91baf-a935-4bf5-93c1-c2034a1690d3");

            var _ = (await _mip.FindMultimediaItem(arrangedEnv.Gallery, guidTestItemNotExists));
        }
    }
}
