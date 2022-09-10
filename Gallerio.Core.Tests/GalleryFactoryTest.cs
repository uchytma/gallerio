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
    public class GalleryFactoryTest
    {
        IGalleryFactory _galleryFactory;
        IMultimediaItemProvider _imip;

        public GalleryFactoryTest()
        {
            _imip = new DummyMultimediaItemProvider();
            _galleryFactory = new GalleryFactory(_imip);
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

            var gallery = _galleryFactory.Create(guid, name, description, date, totalPhotosCount);

            Assert.AreEqual(guid, gallery.Id);
            Assert.AreEqual(name, gallery.Name);
            Assert.AreEqual(description, gallery.Description);
            Assert.AreEqual(date, gallery.Date);
            Assert.AreEqual(totalPhotosCount, gallery.TotalPhotosCount);
        }
    }
}
