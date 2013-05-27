using System;
using System.Linq.Expressions;
using Lucene.Net.Documents;

namespace Lucene.FluentMapping.Conversion
{
    public class DateTimeFieldMap<T> : NumericFieldMap<T, DateTime>
    {
        private readonly long _nullValue = DateTime.MinValue.Ticks;
        
        public DateTimeFieldMap(Expression<Func<T, DateTime>> property)
            : base(property)
        { }

        public DateTimeFieldMap(Expression<Func<T, DateTime?>> property)
            : base(property)
        { }

        protected override DateTime? Convert(ValueType value)
        {
            if ((long)value == _nullValue)
                return null;

            return new DateTime((long)value);
        }

        protected override void SetValue(NumericField field, DateTime? value)
        {
            var ticks = value.HasValue ? value.Value.Ticks : _nullValue;

            field.SetLongValue(ticks);
        }
    }
}