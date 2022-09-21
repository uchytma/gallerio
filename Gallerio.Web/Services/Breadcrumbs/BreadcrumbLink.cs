namespace Gallerio.Web.Services.Breadcrumbs
{
    public class BreadcrumbLink
    {
        public BreadcrumbLink(string address, string title, bool isActive)
        {
            Address = address;
            Title = title;
            IsActive = isActive;
        }

        public string Address { get; }
        public string Title { get; }
        public bool IsActive { get; }
    }
}
