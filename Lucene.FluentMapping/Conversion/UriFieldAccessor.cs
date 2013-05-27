using System;
using Lucene.Net.Documents;

namespace Lucene.FluentMapping.Conversion
{
    public class UriFieldAccessor : IFieldAccessor<Field, Uri>
    {
        public void SetValue(Field field, Uri value)
        {
            var fieldValue = value == null ? null : value.ToString();

            field.SetValue(fieldValue);
        }

        public Uri GetValue(Field field)
        {
            return new Uri(field.StringValue);
        }
    }
}