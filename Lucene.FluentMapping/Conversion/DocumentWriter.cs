using System.Collections.Generic;
using System.Linq;
using Lucene.Net.Documents;

namespace Lucene.FluentMapping.Conversion
{
    public class DocumentWriter<T>
    {
        private readonly List<IFieldWriter<T>> _writers;

        public Document Document { get; private set; }

        public DocumentWriter(IEnumerable<IFieldWriterFactory<T>> mappings, Document document = null)
        {
            _writers = mappings.Select(x => x.CreateFieldWriter()).ToList();
            
            Document = document ?? new Document();
            
            foreach (var writer in _writers)
                Document.Add(writer.Field);
        }

        public void UpdateFrom(T source)
        {
            foreach (var writer in _writers)
                writer.WriteValueFrom(source);
        }
    }
}