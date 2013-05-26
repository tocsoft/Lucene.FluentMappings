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
            return @this.Add(new UriFieldMapping<T>(property, indexed ? Field.Index.ANALYZED : Field.Index.NOT_ANALYZED));
        }
    }

    public class UriFieldMapping<T> : StringLikeFieldMapping<T, Uri>
    {
        public UriFieldMapping(Expression<Func<T, Uri>> property, Field.Index index) 
            : base(property, index)
        { }

        protected override Uri FromString(string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            return new Uri(value);
        }
    }
}