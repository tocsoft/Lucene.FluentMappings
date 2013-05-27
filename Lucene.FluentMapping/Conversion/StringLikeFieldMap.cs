using System;
using System.Linq.Expressions;
using Lucene.FluentMapping.Configuration;
using Lucene.Net.Documents;

namespace Lucene.FluentMapping.Conversion
{
    public abstract class StringLikeFieldMap<T, TProperty> : IFieldMap<T>, IConfigurableFieldMap<TextFieldOptions> 
        where TProperty : class
    {
        private readonly TextFieldOptions _options = new TextFieldOptions();
        private readonly string _name;
        private readonly Func<T, TProperty> _getValue;
        private readonly Action<T, TProperty> _setValue;

        protected abstract TProperty FromString(string value);

        protected StringLikeFieldMap(Expression<Func<T, TProperty>> property)
        {
            _name = ReflectionHelper.GetPropertyName(property);
            _getValue = ReflectionHelper.GetGetter(property);
            _setValue = ReflectionHelper.GetSetter(property);
        }
        
        protected virtual string ToString(TProperty value)
        {
            if (value == null)
                return null;

            return value.ToString();
        }
        
        public IFieldWriter<T> CreateFieldWriter()
        {
            var field = new Field(_name, string.Empty, _options.Store, _options.Index, _options.TermVector);

            return FieldWriter.For(field, _getValue, (f, x) => f.SetValue(ToString(x)));
        }
        
        public IFieldReader<T> CreateFieldReader()
        {
            return new FieldReader<T, TProperty>(d => FromString(d.Get(_name)), _setValue);
        }

        public void Configure(Action<TextFieldOptions> configure)
        {
            if (configure != null)
                configure(_options);
        }
    }
}