using System;
using System.Linq.Expressions;
using Lucene.FluentMapping.Configuration;
using Lucene.Net.Documents;

namespace Lucene.FluentMapping.Conversion
{
    public static class DateTimeFieldMapping
    {
        public static MappingBuilder<T> Map<T>(this MappingBuilder<T> @this, Expression<Func<T, DateTime>> property, bool indexed = false)
        {
            return @this.Add(new DateTimeFieldMapping<T>(property)
                                 .Configure(o => o.Index = indexed));
        }

        public static MappingBuilder<T> Map<T>(this MappingBuilder<T> @this, Expression<Func<T, DateTime?>> property, bool indexed = false)
        {
            return @this.Add(new DateTimeFieldMapping<T>(property)
                                 .Configure(o => o.Index = indexed));
        }
    }

    public class DateTimeFieldMapping<T> : NumericFieldMapping<T, DateTime>
    {
        private readonly long _nullValue = DateTime.MinValue.Ticks;
        
        public DateTimeFieldMapping(Expression<Func<T, DateTime>> property)
            : base(property)
        { }

        public DateTimeFieldMapping(Expression<Func<T, DateTime?>> property)
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