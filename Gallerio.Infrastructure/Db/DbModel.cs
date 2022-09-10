namespace Gallerio.Infrastructure.Db
{
    public record DbModel(List<GalleryModel> Galleries)
    {
        public static DbModel Empty => new DbModel(new List<GalleryModel>());
    }

    public record GalleryModel(Guid Id, string Name, string Description, string Date, int PhotosTotalCount);
}
