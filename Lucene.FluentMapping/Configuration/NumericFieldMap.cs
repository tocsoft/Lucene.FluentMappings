using System;
using System.Linq.Expressions;
using Lucene.FluentMapping.Conversion;
using Lucene.Net.Documents;

namespace Lucene.FluentMapping.Configuration
{
    public class NumericFieldMap<T, TProperty> : IFieldMap<T>, IConfigurableFieldMap<NumericFieldOptions>
        where TProperty : struct
    {
        private readonly NumericFieldOptions _options = new NumericFieldOptions();
        private readonly IFieldAccessor<NumericField, TProperty?> _fieldAccessor;
        private readonly string _name;
        private readonly Func<T, TProperty?> _getValue;
        private readonly Action<T, TProperty?> _setValue;

        public NumericFieldOptions Options
        {
            get { return _options; }
        }

        public NumericFieldMap(Expression<Func<T, TProperty>> property, IFieldAccessor<NumericField, TProperty?> fieldAccessor)
        {
            _fieldAccessor = fieldAccessor;
            _name = ReflectionHelper.GetPropertyName(property);
            _getValue = ReflectionHelper.GetGetter(property).Bind();
            _setValue = ReflectionHelper.GetSetter(property).Bind();
        }

        public NumericFieldMap(Expression<Func<T, TProperty?>> property, IFieldAccessor<NumericField, TProperty?> fieldAccessor)
        {
            _fieldAccessor = fieldAccessor;
            _name = ReflectionHelper.GetPropertyName(property);
            _getValue = ReflectionHelper.GetGetter(property);
            _setValue = ReflectionHelper.GetSetter(property);
        }

        public IFieldWriter<T> CreateFieldWriter()
        {
            var field = new NumericField(_name, Options.Precision, Options.Store, Options.Index);

            return FieldWriter.For(field, _getValue, _fieldAccessor.SetValue);
        }

        public IFieldReader<T> CreateFieldReader()
        {
            return new FieldReader<T, TProperty?>(GetValue, _setValue);
        }

        private TProperty? GetValue(Document d)
        {
            var field = d.GetFieldable(_name) as NumericField;

            if (field == null || field.NumericValue == null)
                return null;

            return _fieldAccessor.GetValue(field);
        }
    }
}