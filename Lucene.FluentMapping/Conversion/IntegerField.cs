using System;
using Lucene.Net.Documents;

namespace Lucene.FluentMapping.Conversion
{
    public class IntegerField<T> : IField<T>
    {
        private const int IntegerNullValue = int.MinValue;
        
        private readonly Func<T, int?> _getValue;

        private readonly NumericField _field;

        public IFieldable Field
        {
            get { return _field; }
        }

        public IntegerField(Func<T, int?> getValue, NumericField field)
        {
            _getValue = getValue;
            _field = field;
        }

        public void SetValueFrom(T instance)
        {
            var value = _getValue(instance);

            _field.SetIntValue(value.HasValue ? value.Value : IntegerNullValue);
        }
    }
}