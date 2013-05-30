using System;
using System.Reflection;
using Lucene.FluentMapping.Conversion;
using Lucene.Net.Documents;

namespace Lucene.FluentMapping.Configuration
{
    public class NumericFieldMap<T, TProperty> : IFieldMap<T>, IConfigurableFieldMap<NumericFieldOptions>
        where TProperty : struct
    {
        private readonly NumericFieldOptions _options = new NumericFieldOptions();
        private readonly PropertyInfo _property;
        private readonly IFieldAccessor<NumericField, TProperty?> _fieldAccessor;
        
        public NumericFieldOptions Options
        {
            get { return _options; }
        }

        public NumericFieldMap(PropertyInfo property, IFieldAccessor<NumericField, TProperty?> fieldAccessor)
        {
            _property = property;
            _fieldAccessor = fieldAccessor;
        }
        
        public IFieldWriter<T> CreateFieldWriter()
        {
            var field = new NumericField(_property.Name, _options.Precision, _options.Store, _options.Index)
                {
                    Boost = _options.Boost
                };

            return FieldWriter.For(field, Getter(), _fieldAccessor.SetValue);
        }

        public IFieldReader<T> CreateFieldReader()
        {
            return new FieldReader<T, TProperty?>(GetValue, Setter());
        }

        private TProperty? GetValue(Document d)
        {
            var field = d.GetFieldable(_property.Name) as NumericField;

            if (field == null || field.NumericValue == null)
                return null;

            return _fieldAccessor.GetValue(field);
        }

        private Func<T, TProperty?> Getter()
        {
            return IsNullable()
                       ? _property.GetGetter<T, TProperty?>()
                       : _property.GetGetter<T, TProperty>().Bind();
        }

        private Action<T, TProperty?> Setter()
        {
            return IsNullable()
                       ? _property.GetSetter<T, TProperty?>()
                       : _property.GetSetter<T, TProperty>().Bind();
        }

        private bool IsNullable()
        {
            return Nullable.GetUnderlyingType(_property.PropertyType) != null;
        }
    }
}