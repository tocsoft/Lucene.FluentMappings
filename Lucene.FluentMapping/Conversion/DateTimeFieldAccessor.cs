using System;
using Lucene.Net.Documents;

namespace Lucene.FluentMapping.Conversion
{
    public class DateTimeFieldAccessor : IFieldAccessor<NumericField, DateTime?>
    {
        private readonly long _nullValue = DateTime.MinValue.Ticks;
        
        public DateTime? GetValue(NumericField field)
        {
            var longValue = (long) field.NumericValue;

            if (longValue == _nullValue)
                return null;

            return new DateTime(longValue);
        }

        public void SetValue(NumericField field, DateTime? value)
        {
            var ticks = value.HasValue ? value.Value.Ticks : _nullValue;

            field.SetLongValue(ticks);
        }
    }
}