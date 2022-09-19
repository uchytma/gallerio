using Gallerio.Core.GalleryAggregate;
using Gallerio.Core.GalleryAggregate.Services;
using Gallerio.Core.Interfaces.Core;
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
    public class GalleryFactoryTest
    {

        public GalleryFactoryTest()
        {
        }

        /// <summary>
        /// Test that the gallery factory can create new Gallery
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task TestFindGallery()
        {
            Guid guid = Guid.NewGuid();
            string name = $"name_{Guid.NewGuid}";
            string description = $"description_{Guid.NewGuid}";
            string date = $"date_{Guid.NewGuid}";
            int totalPhotosCount = 20;

            var gallery = new Gallery(guid, name, description, date, totalPhotosCount, Enumerable.Empty<MultimediaSource>());

            Assert.AreEqual(guid, gallery.Id);
            Assert.AreEqual(name, gallery.Name);
            Assert.AreEqual(description, gallery.Description);
            Assert.AreEqual(date, gallery.Date);
            Assert.AreEqual(totalPhotosCount, gallery.TotalPhotosCount);
        }
    }
}
