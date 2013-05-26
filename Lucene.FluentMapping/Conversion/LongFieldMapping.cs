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

        public IFieldWriter<T> CreateFieldWriter()
        {
            var field = new NumericField(_name, Field.Store.YES, _index);

            return FieldWriter.For(field, _getValue, (f, i) => f.SetLongValue(i.HasValue ? i.Value : NullValue));
        }

        public Setter<T> ValueFrom(Document document)
        {
            var value = GetValue(document);

            return new Setter<T>(x => _setValue(x, value));
        }

        private long? GetValue(Document document)
        {
            var field = document.GetFieldable(_name);

            return Convert(field as NumericField);
        }

        private long? Convert(NumericField numericField)
        {
            if (numericField == null || numericField.NumericValue == null)
                return null;

            return Value((long) numericField.NumericValue);
        }

        private static long? Value(long numericValue)
        {
            if (numericValue == NullValue)
                return null;

            return numericValue;
        }
    }
}