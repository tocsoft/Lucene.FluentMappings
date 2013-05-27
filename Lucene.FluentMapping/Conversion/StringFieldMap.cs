using System;
using System.Linq.Expressions;
using Lucene.FluentMapping.Configuration;

namespace Lucene.FluentMapping.Conversion
{
    public static class StringFieldMap
    {
        public static IConfigurableFieldMap<TextFieldOptions> Map<T>(this MappingBuilder<T> @this, Expression<Func<T, string>> property)
        {
            return @this.Add(new StringFieldMap<T>(property));
        }
    }

    public class StringFieldMap<T> : StringLikeFieldMap<T, string>
    {
        public StringFieldMap(Expression<Func<T, string>> property)
            : base(property)
        { }

        protected override string FromString(string value)
        {
            return value;
        }
    }
}