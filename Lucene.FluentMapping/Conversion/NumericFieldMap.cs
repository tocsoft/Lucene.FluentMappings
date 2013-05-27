using System;
using System.Linq.Expressions;
using Lucene.FluentMapping.Configuration;
using Lucene.Net.Documents;

namespace Lucene.FluentMapping.Conversion
{
    public abstract class NumericFieldMap<T, TProperty> : IFieldMap<T>, IConfigurable<T, NumericFieldOptions>
        where TProperty : struct
    {
        private readonly string _name;
        private readonly Func<T, TProperty?> _getValue;
        private readonly Action<T, TProperty?> _setValue;
        private readonly NumericFieldOptions _options = new NumericFieldOptions();

        protected abstract TProperty? Convert(ValueType value);
        protected abstract void SetValue(NumericField field, TProperty? value);

        protected NumericFieldMap(Expression<Func<T, TProperty>> property)
        {
            _name = ReflectionHelper.GetPropertyName(property);
            _getValue = ReflectionHelper.GetGetter(property).Bind();
            _setValue = ReflectionHelper.GetSetter(property).Bind();
        }

        protected NumericFieldMap(Expression<Func<T, TProperty?>> property)
        {
            _name = ReflectionHelper.GetPropertyName(property);
            _getValue = ReflectionHelper.GetGetter(property);
            _setValue = ReflectionHelper.GetSetter(property);
        }

        public IFieldWriter<T> CreateFieldWriter()
        {
            var field = new NumericField(_name, _options.Precision, _options.Store, _options.Index);

            return FieldWriter.For(field, _getValue, SetValue);
        }

        public IFieldReader<T> CreateFieldReader()
        {
            return new FieldReader<T, TProperty?>(d => Convert(d.GetFieldable(_name) as NumericField), _setValue);
        }

        public IFieldMap<T> Configure(Action<NumericFieldOptions> configure)
        {
            configure(_options);

            return this;
        }

        private TProperty? Convert(NumericField field)
        {
            if (field == null || field.NumericValue == null)
                return default(TProperty);

            return Convert(field.NumericValue);
        }
    }
}