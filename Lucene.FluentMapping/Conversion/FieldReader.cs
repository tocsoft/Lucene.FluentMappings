using System;
using Lucene.Net.Documents;

namespace Lucene.FluentMapping.Conversion
{
    public class FieldReader<T, TProp> : IFieldReader<T>
    {
        private Func<Document, TProp> _getValue;
        private Action<T, TProp> _setValue;

        public FieldReader(Func<Document, TProp> getValue, Action<T, TProp> setValue)
        {
            _getValue = getValue;
            _setValue = setValue;
        }

        public void Apply(Document document, T instance)
        {
            var value = _getValue(document);

            _setValue(instance, value);
        }
    }
}