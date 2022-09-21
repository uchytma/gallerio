namespace Gallerio.Web.Services.Breadcrumbs
{
    public interface IBreadcrumbsProvider
    {
        IEnumerable<BreadcrumbLink> Links { get; }

        event Action? StateChanged;

        void Pop();

        void Push(BreadcrumbLink link);

        void Clear();
    }
}