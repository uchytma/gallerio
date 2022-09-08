namespace Gallerio.Web.Client.Services.Breadcrumbs
{
    public class BreadcrumbsProvider : IBreadcrumbsProvider
    {
        private Stack<BreadcrumbLink> _links = new Stack<BreadcrumbLink>();

        public IEnumerable<BreadcrumbLink> Links => _links.Reverse().AsEnumerable();

        public void Push(BreadcrumbLink link)
        {
            _links.Push(link);
            NotifyStateChanged();
        }

        public void Pop()
        {
            _links.Pop();
            NotifyStateChanged();
        }

        public void Clear()
        {
            _links.Clear();
            NotifyStateChanged();
        }

        public event Action? OnChange;

        private void NotifyStateChanged()
        {
            OnChange?.Invoke();
        }
    }
}
