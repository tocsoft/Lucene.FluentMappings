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
            var propertyInfo = ReflectionHelper.GetPropertyInfo(property);

            return new StringFieldMap<T, TProperty>(propertyInfo, fieldAccessor);
        }

        public static NumericFieldMap<T, TProperty> For<T, TProperty>(
            Expression<Func<T, TProperty>> property,
            IFieldAccessor<NumericField, TProperty?> fieldAccessor)
            where TProperty : struct
        {
            var propertyInfo = ReflectionHelper.GetPropertyInfo(property);

            return new NumericFieldMap<T, TProperty>(propertyInfo, fieldAccessor);
        }

        public static NumericFieldMap<T, TProperty> For<T, TProperty>(
            Expression<Func<T, TProperty?>> property,
            IFieldAccessor<NumericField, TProperty?> fieldAccessor)
            where TProperty : struct
        {
            var propertyInfo = ReflectionHelper.GetPropertyInfo(property);

            return new NumericFieldMap<T, TProperty>(propertyInfo, fieldAccessor);
        }
    }
}