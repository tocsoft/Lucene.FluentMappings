using System.Collections.Generic;
using System.Linq;
using Lucene.Net.Documents;

namespace Lucene.FluentMapping.Conversion
{
    public class DocumentWriter<T>
    {
        private readonly IList<IField<T>> _fields;

        public Document Document { get; private set; }

        public DocumentWriter(IEnumerable<IFieldWriter<T>> mappings, Document document = null)
        {
            _fields = mappings.Select(x => x.CreateField()).ToList();
            
            Document = document ?? new Document();
            
            foreach (var field in _fields)
                Document.Add(field.Field);
        }

        public void UpdateFrom(T source)
        {
            foreach (var field in _fields)
                field.SetValueFrom(source);
        }
    }
}