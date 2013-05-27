using System;
using Lucene.FluentMapping.Conversion;
using Lucene.Net.Documents;

namespace Lucene.FluentMapping.Configuration
{
    public class TimestampMapping<T> : IFieldMap<T>
    {
        private readonly string _name;
        private readonly DateTools.Resolution _datePrecision;

        public TimestampMapping(string name, DateTools.Resolution datePrecision = null)
        {
            _name = name;
            _datePrecision = datePrecision ??  DateTools.Resolution.MINUTE;
        }

        public IFieldWriter<T> CreateFieldWriter()
        {
            var field = new Field(_name, string.Empty, Field.Store.YES, Field.Index.ANALYZED);

            return new FieldWriter<Field, T, string>(field, _ => Timestamp(), (f, x) => f.SetValue(x));
        }

        public IFieldReader<T> CreateFieldReader()
        {
            return new FieldReader<T, string>(d => null, (t, s) => { });
        }

        private string Timestamp()
        {
            return DateTools.DateToString(DateTime.Now, _datePrecision);
        }
    }
}