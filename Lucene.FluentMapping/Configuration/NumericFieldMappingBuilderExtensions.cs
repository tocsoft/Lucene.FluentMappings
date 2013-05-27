using System;
using System.Linq.Expressions;
using Lucene.FluentMapping.Conversion;

namespace Lucene.FluentMapping.Configuration
{
    public static class NumericFieldMappingBuilderExtensions
    {
        public static IConfigurableFieldMap<NumericFieldOptions> Map<T>(this MappingBuilder<T> @this, Expression<Func<T, int>> property)
        {
            return @this.Map(FieldMap.For(property, new IntFieldAccessor()));
        }

        public static IConfigurableFieldMap<NumericFieldOptions> Map<T>(this MappingBuilder<T> @this, Expression<Func<T, int?>> property)
        {
            return @this.Map(FieldMap.For(property, new IntFieldAccessor()));
        }

        public static IConfigurableFieldMap<NumericFieldOptions> Map<T>(this MappingBuilder<T> @this, Expression<Func<T, long>> property)
        {
            return @this.Map(FieldMap.For(property, new LongFieldAccessor()));
        }

        public static IConfigurableFieldMap<NumericFieldOptions> Map<T>(this MappingBuilder<T> @this, Expression<Func<T, long?>> property)
        {
            return @this.Map(FieldMap.For(property, new LongFieldAccessor()));
        }

        public static IConfigurableFieldMap<NumericFieldOptions> Map<T>(this MappingBuilder<T> @this, Expression<Func<T, decimal>> property)
        {
            return @this.Map(FieldMap.For(property, new DecimalFieldAccessor()));
        }

        public static IConfigurableFieldMap<NumericFieldOptions> Map<T>(this MappingBuilder<T> @this, Expression<Func<T, decimal?>> property)
        {
            return @this.Map(FieldMap.For(property, new DecimalFieldAccessor()));
        }
    }
}