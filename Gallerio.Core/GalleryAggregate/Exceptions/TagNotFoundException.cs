namespace Gallerio.Core.GalleryAggregate.Exceptions
{
    public class TagNotFoundException : ApplicationException
    {
        public TagNotFoundException() : base("Tag not found.")
        {
        }
    }
}
