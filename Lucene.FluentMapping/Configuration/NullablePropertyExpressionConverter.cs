using System;

namespace Lucene.FluentMapping.Configuration
{
    public static class NullablePropertyExpressionConverter
    {
        public static Func<TSubject, TProp> Bind<TSubject, TProp>(this Func<TSubject, TProp?> getter)
            where TProp : struct
        {
            return x => 
            { 
                var value = getter(x);

                if (value.HasValue)
                    return value.Value;

                return default(TProp);
            };
        }

        public static Func<TSubject, TProp?> Bind<TSubject, TProp>(this Func<TSubject, TProp> getter)
            where TProp : struct
        {
            return x => getter(x);
        }
        
        public static Action<TSubject, TProperty?> Bind<TSubject, TProperty>(this Action<TSubject, TProperty> setter)
            where TProperty : struct
        {
            return (x, v) =>
                {
                    if (v.HasValue)
                        setter(x, v.Value);
                };
        }
        
        public static Action<TSubject, TProperty> Bind<TSubject, TProperty>(this Action<TSubject, TProperty?> setter)
            where TProperty : struct
        {
            return (s, p) => setter(s, p);
        }
    }
}