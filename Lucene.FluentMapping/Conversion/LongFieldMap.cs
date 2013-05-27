using System;
using System.Linq.Expressions;
using Lucene.Net.Documents;

namespace Lucene.FluentMapping.Conversion
{
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