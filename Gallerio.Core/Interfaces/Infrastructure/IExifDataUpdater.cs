using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallerio.Core.Interfaces.Infrastructure
{
    public interface IExifDataUpdater
    {
        public Task SetExifCustomData<T>(T data, string imageFullPath);
    }
}
