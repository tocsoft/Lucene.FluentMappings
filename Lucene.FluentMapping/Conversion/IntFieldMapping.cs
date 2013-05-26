using System;
using System.Linq.Expressions;
using Lucene.FluentMapping.Configuration;
using Lucene.Net.Documents;

namespace Lucene.FluentMapping.Conversion
{
    public static class IntFieldMapping
    {
        public static MappingBuilder<T> Map<T>(this MappingBuilder<T> @this, Expression<Func<T, int>> property, bool indexed = false)
        {
            return @this.Add(new IntFieldMapping<T>(property, indexed));
        }

        public static MappingBuilder<T> Map<T>(this MappingBuilder<T> @this, Expression<Func<T, int?>> property, bool indexed = false)
        {
            return @this.Add(new IntFieldMapping<T>(property, indexed));
        }
    }

    public class IntFieldMapping<T> : IFieldMap<T>
    {
        // TODO - don't store null value!!

        private const int IntegerNullValue = int.MinValue;

        private readonly Func<T, int?> _getValue;
        private readonly Action<T, int?> _setValue;
        private readonly string _name;
        private readonly bool _index;

        public IntFieldMapping(Expression<Func<T, int>> property, bool index = false)
        {
            _getValue = ReflectionHelper.GetGetter(property).Bind();
            _setValue= ReflectionHelper.GetSetter(property).Bind();
            _name = ReflectionHelper.GetPropertyName(property);
            _index = index;
        }
        
        public IntFieldMapping(Expression<Func<T, int?>> property, bool index = false)
        {
            _getValue = ReflectionHelper.GetGetter(property);
            _setValue = ReflectionHelper.GetSetter(property);
            _name = ReflectionHelper.GetPropertyName(property);
            _index = index;
        }

        public IFieldWriter<T> CreateFieldWriter()
        {
            var field = new NumericField(_name, Field.Store.YES, _index);

            return FieldWriter.For(field, _getValue, (f, i) => f.SetIntValue(i.HasValue ? i.Value : IntegerNullValue));
        }

        public Setter<T> ValueFrom(Document document)
        {
            var field = document.GetFieldable(_name);

            var value = Convert(field as NumericField);

            return new Setter<T>(x => _setValue(x, value));
        }

        private static int? Convert(NumericField field)
        {
            if (field == null || field.NumericValue == null)
                return null;

            return Value((int) field.NumericValue);
        }

        private static int? Value(int numericValue)
        {
            var value = numericValue;

            if (value == IntegerNullValue)
                return null;

            return value;
        }
    }
}