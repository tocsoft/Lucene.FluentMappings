using System;
using System.Linq.Expressions;
using Lucene.FluentMapping.Configuration;

namespace Lucene.FluentMapping.Conversion
{
    public static class UriFieldMapping
    {
        public static IConfigurableFieldMap<TextFieldOptions> Map<T>(this MappingBuilder<T> @this, Expression<Func<T, Uri>> property)
        {
            return @this.Add(new UriFieldMap<T>(property));
        }
    }

    public class UriFieldMap<T> : StringLikeFieldMap<T, Uri>
    {
        public UriFieldMap(Expression<Func<T, Uri>> property)
            : base(property)
        { }

        protected override Uri FromString(string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            return new Uri(value);
        }
    }
}