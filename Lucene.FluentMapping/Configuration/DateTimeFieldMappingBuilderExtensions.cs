using System;
using System.Linq.Expressions;
using Lucene.FluentMapping.Conversion;

namespace Lucene.FluentMapping.Configuration
{
    public static class DateTimeFieldMappingBuilderExtensions
    {
        public static IConfigurableFieldMap<NumericFieldOptions> Map<T>(this MappingBuilder<T> @this, Expression<Func<T, DateTime>> property)
        {
            return @this.Add(new DateTimeFieldMap<T>(property));
        }

        public static IConfigurableFieldMap<NumericFieldOptions> Map<T>(this MappingBuilder<T> @this, Expression<Func<T, DateTime?>> property)
        {
            return @this.Add(new DateTimeFieldMap<T>(property));
        }
    }
}