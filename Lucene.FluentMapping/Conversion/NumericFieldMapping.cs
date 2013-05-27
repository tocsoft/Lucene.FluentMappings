using System;
using System.Linq.Expressions;
using Lucene.FluentMapping.Configuration;
using Lucene.Net.Documents;

namespace Lucene.FluentMapping.Conversion
{
    public abstract class NumericFieldMapping<T, TProperty> : IFieldMap<T>
        where TProperty : struct
    {
        private readonly string _name;
        private readonly Func<T, TProperty?> _getValue;
        private readonly Action<T, TProperty?> _setValue;
        private readonly bool _index;

        protected abstract TProperty? Convert(ValueType value);
        protected abstract void SetValue(NumericField field, TProperty? value);

        protected NumericFieldMapping(Expression<Func<T, TProperty>> property, bool index = false)
        {
            _name = ReflectionHelper.GetPropertyName(property);
            _getValue = ReflectionHelper.GetGetter(property).Bind();
            _setValue = ReflectionHelper.GetSetter(property).Bind();
            _index = index;
        }

        protected NumericFieldMapping(Expression<Func<T, TProperty?>> property, bool index = false)
        {
            _name = ReflectionHelper.GetPropertyName(property);
            _getValue = ReflectionHelper.GetGetter(property);
            _setValue = ReflectionHelper.GetSetter(property);
            _index = index;
        }

        public IFieldWriter<T> CreateFieldWriter()
        {
            var field = new NumericField(_name, Field.Store.YES, _index);

            return FieldWriter.For(field, _getValue, SetValue);
        }

        public Setter<T> ValueFrom(Document document)
        {
            var field = document.GetFieldable(_name) as NumericField;
            
            return new Setter<T>(x => _setValue(x, Convert(field)));
        }

        private TProperty? Convert(NumericField field)
        {
            if (field == null || field.NumericValue == null)
                return default(TProperty);

            return Convert(field.NumericValue);
        }
    }
}