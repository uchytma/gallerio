using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallerio.Core.Tests.Extensions
{
    internal static class CustomAssert
    {
        public static void AreEqual<T>(T expected, T actual, EqualityComparer<T> comparer)
        {
            Assert.IsTrue(comparer.Equals(expected, actual));
        }
    }
}
