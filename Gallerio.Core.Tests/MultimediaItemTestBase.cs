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
    public abstract class MultimediaItemTestBase
    {

        protected DummyMultimediaItemsRepo DummyMultimediaItemsRepo { get; init; }
        protected DummyGalleryRepo DummyGalleryRepo { get; init; }

        protected MultimediaItemTestBase()
        {
            DummyGalleryRepo = DummyGalleryRepo.CreateWithData();
            DummyMultimediaItemsRepo = new DummyMultimediaItemsRepo();
        }

        protected async Task<(Gallery Gallery, MultimediaSource Source, MultimediaItem Item)> Arrange()
        {
            var galleryToTest = DummyGalleryRepo.GetExistingGalleries().First();
            var multimediaSourceToTest = galleryToTest.MultimediaSources.First();
            await DummyMultimediaItemsRepo.CreateDefaultSourceItems(multimediaSourceToTest);
            var guidTestItem = new Guid("d1f91baf-a935-4bf5-93c1-c2034a1690d4");
            var itemToTest = await DummyMultimediaItemsRepo.FindMultimediaItem(multimediaSourceToTest, guidTestItem);
            Assert.IsNotNull(itemToTest);
            return (galleryToTest, multimediaSourceToTest, itemToTest);
        }
    }
}
