using System;
using System.Linq.Expressions;
using Lucene.FluentMapping.Configuration;
using Lucene.Net.Documents;

namespace Lucene.FluentMapping.Conversion
{
    public static class DecimalFieldMapping
    {
        public static MappingBuilder<T> Map<T>(this MappingBuilder<T> @this, Expression<Func<T, decimal>> property, bool indexed = false)
        {
            return @this.Add(new DecimalFieldMapping<T>(property).Configure(o => o.Index = indexed));
        }

        public static MappingBuilder<T> Map<T>(this MappingBuilder<T> @this, Expression<Func<T, decimal?>> property, bool indexed = false)
        {
            return @this.Add(new DecimalFieldMapping<T>(property).Configure(o => o.Index = indexed));
        }
    }

    public class DecimalFieldMapping<T> : NumericFieldMapping<T, decimal>
    {
        private const double NullValue = double.NaN;

        public DecimalFieldMapping(Expression<Func<T, decimal>> property)
            : base(property)
        { }

        public DecimalFieldMapping(Expression<Func<T, decimal?>> property)
            : base(property)
        { }

        protected override decimal? Convert(ValueType value)
        {
            var doubleValue = (double) value;

            if (double.IsNaN(doubleValue))
                return null;

            return (decimal) doubleValue;
        }

        protected override void SetValue(NumericField field, decimal? value)
        {
            var fieldValue = value.HasValue ? (double)value.Value : NullValue;

            field.SetDoubleValue(fieldValue);
        }
    }
}