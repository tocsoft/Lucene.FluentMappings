using Lucene.Net.Documents;

namespace Lucene.FluentMapping.Conversion
{
    public class StringFieldAccessor : IFieldAccessor<Field, string>
    {
        public void SetValue(Field field, string value)
        {
            field.SetValue(value);
        }

        public string GetValue(Field field)
        {
            return field.StringValue;
        }
    }
}