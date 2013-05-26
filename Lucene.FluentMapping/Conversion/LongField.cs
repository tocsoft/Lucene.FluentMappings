using System;
using Lucene.Net.Documents;

namespace Lucene.FluentMapping.Conversion
{
    public class LongField<T> : IField<T>
    {
        private const long NullValue = long.MinValue;

        private readonly Func<T, long?> _getValue;

        private readonly NumericField _field;

        public IFieldable Field
        {
            get { return _field; }
        }

        public LongField(Func<T, long?> getValue, NumericField field)
        {
            _getValue = getValue;
            _field = field;
        }

        public void SetValueFrom(T instance)
        {
            var value = _getValue(instance);

            _field.SetLongValue(value.HasValue ? value.Value : NullValue);
        }
    }
}