using System;
using System.Linq.Expressions;
using Lucene.FluentMapping.Configuration;
using Lucene.Net.Documents;

namespace Lucene.FluentMapping.Conversion
{
    public static class LongFieldMap
    {
        public static MappingBuilder<T> Map<T>(this MappingBuilder<T> @this, Expression<Func<T, long>> property, bool indexed = false)
        {
            return @this.Add(new LongFieldMap<T>(property).Configure(o => o.Index = indexed));
        }

        public static MappingBuilder<T> Map<T>(this MappingBuilder<T> @this, Expression<Func<T, long?>> property, bool indexed = false)
        {
            return @this.Add(new LongFieldMap<T>(property).Configure(o => o.Index = indexed));
        }
    }

    public class LongFieldMap<T> : NumericFieldMap<T, long>
    {
        private const long NullValue = long.MinValue;

        public LongFieldMap(Expression<Func<T, long>> property)
            : base(property)
        { }

        public LongFieldMap(Expression<Func<T, long?>> property)
            : base(property)
        { }

        protected override long? Convert(ValueType value)
        {
            var longValue = (long)value;

            if (longValue == NullValue)
                return null;

            return longValue;
        }

        protected override void SetValue(NumericField field, long? value)
        {
            field.SetLongValue(value.HasValue ? value.Value : NullValue);
        }
    }
}