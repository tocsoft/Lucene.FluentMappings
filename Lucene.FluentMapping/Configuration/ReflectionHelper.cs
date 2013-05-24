using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Lucene.FluentMapping.Configuration
{
    public static class ReflectionHelper
    {
        public static string GetPropertyName<T, TProperty>(Expression<Func<T, TProperty>> expression)
        {
            var property = GetPropertyInfo(expression);

            if (property == null)
                throw new ArgumentException("Expression {0} is not a property accessor");

            return property.Name;
        }
       
        public static Func<T, TProperty> GetGetter<T, TProperty>(Expression<Func<T, TProperty>> expression)
        {
            var property = GetPropertyInfo(expression);

            return GetGetter<T, TProperty>(property);
        }

        public static Action<T, TProperty> GetSetter<T, TProperty>(Expression<Func<T, TProperty>> expression)
        {
            var property = GetPropertyInfo(expression);

            return GetSetter<T, TProperty>(property);
        }

        private static PropertyInfo GetPropertyInfo<T, TProperty>(Expression<Func<T, TProperty>> expression)
        {
            var memberExpression = expression.Body as MemberExpression;

            if (memberExpression == null)
                return null;

            return memberExpression.Member as PropertyInfo;
        }
        
        private static Func<T, TProperty> GetGetter<T, TProperty>(PropertyInfo propertyInfo)
        {
            Validate<T, TProperty>(propertyInfo);

            var instance = Expression.Parameter(propertyInfo.DeclaringType, "i");
            var property = Expression.Property(instance, propertyInfo);
            var func = Expression.Lambda(property, instance).Compile();

            return (Func<T, TProperty>)func;
        }

        private static Action<T, TProperty> GetSetter<T, TProperty>(this PropertyInfo propertyInfo)
        {
            Validate<T, TProperty>(propertyInfo);

            var instance = Expression.Parameter(propertyInfo.DeclaringType, "i");
            var argument = Expression.Parameter(typeof(TProperty), "a");

            var setterCall = Expression.Call(
                instance,
                propertyInfo.GetSetMethod(),
                argument);

            return (Action<T, TProperty>)Expression.Lambda(setterCall, instance, argument)
                                             .Compile();
        }

        private static void Validate<T, TProperty>(PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
                throw new ArgumentNullException("propertyInfo");

            if (typeof (T) != propertyInfo.DeclaringType)
                throw new ArgumentException(string.Format("{0} is not a property of {1}", propertyInfo.Name, typeof (T).Name));

            if (typeof (TProperty) != propertyInfo.PropertyType)
                throw new ArgumentException(string.Format("Property {0} is not of expected type {1}", propertyInfo.Name, typeof (TProperty).Name));
        }
    }
}