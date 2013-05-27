using Lucene.Net.Documents;

namespace Lucene.FluentMapping.Conversion
{
    public class IntFieldAccessor : IFieldAccessor<NumericField, int?>
    {
        private const int NullValue = int.MinValue;

        public void SetValue(NumericField field, int? value)
        {
            field.SetIntValue(value.HasValue ? value.Value : NullValue);
        }

        public int? GetValue(NumericField field)
        {
            var intValue = (int)field.NumericValue;

            return intValue == NullValue ? (int?)null : intValue;
        }
    }
}