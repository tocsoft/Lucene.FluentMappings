using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Lucene.FluentMapping.Configuration
{
    public static class AssemblyTypeResolutionExtensions
    {
        public static IEnumerable<Type> GetTypesImplementing(this IEnumerable<Assembly> @this, Type interfaceType)
        {
            return @this.SelectMany(a => a.GetTypes())
                        .Where(interfaceType.IsAssignableFrom);
        }
    }
}