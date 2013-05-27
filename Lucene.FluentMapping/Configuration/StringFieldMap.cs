using System;
using System.Linq.Expressions;
using Lucene.FluentMapping.Conversion;
using Lucene.Net.Documents;

namespace Lucene.FluentMapping.Configuration
{
    public class StringFieldMap<T, TProperty> : IFieldMap<T>, IConfigurableFieldMap<TextFieldOptions> 
        where TProperty : class
    {
        private readonly IFieldAccessor<Field, TProperty> _fieldAccessor;
        private readonly TextFieldOptions _options = new TextFieldOptions();
        private readonly string _name;
        private readonly Func<T, TProperty> _getValue;
        private readonly Action<T, TProperty> _setValue;
        
        public TextFieldOptions Options
        {
            get { return _options; }
        }

        public StringFieldMap(Expression<Func<T, TProperty>> property, IFieldAccessor<Field, TProperty> fieldAccessor)
        {
            _fieldAccessor = fieldAccessor;
            _name = ReflectionHelper.GetPropertyName(property);
            _getValue = ReflectionHelper.GetGetter(property);
            _setValue = ReflectionHelper.GetSetter(property);
        }
        
        public IFieldWriter<T> CreateFieldWriter()
        {
            var field = new Field(_name, string.Empty, _options.Store, _options.Index, _options.TermVector)
                {
                    Boost = _options.Boost
                };

            return FieldWriter.For(field, _getValue, _fieldAccessor.SetValue);
        }

        public IFieldReader<T> CreateFieldReader()
        {
            return new FieldReader<T, TProperty>(GetValue, _setValue);
        }

        private TProperty GetValue(Document d)
        {
            var field = d.GetField(_name);

            if (field == null || field.StringValue == null)
                return null;

            return _fieldAccessor.GetValue(field);
        }
    }
}