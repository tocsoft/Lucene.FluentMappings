using System;
using System.Linq.Expressions;
using Lucene.FluentMapping.Conversion;

namespace Lucene.FluentMapping.Configuration
{
    public static class NumericFieldMappingBuilderExtensions
    {
        public static IConfigurableFieldMap<NumericFieldOptions> Map<T>(this MappingBuilder<T> @this, Expression<Func<T, int>> property)
        {
            return @this.Add(new IntFieldMap<T>(property));
        }

        public static IConfigurableFieldMap<NumericFieldOptions> Map<T>(this MappingBuilder<T> @this, Expression<Func<T, int?>> property)
        {
            return @this.Add(new IntFieldMap<T>(property));
        }

        public static IConfigurableFieldMap<NumericFieldOptions> Map<T>(this MappingBuilder<T> @this, Expression<Func<T, long>> property)
        {
            return @this.Add(new LongFieldMap<T>(property));
        }

        public static IConfigurableFieldMap<NumericFieldOptions> Map<T>(this MappingBuilder<T> @this, Expression<Func<T, long?>> property)
        {
            return @this.Add(new LongFieldMap<T>(property));
        }

        public static IConfigurableFieldMap<NumericFieldOptions> Map<T>(this MappingBuilder<T> @this, Expression<Func<T, decimal>> property)
        {
            return @this.Add(new DecimalFieldMap<T>(property));
        }

        public static IConfigurableFieldMap<NumericFieldOptions> Map<T>(this MappingBuilder<T> @this, Expression<Func<T, decimal?>> property)
        {
            return @this.Add(new DecimalFieldMap<T>(property));
        }
    }
}