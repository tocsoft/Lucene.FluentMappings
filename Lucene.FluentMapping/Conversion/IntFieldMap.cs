using System;
using System.Linq.Expressions;
using Lucene.FluentMapping.Configuration;
using Lucene.Net.Documents;

namespace Lucene.FluentMapping.Conversion
{
    public static class IntFieldMap
    {
        public static IConfigurableFieldMap<NumericFieldOptions> Map<T>(this MappingBuilder<T> @this, Expression<Func<T, int>> property)
        {
            return @this.Add(new IntFieldMap<T>(property));
        }

        public static IConfigurableFieldMap<NumericFieldOptions> Map<T>(this MappingBuilder<T> @this, Expression<Func<T, int?>> property)
        {
            return @this.Add(new IntFieldMap<T>(property));
        }
    }

    public class IntFieldMap<T> : NumericFieldMap<T, int>
    {
        private const int NullValue = int.MinValue;

        public IntFieldMap(Expression<Func<T, int>> property) 
            : base(property)
        { }

        public IntFieldMap(Expression<Func<T, int?>> property)
            : base(property)
        { }

        protected override int? Convert(ValueType value)
        {
            var integerValue = (int)value;

            if (integerValue == NullValue)
                return null;

            return integerValue;
        }

        protected override void SetValue(NumericField field, int? value)
        {
            field.SetIntValue(value.HasValue ? value.Value : NullValue);
        }
    }
}