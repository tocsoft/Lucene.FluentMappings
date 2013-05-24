using System;
using System.Collections.Generic;
using System.Linq;

namespace Lucene.FluentMapping.Configuration
{
    public static class AppDomainExtensions
    {
        public static IEnumerable<Type> GetTypesImplementing(this AppDomain @this, Type interfaceType)
        {
            return @this.GetAssemblies()
                        .SelectMany(a => a.GetTypes())
                        .Where(interfaceType.IsAssignableFrom);
        }
    }
}