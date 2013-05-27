using Lucene.Net.Documents;

namespace Lucene.FluentMapping.Conversion
{
    public class LongFieldAccessor : IFieldAccessor<NumericField, long?>
    {
        private const long NullValue = long.MinValue;
        
        public long? GetValue(NumericField field)
        {
            var longValue = (long)field.NumericValue;

            if (longValue == NullValue)
                return null;

            return longValue;
        }

        public void SetValue(NumericField field, long? value)
        {
            field.SetLongValue(value.HasValue ? value.Value : NullValue);
        }
    }
}