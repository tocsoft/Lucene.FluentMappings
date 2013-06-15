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
            return @this.SelectMany(a => a.SafeGetTypes())
                        .Where(interfaceType.IsAssignableFrom);
        }

        /// <summary>
        /// Safely the get types accessible from the assembly.
        /// </summary>
        /// <remarks>This will ignore any types where dependencies fail to load</remarks>
        /// <param name="this">The this.</param>
        /// <returns></returns>
        public static IEnumerable<Type> SafeGetTypes(this Assembly @this)
        {
            try
            {
                return @this.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                //these is a list for types that where successfully loaded from the assembly
                return ex.Types;
            }
        }
    }
}