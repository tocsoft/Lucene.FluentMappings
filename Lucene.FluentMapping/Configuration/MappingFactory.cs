using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Lucene.FluentMapping.Conversion;

namespace Lucene.FluentMapping.Configuration
{
    public static class MappingFactory
    {
        private static readonly ConcurrentDictionary<Type, object> _mappings = new ConcurrentDictionary<Type, object>();

        public static IEnumerable<IFieldMap<T>> GetMappings<T>(Assembly sourceAssembly = null)
        {
            return GetMappings<T>(new[] {sourceAssembly ?? typeof (T).Assembly});
        }

        public static IEnumerable<IFieldMap<T>> GetMappings<T>(Assembly[] sourceAssemblies)
        {
            return (IEnumerable<IFieldMap<T>>) _mappings.GetOrAdd(typeof (T), _ => BuildMappings<T>( sourceAssemblies));
        }

        private static object BuildMappings<T>(Assembly[] sourceAssemblies)
        {
            var mappingConfiguration = BuildMappingConfiguration<T>(sourceAssemblies);

            return mappingConfiguration.BuildMappings();
        }

        private static IMappingConfiguration<T> BuildMappingConfiguration<T>(Assembly[] sourceAssemblies)
        {
            var configurationType = FindMappingConfigurationType(typeof(T), sourceAssemblies);

            return Activator.CreateInstance(configurationType) as IMappingConfiguration<T>;
        }

        private static Type FindMappingConfigurationType(Type mappedType, Assembly[] sourceAssemblies)
        {
            var interfaceType = typeof (IMappingConfiguration<>).MakeGenericType(mappedType);

            var implementerType = FindImplementer(interfaceType, sourceAssemblies);

            if (implementerType == null)
                throw NotFound(interfaceType, sourceAssemblies);

            return implementerType;
        }

        private static Type FindImplementer(Type interfaceType, Assembly[] sourceAssemblies)
        {
            return sourceAssemblies
                .GetTypesImplementing(interfaceType)
                .FirstOrDefault();
        }

        private static ArgumentException NotFound(Type interfaceType, IEnumerable<Assembly> scannedAssemblies)
        {
            var interfaceName = string.Concat(interfaceType.Name.Remove(interfaceType.Name.Length - 2), "<",
                                              interfaceType.GetGenericArguments().First().Name, ">");

            var locations = string.Join(Environment.NewLine, scannedAssemblies.Select(x => x.FullName));

            var message = string.Format("Type implementing {0} not found. Looked in:{1} {2}", interfaceName, Environment.NewLine, locations);

            return new ArgumentException(message);
        }
    }
}