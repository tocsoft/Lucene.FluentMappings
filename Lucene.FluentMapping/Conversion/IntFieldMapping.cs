using System;
using System.Linq.Expressions;
using Lucene.FluentMapping.Configuration;
using Lucene.Net.Documents;

namespace Lucene.FluentMapping.Conversion
{
    public static class IntFieldMapping
    {
        public static MappingBuilder<T> Map<T>(this MappingBuilder<T> @this, Expression<Func<T, int>> property, bool indexed = false)
        {
            return @this.Add(new IntFieldMapping<T>(property).Configure(o => o.Index = indexed));
        }

        public static MappingBuilder<T> Map<T>(this MappingBuilder<T> @this, Expression<Func<T, int?>> property, bool indexed = false)
        {
            return @this.Add(new IntFieldMapping<T>(property).Configure(o => o.Index = indexed));
        }
    }

    public class IntFieldMapping<T> : NumericFieldMapping<T, int>
    {
        private const int NullValue = int.MinValue;

        public IntFieldMapping(Expression<Func<T, int>> property) 
            : base(property)
        { }

        public IntFieldMapping(Expression<Func<T, int?>> property)
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