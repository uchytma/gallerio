namespace Gallerio.Web.Client.Services.Breadcrumbs
{
    public interface IBreadcrumbsProvider
    {
        IEnumerable<BreadcrumbLink> Links { get; }

        event Action? OnChange;

        void Pop();
        void Push(BreadcrumbLink link);

        void Clear();
    }
}