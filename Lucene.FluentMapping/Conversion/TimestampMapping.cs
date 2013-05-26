using System;
using Lucene.Net.Documents;

namespace Lucene.FluentMapping.Conversion
{
    public class TimestampMapping<T> : IFieldMap<T>
    {
        private static readonly Setter<T> _noOp = new Setter<T>(_ => { });

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

        public Setter<T> ValueFrom(Document document)
        {
            return _noOp;
        }

        private string Timestamp()
        {
            return DateTools.DateToString(DateTime.Now, _datePrecision);
        }
    }
}