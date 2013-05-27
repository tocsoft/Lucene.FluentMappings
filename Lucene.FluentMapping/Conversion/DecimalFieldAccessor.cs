using Lucene.Net.Documents;

namespace Lucene.FluentMapping.Conversion
{
    public class DecimalFieldAccessor : IFieldAccessor<NumericField, decimal?>
    {
        private const double NullValue = double.NaN;

        public decimal? GetValue(NumericField field)
        {
            var doubleValue = (double) field.NumericValue;

            if (double.IsNaN(doubleValue))
                return null;

            return (decimal) doubleValue;
        }

        public void SetValue(NumericField field, decimal? value)
        {
            var fieldValue = value.HasValue ? (double)value.Value : NullValue;

            field.SetDoubleValue(fieldValue);
        }
    }
}