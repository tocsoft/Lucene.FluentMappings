using System;
using System.Linq.Expressions;
using Lucene.FluentMapping.Conversion;
using Lucene.Net.Documents;

namespace Lucene.FluentMapping.Configuration
{
    public static class FieldMap
    {
        public static StringFieldMap<T, TProperty> For<T, TProperty>(
            Expression<Func<T, TProperty>> property,
            IFieldAccessor<Field, TProperty> fieldAccessor)
            where TProperty : class
        {
            return new StringFieldMap<T, TProperty>(property, fieldAccessor);
        }

        public static NumericFieldMap<T, TProperty> For<T, TProperty>(
            Expression<Func<T, TProperty>> property,
            IFieldAccessor<NumericField, TProperty?> fieldAccessor)
            where TProperty : struct
        {
            return new NumericFieldMap<T, TProperty>(property, fieldAccessor);
        }

        public static NumericFieldMap<T, TProperty> For<T, TProperty>(
            Expression<Func<T, TProperty?>> property,
            IFieldAccessor<NumericField, TProperty?> fieldAccessor)
            where TProperty : struct
        {
            return new NumericFieldMap<T, TProperty>(property, fieldAccessor);
        }
    }
}