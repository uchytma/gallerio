using Gallerio.Api.Options;
using Gallerio.Core.GalleryAggregate.Services;
using Gallerio.Core.Interfaces;
using Gallerio.Infrastructure.Db;
using Gallerio.Infrastructure.Services.Repositories;

namespace Gallerio.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
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