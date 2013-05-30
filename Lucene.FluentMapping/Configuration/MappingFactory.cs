using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Lucene.FluentMapping.Configuration
{
    internal static class MappingFactory<T>
    {
        private static IEnumerable<IFieldMap<T>> _mappings;

        public static IEnumerable<IFieldMap<T>> GetMappings(Assembly sourceAssembly = null)
        {
            return GetMappings(new[] { sourceAssembly ?? typeof(T).Assembly });
        }

        public static IEnumerable<IFieldMap<T>> GetMappings(Assembly[] sourceAssemblies)
        {
            if (_mappings != null)
                return _mappings;
            
            _mappings = BuildMappings(sourceAssemblies);

            return _mappings;
        }

        private static IEnumerable<IFieldMap<T>> BuildMappings(Assembly[] sourceAssemblies)
        {
            var mappingConfiguration = BuildMappingConfiguration(sourceAssemblies);

            return mappingConfiguration.BuildMappings();
        }

        private static IMappingConfiguration<T> BuildMappingConfiguration(Assembly[] sourceAssemblies)
        {
            var configurationType = FindMappingConfigurationType(sourceAssemblies);

            return Activator.CreateInstance(configurationType) as IMappingConfiguration<T>;
        }

        private static Type FindMappingConfigurationType(Assembly[] sourceAssemblies)
        {
            var implementerType = FindImplementer(sourceAssemblies);

            if (implementerType == null)
                throw NotFound(sourceAssemblies);

            return implementerType;
        }

        private static Type FindImplementer(IEnumerable<Assembly> sourceAssemblies)
        {
            return sourceAssemblies
                .GetTypesImplementing(typeof(IMappingConfiguration<T>))
                .FirstOrDefault();
        }

        private static ArgumentException NotFound(IEnumerable<Assembly> scannedAssemblies)
        {
            var name = typeof (IMappingConfiguration<T>).Name;

            var interfaceName = string.Concat(name.Remove(name.Length - 2), "<", typeof(T).Name, ">");

            var locations = string.Join(Environment.NewLine, scannedAssemblies.Select(x => x.FullName));

            var message = string.Format("Type implementing {0} not found. Looked in:{1} {2}", interfaceName, Environment.NewLine, locations);

            return new ArgumentException(message);
        }
    }
}