using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Lucene.FluentMapping.Conversion;

namespace Lucene.FluentMapping.Configuration
{
    public static class MappingFactory
    {
        private static readonly ConcurrentDictionary<Type, object> _mappingConfigurations = new ConcurrentDictionary<Type, object>();

        public static IEnumerable<IFieldMap<T>> GetMappings<T>()
        {
            return GetMappingConfiguration<T>().BuildMappings();
        }
        
        private static IMappingConfiguration<T> GetMappingConfiguration<T>()
        {
            return (IMappingConfiguration<T>)_mappingConfigurations.GetOrAdd(typeof (T), BuildMappingConfiguration);
        }

        private static object BuildMappingConfiguration(Type mappedType)
        {
            var configurationType = FindMappingConfigurationType(mappedType);

            return Activator.CreateInstance(configurationType);
        }

        private static Type FindMappingConfigurationType(Type mappedType)
        {
            var interfaceType = typeof (IMappingConfiguration<>).MakeGenericType(mappedType);

            var implementerType = FindImplementer(interfaceType);

            if (implementerType == null)
                throw new ArgumentException("Type implementing {0} not found", interfaceType.Name);

            return implementerType;
        }

        private static Type FindImplementer(Type interfaceType)
        {
            return AppDomain.CurrentDomain
                .GetTypesImplementing(interfaceType)
                .FirstOrDefault();
        }
    }
}