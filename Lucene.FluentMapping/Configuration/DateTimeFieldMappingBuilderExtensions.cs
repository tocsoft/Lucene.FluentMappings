using System;
using System.Linq.Expressions;
using Lucene.FluentMapping.Conversion;

namespace Lucene.FluentMapping.Configuration
{
    public static class DateTimeFieldMappingBuilderExtensions
    {
        public static IConfigurableFieldMap<NumericFieldOptions> Map<T>(this MappingBuilder<T> @this, Expression<Func<T, DateTime>> property)
        {
            return @this.Map(new NumericFieldMap<T, DateTime>(property, new DateTimeFieldAccessor()));
        }

        public static IConfigurableFieldMap<NumericFieldOptions> Map<T>(this MappingBuilder<T> @this, Expression<Func<T, DateTime?>> property)
        {
            return @this.Map(new NumericFieldMap<T, DateTime>(property, new DateTimeFieldAccessor()));
        }
    }
}