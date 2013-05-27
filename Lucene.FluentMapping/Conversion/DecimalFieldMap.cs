using System;
using System.Linq.Expressions;
using Lucene.FluentMapping.Configuration;
using Lucene.Net.Documents;

namespace Lucene.FluentMapping.Conversion
{
    public static class DecimalFieldMap
    {
        public static IConfigurableFieldMap<NumericFieldOptions> Map<T>(this MappingBuilder<T> @this, Expression<Func<T, decimal>> property)
        {
            return @this.Add(new DecimalFieldMap<T>(property));
        }

        public static IConfigurableFieldMap<NumericFieldOptions> Map<T>(this MappingBuilder<T> @this, Expression<Func<T, decimal?>> property)
        {
            return @this.Add(new DecimalFieldMap<T>(property));
        }
    }

    public class DecimalFieldMap<T> : NumericFieldMap<T, decimal>
    {
        private const double NullValue = double.NaN;

        public DecimalFieldMap(Expression<Func<T, decimal>> property)
            : base(property)
        { }

        public DecimalFieldMap(Expression<Func<T, decimal?>> property)
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