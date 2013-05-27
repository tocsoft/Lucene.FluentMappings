using System;
using System.Linq.Expressions;
using Lucene.FluentMapping.Configuration;
using Lucene.Net.Documents;

namespace Lucene.FluentMapping.Conversion
{
    public static class StringFieldMap
    {
        public static MappingBuilder<T> Map<T>(this MappingBuilder<T> @this, Expression<Func<T, string>> property, bool indexed = false)
        {
            return @this.Add(new StringFieldMap<T>(property).Configure(x => x.Index = indexed ? Field.Index.ANALYZED : Field.Index.NOT_ANALYZED));
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