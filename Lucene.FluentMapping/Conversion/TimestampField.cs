using System;
using Lucene.Net.Documents;

namespace Lucene.FluentMapping.Conversion
{
    public class TimestampField<T> : IField<T>
    {
        private readonly Field _field;
        private readonly DateTools.Resolution _datePrecision;

        public IFieldable Field 
        {
            get { return _field; }
        }

        public TimestampField(Field field, DateTools.Resolution datePrecision)
        {
            _field = field;
            _datePrecision = datePrecision;
        }

        public void SetValueFrom(T instance)
        {
            var stringValue = DateTools.DateToString(DateTime.Now, _datePrecision);

            _field.SetValue(stringValue);
        }
    }
}