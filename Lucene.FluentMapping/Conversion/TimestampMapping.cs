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
        
        public IField<T> CreateField()
        {
            var field = new Field(_name, string.Empty, Field.Store.YES, Field.Index.ANALYZED);

            return new TimestampField<T>(field, _datePrecision);
        }

        public Setter<T> ValueFrom(Document document)
        {
            return _noOp;
        }
    }
}