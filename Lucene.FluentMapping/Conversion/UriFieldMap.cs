using System;
using System.Linq.Expressions;

namespace Lucene.FluentMapping.Conversion
{
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