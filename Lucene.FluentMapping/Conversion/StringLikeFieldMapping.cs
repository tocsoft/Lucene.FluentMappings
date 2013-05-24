using System;
using System.Linq.Expressions;
using Lucene.FluentMapping.Configuration;
using Lucene.Net.Documents;

namespace Lucene.FluentMapping.Conversion
{
    public abstract class StringLikeFieldMapping<T, TProperty> : IFieldMap<T>
        where TProperty : class
    {
        private readonly string _name;
        private readonly Func<T, TProperty> _getValue;
        private readonly Action<T, TProperty> _setValue;
        private readonly Field.Index? _index;

        protected abstract TProperty FromString(string value);

        protected StringLikeFieldMapping(Expression<Func<T, TProperty>> property, Field.Index? index = null)
        {
            _name = ReflectionHelper.GetPropertyName(property);
            _getValue = ReflectionHelper.GetGetter(property);
            _setValue = ReflectionHelper.GetSetter(property);
            _index = index;
        }
        
        protected virtual string ToString(TProperty value)
        {
            if (value == null)
                return null;

            return value.ToString();
        }

        public IFieldable GetField(T instance)
        {
            var value = GetValue(instance);

            if (value == null)
                return null;

            return new Field(_name, value, Field.Store.YES, _index ?? Field.Index.NOT_ANALYZED);
        }

        public Setter<T> ValueFrom(Document document)
        {
            var field = document.Get(_name);
            
            return new Setter<T>(x => _setValue(x, FromString(field)));
        }

        private string GetValue(T instance)
        {
            var value = _getValue(instance);

            return ToString(value);
        }
    }
}