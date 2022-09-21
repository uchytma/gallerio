using Gallerio.Api.Options;
using Gallerio.Core.GalleryAggregate.Services;
using Gallerio.Core.Interfaces.Core;
using Gallerio.Core.Interfaces.Infrastructure;
using Gallerio.Infrastructure.Services.ExifDataUpdater;
using Gallerio.Infrastructure.Services.MainJsonDb;
using Gallerio.Infrastructure.Services.MetadataExtractor;
using Gallerio.Infrastructure.Services.MultimediaItemsJsonFileDb;
using Gallerio.Infrastructure.Services.Repositories;

namespace Gallerio.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddCors(policy =>
            {
                policy.AddPolicy("CorsLocalhostFe", builder => builder.WithOrigins("https://localhost:7176/")
                     .SetIsOriginAllowed((host) => true)
                     .AllowAnyMethod()
                     .AllowAnyHeader()
                     .AllowCredentials());
            });

            builder.Services.AddAuthorization();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SupportNonNullableReferenceTypes();
            });

            builder.Services.AddSingleton<IGalleryProvider, GalleryProvider>();
            builder.Services.AddSingleton<IGalleryUpdater, GalleryUpdater>();
            builder.Services.AddSingleton<IGalleryReadOnlyRepo, GalleryRepoJsonFile>();
            builder.Services.AddSingleton<IGalleryUpdateRepo, GalleryRepoJsonFile>();
            builder.Services.AddSingleton<IGalleryIndexerFactory, GalleryIndexerFactory>();

            builder.Services.AddSingleton<IMultimediaItemProvider, MultimediaItemProvider>();
            builder.Services.AddSingleton<IMultimediaItemUpdater, MultimediaItemUpdater>();
            builder.Services.AddSingleton<IMultimediaItemsReadonlyRepo, MultimediaItemsRepoJsonFile>();
            builder.Services.AddSingleton<IMultimediaItemsUpdateRepo, MultimediaItemsRepoJsonFile>();

            builder.Services.AddSingleton<IMetadataExtractor, MetadataExtractorService>();
            builder.Services.AddSingleton<IExifDataUpdater, SharpImageExifUpdater>();

            builder.Services.AddSingleton<JsonFileMultimediaItemsFactory>();
            builder.Services.AddSingleton<JsonFileDb>();

            builder.Services.Configure<ApplicationOptions>(builder.Configuration.GetSection("Application"));

            builder.Services.AddControllers();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("CorsLocalhostFe");

            app.UseRouting();

            app.UseHttpsRedirection();
            app.UseAuthorization();

          
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.Run();
        }
    }
}