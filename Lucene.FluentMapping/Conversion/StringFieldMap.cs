using System;
using System.Linq.Expressions;

namespace Lucene.FluentMapping.Conversion
{
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