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
        private static readonly ConcurrentDictionary<Type, object> _mappingConfigurations = new ConcurrentDictionary<Type, object>();

        public static IEnumerable<IFieldMap<T>> GetMappings<T>(Assembly sourceAssembly)
        {
            return GetMappings<T>(new[] {sourceAssembly});
        }

        public static IEnumerable<IFieldMap<T>> GetMappings<T>(Assembly[] sourceAssemblies)
        {
            return GetMappingConfiguration<T>(sourceAssemblies).BuildMappings();
        }

        private static IMappingConfiguration<T> GetMappingConfiguration<T>(Assembly[] sourceAssemblies)
        {
            return (IMappingConfiguration<T>)_mappingConfigurations.GetOrAdd(typeof (T), t => BuildMappingConfiguration(t, sourceAssemblies));
        }

        private static object BuildMappingConfiguration(Type mappedType, Assembly[] sourceAssemblies)
        {
            var configurationType = FindMappingConfigurationType(mappedType, sourceAssemblies);

            return Activator.CreateInstance(configurationType);
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