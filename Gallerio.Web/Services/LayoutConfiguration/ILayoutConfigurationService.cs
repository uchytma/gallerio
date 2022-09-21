namespace Gallerio.Web.Services.LayoutConfiguration
{
    public interface ILayoutConfigurationService
    {
        bool NoPadding { get; set; }

        event Action? StateChanged;
    }
}