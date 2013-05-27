using System;
using System.Linq.Expressions;
using Lucene.FluentMapping.Configuration;
using Lucene.Net.Documents;

namespace Lucene.FluentMapping.Conversion
{
    public static class UriFieldMapping
    {
        public static MappingBuilder<T> Map<T>(this MappingBuilder<T> @this, Expression<Func<T, Uri>> property, bool indexed = false)
        {
            return @this.Add(new UriFieldMap<T>(property).Configure(x => x.Index = indexed ? Field.Index.ANALYZED : Field.Index.NOT_ANALYZED));
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