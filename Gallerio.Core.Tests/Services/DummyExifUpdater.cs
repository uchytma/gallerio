using Gallerio.Core.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallerio.Core.Tests.Services
{
    internal class DummyExifUpdater : IExifDataUpdater
    {
        public Task SetExifCustomData<T>(T data, string imageFullPath)
        {
            return Task.CompletedTask;
        }
    }
}
