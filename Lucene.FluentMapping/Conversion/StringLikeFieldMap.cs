using System;
using System.Linq.Expressions;
using Lucene.FluentMapping.Configuration;
using Lucene.Net.Documents;

namespace Lucene.FluentMapping.Conversion
{
    public abstract class StringLikeFieldMap<T, TProperty> : IFieldMap<T>, IConfigurable<T, TextFieldOpions> 
        where TProperty : class
    {
        private readonly TextFieldOpions _options = new TextFieldOpions();
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

        public IFieldMap<T> Configure(Action<TextFieldOpions> configure)
        {
            configure(_options);

            return this;
        }
    }
}