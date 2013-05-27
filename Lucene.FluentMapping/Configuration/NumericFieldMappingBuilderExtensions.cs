using System;
using System.Linq.Expressions;
using Lucene.FluentMapping.Conversion;

namespace Lucene.FluentMapping.Configuration
{
    public static class NumericFieldMappingBuilderExtensions
    {
        public static IConfigurableFieldMap<NumericFieldOptions> Map<T>(this MappingBuilder<T> @this, Expression<Func<T, int>> property)
        {
            return @this.Add(new NumericFieldMap<T, int>(property, new IntFieldAccessor()));
        }

        public static IConfigurableFieldMap<NumericFieldOptions> Map<T>(this MappingBuilder<T> @this, Expression<Func<T, int?>> property)
        {
            return @this.Add(new NumericFieldMap<T, int>(property, new IntFieldAccessor()));
        }

        public static IConfigurableFieldMap<NumericFieldOptions> Map<T>(this MappingBuilder<T> @this, Expression<Func<T, long>> property)
        {
            return @this.Add(new NumericFieldMap<T, long>(property, new LongFieldAccessor()));
        }

        public static IConfigurableFieldMap<NumericFieldOptions> Map<T>(this MappingBuilder<T> @this, Expression<Func<T, long?>> property)
        {
            return @this.Add(new NumericFieldMap<T, long>(property, new LongFieldAccessor()));
        }

        public static IConfigurableFieldMap<NumericFieldOptions> Map<T>(this MappingBuilder<T> @this, Expression<Func<T, decimal>> property)
        {
            return @this.Add(new NumericFieldMap<T, decimal>(property, new DecimalFieldAccessor()));
        }

        public static IConfigurableFieldMap<NumericFieldOptions> Map<T>(this MappingBuilder<T> @this, Expression<Func<T, decimal?>> property)
        {
            return @this.Add(new NumericFieldMap<T, decimal>(property, new DecimalFieldAccessor()));
        }
    }
}