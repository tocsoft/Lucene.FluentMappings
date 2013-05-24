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
        
        public IFieldable GetField(T instance)
        {
            var stringValue = DateTools.DateToString(DateTime.Now, _datePrecision);

            return new Field(_name, stringValue ?? string.Empty, Field.Store.YES, Field.Index.ANALYZED);
        }

        public Setter<T> ValueFrom(Document document)
        {
            return _noOp;
        }
    }
}