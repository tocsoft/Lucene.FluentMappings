using System;

namespace Lucene.FluentMapping.Conversion
{
    public class Setter<T>
    {
        private readonly Action<T> _setValue;

        public Setter(Action<T> setValue)
        {
            _setValue = setValue;
        }

        public void Apply(T instance)
        {
            _setValue(instance);
        }
    }
}