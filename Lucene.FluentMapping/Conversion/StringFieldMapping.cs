using System;
using System.Linq.Expressions;
using Lucene.FluentMapping.Configuration;
using Lucene.Net.Documents;

namespace Lucene.FluentMapping.Conversion
{
    public static class StringFieldMapping
    {
        public static MappingBuilder<T> Map<T>(this MappingBuilder<T> @this, Expression<Func<T, string>> property, bool indexed = false)
        {
            return @this.Add(new StringFieldMapping<T>(property, indexed ? Field.Index.ANALYZED : Field.Index.NOT_ANALYZED));
        }
    }

    public class StringFieldMapping<T> : StringLikeFieldMapping<T, string>
    {
        public StringFieldMapping(Expression<Func<T, string>> property, Field.Index index) 
            : base(property, index)
        { }

        protected override string FromString(string value)
        {
            return value;
        }
    }
}