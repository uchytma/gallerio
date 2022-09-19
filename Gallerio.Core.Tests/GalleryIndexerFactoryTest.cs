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
    public class GalleryIndexerFactoryTest
    {
        private GalleryIndexerFactory _gif;

        public GalleryIndexerFactoryTest()
        {
            _gif = new GalleryIndexerFactory(default!, default!, default!, default!);
        }

        /// <summary>
        /// Test that GalleryIndexerFactory always create new instance and do not reuse them.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task TestGalleryIndexerFactoryCreateNewInstances()
        {
            var instance1 = _gif.Create();
            var instance2 = _gif.Create();
            Assert.AreNotEqual(instance1, instance2);
        }

    }
}
