namespace Gallerio.Web.Services.LayoutConfiguration
{
    public class LayoutConfigurationService : ILayoutConfigurationService
    {
        private bool _zeroPadding;

        public bool NoPadding
        {
            get => _zeroPadding;
            set
            {
                _zeroPadding = value;
                NotifyStateChanged();
            }
        }

        public event Action? StateChanged;
        private void NotifyStateChanged() => StateChanged?.Invoke();
    }
}
