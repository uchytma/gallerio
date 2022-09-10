using Gallerio.Core.GalleryAggregate.Services;
using Gallerio.Core.Interfaces;
using Gallerio.Infrastructure.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallerio.Core.Tests
{
    [TestClass]
    public class MultimediaItemProviderTest
    {

        MultimediaItemProvider _imip;

        public MultimediaItemProviderTest()
        {
            _imip = new MultimediaItemProvider();
        }

        /// <summary>
        /// Test that the gallery can return valid multimedia items.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task TestGetGalleryMultimedia()
        {
            var guidTestItem = new Guid("d1f91baf-a935-4bf5-93c1-c2034a1690d4");

            var multimediaItems = (await _imip.GetMultimediaItems(Guid.NewGuid())).ToList();

            Assert.AreEqual(2, multimediaItems.Count);

            //Select one media item and test its properties.
            var testItem = multimediaItems.SingleOrDefault(d => d.Id == guidTestItem);
            Assert.IsNotNull(testItem);
            Assert.AreEqual(guidTestItem, testItem.Id);
            Assert.AreEqual("test1.jpg", testItem.Name);
        }
    }
}
