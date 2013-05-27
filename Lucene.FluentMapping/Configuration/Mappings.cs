using System;
using System.Collections.Generic;
using Lucene.FluentMapping.Conversion;

namespace Lucene.FluentMapping.Configuration
{
    public static class Mappings
    {
        public static IEnumerable<IFieldMap<T>> For<T>(Action<MappingBuilder<T>> action)
        {
            var builder = new MappingBuilder<T>();

            action(builder);

            return builder.Mappings;
        }
    }
}