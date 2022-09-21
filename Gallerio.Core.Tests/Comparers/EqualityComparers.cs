using Gallerio.Core.GalleryAggregate;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallerio.Core.Tests.Comparers
{
    internal class GalleryEqualityComparer : EqualityComparer<Gallery>
    {
        public override bool Equals(Gallery? x, Gallery? y)
        {
            if (x == null && y == null) return true;
            if (x == null || y == null) return false;   

            return (x.Id == y.Id 
                && x.Description == y.Description
                && x.Name == y.Name 
                && x.TotalPhotosCount == y.TotalPhotosCount 
                && x.Date == y.Date
                && MultimediaSourcesEquals(x.MultimediaSources, y.MultimediaSources));
        }

        private bool MultimediaSourcesEquals(IReadOnlyCollection<MultimediaSource> x, IReadOnlyCollection<MultimediaSource> y)
        {
            if (x == null && y == null) return true;
            if (x == null || y == null) return false;
            if (x.Count() != y.Count()) return false;

            var multimediaSourceComparer = new MultimediaSourceEqualityComparer();

            foreach (var itemX in x)
            {
                var itemY = y.FirstOrDefault(d => d.Id == itemX.Id);
                if (itemY == null) return false;
                if (!multimediaSourceComparer.Equals(itemX, itemY)) return false;
            }

            return true;
        }

        public override int GetHashCode([DisallowNull] Gallery obj)
        {
            throw new NotImplementedException();
        }
    }

    internal class MultimediaSourceEqualityComparer : EqualityComparer<MultimediaSource>
    {
        public override bool Equals(MultimediaSource? x, MultimediaSource? y)
        {
            if (x == null && y == null) return true;
            if (x == null || y == null) return false;

            return (x.Id == y.Id && x.SourceDir == y.SourceDir);
        }

        public override int GetHashCode([DisallowNull] MultimediaSource obj)
        {
            throw new NotImplementedException();
        }
    }

    internal class MultimediaItemEqualityComparer : EqualityComparer<MultimediaItem>
    {
        public override bool Equals(MultimediaItem? x, MultimediaItem? y)
        {
            if (x == null && y == null) return true;
            if (x == null || y == null) return false;

            var multimediaSourceComparer = new MultimediaSourceEqualityComparer();


            return (x.Id == y.Id 
                && x.FullPath == y.FullPath
                && x.PartialPath == y.PartialPath
                && x.CapturedDateTime == y.CapturedDateTime
                && x.MimeType == y.MimeType
                && multimediaSourceComparer.Equals(x.Source, y.Source)
                && TagsEquals(x.Tags, y.Tags));
        }

        private bool TagsEquals(IEnumerable<string> x, IEnumerable<string> y)
        {
            if (x == null && y == null) return true;
            if (x == null || y == null) return false;

            return x.SequenceEqual(y);

        }

        public override int GetHashCode([DisallowNull] MultimediaItem obj)
        {
            throw new NotImplementedException();
        }
    }
}
