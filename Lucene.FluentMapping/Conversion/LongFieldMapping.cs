using System;
using System.Linq.Expressions;
using Lucene.FluentMapping.Configuration;
using Lucene.Net.Documents;

namespace Lucene.FluentMapping.Conversion
{
    public static class LongFieldMapping
    {
        public static MappingBuilder<T> Map<T>(this MappingBuilder<T> @this, Expression<Func<T, long>> property, bool indexed = false)
        {
            return @this.Add(new LongFieldMapping<T>(property, indexed));
        }

        public static MappingBuilder<T> Map<T>(this MappingBuilder<T> @this, Expression<Func<T, long?>> property, bool indexed = false)
        {
            return @this.Add(new LongFieldMapping<T>(property, indexed));
        }
    }

    public class LongFieldMapping<T> : IFieldMap<T>
    {
        private const long NullValue = long.MinValue;

        private readonly Func<T, long?> _getValue;
        private readonly Action<T, long?> _setValue;

        private readonly string _name;
        private readonly bool _index;

        public LongFieldMapping(Expression<Func<T, long?>> property, bool index = false)
        {
            _getValue = ReflectionHelper.GetGetter(property);
            _setValue = ReflectionHelper.GetSetter(property);
            _name = ReflectionHelper.GetPropertyName(property);
            _index = index;
        }

        public LongFieldMapping(Expression<Func<T, long>> property, bool index = false)
        {
            _getValue = ReflectionHelper.GetGetter(property).Bind();
            _setValue = ReflectionHelper.GetSetter(property).Bind();
            _name = ReflectionHelper.GetPropertyName(property);
            _index = index;
        }

        public IFieldable GetField(T instance)
        {
            var field = new NumericField(_name, Field.Store.YES, _index);

            var value = _getValue(instance);

            field.SetLongValue(value.HasValue ? value.Value : NullValue);

            return field;
        }

        public Setter<T> ValueFrom(Document document)
        {
            var value = GetValue(document);

            return new Setter<T>(x => _setValue(x, value));
        }

        private long? GetValue(Document document)
        {
            var value = document.Get(_name);

            return Convert(value);
        }

        private static long? Convert(string s)
        {
            if (s == null)
                return null;

            var value = long.Parse(s);

            if (value == NullValue)
                return null;

            return value;
        }
    }
}